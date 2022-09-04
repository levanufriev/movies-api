using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly DataContext _context;

        public MovieController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> Get()
        {
            return Ok(await _context.Movies.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return BadRequest("Movie not found.");
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<List<Movie>>> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return Ok(await _context.Movies.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Movie>>> UpdateMovie(Movie request)
        {
            var dbMovie = await _context.Movies.FindAsync(request.Id);
            if (dbMovie == null)
                return BadRequest("Movie not found.");

            dbMovie.Name = request.Name;
            dbMovie.Year = request.Year;
            dbMovie.Director = request.Director;

            await _context.SaveChangesAsync();

            return Ok(await _context.Movies.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Movie>>> Delete(int id)
        {
            var dbMovie = await _context.Movies.FindAsync(id);
            if (dbMovie == null)
                return BadRequest("Movie not found.");

            _context.Movies.Remove(dbMovie);
            await _context.SaveChangesAsync();

            return Ok(await _context.Movies.ToListAsync());
        }
    }
}
