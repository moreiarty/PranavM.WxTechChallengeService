using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.Configurations;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Responses;
using Microsoft.Extensions.Options;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Implementation
{
    public class WooliesRecruitmentShopperHistoryClient : IWooliesRecruitmentShopperHistoryClient
    {
        private readonly HttpClient _httpClient;
        private readonly WooliesRecruitmentServiceConfig _wooliesRecruitmentConfig;

        public WooliesRecruitmentShopperHistoryClient(
            HttpClient client,
            IOptionsSnapshot<WooliesRecruitmentServiceConfig> wooliesRecruitmentConfig)
        {
            _httpClient = client;
            _wooliesRecruitmentConfig = wooliesRecruitmentConfig.Value;
            _httpClient.BaseAddress = new Uri(_wooliesRecruitmentConfig.BaseUrl);
        }

        public async Task<List<GetShopperHistoryResponse>> GetShopperHistory()
        {
            var response = await _httpClient.GetAsync($"shopperHistory?token={_wooliesRecruitmentConfig.Token}");

            return await HandleHttpResponse<List<GetShopperHistoryResponse>>(response);
        }

        public async Task<T> HandleHttpResponse<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}