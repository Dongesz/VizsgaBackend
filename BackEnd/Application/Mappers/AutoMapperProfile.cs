using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UsersGetDto>();
            CreateMap<User, UsersSendDto>();
            CreateMap<Scoreboard, ScoreboardGetDto>();
            CreateMap<Scoreboard, ScoreboardSendDto>();
        }
    }
}
