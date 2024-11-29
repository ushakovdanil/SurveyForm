using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using Api.Models.Model;
using Api.Services.Abstract;

namespace Api.Services
{
	public class SurveyValidatingService: ISurveyValidatingService
    {
        private readonly INetworkService _networkService;
		public SurveyValidatingService(INetworkService networkService)
		{
            _networkService = networkService;
		}

        public async Task<ServiceResponse<SurveyFormResponse<List<SurveyQuestionDto>>>> CheckIsSurveyActiveWithQuestions(Guid queueId)
        {
            var data = new Dictionary<string, object>();
            data.Add("queueId", queueId);
            var isSurveyActive = await _networkService.MakeApiCall<SurveyFormResponse<List<SurveyQuestionDto>>>(
                "CheckSurveyIsActive",
                HttpMethod.Get,
                new CancellationToken(),
                data);
            if (!isSurveyActive.IsSuccess)
            {
                return isSurveyActive;
            }

            return isSurveyActive;
        }

        public async Task<ServiceResponse> SaveUserSurveyAnswer(SurveyAnswerDto surveyAnswerDto)
        {
            var data = new Dictionary<string, object>();
            data.Add("surveyQuestionId", surveyAnswerDto.SurveyQuestionId);
            var surveyQuestionInfo = await _networkService.MakeApiCall<SurveyFormResponse<SurveyQuestionDto>>(
                "GetCurrentSurveyQuestion",
                HttpMethod.Get,
                new CancellationToken(),
                data
            ).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(surveyQuestionInfo.Response.Response.RegularExpression))
            {
                if (!ValidateAnswer(surveyAnswerDto.AnswerData, surveyQuestionInfo.Response.Response.RegularExpression))
                {
                    return ServiceResponseBuilder.Failure(surveyQuestionInfo.Response.Response.ValidationErrorText);
                }
            }

            var saveResponse = await _networkService.MakeApiCall<SurveyFormResponse<string>>(
                "SaveUserQurveyAnswer",
                HttpMethod.Post,
                new CancellationToken(),
                surveyAnswerDto
            ).ConfigureAwait(false);

            return saveResponse.IsSuccess
                ? ServiceResponseBuilder.Success()
                : ServiceResponseBuilder.Failure("Помилка при збереженні відповіді.");
        }

        private bool ValidateAnswer(string answer, string pattern)
        {
            return Regex.IsMatch(answer, pattern);
        }
    }
}

