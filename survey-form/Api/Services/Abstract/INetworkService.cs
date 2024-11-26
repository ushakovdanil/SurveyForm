using System;
using Api.Models.Model;

namespace Api.Services.Abstract
{
	public interface INetworkService
	{
        Task<ServiceResponse<TResult>> MakeApiCall<TResult>(
            string url,
            HttpMethod method,
            CancellationToken cancellationToken = default,
            object data = null)
            where TResult : class;
    }
}

