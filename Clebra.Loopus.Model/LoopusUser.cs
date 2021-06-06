using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class LoopusUser : BaseModel
    {
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [MaxLength(128)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress]
        public string Mail { get; set; }

        [MaxLength(32)]
        public string Identification { get; set; }

        public string UserFileUrl { get; set; }

        public LoopusUserRoleType RoleType { get; set; }
        
        public virtual IEnumerable<Address> Addresses { get; set; }
        
    }
}