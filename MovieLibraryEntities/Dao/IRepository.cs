using MovieLibraryEntities.Models;

namespace MovieLibraryEntities.Dao
{
    public interface IRepository
    {
       
        IEnumerable<Movie> GetAllMovies();
        Task<Movie?> GetMovie(int id);
        Task<IEnumerable<Movie>> GetMovieByTitle(string title);
        Task<Movie?> AddMovie(Movie movie);
        Task<Movie?> UpdateMovie(Movie movie);
        IEnumerable<User> GetAllUsers();
        Task<User?> GetUser(int id);
        Task<User?> AddUser(User user);


        IEnumerable<Movie> Search(string searchString);
        IEnumerable<Movie> ListMovies();
        
        bool DeleteMovie(string title);
        bool UpdateMovieTitle(string title, string newTitle);

        bool UpdateRating(Movie movie);

        IEnumerable<Movie> GetTopRatedMovie(int startAge, int endAge);

        IEnumerable<Movie> GetTopRatedMovie(Occupation occupation);

    }
}
