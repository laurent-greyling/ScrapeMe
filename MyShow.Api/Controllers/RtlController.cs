using Microsoft.AspNetCore.Mvc;
using MyShow.Api.Models;
using MyShow.Api.Services;
using System;
using System.Collections.Generic;
using System.Net;

namespace MyShow.Api.Controllers
{
    /// <summary>
    /// Retreive TV shows with Cast members
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RtlController : ControllerBase
    {
        private readonly IShowDetailsService _showDetailsService;

        public RtlController(IShowDetailsService showDetailsService)
        {
            _showDetailsService = showDetailsService;
        }

        /// <summary>
        /// Retrieve TV shows with the cast members
        /// </summary>
        /// <response code="200">Details of show</response>
        /// <response code="400">Could not retrieve show</response>
        /// <response code="404">Could not find show</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ShowDetailsModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetShows()
        {
            try
            {
                var showDetails = _showDetailsService.Retrieve();
                return Ok(showDetails);
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundObjectResult(new
                {
                    message = ex.Message,
                    currentDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    message = ex.Message,
                    currentDate = DateTime.Now
                });
            }
        }

        /// <summary>
        /// Retrieve TV show with the cast members who are in the show based on Show Id
        /// </summary>
        /// <response code="200">Details of show</response>
        /// <response code="400">Could not retrieve show</response>
        /// <response code="404">Could not find show</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Id")]
        [ProducesResponseType(typeof(ShowDetailsModel), (int)HttpStatusCode.OK)]
        public IActionResult GetShow(int id)
        {
            try
            {
                var showDetails = _showDetailsService.Retrieve(id);
                return Ok(showDetails);
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundObjectResult(new
                {
                    message = ex.Message,
                    currentDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    message = ex.Message,
                    currentDate = DateTime.Now
                });
            }
        }

        /// <summary>
        /// Retrieve TV show with the cast members who are in the show based on Show Name
        /// </summary>
        /// <response code="200">Details of show</response>
        /// <response code="400">Could not retrieve show</response>
        /// <response code="404">Could not find show</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Name")]
        [ProducesResponseType(typeof(ShowDetailsModel), (int)HttpStatusCode.OK)]
        public IActionResult GetShow(string name)
        {
            try
            {
                var showDetails = _showDetailsService.Retrieve(name);
                return Ok(showDetails);
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundObjectResult(new
                {
                    message = ex.Message,
                    currentDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    message = ex.Message,
                    currentDate = DateTime.Now
                });
            }
        }
    }
}
