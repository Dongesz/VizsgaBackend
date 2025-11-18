using AutoMapper;
using BackEnd.Application.DTOs;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public UsersController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var user = _context.Users.ToList();
                var userDto = _mapper.Map<List<UsersGetDto>>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                var userDto = _mapper.Map<UsersGetDto>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                var userDto = _mapper.Map<UsersSendDto>(user);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, userDto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                var userDto = _mapper.Map<UsersSendDto>(user);
                return NoContent();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                var scores = _context.Scoreboards.Where(s => s.UserId == id);
                _context.Scoreboards.RemoveRange(scores);

                _context.Users.Remove(user);
                _context.SaveChanges();
                var userDto = _mapper.Map<UsersSendDto>(user);
                return NoContent();

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
