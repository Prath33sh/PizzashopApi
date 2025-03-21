using System;
using Pizzashop.Enums;

namespace Pizzashop.Models;

public class Order
{
    public string? OrderId   { get; set;}

    public DateTime CreatedDate { get; set;} = DateTime.Now;

    public OrderStatus OrderStatus { get; set;}

    public OrderType  OrderType {get; set;} 

    public DateTime? ETA { get; set;}
}
