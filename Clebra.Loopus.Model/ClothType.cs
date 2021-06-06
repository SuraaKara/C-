using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class ClothType : BaseModel
    {
        [MaxLength(128)]
        public string Name { get; set; }
        
        public ICollection<YarnType> YarnTypes { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}