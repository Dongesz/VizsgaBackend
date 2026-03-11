using System.Threading;
using System.Threading.Tasks;
using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.News;

namespace BackEnd.Application.Services
{
    public interface INewsService
    {
        Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseOutputDto> CreateAsync(NewsCreateInputDto dto, CancellationToken cancellationToken = default);
    }
}

