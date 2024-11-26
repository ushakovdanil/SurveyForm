using System;
using Api.Models.Model;
using Api.Services.Abstract;

namespace Api.Services
{
	public class SurveyValidatingService: ISurveyValidatingService
    {
		public SurveyValidatingService()
		{

		}

        public Task<ServiceResponse<List<SurveyQuestionDto>>> CheckIsSurveyActive(Guid queueId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> SaveUserSurveyAnswer(SurveyAnswerDto surveyAnswerDto)
        {
            throw new NotImplementedException();
        }
    }
}

