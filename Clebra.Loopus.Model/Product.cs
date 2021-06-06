using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class Product : BaseModel
    {
        [MaxLength(128)]
        public string Code { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(512),DataType(DataType.MultilineText)]
        public string Description { get; set;}
   
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }

        public Guid? ProductStockId { get; set; }
        public ProductStock ProductStock { get; set; }

        public Guid? CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public Guid? DiscountDefinitionId { get; set; }
        public DiscountDefinition DiscountDefinition { get; set; }

        public Guid? ClothTypeId { get; set; }
        public ClothType ClothType { get; set; }

        public ICollection<ProductFile> Files { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Color> Colors { get; set; }
        public ICollection<ProductSize> Sizes { get; set; }


    }
}