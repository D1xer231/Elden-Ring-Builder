using Elden_Ring_Builder.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Elden_Ring_Builder.Services
{
    using System.Text.Json;

    public class WeaponApiService
    {
        private readonly HttpClient _http = new();

        public async Task<List<WeaponApiModel>> GetWeaponsAsync()
        {
            string json = await _http.GetStringAsync("https://eldenring.fanapis.com/api/weapons");

            var response = JsonSerializer.Deserialize<WeaponResponse>(json);
            return response.data;
        }
    }
}
