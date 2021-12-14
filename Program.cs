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
            Console.WriteLine("3) List of Movies");
            Console.WriteLine("4) Add Actor details");
            Console.WriteLine("5) Remove Actor details");
            Console.WriteLine("6) List of Actors");
            Console.WriteLine("7) Exit");
            Console.Write("\r\nSelect an option: ");
             Console.WriteLine("Choose an option: again");

            switch (Console.ReadLine())
            {
                case "1":
                    AddMovie();
                    return true;
                case "2":
                    RemoveMovie();
                    return true;
                case "3":
                    MovieList();
                    return true;
                case "4":
                    AddActor();
                    return true;
                case "5":
                    RemoveActor();
                    return true;
                case "6":
                    ActorList();
                    return true;
                case "7":
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
                var m = database.Movies.SingleOrDefault(t => t.MovieName == movie.MovieName);
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
                var m = database.Movies.SingleOrDefault(t=> t.MovieName == movie.MovieName);
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

        private static void MovieList()
        {
            using (var database = new Model1())
            {
                var movielist = database.Movies;
                {
                    try
                    {
                        foreach (var movie in movielist)
                        {
                            Console.WriteLine("List of Movies - " + movie.MovieName);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        private static void AddActor()
        {
            using (var database = new Model1())
            {
                var actor = new Actor();
                Console.WriteLine($"Enter the actor name which you want to add:" + actor.ActorName);
                actor.ActorName = Console.ReadLine();
                var a = database.Actors.SingleOrDefault(t=>t.ActorName == actor.ActorName);
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
                var a = database.Actors.SingleOrDefault(t=>t.ActorName == actor.ActorName);
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

        private static void ActorList()
        {
            using (var database = new Model1())
            {
                var actorlist = database.Actors;
                {
                    try
                    {
                        foreach (var actor in actorlist)
                        {
                            Console.WriteLine("List of Actors - " + actor.ActorName);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
