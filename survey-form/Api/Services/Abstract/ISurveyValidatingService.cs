using System;
using Api.Models.Model;

namespace Api.Services.Abstract
{
	public interface ISurveyValidatingService
	{
		public Task<ServiceResponse<SurveyFormResponse<List<SurveyQuestionDto>>>> CheckIsSurveyActiveWithQuestions(Guid queueId);
		public Task<ServiceResponse> SaveUserSurveyAnswer(SurveyAnswerDto surveyAnswerDto);
	}
}

