using System;
using System.Net.Http;
using System.Threading.Tasks;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.Configurations;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Responses;
using Microsoft.Extensions.Options;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Implementation
{
    public class WooliesRecruitmentTrolleyClient : IWooliesRecruitmentTrolleyClient
    {
        private readonly HttpClient _httpClient;
        private readonly WooliesRecruitmentServiceConfig _wooliesRecruitmentConfig;
        public WooliesRecruitmentTrolleyClient(
            HttpClient client,
            IOptionsSnapshot<WooliesRecruitmentServiceConfig> wooliesRecruitmentConfig)
        {
            _httpClient = client;
            _wooliesRecruitmentConfig = wooliesRecruitmentConfig.Value;
            _httpClient.BaseAddress = new Uri(_wooliesRecruitmentConfig.BaseUrl);
        }

        public async Task<CalculateTrolleyResponse> CalculateTrolleyTotal()
        {
            throw new NotImplementedException();
        }
    }
}