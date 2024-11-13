using Assesment.DTO;
using Assesment.Models;
using AutoMapper;

namespace Assesment
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserProfile, UserProfileDTO>();
            CreateMap<Post, PostDTO>();
            CreateMap<PostCreateDTO, Post>();
            CreateMap<PostUpdateDTO, Post>();
        }
    }
}
