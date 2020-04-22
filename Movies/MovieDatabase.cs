using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        private static string[] genres;

        public static string[] Geres => genres;

        public IEnumerable<string> Genres
        {
            get
            {
                HashSet<string> genres = new HashSet<string>();
                foreach(Movie movie in All)
                {
                    if(movie.MajorGenre != null)
                    {
                        genres.Add(movie.MajorGenre);
                    }
                }
                genres = genreSet.ToArray();
            }
        }

        public static IEnumerable<Movie> FilterByMPAARating(IENumerable<Movie> movies, IEnumerable<string> ratings)
        {
            if(ratings == null || ratings.Count() == 0) return movies;
            List<Movies> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                {
                    results.Add(movie);
                }
            }
            return results; 
        }

        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if(min == null && max == null) return movies;
            var results = new List<Movie>();
            if(min == null)
            {
                foreach(Movie movie in movies)
                {
                    if(movie.IMDBRating <= max) results.Add(movie);
                }
                return results;
            }
            if(max == null)
            {
                foreach(Movie movie in movies)
                {
                    if(movie.IMDBRating >= min) results.Add(movie);
                }
                return results;
            }
            foreach(Movie movie in movies)
            {
                if(movie.IMDBRating >= min && movie.IMDBRating <= max)
                {
                    results.Add(movie);
                }
                return results;
            }
        }

        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();
            if(SearchTerms == null) return All;
            foreach(Movie movie in All)
            {
                if(movie.Title != null && movie.Title.Contains(terms, StringCompareison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

     
        public static string[] MPAARatings
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }
    }
}
