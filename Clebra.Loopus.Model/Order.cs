using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
   public class Order : BaseModel
    {
        [MaxLength(128)]
        public string BillNo { get; set; }
        [MaxLength(128)]
        public string OrderNo { get; set; }
        
        public decimal TotalProductPrice { get; set; }
        public decimal TotalTaxPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPrice { get; set; }


        public Guid? UserId { get; set; }
        public LoopusUser LoopusUser { get; set; }
        
        public LoopusExchangeType LoopusExchangeType { get; set; }
        public decimal ExchangeRate { get; set; }

        public Guid? BillAddressId { get; set; }
        public Address BillAddress { get; set; }
        
        public Guid? DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        public LoopusOrderStatus OrderStatus { get; set; }
        public LoopusPaymentType PaymentType { get; set; }
        public OrderType OrderType { get; set; }

        public IList<OrderLine> OrderLines { get; set; }

       


    }
}
