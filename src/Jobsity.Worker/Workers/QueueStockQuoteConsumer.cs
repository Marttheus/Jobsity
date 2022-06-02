using Jobsity.Application.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobsity.Worker.Handlers;

namespace Jobsity.Worker.Workers
{
    public class QueueStockQuoteConsumer : IConsumer<StockConsumer>
    {
        public async Task Consume(ConsumeContext<StockConsumer> context)
        {
            var stockQuotePerShare = await StockQuoteHandler.GetCurrentStockQuote(context.Message.StockCode);

            await context.RespondAsync(new StockResponse(stockQuotePerShare));
        }
    }
}
