using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorECommerceDemo.Shared
{
    public class OrderItem
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public OrderItem()
        {
            
        }
        public OrderItem(CartProductDTO item)
        {
            ProductId = item.ProductId;
            ProductTypeId = item.ProductTypeId;
            Quantity = item.Quantity;
            TotalPrice = item.Price * item.Quantity;
        }
    }
}
