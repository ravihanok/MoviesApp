using System;
using System.Collections.Generic;

#nullable disable

namespace MoviesApp.MovieModals
{
    public partial class Movie
    {
        public Movie()
        {
            Stills = new HashSet<Still>();
        }

        public int Id { get; set; }
        public string Language { get; set; }
        public string Location { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string SoundEffects { get; set; }
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string ListingType { get; set; }
        public decimal? ImdbRating { get; set; }

        public virtual ICollection<Still> Stills { get; set; }
    }
}
