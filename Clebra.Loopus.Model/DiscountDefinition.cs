using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
   public class DiscountDefinition : BaseModel
    {
        [MaxLength(128)]
        public string DiscountCode { get; set; }
        public DiscountDefinitionType Type { get; set; }

        
        public double Value { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
