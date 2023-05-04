using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.Utils
{
    public class MappingRequestAttribute:Attribute
    {
        public string Description { get; set; }
        public MappingRequestAttribute(string description)
        {
            Description = description;
        }

    }
}
