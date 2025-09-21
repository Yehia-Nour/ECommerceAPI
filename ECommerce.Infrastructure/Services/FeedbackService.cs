using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.FeedbackDTOs;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Domain.Entities;


namespace ECommerce.Infrastructure.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<FeedbackResponseDTO>>> GetAllFeedbackAsync()
    {
        var feedbacks = await _unitOfWork.Feedbacks.GetAllAsync();

        var feedbackList = _mapper.Map<List<FeedbackResponseDTO>>(feedbacks);

        return new ApiResponse<List<FeedbackResponseDTO>>(200, feedbackList, true);
    }

    public async Task<ApiResponse<FeedbackResponseDTO>> SubmitFeedbackAsync(FeedbackCreateDTO feedbackCreateDTO, int customerId)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
            return new ApiResponse<FeedbackResponseDTO>(404, "Customer not found.");

            var product = await _unitOfWork.Products.GetByIdAsync(feedbackCreateDTO.ProductId);
        if (product == null)
            return new ApiResponse<FeedbackResponseDTO>(404, "Product not found.");

        var orderItem = await _unitOfWork.OrderItems.GetDeliveredOrderItemAsync(feedbackCreateDTO.ProductId, customerId);
        if (orderItem == null)
            return new ApiResponse<FeedbackResponseDTO>(400, "Invalid OrderItemId. Customer must have purchased the product.");

        bool alreadyExists = await _unitOfWork.Feedbacks
                .ExistsForCustomerAndProductAsync(customerId, feedbackCreateDTO.ProductId);
        if (alreadyExists)
            return new ApiResponse<FeedbackResponseDTO>(400, "Feedback for this product and order item already exists.");

        var feedback = _mapper.Map<Feedback>(feedbackCreateDTO);
        feedback.CustomerId = customerId;
        await _unitOfWork.Feedbacks.AddAsync(feedback);
        await _unitOfWork.SaveChangesAsync();

        var feedbackResponse = _mapper.Map<FeedbackResponseDTO>(feedback);

        return new ApiResponse<FeedbackResponseDTO>(200, feedbackResponse, true);
    }

    public async Task<ApiResponse<ProductFeedbackResponseDTO>> GetFeedbackForProductAsync(int productId)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(productId);
        if (product == null)
            return new ApiResponse<ProductFeedbackResponseDTO>(404, "Product not found.");

        var feedbacks = await _unitOfWork.Feedbacks.GetFeedbacksWithCustomerByProductIdAsync(productId);
        double averageRating = 0;

        List<CustomerFeedback> customerFeedbacks = new List<CustomerFeedback>();
        if (feedbacks.Any())    
        {
            averageRating = feedbacks.Average(f => f.Rating);
            customerFeedbacks = _mapper.Map<List<CustomerFeedback>>(feedbacks);
        }


        var productFeedbackResponse = new ProductFeedbackResponseDTO
        {
            ProductId = product.Id,
            ProductName = product.Name,
            AverageRating = Math.Round(averageRating, 2),
            Feedbacks = customerFeedbacks
        };
        return new ApiResponse<ProductFeedbackResponseDTO>(200, productFeedbackResponse, true);
    }

    public async Task<ApiResponse<FeedbackResponseDTO>> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackUpdateDTO, int customerId)
    {
        var feedback = await _unitOfWork.Feedbacks.GetFeedbackWithDetailsByIdAndCustomerAsync(feedbackUpdateDTO.FeedbackId, customerId);
        if (feedback == null)
            return new ApiResponse<FeedbackResponseDTO>(404, "Either Feedback or Customer not found.");

        _mapper.Map(feedbackUpdateDTO, feedback);
        await _unitOfWork.SaveChangesAsync();

        var feedbackResponse = _mapper.Map<FeedbackResponseDTO>(feedback);

        return new ApiResponse<FeedbackResponseDTO>(200, feedbackResponse, true);
    }

    public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteFeedbackAsync(int feedbackId, int customerId)
    {
        var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(feedbackId);

        if (feedback == null)
            return new ApiResponse<ConfirmationResponseDTO>(404, "Feedback not found.");
        if (feedback.CustomerId != customerId)
            return new ApiResponse<ConfirmationResponseDTO>(401, "You are not authorized to delete this feedback.");

        _unitOfWork.Feedbacks.Delete(feedback);
        await _unitOfWork.SaveChangesAsync();

        var confirmation = new ConfirmationResponseDTO
        {
            Message = $"Feedback with Id {feedbackId} deleted successfully."
        };
        return new ApiResponse<ConfirmationResponseDTO>(200, confirmation);
    }
}
