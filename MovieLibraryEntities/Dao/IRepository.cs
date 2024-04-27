using MovieLibraryEntities.Models;

namespace MovieLibraryEntities.Dao
{
    public interface IRepository
    {
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> Search(string searchString);
        IEnumerable<Movie> ListMovies();
        bool AddMovie(Movie movie);
        bool DeleteMovie(string title);
        bool UpdateMovieTitle(string title, string newTitle);
    }
}
