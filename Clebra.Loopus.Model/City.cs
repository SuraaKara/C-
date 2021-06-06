using Clebra.Loopus.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Model
{
    public class City : BaseModel
    {
        [MaxLength(128)]
        public string name { get; set; }

        public Guid? DistrictId { get; set; }
        public District District { get; set; }
    }
}
