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
        [BindProperty]
        public string SearchTerms{get;set;}

        public IEnumerable<Movie> Movies {get; protected set;}

        [BindProperty]
        public string[] MPAARatings {get;set;}

        [BindProperty]
        public string[] Genres {get;set;}

        public double? IMDBMin {get; set;}

        public double? IMDBMax {get;set;}

        public void OnGet(string SearchTerms, string[] MPAARatings, string[] Genre, double IMDBMin, double IMDBMax)
        {
            string terms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            IMDBMin = double.Parse(Request.Query["IMDBMin"]);
            IMDBMax = double.Parse(Request.Query["IMDBMax"]);
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
        }
    }
}
