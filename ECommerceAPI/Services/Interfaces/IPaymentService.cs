using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.PaymentDTOs;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<ApiResponse<PaymentResponseDTO>> ProcessPaymentAsync(PaymentRequestDTO paymentRequest, int customerId);
        Task<ApiResponse<PaymentResponseDTO>> GetPaymentByIdAsync(int paymentId);
        Task<ApiResponse<PaymentResponseDTO>> GetPaymentByOrderIdAsync(int orderId);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdatePaymentStatusAsync(PaymentStatusUpdateDTO statusUpdate);
        Task<ApiResponse<ConfirmationResponseDTO>> CompleteCODPaymentAsync(CODPaymentUpdateDTO codPaymentUpdateDTO);
    }
}
