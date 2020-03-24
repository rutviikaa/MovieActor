using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MovieActor
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cn = new SqlConnection("Data Source=LAPTOP-TJ920A78;Initial Catalog=MovieList;Integrated Security=True");
            cn.Open();
            
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
            cn.Close();
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add Movie details");
            Console.WriteLine("2) Remove Movie details");
            Console.WriteLine("3) Add Actor details");
            Console.WriteLine("4) Remove Actor details");
            Console.WriteLine("5) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddMovie();
                    return true;
                case "2":
                    RemoveMovie();
                    return true;
                case "3":
                    AddActor();
                    return true;
                case "4":
                    RemoveActor();
                    return true;
                case "5":
                    return false;
                default:
                    return true;
            }
        }
        private static void AddMovie()
        {
            using (var database = new Model1())
            {
                var movie = new Movy();
                Console.WriteLine($"Enter the movie name which you want to add:"+movie.MovieName);
                movie.MovieName = Console.ReadLine();
                var m = (from mv in database.Movies where mv.MovieName == movie.MovieName select mv).SingleOrDefault();
                if (m == null)
                {
                    database.Movies.Add(movie);
                    database.SaveChanges();
                    Console.WriteLine("Movie added to the list");
                }
                else
                {
                    Console.WriteLine("There is already such movie in the list");
                    throw new DuplicateWaitObjectException(nameof(movie.MovieName), "There is already such movie in the list");
                }
                
            }
        }

        private static void RemoveMovie()
        {
            using (var database = new Model1())
            {
                
                var movie = new Movy();
                Console.WriteLine($"Enter the movie name which you want to delete:"+movie.MovieName);
                movie.MovieName = Console.ReadLine();
                var m = (from mv in database.Movies where mv.MovieName == movie.MovieName select mv).SingleOrDefault();
                if (m == null)
                {
                    Console.WriteLine("There is no such movie in the list");
                    throw new ArgumentOutOfRangeException(nameof(movie.MovieName), "There is no such movie in the list");
                }
                database.Movies.Remove(m);
                database.SaveChanges();
                Console.WriteLine("Movie removed from the list");
                
            }
        }

        private static void AddActor()
        {
            using (var database = new Model1())
            {
                var actor = new Actor();
                Console.WriteLine($"Enter the actor name which you want to add:" + actor.ActorName);
                actor.ActorName = Console.ReadLine();
                var a = (from act in database.Actors where act.ActorName == actor.ActorName select act).SingleOrDefault();
                if (a == null)
                {
                    database.Actors.Add(actor);
                    database.SaveChanges();
                    Console.WriteLine("Actor added to the list");
                }
                else
                {
                    Console.WriteLine("There is already such actor in the list");
                    throw new DuplicateWaitObjectException(nameof(actor.ActorName), "There is already such actor in the list");
                }
            }
        }

        private static void RemoveActor()
        {
            using (var database = new Model1())
            {
                var actor = new Actor();
                Console.WriteLine($"Enter the actor name which you want to delete:"+actor.ActorName);
                actor.ActorName = Console.ReadLine();
                var a = (from act in database.Actors where act.ActorName == actor.ActorName select act).SingleOrDefault();
                if (a == null)
                {
                    Console.WriteLine("There is no such actor in the list");
                    throw new ArgumentOutOfRangeException(nameof(actor.ActorName), "There is no such actor in the list");
                }
                
                database.Actors.Remove(actor);
                database.SaveChanges();
                Console.WriteLine("Actor removed from the list");
            }
        }
    }
}
