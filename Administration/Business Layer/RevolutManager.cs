using Business_Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Administration.Business_Layer
{
    public class RevolutManager
    {
        public async Task<T> Get<T>(string Endpoint, Gateway Gateway)
        {
            HttpClient _httpClient = new HttpClient();
            JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();
            string responseContent = "";
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Gateway.GatewayBearerToken);
                var response = await _httpClient.GetAsync(Gateway.BaseAddress + Endpoint);
                if (response.Content != null)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                if (response.IsSuccessStatusCode)
                {
                    //  _logger.Info(responseContent);


                    return JsonConvert.DeserializeObject<T>(responseContent, _jsonSerializerSettings);
                }
                else
                {
                    //  _logger.Error(response.StatusCode + "Error: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, responseContent);
            }
            return default(T);
        }

        public async Task<Result<T>> Post<T>(string Endpoint, Gateway Gateway, object obj)
        {
            JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();
            HttpClient _httpClient = new HttpClient();
            string responseContent = "";
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Gateway.GatewayBearerToken);
                string postData = JsonConvert.SerializeObject(obj, _jsonSerializerSettings);
                var response = await _httpClient.PostAsync(Gateway.BaseAddress + Endpoint, new StringContent(postData, Encoding.UTF8, "application/json"));
                if (response.Content != null)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                if (response.IsSuccessStatusCode)
                {
                    return Result.Ok(JsonConvert.DeserializeObject<T>(responseContent, _jsonSerializerSettings));
                }
                else if (!string.IsNullOrEmpty(responseContent))
                {
                    return Result.Fail<T>(JsonConvert.DeserializeObject<ErrorModel>(responseContent, _jsonSerializerSettings).Message);
                }
                else
                {
                    //_logger.Error($"Error posting data. Status code: {response.StatusCode}, Response: null");
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
            }
            return Result.Fail<T>();
        }


    }
}