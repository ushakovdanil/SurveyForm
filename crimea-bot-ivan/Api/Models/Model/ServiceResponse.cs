using System;
using FluentValidation.Results;

namespace Api.Models.Model
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public ServiceResponse(List<string> errors)
        {
            IsSuccess = false;
            Error = string.Join(" ", errors);
        }

        public ServiceResponse(string error)
        {
            IsSuccess = false;
            Error = error;
        }

        public ServiceResponse()
        {
            IsSuccess = true;
        }

        public static implicit operator ServiceResponse(List<ValidationFailure> validationResults)
        {
            var errors = validationResults
                .Select(error => error.ErrorMessage)
                .ToList<string>();

            return new ServiceResponse(errors);
        }
    }

    /// <summary>
    /// Base class for helper services responses which can provide errors or generic types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Response { get; private set; }

        public ServiceResponse(T response)
            : base()
        {
            Response = response;
        }

        public ServiceResponse(List<string> errors = null)
            : base(errors)
        {
        }

        public static implicit operator ServiceResponse<T>(List<ValidationFailure> validationResults)
        {
            var errors = validationResults
                .Select(error => error.ErrorMessage)
                .ToList<string>();

            return new ServiceResponse<T>(errors);
        }

        public static implicit operator ServiceResponse<T>(T result)
        {
            return ServiceResponseBuilder.Success(result);
        }
    }

    public static class ServiceResponseBuilder
    {
        public static ServiceResponse<T> Success<T>(T response) => new(response);
        public static ServiceResponse Success() => new();

        public static ServiceResponse Failure(string error = null)
        {
            return new ServiceResponse(error);
        }

        public static ServiceResponse Failure(List<string> errors = null)
        {
            return new ServiceResponse(errors);
        }

        public static ServiceResponse<T> Failure<T>(List<string> errors = null)
        {
            return new ServiceResponse<T>(errors);
        }


    }
}

