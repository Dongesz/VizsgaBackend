using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Application.Services
{
    public interface IAuthApiClient
    {
        Task<bool> DeleteIdentityUserAsync(string authUserId, CancellationToken cancellationToken = default);
        Task<bool> UpdateIdentityUserNameAsync(string authUserId, string newUserName, CancellationToken cancellationToken = default);
    }
}

