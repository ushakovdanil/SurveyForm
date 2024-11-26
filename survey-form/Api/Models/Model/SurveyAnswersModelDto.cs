using System;
using System.Runtime.Serialization;

namespace Api.Models.Model
{
    [DataContract]
    public class SurveyFormResponse<T>
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "response")]
        public T Response { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }
    }

    [DataContract]
    public class SurveyForm
    {
        [DataMember(Name = "queueId")]
        public Guid QueueId { get; set; }

        [DataMember(Name = "surveyId")]
        public Guid SurveyId { get; set; }
    }

    public class IsSurveyQueueIsActualResponse
    {
        public bool IsActive { get; set; }
        public Guid SurveyId { get; set; }
    }



    [DataContract]
    public class SurveyQuestionDto
    {
        public SurveyQuestionDto()
        {

        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "nextQuestionId")]
        public Guid NextQuestionId { get; set; }

        [DataMember(Name = "isFirstQuestion")]
        public bool IsFirstQuestion { get; set; }

        [DataMember(Name = "isLastQuestion")]
        public bool IsLastQuestion { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }

        [DataMember(Name = "questionText")]
        public string QuestionText { get; set; }

        [DataMember(Name = "regularExpression")]
        public string RegularExpression { get; set; }

        [DataMember(Name = "validationErrorText")]
        public string ValidationErrorText { get; set; }

    }

    [DataContract]
    public class SurveyAnswerDto
    {
        [DataMember(Name = "surveyQuestionId")]
        public Guid SurveyQuestionId { get; set; }

        [DataMember(Name = "questionId")]
        public Guid QuestionId { get; set; }

        [DataMember(Name = "answerData")]
        public string AnswerData { get; set; }

        [DataMember(Name = "queueId")]
        public Guid QueueId { get; set; }
    }

    public static class SurveyStatusConstatns
    {
        public static Guid InProgress => Guid.Parse("f8673142-c49c-4898-995f-97ccceab584f");
        public static Guid Canceled => Guid.Parse("dd2c5bfd-04a5-4505-b3db-a64485b22658");
        public static Guid Completed => Guid.Parse("d1466228-a9aa-481c-9df2-02d0c7818027");
        public static Guid Waiting => Guid.Parse("1c229941-aa8d-4e44-9ae1-93bf2f5266ba");
        public static Guid Ignored => Guid.Parse("d2cb73e4-ebbb-4a9b-a9f5-a69386592040");
    }

    public enum ResponseType
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        NotFound = 404,
    }
}

