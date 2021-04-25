using MyShow.Api.Models;
using System.Collections.Generic;

namespace MyShow.Api.Services
{
    /// <summary>
    /// Service to retrieve the show details from the DB
    /// </summary>
    public interface IShowDetailsService
    {
        /// <summary>
        /// Retrieve show details on Show id
        /// </summary>
        /// <returns></returns>
        List<ShowDetailsModel> Retrieve();

        /// <summary>
        /// Retrieve show details on Show id
        /// </summary>
        /// <returns></returns>
        ShowDetailsModel Retrieve(int id);

        /// <summary>
        /// Retrieve show details based on Show name
        /// </summary>
        /// <returns></returns>
        ShowDetailsModel Retrieve(string name);
    }
}
