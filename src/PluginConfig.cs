using System.Text.Json.Serialization;

namespace SimpleResetKnife_SwiftlyS2
{
    public class PluginConfig
    {
        [JsonPropertyName("prefix")]
        public string Prefix { get; set; } = " {DarkRed}[PoncikMarket] {White}";

        [JsonPropertyName("cooldown_seconds")]
        public int CooldownSeconds { get; set; } = 10;
    }
}