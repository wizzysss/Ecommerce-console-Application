using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Models
{
    public class OrderItem
    {
        //public int OrderItemId { get; set; } // Primary Key
        //public int? OrderId { get; set; } // Foreign Key
        //public int? ProductId { get; set; } // Foreign Key
        //public int? Quantity { get; set; }
        private int orderItemId;
        private int orderId;
        private int productId;
        private int quantity;

        // Order_item_id (Primary Key)
        public int OrderItemId
        {
            get { return orderItemId; }
            set { orderItemId = value; }
        }

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public OrderItem() { 
        
        }
    public OrderItem(int orderitem,int orderid,int productid,int quantity)
        {
            OrderItemId = orderitem;
            OrderId = orderid;
            ProductId = productid;
            Quantity = quantity; 
        }

        public override string ToString()
        {
            return $"{OrderItemId} {OrderId} {ProductId} {Quantity}";
        }
    }
}
