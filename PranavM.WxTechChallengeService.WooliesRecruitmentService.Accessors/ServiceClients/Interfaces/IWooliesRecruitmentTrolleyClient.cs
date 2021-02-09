using System.Threading.Tasks;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Requests;
using PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Responses;

namespace PranavM.WxTechChallengeService.WooliesRecruitmentService.Accessors.ServiceClients.Interfaces
{
    public interface IWooliesRecruitmentTrolleyClient
    {
        Task<decimal> CalculateTrolleyTotal(CalculateTrolleyRequest calculateTrolleyRequest);
    }
}