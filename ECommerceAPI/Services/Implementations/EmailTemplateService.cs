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

        <!-- Header -->
        <div style='background-color: #007bff; padding: 15px; text-align: center; color: #ffffff;'>
            <h2 style='margin: 0;'>Order Confirmation</h2>
        </div>

        <!-- Greeting -->
        <p style='margin: 20px 0 5px 0;'>
            Dear {order.Customer.FirstName} {order.Customer.LastName},
        </p>
        <p style='margin: 5px 0 20px 0;'>
            Thank you for your order. Please find your invoice below.
        </p>

        <!-- Order Info -->
        <table style='width: 100%; border-collapse: collapse; margin-bottom: 20px;'>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'>
                    <strong>Order Number:</strong>
                </td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{order.OrderNumber}</td>
            </tr>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'>
                    <strong>Order Date:</strong>
                </td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>
                    {order.OrderDate:MMMM dd, yyyy}
                </td>
            </tr>
        </table>

        <!-- Order Summary -->
        <h3 style='color: #007bff; border-bottom: 2px solid #eeeeee; padding-bottom: 5px;'>Order Summary</h3>
        <table style='width: 100%; border-collapse: collapse; margin-bottom: 20px;'>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Sub Total:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{order.TotalBaseAmount:C}</td>
            </tr>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Discount:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>-{order.TotalDiscountAmount:C}</td>
            </tr>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Shipping Cost:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{order.ShippingCost:C}</td>
            </tr>
            <tr style='font-weight: bold;'>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Total Amount:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{order.TotalAmount:C}</td>
            </tr>
        </table>

        <!-- Order Items -->
        <h3 style='color: #007bff; border-bottom: 2px solid #eeeeee; padding-bottom: 5px;'>Order Items</h3>
        <table style='width: 100%; border-collapse: collapse; margin-bottom: 20px;'>
            <tr style='background-color: #28a745; color: #ffffff;'>
                <th style='padding: 8px; border: 1px solid #dddddd;'>Product</th>
                <th style='padding: 8px; border: 1px solid #dddddd;'>Quantity</th>
                <th style='padding: 8px; border: 1px solid #dddddd;'>Unit Price</th>
                <th style='padding: 8px; border: 1px solid #dddddd;'>Discount</th>
                <th style='padding: 8px; border: 1px solid #dddddd;'>Total Price</th>
            </tr>
            {string.Join("", order.OrderItems.Select(item => $@"
                <tr>
                    <td style='padding: 8px; border: 1px solid #dddddd;'>{item.Product.Name}</td>
                    <td style='padding: 8px; border: 1px solid #dddddd;'>{item.Quantity}</td>
                    <td style='padding: 8px; border: 1px solid #dddddd;'>{item.UnitPrice:C}</td>
                    <td style='padding: 8px; border: 1px solid #dddddd;'>{item.Discount:C}</td>
                    <td style='padding: 8px; border: 1px solid #dddddd;'>{item.TotalPrice:C}</td>
                </tr>"))}
        </table>

        <!-- Payment Details -->
        <h3 style='color: #007bff; border-bottom: 2px solid #eeeeee; padding-bottom: 5px;'>Payment Details</h3>
        <table style='width: 100%; border-collapse: collapse; margin-bottom: 20px;'>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Payment Method:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{(payment != null ? payment.PaymentMethod : "N/A")}</td>
            </tr>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Payment Date:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{(payment != null ? payment.PaymentDate.ToString("MMMM dd, yyyy HH:mm") : "N/A")}</td>
            </tr>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Transaction ID:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{(payment != null ? payment.TransactionId : "N/A")}</td>
            </tr>
            <tr>
                <td style='padding: 8px; background-color: #f8f8f8; border: 1px solid #dddddd;'><strong>Status:</strong></td>
                <td style='padding: 8px; border: 1px solid #dddddd;'>{(payment != null ? payment.Status.ToString() : "N/A")}</td>
            </tr>
        </table>

        <p style='margin-top: 20px;'>If you have any questions, please contact our support team.</p>
        <p>Best regards,<br/>Your E-Commerce Store Team</p>
    </div>
</body>
</html>";
        }
    }
}
