using AutoMapper;
using GitHubRestAPI.Business;
using GitHubRestAPI.Model;

namespace GitHubRestAPI.Profiles
{
    public class ListOfLanguagesProfile : Profile
    {
        public ListOfLanguagesProfile()
        {
            CreateMap<LanguagesDTO, CollectionOfLanguages>();
        }
    }
}
