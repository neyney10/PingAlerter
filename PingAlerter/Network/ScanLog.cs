using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    public class ScanLog
    {
        public DateTime Timestamp { get; }
        public IReadOnlyCollection<ScanResultLog> Results { get; }
        public IReadOnlyCollection<ScanAlert> Alerts { get; }

        public ScanLog(DateTime timestamp, IReadOnlyDictionary<string, ScanResult> scanResults, IReadOnlyCollection<ScanAlert> alerts)
        {
            this.Timestamp = timestamp;
            this.Alerts = alerts;
            this.Results = scanResults.Select(
                entry => new ScanResultLog(
                    entry.Key,
                    entry.Value.Min,
                    entry.Value.Max,
                    entry.Value.Avg,
                    entry.Value.Failed
                )
            ).ToList();
        }

        public ScanLog(DateTime timestamp, IReadOnlyCollection<ScanResultLog> results, IReadOnlyCollection<ScanAlert> alerts)
        {
            this.Timestamp = timestamp;
            this.Alerts = alerts;
            this.Results = results;
        }

        public JsonNode ToJson()
        {
            return new JsonObject
            {
                ["Timestamp"] = this.Timestamp,
                ["Results"] = new JsonArray(
                    this.Results.Select(
                        result => result.ToJson()
                    ).ToArray()
                ),
                ["Alerts"] = new JsonArray(
                    this.Alerts.Select(alert => JsonValue.Create(alert.Type.ToString()))
                           .ToArray()
                )
            };
        }

        public static ScanLog FromJson(JsonNode json)
        {
            JsonNode root = json.Root;
            return new ScanLog(
                DateTime.Parse(root["Timestamp"].ToString()),
                root["Results"].AsArray().Select(
                    res => new ScanResultLog(
                        res["Address"].ToString(),
                        res["Min"].GetValue<long>(),
                        res["Max"].GetValue<long>(),
                        res["Avg"].GetValue<long>(),
                        res["Failed"].GetValue<int>()
                    )
                ).ToList(),
                root["Alerts"].AsArray().Select(
                    alert => new ScanAlert(ScanAlert.AlertType.Spike)
               ).ToList()
            );
        }

    }
}
