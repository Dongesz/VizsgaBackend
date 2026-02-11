using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Application.Services
{
    /// <summary>
    /// Simple HTTP client abstraction for calling the AuthApi from the BackEnd.
    /// Currently used for deleting the underlying Identity user when a player deletes their account.
    /// </summary>
    public interface IAuthApiClient
    {
        /// <summary>
        /// Deletes the Identity user with the given authUserId in the AuthApi.
        /// Throws or returns false on failure depending on implementation.
        /// </summary>
        Task<bool> DeleteIdentityUserAsync(string authUserId, CancellationToken cancellationToken = default);
    }
}

