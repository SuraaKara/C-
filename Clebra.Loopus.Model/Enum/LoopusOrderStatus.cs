using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Model
{
    public enum LoopusOrderStatus : byte
    {
        Delivered = 1,
        InCargo = 2,
        OrderReceived = 3
        
    }
}
