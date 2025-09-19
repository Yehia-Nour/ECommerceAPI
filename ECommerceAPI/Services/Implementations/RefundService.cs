using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.RefundDTOs;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.UoW;

namespace ECommerceAPI.Services.Implementations
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
            throw new NotImplementedException();
        }

        public async Task<PaymentGatewayRefundResponseDTO> ProcessRefundPaymentAsync(Refund refund)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<PendingRefundResponseDTO>>> GetEligibleRefundsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<RefundResponseDTO>> ProcessRefundAsync(RefundRequestDTO refundRequest)
        {
            throw new NotFiniteNumberException();
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateRefundStatusAsync(RefundStatusUpdateDTO statusUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<RefundResponseDTO>> GetRefundByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefundSuccessEmailBody(Refund refund, string orderNumber, Cancellation cancellation)
        {
            throw new NotImplementedException();
        }

    }
}
