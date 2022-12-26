using ESMP.STOCK.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMP.STOCK.API.Utils
{
    public class WriteOff
    {
        private static List<HCNRHBean> hCNRHBeans = new List<HCNRHBean>();
        public static List<HCNRHBean> HCNRHBeans { get { return hCNRHBeans; } }
    }
}
