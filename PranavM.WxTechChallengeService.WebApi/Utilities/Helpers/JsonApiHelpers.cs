using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PranavM.WxTechChallengeService.WebApi.Utilities.Helpers
{
    public static class JsonApiHelpers
    {
        private static ActionResult GenerateResponse(
            object responseBody,
            HttpStatusCode statusCode) =>
            new JsonResult(responseBody)
            {
                ContentType = "application/vnd+api.json",
                StatusCode = (int)statusCode
            };

        public static ActionResult ToJsonApiOKResponse(this object response) =>
            GenerateResponse(response, HttpStatusCode.OK);
    }
}