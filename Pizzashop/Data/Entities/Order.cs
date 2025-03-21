using Pizzashop.Enums;

namespace Pizzashop.Data.Entities
{
    
    public class Order
    {

        public int Id { get; set;}  
        public string? OrderId   { get; set;} // TODO: Add max length attrib

        public DateTime CreatedDate { get; set;} = DateTime.Now;

        public OrderStatus OrderStatus { get; set;}

        public OrderType  OrderType {get; set;} 

        public DateTime? ETA { get; set;}
    }
}