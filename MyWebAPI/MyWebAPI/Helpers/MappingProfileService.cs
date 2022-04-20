using AutoMapper;
using MyWebAPI.ViewModels;
using MyWebAPI.Models;

namespace Capstone.Api.Services.Helpers
{
    public class MappingProfileService : Profile
    {
        #region Constructor
        public MappingProfileService()
        {
            CreateMap<User, UserViewModel>();
        }
        #endregion
    }
}