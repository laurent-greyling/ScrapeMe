using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Scraper.Entities
{
    public partial class Shows
    {
        public Shows()
        {
            Cast = new HashSet<Cast>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cast> Cast { get; set; }
    }
}
