using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Scraper.Entities
{
    public partial class Cast
    {
        public string CastShowId { get; set; }
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }

        public virtual Shows Show { get; set; }
    }
}
