using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Services;

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

    public string GetRefundSuccessTemplate(Refund refund, string orderNumber, Cancellation cancellation)
    {
        var egyptZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

        string completedAtStr = refund.CompletedAt.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(refund.CompletedAt.Value, egyptZone)
                .ToString("dd MMM yyyy HH:mm:ss")
            : "N/A";

        return $@"
<html>
<body style='font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; border: 1px solid #ddd;'>
        <div style='padding: 20px; text-align: center; background-color: #2E86C1; color: #ffffff;'>
            <h2 style='margin: 0;'>Your Refund is Complete</h2>
        </div>
        <div style='padding: 20px;'>
            <p>Dear Customer,</p>
            <p>Your refund has been processed successfully. Below are the details:</p>
            <table style='width: 100%; border-collapse: collapse;'>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Order Number</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>{orderNumber}</td>
                </tr>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Refund Transaction ID</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>{refund.TransactionId}</td>
                </tr>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Order Amount</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>₹{cancellation.OrderAmount}</td>
                </tr>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Cancellation Charges</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>₹{cancellation.CancellationCharges ?? 0.00m}</td>
                </tr>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Cancellation Reason</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>{cancellation.Reason}</td>
                </tr>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Refund Method</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>{refund.RefundMethod}</td>
                </tr>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Refunded Amount</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>₹{refund.Amount}</td>
                </tr>
                <tr>
                    <td style='border: 1px solid #ddd; padding: 8px; font-weight: bold;'>Completed At</td>
                    <td style='border: 1px solid #ddd; padding: 8px;'>{completedAtStr}</td>
                </tr>
            </table>
            <p>Thank you for shopping with us.</p>
            <p>Best regards,<br/>The ECommerce Team</p>
        </div>
    </div>
</body>
</html>";
    }

}
