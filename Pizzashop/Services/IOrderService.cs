using Pizzashop.Models;

namespace Pizzashop.Services;
public interface IOrderService
{
    Order PlaceOrder(OrderRequest request);
    Order CancelOrder(string orderId);
}