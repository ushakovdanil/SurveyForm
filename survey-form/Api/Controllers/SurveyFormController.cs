using Api.Models.Model;
using Api.Services;
using Api.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyFormController : BaseController
    {
        [HttpGet]
        [Route("сheckIsSurveyActive")]
        public async Task<IActionResult> CheckIsSurveyActive(
        [FromQuery] Guid queueId,
        ISurveyValidatingService surveyValidatingService)
        {
            var response = await surveyValidatingService.CheckIsSurveyActive(queueId);
            return ResponseFromServiceProcessingResult(response, ResponseType.Ok);
        }

        [HttpPost]
        [Route("saveUserSurveyAnswer")]
        public async Task<IActionResult> SaveUserSurveyAnswer(
            [FromBody] SurveyAnswerDto surveyAnswerDto,
            [FromQuery] Guid queueId,
            ISurveyValidatingService surveyValidatingService)
        {
            var response = await surveyValidatingService.SaveUserSurveyAnswer(surveyAnswerDto);
            return ResponseFromServiceProcessingResult(response, ResponseType.Ok);
        }
    }
}