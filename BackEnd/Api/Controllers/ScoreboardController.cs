using BackEnd.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly DongeszhCastLContext _context;
        public ScoreboardController(DongeszhCastLContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Scoreboards.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(ulong id)
        {
            var scoreboard = _context.Scoreboards.Find(id);
            return Ok(scoreboard);
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
