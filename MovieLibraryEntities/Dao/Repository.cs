using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public IEnumerable<Movie> Search(string searchString)
        {
            var allMovies = _context.Movies;
            var listOfMovies = allMovies.ToList();
            var temp = listOfMovies.Where(x => x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

            return temp;
        }

        public IEnumerable<Movie> ListMovies()
        {
            var allMovies = _context.Movies;
            var listOfMovies = allMovies.ToList();
            return listOfMovies;
        }

        public bool AddMovie(Movie movie)
        {
            if (movie == null)
            {
                return false;
            }

            _context.Add(movie);
            return _context.SaveChanges() > 0;
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
    }
}
