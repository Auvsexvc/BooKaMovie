using eCommerceApp.Data.Base;
using eCommerceApp.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApp.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }

        [DisplayName("Cover")]
        public string ImageURL { get; set; } = string.Empty;

        [DisplayName("Category")]
        public MovieCategory MovieCategory { get; set; }

        public List<ActorMovie> ActorsMovies { get; set; } = null!;
        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; } = null!;

        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; } = null!;
    }
}