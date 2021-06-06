using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
  public class OrderLine : BaseModel
    {
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public double TaxRate { get; set; }
        public decimal Tax { get; set; }
        public double DiscountRate { get; set; }
        public decimal Discount { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
      
        public LoopusExchangeType LoopusExchangeType { get; set; }
        
                

    }
}
