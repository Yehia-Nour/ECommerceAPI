using ECommerceAPI.DTOs;
using ECommerceAPI.DTOs.OrderDTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Services.Implementations;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<ApiResponse<List<OrderResponseDTO>>>> GetAllOrders()
        {
            var response = await _orderService.GetAllOrdersAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<ActionResult<ApiResponse<OrderResponseDTO>>> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<ApiResponse<OrderResponseDTO>>> CreateOrder([FromBody] OrderCreateDTO orderDto)
        {
            var customerId = User.GetCustomerId();
            var response = await _orderService.CreateOrderAsync(orderDto,customerId);

            return StatusCode(response.StatusCode, response);
        }


        [HttpPut("UpdateOrderStatus")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateOrderStatus([FromBody] OrderStatusUpdateDTO statusDto)
        {
            var response = await _orderService.UpdateOrderStatusAsync(statusDto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetOrdersByCustomer")]
        public async Task<ActionResult<ApiResponse<List<OrderResponseDTO>>>> GetOrdersByCustomer()
        {
            var customerId = User.GetCustomerId();

            var response = await _orderService.GetOrdersByCustomerAsync(customerId);

            return StatusCode(response.StatusCode, response);
        }
    }
}
