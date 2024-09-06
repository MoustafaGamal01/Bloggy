global using AutoMapper;

namespace Bloggy.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostShowDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.DisplayName : null))
                .ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.TimeToRead, opt => opt.MapFrom(src => src.TimeToRead))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments != null
                    ? src.Comments.Select(c => new CommentShowDto
                    {
                        Content = c.Content,
                        CreatedAt = c.CreatedAt,
                        Username = c.User != null ? c.User.DisplayName : null,  
                        Img = c.User != null ? c.User.ProfilePicture : null  
                    }).ToList() : new List<CommentShowDto>()));

            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>))
                .ConvertUsing(typeof(PagedResultConverter<,>));
        }
    }


}
