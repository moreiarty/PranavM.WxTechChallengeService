using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PranavM.WxTechChallengeService.WebApi.Utilities.Helpers;

namespace PranavM.WxTechChallengeService.WebApi.Controllers.Implementation
{
    public class WxTechChallengeControllerImplementation : IWxTechChallengeController
    {
        private const string USER_NAME = "Pranav More";

        private const string USER_TOKEN = "9e1b0aee-31bb-4422-b068-ec01afae5f90";

        public async Task<ActionResult<GetUserResponse>> GetUserAsync(string x_Tracking_Id = null)
        {
            var response = new GetUserResponse
            {
                Name = USER_NAME,
                Token = USER_TOKEN,
            };

            return response.ToJsonApiOKResponse();
        }
    }
}