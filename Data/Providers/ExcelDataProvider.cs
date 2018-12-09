using System;
using System.Globalization;
using System.IO;
using System.Timers;
using Data.Candles;
using Data.Interfaces;
using OfficeOpenXml;

namespace Data.Providers
{
    public class ExcelDataProvider : FileDataProvider, IPeriodic
    {
        public Timer Timer { get; } = new Timer(5000);

        public ExcelDataProvider(FileInfo source) : base(source)
        {
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var data = GetData();
            OnDataReceived(data);
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
                    var openValue = Convert.ToDecimal(worksheet.Cells[i, 5].Value);
                    var highValue = Convert.ToDecimal(worksheet.Cells[i, 6].Value);
                    var lowValue = Convert.ToDecimal(worksheet.Cells[i, 7].Value);
                    var closeValue = Convert.ToDecimal(worksheet.Cells[i, 8].Value);

                    var date = DateTime.ParseExact($"{dateValue} {timeValue}", "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                    var candle = new Candle(highValue, lowValue, openValue, closeValue, date);

                    Candles.Enqueue(candle);
                }
            }
        }
    }
}
