using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
   public class Comment : BaseModel
    {
        [MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(512), DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        
        public Guid? UserId { get; set; }
        public LoopusUser LoopusUser { get; set; }

    }
}
