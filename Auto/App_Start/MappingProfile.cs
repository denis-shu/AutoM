using AutoMapper;
using Logs;
using Logs.Models.ModelDTO;


namespace testAdmin.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDetail, UserDetailDTO>();
        }
    }
}