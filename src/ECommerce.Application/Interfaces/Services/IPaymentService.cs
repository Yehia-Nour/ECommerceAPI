using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.PaymentDTOs;

namespace ECommerce.Application.Interfaces.Services;

public interface IPaymentService
{
    Task<ApiResponse<PaymentResponseDTO>> ProcessPaymentAsync(PaymentRequestDTO paymentRequest, int customerId);
    Task<ApiResponse<PaymentResponseDTO>> GetPaymentByIdAsync(int paymentId);
    Task<ApiResponse<PaymentResponseDTO>> GetPaymentByOrderIdAsync(int orderId);
    Task<ApiResponse<ConfirmationResponseDTO>> UpdatePaymentStatusAsync(PaymentStatusUpdateDTO statusUpdate);
    Task<ApiResponse<ConfirmationResponseDTO>> CompleteCODPaymentAsync(CODPaymentUpdateDTO codPaymentUpdateDTO);
}