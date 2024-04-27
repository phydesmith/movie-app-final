using MovieApp.Services;
using MovieLibraryEntities.Dao;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    internal class MainService : IMainService
    {
        private readonly IRepository _repository;

        public MainService(IRepository repository)
        {
            _repository = repository;
        }

        public void Invoke()
        {

            //  ADD FUNCTIONALITY
            Movie newMovie = new Movie()
            {
                Title = "Die Hard 20 (2024)",
                ReleaseDate = DateTime.Now,
            };
            //Console.WriteLine($"Movie added: { _repository.AddMovie(newMovie) }");

            //  LIST FUNCTIONALITY
            List<Movie> movies = _repository.ListMovies().ToList();
            Console.WriteLine("Press any key to continue to list....");
            ConsoleKeyInfo cki = Console.ReadKey();
            int i = 0;
            do
            {
                Console.WriteLine(movies[i].Title);
                i++;
                if (i % 100 == 0)
                {
                    Console.WriteLine("Continue? (enter for yes, any key to exit)");
                    cki = Console.ReadKey();
                }
            } while (cki.Key == ConsoleKey.Enter);

            //  UPDATE FUNCTIONALITY
            System.Console.WriteLine("Enter movie title to Update: ");
            var title = Console.ReadLine();

            System.Console.WriteLine("Enter Updated movie title: ");
            var newTitle = Console.ReadLine();

            _repository.UpdateMovieTitle(title, newTitle);


            //  DELETE FUNCTIONALITY
            Console.WriteLine("Press any key to continue to delete...");
            Console.ReadKey();
            Console.WriteLine($"Movie deleted: {_repository.DeleteMovie(newMovie.Title)}");
        }

    }
}
