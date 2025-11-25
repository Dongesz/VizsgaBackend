using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        // Konstruktor a modelek is dtok kapcsolatanak meghatarozasara, ez az automapper mukodesehez szukseges. 
        public AutoMapperProfile()
        {
            // Mapper config a Users modelre
            CreateMap<User, UsersGetDto>();        
            CreateMap<UsersSendDto, User>();       
            CreateMap<User, UsersSendDto>();       

            // Mapper config a Scoreboard modelre
            CreateMap<Scoreboard, ScoreboardGetDto>();   
            CreateMap<ScoreboardSendDto, Scoreboard>();  
            CreateMap<Scoreboard, ScoreboardSendDto>();  
        }
    }
}
