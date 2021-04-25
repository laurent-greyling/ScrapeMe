namespace MyShow.Api.Models
{
    public class CastDetailsModel
    {
        /// <summary>
        /// Id of the show and cast member -> unique
        /// </summary>
        public string CastShowId { get; set; }

        /// <summary>
        /// Cast member ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Show Id
        /// </summary>
        public int ShowId { get; set; }

        /// <summary>
        /// Name of cast member
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cast member Birthday
        /// </summary>
        public string Birthday { get; set; }
    }
}
