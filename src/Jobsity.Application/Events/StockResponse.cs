using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.Events
{
    public class StockResponse
    {
        public string StockQuotePerShare { get; set; }

        public StockResponse(string stockQuotePerShare)
        {
            StockQuotePerShare = stockQuotePerShare;
        }
    }
}
