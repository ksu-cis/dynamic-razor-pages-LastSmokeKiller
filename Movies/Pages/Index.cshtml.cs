using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; } = "";

        public IEnumerable<Movie> Movies {get; protected set;}

        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings {get;set;}

        [BindProperty(SupportsGet = true)]
        public string[] Genres {get;set;}

        [BindProperty(SupportsGet = true)]
        public double? RTMin { get; set; }
       
        [BindProperty(SupportsGet =true)]
        public double? RTMax { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin {get; set;}

        [BindProperty(SupportsGet = true)]
        public double? IMDBMax {get;set;}

        public void OnGet(double? IMDBMax, double? IMDBMin, double? RTMax, double? RTMin)
        {
            /*
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.RTMin = RTMin;
            this.RTMax = RTMax;
            SearchTerms = Request.Query["SearchTerms"];     //my code wouldn't work the same as the tutorial so it had to stay like this
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRotten(Movies, RTMin, RTMax);
            */
            Movies = MovieDatabase.All;
            if (SearchTerms != null)
            {
                Movies = from movie in Movies
                         where movie.Title != null
    && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase)
                         select movie;
            }
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null
                && MPAARatings.Contains(movie.MPAARating));
            }
            if(Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null &&
                Genres.Contains(movie.MajorGenre));
            }
            if(IMDBMax != null && IMDBMin != null)
            {
                if(IMDBMin == null)
                {
                    Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax);
                }
                if(IMDBMax == null)
                {
                    Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin);
                }
                else
                {
                    Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
                }
            }
            if(RTMax != null && RTMin != null)
            {
                if(RTMin == null)
                {
                    Movies = Movies.Where(movie => movie.RottenTomatoesRating <= RTMax);
                }
                if(RTMax == null)
                {
                    Movies = Movies.Where(movie => movie.RottenTomatoesRating >= RTMin);
                }
                else
                {
                    Movies = Movies.Where(movie => movie.RottenTomatoesRating >= RTMin && movie.RottenTomatoesRating <= RTMax);
                }
            }
        }
    }
}
