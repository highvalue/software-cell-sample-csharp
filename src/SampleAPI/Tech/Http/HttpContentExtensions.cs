﻿using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleAPI.Tech.Http
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content) =>
            await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync());
    }
}
