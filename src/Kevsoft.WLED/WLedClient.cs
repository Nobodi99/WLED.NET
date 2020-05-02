﻿using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kevsoft.WLED
{
    public class WLedClient
    {
        private readonly HttpClient _client;

        public WLedClient(HttpMessageHandler httpMessageHandler, string baseUri)
        {
            _client = new HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri(baseUri, UriKind.Absolute),
            };
        }

        public WLedClient(string baseUri) : this(new HttpClientHandler(), baseUri)
        {

        }

        public async Task<WLedRootResponse> Get()
        {
            var message = await _client.GetAsync("json");

            message.EnsureSuccessStatusCode();

            var response = await JsonSerializer.DeserializeAsync<WLedRootResponse>(await message.Content.ReadAsStreamAsync());

            return response;
        }

        public async Task<State> GetState()
        {
            var message = await _client.GetAsync("json/state");

            message.EnsureSuccessStatusCode();

            var response = await JsonSerializer.DeserializeAsync<State>(await message.Content.ReadAsStreamAsync());

            return response;
        }
    }
}
