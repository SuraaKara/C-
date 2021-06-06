using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class ProductCategory : BaseModel
    {
        [MaxLength(128)]
        public string Code { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }

        public Guid? MasterId { get; set; }
        public ProductCategory Master { get; set; }

        public IList<ProductCategory> SubCategories { get; set; }
    }
}