using eCommerceApp.Models;

namespace eCommerceApp.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public List<Producer> Producers { get; set; }

        public List<Cinema> Cinemas { get; set; }

        public List<Actor> Actors { get; set; }

        public NewMovieDropdownsVM()
        {
            Producers = new();
            Cinemas = new();
            Actors = new();
        }
    }
}