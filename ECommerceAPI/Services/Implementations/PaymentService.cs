using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.PaymentDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Models.Enums;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;

namespace ECommerceAPI.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IMapper _mapper;


        public PaymentService(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PaymentResponseDTO>> ProcessPaymentAsync(PaymentRequestDTO paymentRequest, int customerId)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var order = await _unitOfWork.Orders.GetOrderWithPaymentAsync(paymentRequest.OrderId, customerId);

                if (order == null)
                    return new ApiResponse<PaymentResponseDTO>(404, "Order not found.");

                if (Math.Round(paymentRequest.Amount, 2) != Math.Round(order.TotalAmount, 2))
                    return new ApiResponse<PaymentResponseDTO>(400, "Payment amount does not match the order total.");

                Payment payment;

                if (order.Payment != null)
                {
                    if (order.Payment.Status == PaymentStatus.Failed &&
                        order.OrderStatus == OrderStatus.Pending)
                    {
                        payment = order.Payment;
                        payment.PaymentMethod = paymentRequest.PaymentMethod;
                        payment.Amount = paymentRequest.Amount;
                        payment.PaymentDate = DateTime.UtcNow;
                        payment.Status = PaymentStatus.Pending;
                        payment.TransactionId = null;

                        _unitOfWork.Payments.Update(payment);
                    }
                    else
                        return new ApiResponse<PaymentResponseDTO>(400, "Order already has an associated payment.");
                }
                else
                {
                    payment = new Payment
                    {
                        OrderId = paymentRequest.OrderId,
                        PaymentMethod = paymentRequest.PaymentMethod,
                        Amount = paymentRequest.Amount,
                        PaymentDate = DateTime.UtcNow,
                        Status = PaymentStatus.Pending
                    };

                    await _unitOfWork.Payments.AddAsync(payment);
                }

                if (!IsCashOnDelivery(paymentRequest.PaymentMethod))
                {
                    var simulatedStatus = await SimulatePaymentGateway();
                    payment.Status = simulatedStatus;

                    if (simulatedStatus == PaymentStatus.Completed)
                    {
                        payment.TransactionId = GenerateTransactionId();
                        order.OrderStatus = OrderStatus.Processing;
                    }
                }
                else
                {
                    order.OrderStatus = OrderStatus.Processing;
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                if (order.OrderStatus == OrderStatus.Processing)
                {
                    string subject = $"Order Confirmation - {order.OrderNumber}";
                    string body = _emailTemplateService.GetOrderConfirmationTemplate(order, payment);
                    await _emailService.SendEmailAsync(order.Customer.Email, subject, body, true);
                }

                var paymentResponse = _mapper.Map<PaymentResponseDTO>(payment);


                return new ApiResponse<PaymentResponseDTO>(200, paymentResponse);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new ApiResponse<PaymentResponseDTO>(500, "An unexpected error occurred while processing the payment.");
            }
        }

        public async Task<ApiResponse<PaymentResponseDTO>> GetPaymentByIdAsync(int paymentId)
        {
            try
            {
                var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);

                if (payment == null)
                    return new ApiResponse<PaymentResponseDTO>(404, "Payment not found.");

                var paymentResponse = _mapper.Map<PaymentResponseDTO>(payment);

                return new ApiResponse<PaymentResponseDTO>(200, paymentResponse);
            }
            catch (Exception)
            {
                return new ApiResponse<PaymentResponseDTO>(500, "An unexpected error occurred while retrieving the payment.");
            }
        }

        public async Task<ApiResponse<PaymentResponseDTO>> GetPaymentByOrderIdAsync(int orderId)
        {
            try
            {
                var payment = await _unitOfWork.Payments.GetPaymentByOrderIdAsync(orderId);

                if (payment == null)
                    return new ApiResponse<PaymentResponseDTO>(404, "Payment not found for this order.");

                var paymentResponse = _mapper.Map<PaymentResponseDTO>(payment);


                return new ApiResponse<PaymentResponseDTO>(200, paymentResponse);
            }
            catch (Exception)
            {
                return new ApiResponse<PaymentResponseDTO>(500, "An unexpected error occurred while retrieving the payment.");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdatePaymentStatusAsync(PaymentStatusUpdateDTO statusUpdate)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var payment = await _unitOfWork.Payments.GetPaymentWithOrderAsync(statusUpdate.PaymentId);

                if (payment == null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Payment not found.");

                payment.Status = statusUpdate.Status;

                if (statusUpdate.Status == PaymentStatus.Completed && !IsCashOnDelivery(payment.PaymentMethod))
                {
                    payment.TransactionId = statusUpdate.TransactionId;
                    payment.Order.OrderStatus = OrderStatus.Processing;
                }

                _unitOfWork.Payments.Update(payment);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                if (payment.Order.OrderStatus == OrderStatus.Processing)
                {
                    string subject = $"Order Confirmation - {payment.Order.OrderNumber}";
                    string body = _emailTemplateService.GetOrderConfirmationTemplate(payment.Order, payment);
                    await _emailService.SendEmailAsync(payment.Order.Customer.Email, subject, body, true);
                }

                var confirmation = new ConfirmationResponseDTO
                {
                    Message = $"Payment with ID {payment.Id} updated to status '{payment.Status}'."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new ApiResponse<ConfirmationResponseDTO>(500, "An unexpected error occurred while updating the payment status.");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> CompleteCODPaymentAsync(CODPaymentUpdateDTO codPaymentUpdateDTO)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var payment = await _unitOfWork.Payments.GetPaymentWithOrderAsync(codPaymentUpdateDTO.PaymentId);

                if (payment == null || payment.OrderId != codPaymentUpdateDTO.OrderId)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Payment not found for this order.");

                if (payment.Order == null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "No order associated with this payment.");

                if (payment.Order.OrderStatus != OrderStatus.Shipped)
                    return new ApiResponse<ConfirmationResponseDTO>(400, $"Order cannot be marked as Delivered from {payment.Order.OrderStatus} state.");

                if (!IsCashOnDelivery(payment.PaymentMethod))
                    return new ApiResponse<ConfirmationResponseDTO>(409, "Payment method is not Cash on Delivery.");

                payment.Status = PaymentStatus.Completed;
                payment.Order.OrderStatus = OrderStatus.Delivered;

                _unitOfWork.Payments.Update(payment);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                var confirmation = new ConfirmationResponseDTO
                {
                    Message = $"COD Payment for Order ID {payment.Order.Id} has been marked as 'Completed' and the order status updated to 'Delivered'."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new ApiResponse<ConfirmationResponseDTO>(500, "An unexpected error occurred while completing the COD payment.");
            }
        }


        private bool IsCashOnDelivery(string method)
            => method.Equals("COD", StringComparison.OrdinalIgnoreCase);

        private async Task<PaymentStatus> SimulatePaymentGateway()
        {
            await Task.Delay(500);
            return PaymentStatus.Completed;
        }

        private string GenerateTransactionId()
            => Guid.NewGuid().ToString("N");
    }
}
