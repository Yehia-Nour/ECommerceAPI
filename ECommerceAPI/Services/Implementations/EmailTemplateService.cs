using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string GetOrderConfirmationTemplate(Order order, Payment? payment)
        {
            return $@"
<html>
<head>
    <meta charset='UTF-8'>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px;'>
    <div style='max-width: 700px; margin: auto; background-color: #ffffff; padding: 20px; border: 1px solid #dddddd;'>
        <div style='background-color: #007bff; padding: 15px; text-align: center; color: #ffffff;'>
            <h2 style='margin: 0;'>Order Confirmation</h2>
        </div>
        <p>Dear {order.Customer.FirstName} {order.Customer.LastName},</p>
        <p>Thank you for your order. Please find your invoice below.</p>
        
        <!-- Order Summary -->
        <h3 style='color: #007bff;'>Order Summary</h3>
        <p><strong>Total Amount:</strong> {order.TotalAmount:C}</p>

        <h3 style='color: #007bff;'>Payment Details</h3>
        <p><strong>Method:</strong> {(payment != null ? payment.PaymentMethod : "N/A")}</p>
        <p><strong>Status:</strong> {(payment != null ? payment.Status.ToString() : "N/A")}</p>
        
        <p>Best regards,<br/>Your E-Commerce Store Team</p>
    </div>
</body>
</html>";
        }

        public string GetCancellationAcceptedTemplate(Cancellation cancellation)
        {
            return $@"
<html>
<head><meta charset='UTF-8'></head>
<body style='font-family: Arial, sans-serif; background-color: #f0f8ff; padding: 20px;'>
<div style='max-width: 600px; margin: auto; background-color: #fff; padding: 20px; border: 1px solid #ccc;'>
    <div style='background-color: #28a745; padding: 15px; text-align: center; color: #fff;'>
        <h2>Cancellation Request {cancellation.Status}</h2>
    </div>
    <p>Dear {cancellation.Order.Customer.FirstName} {cancellation.Order.Customer.LastName},</p>
    <p>Your cancellation request for Order <strong>#{cancellation.Order.OrderNumber}</strong> has been <span style='color:#28a745;font-weight:bold;'>{cancellation.Status}</span>.</p>

    <h3 style='color:#28a745;'>Cancellation Details</h3>
    <ul>
        <li><strong>Reason:</strong> {cancellation.Reason}</li>
        <li><strong>Admin Remark:</strong> {cancellation.Remarks}</li>
        <li><strong>Requested At:</strong> {cancellation.RequestedAt:MMMM dd, yyyy HH:mm}</li>
        <li><strong>Processed At:</strong> {(cancellation.ProcessedAt.HasValue ? cancellation.ProcessedAt.Value.ToString("MMMM dd, yyyy HH:mm") : "N/A")}</li>
        <li><strong>Order Amount:</strong> {cancellation.OrderAmount:C}</li>
        <li><strong>Charges:</strong> {cancellation.CancellationCharges:C}</li>
        <li><strong>Refund:</strong> {cancellation.OrderAmount - (cancellation.CancellationCharges ?? 0):C}</li>
    </ul>

    <p>Thank you for shopping with us.</p>
</div>
</body>
</html>";
        }

        public string GetCancellationRejectedTemplate(Cancellation cancellation)
        {
            return $@"
<html>
<head><meta charset='UTF-8'></head>
<body style='font-family: Arial, sans-serif; background-color: #f8f9fa; padding: 20px;'>
<div style='max-width:600px; margin:auto; background:#fff; padding:20px; border-radius:8px; box-shadow:0 4px 8px rgba(0,0,0,0.1);'>
    <div style='background-color:#dc3545; padding:20px; text-align:center; color:#fff;'>
        <h2>Cancellation Request Rejected</h2>
    </div>
    <p>Dear {cancellation.Order.Customer.FirstName} {cancellation.Order.Customer.LastName},</p>
    <p>Your cancellation request for Order <strong>#{cancellation.Order.OrderNumber}</strong> has been <span style='color:#dc3545;'>Rejected</span>.</p>

    <h3 style='color:#dc3545;'>Rejection Details</h3>
    <ul>
        <li><strong>Reason:</strong> {cancellation.Reason}</li>
        <li><strong>Admin Remark:</strong> {cancellation.Remarks}</li>
        <li><strong>Requested At:</strong> {cancellation.RequestedAt:MMMM dd, yyyy HH:mm}</li>
        <li><strong>Processed At:</strong> {(cancellation.ProcessedAt.HasValue ? cancellation.ProcessedAt.Value.ToString("MMMM dd, yyyy HH:mm") : "N/A")}</li>
    </ul>

    <p>If you have any questions, please contact our support team.</p>
</div>
</body>
</html>";
        }
    }
}
