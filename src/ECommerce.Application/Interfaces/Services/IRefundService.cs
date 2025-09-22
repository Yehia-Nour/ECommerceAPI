using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.RefundDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

public interface IRefundService
{
    Task<ApiResponse<List<RefundResponseDTO>>> GetAllRefundsAsync();
    Task<ApiResponse<RefundResponseDTO>> GetRefundByIdAsync(int id);
    Task<ApiResponse<List<PendingRefundResponseDTO>>> GetEligibleRefundsAsync();
    Task<ApiResponse<RefundResponseDTO>> ProcessRefundAsync(RefundRequestDTO refundRequest);
    Task<ApiResponse<ConfirmationResponseDTO>> UpdateRefundStatusAsync(RefundStatusUpdateDTO statusUpdate);
    Task<PaymentGatewayRefundResponseDTO> ProcessRefundPaymentAsync(Refund refund);
}