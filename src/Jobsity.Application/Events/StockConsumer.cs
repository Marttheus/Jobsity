using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.Events
{
    public class StockConsumer
    {
        public string StockCode { get; set; }

        public StockConsumer(string stockCode)
        {
            StockCode = stockCode;
        }
    }
}
