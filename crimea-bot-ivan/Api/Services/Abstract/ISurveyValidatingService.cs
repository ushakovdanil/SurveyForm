using System;
using Api.Models.Model;

namespace Api.Services.Abstract
{
	public interface ISurveyValidatingService
	{
		public Task<ServiceResponse<List<SurveyQuestionDto>>> CheckIsSurveyActive(Guid queueId);
		public Task<ServiceResponse<string>> SaveUserSurveyAnswer(SurveyAnswerDto surveyAnswerDto);
	}
}

