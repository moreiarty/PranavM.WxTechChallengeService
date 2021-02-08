using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.Configurations;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Responses;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Implementation
{
    public class WooliesRecruitmentProductsClient : IWooliesRecruitmentProductsClient
    {
        private readonly HttpClient _httpClient;
        private readonly WooliesRecruitmentServiceConfig _wooliesRecruitmentConfig;

        public WooliesRecruitmentProductsClient(
            HttpClient client,
            IOptionsSnapshot<WooliesRecruitmentServiceConfig> wooliesRecruitmentConfig)
        {
            _httpClient = client;
            _wooliesRecruitmentConfig = wooliesRecruitmentConfig.Value;
            _httpClient.BaseAddress = new Uri(_wooliesRecruitmentConfig.BaseUrl);
        }

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"products?token={_wooliesRecruitmentConfig.Token}");

            return await HandleHttpResponse<List<ProductResponse>>(response);
        }

        public async Task<T> HandleHttpResponse<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}