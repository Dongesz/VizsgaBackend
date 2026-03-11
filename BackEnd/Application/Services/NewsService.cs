using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.News;
using BackEnd.Application.Helpers;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UploadHelper _uploadHelper;

        public NewsService(DatabaseContext context, IMapper mapper, UploadHelper uploadHelper)
        {
            _context = context;
            _mapper = mapper;
            _uploadHelper = uploadHelper;
        }

        public async Task<ResponseOutputDto> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var items = await _context.News
                .OrderByDescending(n => n.Date)
                .ToListAsync(cancellationToken);

            var dtos = _mapper.Map<List<NewsGetOutputDto>>(items);

            foreach (var n in dtos)
            {
                if (!string.IsNullOrWhiteSpace(n.Image) && !n.Image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    n.Image = $"https://dongesz.com/images/{n.Image}";
                }
            }

            return new ResponseOutputDto
            {
                Message = "Successful fetch!",
                Success = true,
                Result = dtos
            };
        }

        public async Task<ResponseOutputDto> CreateAsync(NewsCreateInputDto dto, CancellationToken cancellationToken = default)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var uploadResult = await _uploadHelper.UploadFileAsync(dto.Image, rootPath);
            if (uploadResult.Success != true || uploadResult.Result == null)
            {
                return new ResponseOutputDto
                {
                    Message = uploadResult.Message,
                    Success = false,
                    Result = uploadResult.Result
                };
            }

            var fileName = uploadResult.Result.ToString();

            var entity = new News
            {
                Title = dto.Title,
                Image = fileName,
                Date = dto.Date,
                Content = dto.Content
            };

            _context.News.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            var output = _mapper.Map<NewsGetOutputDto>(entity);

            if (!string.IsNullOrWhiteSpace(output.Image) && !output.Image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                output.Image = $"https://dongesz.com/images/{output.Image}";
            }

            return new ResponseOutputDto
            {
                Message = "News created successfully!",
                Success = true,
                Result = output
            };
        }
    }
}

