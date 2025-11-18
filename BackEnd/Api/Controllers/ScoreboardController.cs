using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Application.Mappers;
using BackEnd.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreboardController : ControllerBase, IScoreboardController
    {
        public Task<ScoreboardGetDto> CreateAsync(ScoreboardSendDto dto, CancellationToken cancellationToken = default)
        {
            
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ScoreboardGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ScoreboardGetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, ScoreboardSendDto dto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
