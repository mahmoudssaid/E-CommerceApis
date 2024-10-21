using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public  class DeliveryMethodResult
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DelivryTime { get; set; }
        public decimal Price { get; set; }
    }
}
