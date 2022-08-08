﻿using ApiModulo9.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace ApiModulo9.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    public class ProductControllerV2 : ControllerBase
    {
        private const string ApiTestURL = "https://fakestoreapi.com/products";
        private const string AppID = "62f0c4e25d948335ddfbf73e";
        private readonly HttpClient _httpClient;

        public ProductControllerV2(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }
        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetProductDataV2")]

        public async Task<IActionResult> GetUsersDataAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("app-id", AppID);
            var response = await _httpClient.GetStreamAsync(ApiTestURL);
            var usersData = await JsonSerializer.DeserializeAsync<ProductResponseData>(response);

            return Ok(usersData);
        }
    }
}