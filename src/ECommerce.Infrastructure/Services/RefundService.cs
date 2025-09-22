using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.RefundDTOs;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;


namespace ECommerce.Infrastructure.Services
{
    public class RefundService : IRefundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public RefundService(IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<RefundResponseDTO>>> GetAllRefundsAsync()
        {
            var refunds = await _unitOfWork.Refunds.GetAllAsync();

            var refundList = _mapper.Map<List<RefundResponseDTO>>(refunds);

            return new ApiResponse<List<RefundResponseDTO>>(200, refundList, true);
        }

        public async Task<ApiResponse<RefundResponseDTO>> GetRefundByIdAsync(int id)
        {
            var refund = await _unitOfWork.Refunds.GetRefundWithOrderDetailsByIdAsync(id);

            if (refund == null)
                return new ApiResponse<RefundResponseDTO>(404, "Refund not found.");

            var refundResponse = _mapper.Map<RefundResponseDTO>(refund);

            return new ApiResponse<RefundResponseDTO>(200, refundResponse, true);
        }

        public async Task<ApiResponse<List<PendingRefundResponseDTO>>> GetEligibleRefundsAsync()
        {
            var cancellations = await _unitOfWork.Cancellations.GetEligibleRefundsAsync();

            var eligibleRefunds = _mapper.Map<List<PendingRefundResponseDTO>>(cancellations);

            return new ApiResponse<List<PendingRefundResponseDTO>>(200, eligibleRefunds, true);
        }

        public async Task<ApiResponse<RefundResponseDTO>> ProcessRefundAsync(RefundRequestDTO refundRequest)
        {
            var cancellation = await _unitOfWork.Cancellations.GetCancellationWithDetailsByIdAsync(refundRequest.CancellationId);
            if (cancellation == null)
                return new ApiResponse<RefundResponseDTO>(404, "Cancellation request not found.");
            if (cancellation.Status != CancellationStatus.Approved)
                return new ApiResponse<RefundResponseDTO>(400, "Only approved cancellations are eligible for refunds.");

            var existingRefund = await _unitOfWork.Refunds.GetRefundByCancellationIdAsync(refundRequest.CancellationId);
            if (existingRefund != null)
                return new ApiResponse<RefundResponseDTO>(400, "Refund for this cancellation request has already been initiated.");

            var payment = cancellation.Order.Payment;
            if (payment == null || payment.PaymentMethod.ToLower() == "COD")
                return new ApiResponse<RefundResponseDTO>(400, "No payment associated with the order.");

            decimal computedRefundAmount = cancellation.OrderAmount - (cancellation.CancellationCharges ?? 0.00m);
            if (computedRefundAmount <= 0)
                return new ApiResponse<RefundResponseDTO>(400, "Computed refund amount is not valid.");

            var refund = _mapper.Map<Refund>(refundRequest);

            refund.PaymentId = payment.Id;
            refund.Amount = computedRefundAmount;

            await _unitOfWork.Refunds.AddAsync(refund);
            await _unitOfWork.SaveChangesAsync();

            var gatewayResponse = await ProcessRefundPaymentAsync(refund);
            if (gatewayResponse.IsSuccess)
            {
                refund.Status = RefundStatus.Completed;
                refund.TransactionId = gatewayResponse.TransactionId;
                refund.CompletedAt = DateTime.UtcNow;
                payment.Status = PaymentStatus.Refunded;

                _unitOfWork.Payments.Update(payment);
                if (cancellation.Order.Customer != null && !string.IsNullOrEmpty(cancellation.Order.Customer.Email))
                    await _emailService.SendRefundSuccessEmailAsync(refund, cancellation, $"Your Refund Has Been Processed Successfully, Order #{cancellation.Order.OrderNumber}");
                else
                    refund.Status = RefundStatus.Failed;
            }

            _unitOfWork.Refunds.Update(refund);
            await _unitOfWork.SaveChangesAsync();

            var refundResponse = _mapper.Map<RefundResponseDTO>(refund);

            return new ApiResponse<RefundResponseDTO>(200, refundResponse, true);
        }


        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateRefundStatusAsync(RefundStatusUpdateDTO statusUpdate)
        {
            var refund = await _unitOfWork.Refunds.GetRefundWithDetailsByIdAsync(statusUpdate.RefundId);
            if (refund == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Refund not found.");
            if (refund.Status != RefundStatus.Pending && refund.Status != RefundStatus.Failed)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Only pending or failed refunds can be updated.");

            _mapper.Map(statusUpdate, refund);

            _unitOfWork.Payments.Update(refund.Payment);
            _unitOfWork.Refunds.Update(refund);
            await _unitOfWork.SaveChangesAsync();

            if (refund.Cancellation?.Order?.Customer != null && !string.IsNullOrEmpty(refund.Cancellation.Order.Customer.Email))
                await _emailService.SendRefundSuccessEmailAsync(refund, refund.Cancellation, $"Your Refund Has Been Processed Successfully, Order #{refund.Cancellation.Order.OrderNumber}");

            var confirmation = new ConfirmationResponseDTO
            {
                Message = $"Refund with ID {refund.Id} has been updated to {refund.Status}."
            };
            return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
        }

        public async Task<PaymentGatewayRefundResponseDTO> ProcessRefundPaymentAsync(Refund refund)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            var random = new Random();
            double chance = random.NextDouble();
            if (chance < 0.70)
            {
                return new PaymentGatewayRefundResponseDTO
                {
                    IsSuccess = true,
                    Status = RefundStatus.Completed,
                    TransactionId = $"TXN-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
                };
            }
            else if (chance < 0.90)
            {
                return new PaymentGatewayRefundResponseDTO
                {
                    IsSuccess = false,
                    Status = RefundStatus.Failed
                };
            }
            else
            {
                return new PaymentGatewayRefundResponseDTO
                {
                    IsSuccess = false,
                    Status = RefundStatus.Pending
                };
            }
        }
    }
}
