using Clebra.Loopus.Core.Model;
using Clebra.Loopus.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Model
{
    public class ProductSize : BaseModel
    { 
        [MaxLength(32)]
        public string SizeOption { get; set; }

        public ProductSizeType Type { get; set; }
        public ICollection<Product> Product { get; set; }

    }
}
