using System;
using System.Collections.Generic;

#nullable disable

namespace MoviesApp.MovieModals
{
    public partial class Still
    {
        public int Id { get; set; }
        public string StillImage { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
