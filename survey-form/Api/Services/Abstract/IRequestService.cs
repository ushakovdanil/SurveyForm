using Api.Models.Entities;

namespace Api.Services.Abstract
{
    public interface IRequestService
    {
        Task<Request> GetRequestById(Guid id);
        Task<Request> GetInProgressRequestByUser(UserEntity user);
        void AddRequest(Request request);
        void UpdateRequest(Request request);
        void DeleteRequest(Request request);
    }
}