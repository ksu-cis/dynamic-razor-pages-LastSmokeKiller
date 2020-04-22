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
       
        public string SearchTerms { get; set; } = "";

        public IEnumerable<Movie> Movies {get; protected set;}

   
        public string[] MPAARatings {get;set;}

       
        public string[] Genres {get;set;}

        public double? RTMin { get; set; }

        public double? RTMax { get; set; }
        
        public double? IMDBMin {get; set;}

        
        public double? IMDBMax {get;set;}

        public void OnGet(double? IMDBMax, double? IMDBMin, double? RTMax, double? RTMin)
        {
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
        }
    }
}
