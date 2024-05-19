using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;

namespace MovieLibraryEntities.Dao
{
    public class Repository : IRepository, IDisposable
    {
        private readonly IDbContextFactory<MovieContext> _contextFactory;
        private readonly MovieContext _context;

        public Repository(IDbContextFactory<MovieContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }



        //  MOVIES
        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public async Task<Movie?> GetMovie(int id)
        {
            return await _context
                .Movies
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetMovieByTitle(string title)
        {
            return await _context
                .Movies
                .Include(x => x.MovieGenres)
                .Where(m => m.Title.ToLower().Equals(title.ToLower()))
                .ToListAsync();
        }


        public async Task<Movie?> AddMovie(Movie movie)
        {
            var result = _context.Movies
                .AddAsync(movie).Result.Entity;

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Movie?> UpdateMovie(Movie movie)
        {
            var result = _context.Movies.Update(movie).Entity;
            await _context.SaveChangesAsync();
            return result;
        }


        //  USERS
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<User?> GetUser(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> AddUser(User user)
        {
            var result = _context.Users
                .AddAsync(user).Result.Entity;
            await _context.SaveChangesAsync();
            return result;
        }





        //   OLD METHODS

        public IEnumerable<Movie> ListMovies()
        {
            var allMovies = _context.Movies;
            var listOfMovies = allMovies.ToList();
            return listOfMovies;
        }


        public IEnumerable<Movie> Search(string searchString)
        {
            var allMovies = _context.Movies;
            var listOfMovies = allMovies.ToList();
            var temp = listOfMovies.Where(x => x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

            return temp;
        }

        public bool DeleteMovie(string title)
        {
            var movieToDelete = _context.Movies.FirstOrDefault(m => m.Title == title);
            if (movieToDelete == null)
            {
                return false;
            }
            _context.Movies.Remove(movieToDelete);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateMovieTitle(string title, string newTitle)
        {
            var movieToUpdate = _context.Movies.FirstOrDefault(m => m.Title == title);
            if (movieToUpdate == null)
            {
                return false;
            }
            movieToUpdate.Title = newTitle;
            _context.Movies.Update(movieToUpdate);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateRating(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetTopRatedMovie(int startAge, int endAge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetTopRatedMovie(Occupation occupation)
        {
            throw new NotImplementedException();
        }
    }
}
