using eCommerceApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Models
{
    public class NewMovieVM
    {
        [Display(Description = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Display(Description = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Display(Description = "Movie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Description = "Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Description = "Movie price")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Description = "Movie cover")]
        [Required(ErrorMessage = "Cover is required")]
        public string ImageURL { get; set; } = string.Empty;

        [Display(Description = "Movie category")]
        [Required(ErrorMessage = "Category is required")]
        public MovieCategory MovieCategory { get; set; }

        [Display(Description = "Movie actors")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; } = null!;

        [Display(Description = "Select cinema")]
        [Required(ErrorMessage = "Cinema is required")]
        public int CinemaId { get; set; }

        [Display(Description = "Select producer")]
        [Required(ErrorMessage = "Producer is required")]
        public int ProducerId { get; set; }
    }
}