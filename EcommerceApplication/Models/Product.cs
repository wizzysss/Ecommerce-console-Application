using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Models
{
    public class Product
    {
        //public int ProductId { get; set; } // Primary Key
        //public string Name { get; set; }
        //public int? Price { get; set; }
        //public string Description { get; set; }
        //public int? StockQuantity { get; set; }

        private int productId;
        private string name;
        private int? price;
        private string description;
        private int? stockQuantity;

        // Product_id (Primary Key)
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int? Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int? StockQuantity
        {
            get { return stockQuantity; }
            set { stockQuantity = value; }
        }


        public Product()
        {

        }

        public Product(int productid,string name,int price,string description,int stockquantity)
        {
            ProductId = productid;
            Name = name;
            Price = price;
            Description = description;
            StockQuantity = stockquantity;
        }

        public override string ToString()
        {
            return $"{ProductId} {Name} {Price} {Description}{StockQuantity}";
        }
    }
}
