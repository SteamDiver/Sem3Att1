using System;
using System.Globalization;
using System.IO;
using System.Timers;
using Data.Candles;
using Data.Interfaces;
using OfficeOpenXml;

namespace Data.Providers
{
    public class ExcelDataProvider : FileDataProvider
    {
      
        public ExcelDataProvider(FileInfo source) : base(source)
        {
        }

        protected override void ReadDataFromFile()
        {
            using (var package = new ExcelPackage(Source))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    var dateValue = worksheet.Cells[i, 3].Value;
                    var timeValue = worksheet.Cells[i, 4].Value;
                    var openValue = Convert.ToDecimal(worksheet.Cells[i, 5].Value) / 1e5m;
                    var highValue = Convert.ToDecimal(worksheet.Cells[i, 6].Value) / 1e5m;
                    var lowValue = Convert.ToDecimal(worksheet.Cells[i, 7].Value) / 1e5m;
                    var closeValue = Convert.ToDecimal(worksheet.Cells[i, 8].Value) / 1e5m;

                    var date = DateTime.ParseExact($"{dateValue} {timeValue}", "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                    var candle = new Candle(highValue, lowValue, openValue, closeValue, date);

                    Candles.Enqueue(candle);
                }
            }
        }
    }
}
