using System;

namespace Clebra.Loopus.Core.Model
{
    public abstract class BaseModel : ILoopusModel
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid();
            this.IsActive = true;
        }
        
        public Guid Id { get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}