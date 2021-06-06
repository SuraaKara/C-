using Clebra.Loopus.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clebra.Loopus.Model
{
    public class Color : BaseModel
    {
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Code { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}