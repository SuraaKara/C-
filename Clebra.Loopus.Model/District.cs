using Clebra.Loopus.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Model
{
    public class District : BaseModel
    {
        [MaxLength(128)]
        public string Name { get; set; }

        public Guid? NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}
