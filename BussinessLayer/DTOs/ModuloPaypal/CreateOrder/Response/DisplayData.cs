using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloPaypal.CreateOrder.Response
{
    public record DisplayData(
    [JsonProperty("brand_name")] string BrandName
);
}
