using System;
using System.Net.Http;
using System.Threading.Tasks;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.Configurations;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Responses;
using Microsoft.Extensions.Options;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Requests;
using Newtonsoft.Json;
using System.Text;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Implementation
{
    public class WooliesRecruitmentTrolleyClient : IWooliesRecruitmentTrolleyClient
    {
        private readonly HttpClient _httpClient;
        private readonly WooliesRecruitmentServiceConfig _wooliesRecruitmentConfig;

        private static readonly string CONTENT_TYPE = "application/json";
        public WooliesRecruitmentTrolleyClient(
            HttpClient client,
            IOptionsSnapshot<WooliesRecruitmentServiceConfig> wooliesRecruitmentConfig)
        {
            _httpClient = client;
            _wooliesRecruitmentConfig = wooliesRecruitmentConfig.Value;
            _httpClient.BaseAddress = new Uri(_wooliesRecruitmentConfig.BaseUrl);
        }

        public async Task<decimal> CalculateTrolleyTotal(CalculateTrolleyRequest calculateRequest)
        {
            string requestJsonString = JsonConvert.SerializeObject(calculateRequest);

            var requestHttpContent = new StringContent(requestJsonString, Encoding.UTF8, CONTENT_TYPE);

            var uriBuilder = new UriBuilder(_httpClient.BaseAddress.AbsoluteUri);

            uriBuilder.Path += "trolleyCalculator";
            uriBuilder.Query = $"token={_wooliesRecruitmentConfig.Token}";
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                uriBuilder.Uri
            );
            request.Content = requestHttpContent;

            var response = await _httpClient.SendAsync(request);

            return await HandleHttpResponse<decimal>(response);
        }
        public async Task<T> HandleHttpResponse<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}