using Api.Models.Entities;
using Api.Models.Enums;
using Api.Repository.Abstract;
using Api.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class RequestService : IRequestService
    {
        private IRepository<Request> _requestRepository;

        public RequestService(IRepository<Request> requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<Request> GetRequestById(Guid id)
        {
            return await _requestRepository.FilteredList(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Request> GetInProgressRequestByUser(UserEntity user)
        {
            return await _requestRepository.FilteredList(x => x.User.Id == user.Id && x.Status == UserRequestStatus.InProgres).FirstOrDefaultAsync();
        }

        public void AddRequest(Request request)
        {
            _requestRepository.Insert(request);
        }

        public void DeleteRequest(Request request)
        {
            _requestRepository.Delete(request);
        }

        public void UpdateRequest(Request request)
        {
            _requestRepository.Update(request);
        }
    }
}
