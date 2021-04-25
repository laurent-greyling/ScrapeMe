using MyShow.Api.Models;
using Scraper.Entities;
using Scraper.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MyShow.Api.Services
{
    public class ShowDetailsService : IShowDetailsService
    {
        private readonly IShowsRepository _showsRepository;
        private readonly ICastRepository _castRepository;

        public ShowDetailsService(IShowsRepository showsRepository,
            ICastRepository castRepository)
        {
            _showsRepository = showsRepository;
            _castRepository = castRepository;
        }

        public ShowDetailsModel Retrieve(int id)
        {
            var show = _showsRepository.Retrieve().FirstOrDefault(x => x.Id == id);
            var cast = _castRepository.Retrieve().Where(x => x.ShowId == id).OrderByDescending(x => x.Birthday).ToList();

            return MapShowDetails(show, cast);
        }

        public ShowDetailsModel Retrieve(string name)
        {
            var show = _showsRepository.Retrieve().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            var cast = _castRepository.Retrieve().Where(x => x.ShowId == show.Id).OrderByDescending(x => x.Birthday).ToList();

            return MapShowDetails(show, cast);
        }

        public List<ShowDetailsModel> Retrieve()
        {
            var shows = _showsRepository.Retrieve().ToList();
            var cast = _castRepository.Retrieve().ToList();

            var showList = new List<ShowDetailsModel>();

            foreach (var show in shows)
            {
                var castMembers = cast.Where(x => x.ShowId == show.Id).OrderByDescending(x => x.Birthday).ToList();
                var mappedShow = MapShowDetails(show, castMembers);
                showList.Add(mappedShow);
            }

            return showList;
        }

        private ShowDetailsModel MapShowDetails(Shows show, List<Cast> cast)
        {
            var showDetails = new ShowDetailsModel
            {
                Id = show.Id,
                Name = show.Name
            };

            var castMembers = cast.Select(x =>
            {
                return new CastDetailsModel
                {
                    CastShowId = x.CastShowId,
                    Id = x.Id,
                    ShowId = x.ShowId,
                    Name = x.Name,
                    Birthday = x.Birthday
                };
            }).ToList();

            showDetails.CastMembers = castMembers;

            return showDetails;
        }
    }
}
