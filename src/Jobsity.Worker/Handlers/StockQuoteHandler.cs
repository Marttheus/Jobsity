using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Jobsity.Worker.Model;
using RestSharp;
using System.Globalization;

namespace Jobsity.Worker.Handlers
{
    public class StockQuoteHandler
    {
        private static readonly CultureInfo _culture = new CultureInfo("en-US");

        public static async Task<string> GetCurrentStockQuote(string stockCode)
        {
            var client = new RestClient("https://stooq.com/q/l/");

            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddQueryParameter("s", stockCode);
            request.AddQueryParameter("f", "sd2t2ohlcv");
            request.AddQueryParameter("h", "");
            request.AddQueryParameter("e", "csv");

            var response = await client.ExecuteAsync(request);
            client.Dispose();

            var content = response.Content;
            if (string.IsNullOrEmpty(content))
                return $"Stock {stockCode.ToUpper()} not found.";

            Quote? quote = null;

            using (TextReader textReader = new StringReader(content))
            {
                var csvReader = new CsvReader(textReader, _culture);
                try { quote = csvReader.GetRecords<Quote>().FirstOrDefault(); } catch (ReaderException) { };
            }

            if (quote is null) return $"No quote for the stock {stockCode.ToUpper()} was found.";

            return $"{stockCode.ToUpper()} quote is {string.Format(_culture, "{0:C}", quote.Close)} per share";
        }
    }
}
