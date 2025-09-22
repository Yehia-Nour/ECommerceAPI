using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.CancellationDTOs;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Infrastructure.Services;

public class CancellationService : ICancellationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public CancellationService(IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<CancellationResponseDTO>>> GetAllCancellationsAsync()
    {
        var cancellations = await _unitOfWork.Cancellations.GetAllWithOrdersAsync();

        var cancellationList = _mapper.Map<List<CancellationResponseDTO>>(cancellations);

        return new ApiResponse<List<CancellationResponseDTO>>(200, cancellationList, true);
    }


    public async Task<ApiResponse<CancellationResponseDTO>> GetCancellationByIdAsync(int id)
    {
        var cancellation = await _unitOfWork.Cancellations.GetByIdAsync(id);
        if (cancellation == null)
            return new ApiResponse<CancellationResponseDTO>(404, "Cancellation request not found.");

        var cancellationResponse = _mapper.Map<CancellationResponseDTO>(cancellation);

        return new ApiResponse<CancellationResponseDTO>(200, cancellationResponse, true);
    }

    public async Task<ApiResponse<CancellationResponseDTO>> RequestCancellationAsync(CancellationRequestDTO cancellationRequest, int customerId)
    {
        var order = await _unitOfWork.Orders.GetOrderByIdAndCustomerIdAsync(cancellationRequest.OrderId, customerId);
        if (order == null)
            return new ApiResponse<CancellationResponseDTO>(404, "Order not found.");

        if (order.OrderStatus != OrderStatus.Processing)
            return new ApiResponse<CancellationResponseDTO>(400, "Order is not eligible for cancellation.");

        var existingCancellation = await _unitOfWork.Cancellations.GetCancellationByOrderIdAsync(cancellationRequest.OrderId);
        if (existingCancellation != null)
            return new ApiResponse<CancellationResponseDTO>(400, "A cancellation request for this order already exists.");

        var cancellation = new Cancellation
        {
            OrderId = cancellationRequest.OrderId,
            Reason = cancellationRequest.Reason,
            Status = CancellationStatus.Pending,
            RequestedAt = DateTime.UtcNow,
            OrderAmount = order.TotalAmount,
            CancellationCharges = 0.00m,
        };

        await _unitOfWork.Cancellations.AddAsync(cancellation);
        await _unitOfWork.SaveChangesAsync();

        var cancellationResponse = _mapper.Map<CancellationResponseDTO>(
            cancellation,
            opt => opt.Items["OrderTotalAmount"] = order.TotalAmount
        );

        return new ApiResponse<CancellationResponseDTO>(200, cancellationResponse, true);
    }

    public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCancellationStatusAsync(CancellationStatusUpdateDTO statusUpdate)
    {
        try
        {
            Cancellation? cancellation = null;

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                cancellation = await _unitOfWork.Cancellations
                    .GetCancellationWithOrderAndCustomerAsync(statusUpdate.CancellationId);

                if (cancellation == null)
                    throw new Exception("Cancellation request not found.");

                if (cancellation.Status != CancellationStatus.Pending)
                    throw new Exception("Only pending cancellation requests can be updated.");

                cancellation.Status = statusUpdate.Status;
                cancellation.ProcessedAt = DateTime.UtcNow;
                cancellation.ProcessedBy = statusUpdate.ProcessedBy;
                cancellation.Remarks = statusUpdate.Remarks;

                if (statusUpdate.Status == CancellationStatus.Approved)
                {
                    cancellation.Order.OrderStatus = OrderStatus.Canceled;
                    cancellation.CancellationCharges = statusUpdate.CancellationCharges;

                    var orderItems = await _unitOfWork.OrderItems.GetOrderItemsWithProductsAsync(cancellation.OrderId);

                    foreach (var item in orderItems)
                    {
                        item.Product.StockQuantity += item.Quantity;
                        _unitOfWork.Products.Update(item.Product);
                    }
                }

                _unitOfWork.Cancellations.Update(cancellation);
                await _unitOfWork.SaveChangesAsync();
            });

            if (statusUpdate.Status == CancellationStatus.Approved)
                await _emailService.SendCancellationAcceptedEmailAsync(cancellation!);
            else if (statusUpdate.Status == CancellationStatus.Rejected)
                await _emailService.SendCancellationRejectedEmailAsync(cancellation!);

            var confirmation = new ConfirmationResponseDTO
            {
                Message = $"Cancellation request with ID {cancellation!.Id} has been {cancellation.Status}."
            };

            return new ApiResponse<ConfirmationResponseDTO>(200, confirmation, true);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ConfirmationResponseDTO>(500, ex.Message);
        }
    }

}
