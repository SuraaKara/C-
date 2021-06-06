using System;
using System.ComponentModel.DataAnnotations;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class Address : BaseModel
    {
        public AddressType Type { get; set; }
        [MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(256), DataType(DataType.MultilineText)]
        public string Detail { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public Guid? UserId { get; set; }
        public LoopusUser LoopusUser { get; set; }

        public Guid? CountryId { get; set; }
        public Country Country { get; set; }

    }
}
