using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class ProductFile : BaseModel
    {
        public string Name { get; set; }
        [MaxLength(512), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.ImageUrl)]
        public string FileUrl { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
