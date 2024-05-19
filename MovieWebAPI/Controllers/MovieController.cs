using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibraryEntities.Dao;
using MovieLibraryEntities.Models;

namespace MovieWebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public MovieController(IRepository repository, ILogger<MovieController> logger) {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("movies")]
        public IEnumerable<Movie> MovieIndex()
        {
            return _repository.GetAllMovies();
        }

        [HttpGet("movies/{id:int}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                var result = await _repository.GetMovie(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("movies/{title}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovieByTitle(string title)
        {
            try
            {
                var result = await _repository.GetMovieByTitle(title);
                if (result == null)
                {
                    return NotFound();
                }
                return CreatedAtAction(nameof(GetMovieByTitle), title, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }


        //https://www.pragimtech.com/blog/blazor/post-in-aspnet-core-rest-api/
        [HttpPost("movies")]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest();
                }

                var createdMovie = await _repository.AddMovie(movie);

                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, createdMovie);
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error crating new movie");
            }
        }

        [HttpPut("movies")]
        public async Task<ActionResult<Movie>> UpdateMovie([FromBody] Movie movie)
        {
            try
            {
                if (movie==null)
                { return BadRequest(); }

                var updatedMovie = await _repository.UpdateMovie(movie);
                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, updatedMovie);
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error crating new movie");
            }
        }

        [HttpGet("users")]
        public IEnumerable<User> UserIndex()
        {
            return _repository.GetAllUsers();
        }

        [HttpGet("users/{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var result = await _repository.GetUser(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost("users")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var createdUser = await _repository.AddUser(user);

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUser);
            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Error crating new user");
            }
        }

    }
}
