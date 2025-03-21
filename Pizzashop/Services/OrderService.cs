using Pizzashop.Models;
using Pizzashop.Enums;
using Pizzashop.Data;

namespace Pizzashop.Services;
public class OrderService : IOrderService
{
    private readonly PizzaShopContext _dbContext;

    public OrderService(PizzaShopContext context)
    {
        _dbContext = context;
    }
    public Order PlaceOrder(OrderRequest order)
    {
        var orderId = new Guid().ToString();
        var dbOrder = new Data.Entities.Order(){
            OrderId = orderId,
            CreatedDate = DateTime.UtcNow,
            ETA = DateTime.UtcNow.AddMinutes(30),
            OrderStatus = OrderStatus.ORDERED,
            OrderType = Enum.Parse<OrderType>(order.OrderType)
        };
        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            _dbContext.Orders.Add(dbOrder);
            _dbContext.SaveChanges();
            transaction.Commit();
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine(ex.ToString());
            throw;
        }
        return new Order()
        {
            OrderId = orderId,
            CreatedDate = dbOrder.CreatedDate,
            OrderStatus = OrderStatus.ORDERED,
            OrderType = dbOrder.OrderType,
            ETA = dbOrder.ETA
        };
    }

    public Order CancelOrder(string orderId) => new()
    {
        OrderId = orderId,
        CreatedDate = DateTime.Now,
        ETA = DateTime.Now.AddMinutes(30),
        OrderStatus = OrderStatus.CANCELED
    };
}