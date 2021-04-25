using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShow.Api.Models
{
    public class ShowDetailsModel
    {
        /// <summary>
        /// Show Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the show
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cast members` details
        /// </summary>
        public List<CastDetailsModel> CastMembers { get; set; }
    }
}
