using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Application.DTOs.Scoreboard;
using BackEnd.Application.DTOs.News;
using BackEnd.Application.DTOs.User;
using BackEnd.Domain.Models;

namespace BackEnd.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        // Konstruktor a modelek is dtok kapcsolatanak meghatarozasara, ez az automapper mukodesehez szukseges. 
        public AutoMapperProfile()
        {
            // Mapper config a Users modelre
            CreateMap<User, UsersGetOutputDto>();        
            CreateMap<User, UserResultGetAllOutputDto>();

            // Mapper config a Scoreboard modelre
            CreateMap<Scoreboard, ScoreboardGetOutputDto>();   
            CreateMap<ScoreboardSendInputDto, Scoreboard>();  
            CreateMap<Scoreboard, ScoreboardSendInputDto>();  

            // Mapper config a News modelre
            CreateMap<News, NewsGetOutputDto>();
        }
    }
}
