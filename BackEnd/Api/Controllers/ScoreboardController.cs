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
    public class ScoreboardController : ControllerBase
    {
        private readonly DongeszhCastLContext _context; // referencia a contexthez
        private readonly IMapper _mapper; // referencia az AutoMapperre
        
        public ScoreboardController(DongeszhCastLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var scoreboard = _context.Scoreboards.ToList();
                var scoredto = _mapper.Map<List<ScoreBoardDto>>(scoreboard);

                return Ok(scoredto);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(ulong id)
        {
            try
            {
                var scoreboard = _context.Scoreboards.Find(id);
                var scoredto = _mapper.Map<List<ScoreBoardDto>>(scoreboard);
                return Ok(scoredto);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Scoreboard scoreboard)
        {
            _context.Scoreboards.Add(scoreboard);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = scoreboard.UserId }, scoreboard);
        }

        [HttpPut("{id}")]
        public IActionResult Update(ulong id,Scoreboard scoreboard)
        {
            _context.Entry(scoreboard).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(ulong id)
        {
            var scoreboard = _context.Scoreboards.Find(id);
            _context.Scoreboards.Remove(scoreboard);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
