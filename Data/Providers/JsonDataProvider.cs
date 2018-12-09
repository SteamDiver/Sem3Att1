using System;
using System.Collections.Generic;
using System.IO;
using Data.Candles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data.Providers
{
    public class JsonDataProvider : FileDataProvider
    {
        public JsonDataProvider(FileInfo source) : base(source)
        {
        }

        protected override void ReadDataFromFile()
        {
            using (var stream = new StreamReader(new FileStream(Source.FullName, FileMode.Open)))
            {
                var content = stream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                var t = ((JArray) data["t"]).ToObject<List<long>>();
                var o = ((JArray) data["o"]).ToObject<List<decimal>>();
                var c = ((JArray) data["c"]).ToObject<List<decimal>>();
                var h = ((JArray) data["h"]).ToObject<List<decimal>>();
                var l = ((JArray) data["l"]).ToObject<List<decimal>>();

                for (var i = 0; i < t.Count; i++)
                {
                    var tValue = t[i];
                    var oValue = o[i];
                    var cValue = c[i];
                    var hValue = h[i];
                    var lValue = l[i];

                    Candles.Enqueue(new Candle(tValue, hValue, lValue, oValue, cValue));
                }
            }
        }
    }
}
