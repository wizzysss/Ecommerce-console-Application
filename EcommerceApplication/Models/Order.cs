using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EcommerceApplication.Models
{
    public class Order
    {
       

        private int orderId;
        private int? customerId;
        private DateTime? orderDate;
        private decimal? totalPrice;
        private string shippingAddress;

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public int? CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public DateTime? OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public decimal? TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public string ShippingAddress
        {
            get { return shippingAddress; }
            set { shippingAddress = value; }
        }

        public Order()
        {

        }

        public Order(int orderId,int customerId,DateTime orderDate,decimal totalPrice,string shippingaddr )
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            ShippingAddress = shippingaddr;
        }

        public override string ToString()
        {
            return $"{OrderId} {CustomerId} {OrderDate} {TotalPrice} {ShippingAddress}";
        }
    }
}
