using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class ProductStock : BaseModel
    {
        public double Amount { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }  
    }
}
