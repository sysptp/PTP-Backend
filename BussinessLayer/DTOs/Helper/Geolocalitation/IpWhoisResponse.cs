using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Helper.Geolocalitation
{
    public class IpWhoisResponse
    {
        [JsonPropertyName("ip")]
        public string? Ip { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("continent")]
        public string? Continent { get; set; }

        [JsonPropertyName("continent_code")]
        public string? ContinentCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("country_flag")]
        public string? CountryFlag { get; set; }

        [JsonPropertyName("country_capital")]
        public string? CountryCapital { get; set; }

        [JsonPropertyName("country_phone")]
        public string? CountryPhone { get; set; }

        [JsonPropertyName("country_neighbours")]
        public string? CountryNeighbours { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }

        [JsonPropertyName("asn")]
        public string? Asn { get; set; }

        [JsonPropertyName("org")]
        public string? Organization { get; set; }

        [JsonPropertyName("isp")]
        public string? Isp { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("timezone_name")]
        public string? TimezoneName { get; set; }

        [JsonPropertyName("timezone_dstOffset")]
        public int? TimezoneDstOffset { get; set; }

        [JsonPropertyName("timezone_gmtOffset")]
        public int? TimezoneGmtOffset { get; set; }

        [JsonPropertyName("timezone_gmt")]
        public string? TimezoneGmt { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("currency_code")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string? CurrencySymbol { get; set; }

        [JsonPropertyName("currency_rates")]
        public decimal? CurrencyRates { get; set; }

        [JsonPropertyName("currency_plural")]
        public string? CurrencyPlural { get; set; }
    }
}
