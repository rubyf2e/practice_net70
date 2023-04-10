using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using practiceNet70.Data;
using practiceNet70.Models;

namespace practiceNet70.Controllers
{
    public class WebApiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet()]
        [Produces("application/json")]
        public async Task<IActionResult> GetImportedMovie()
        {
            var httpClient = _httpClientFactory.CreateClient("Movies");
            var httpResponseMessage = await httpClient.GetAsync("");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var responseData = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);

                return new JsonResult(responseData);
            }
            else
            {
                return new JsonResult(new JsonSerializerOptions { PropertyNamingPolicy = null });
            }
        }
      
    }
}
