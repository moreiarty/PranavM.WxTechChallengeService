using System.Collections.Generic;
using System.Threading.Tasks;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Responses;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces
{
    public interface IWooliesRecruitmentShopperHistoryClient
    {
        Task<List<GetShopperHistoryResponse>> GetShopperHistory();
    }
}