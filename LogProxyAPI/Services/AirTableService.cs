using LogProxyAPI.Interfaces;
using LogProxyAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LogProxyAPI.Services
{
    public class AirTableService : IAirTableService
    {              
        readonly HttpClient _httpClient;
        readonly string _serviceURL;

        public AirTableService(IConfiguration configuration)
        {                  
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["AirTable:Token"]);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _serviceURL = configuration["AirTable:URL"];           
        }      

        public async Task<AirTableGetResponseDTO> GetMessagesAsync()
        {           
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(new Uri($"{_serviceURL}Messages?maxRecords=3&view=Grid%20view"));
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AirTableGetResponseDTO>(result);               
            }
            else
            {
                throw new Exception($"Errors when fetching data from AirTable service, status code: {httpResponse.StatusCode}, reason: {httpResponse.ReasonPhrase }");
            }           
        }      

        public async Task<AirTableSaveResponseDTO> SaveMessageAsync(AirTableSaveRequestDTO request)
        {          
            HttpContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync(new Uri($"{_serviceURL}Messages"), content);
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AirTableSaveResponseDTO>(result);
            }
            else
            {
                throw new Exception($"Errors when saving data to AirTable service, status code: {httpResponse.StatusCode}, reason: {httpResponse.ReasonPhrase }");
            }
        }
    }
}
