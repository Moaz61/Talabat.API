using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } = null!;

        //public int DeliveryMethodId { get; set; }  // Foreign Key
        public DeliveryMethod? DeliveryMethod { get; set; } = null!;  // Navigational Property [One]

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();  // Navigational Property [Many]

        public decimal Subtotal { get; set; }

        //To Not Map It Lazm yb2a Awlha Get
        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } = string.Empty; 
    }
}
