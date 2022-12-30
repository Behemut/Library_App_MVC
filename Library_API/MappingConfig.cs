using AutoMapper;
using Library_API.Models;
using Library_API.Models.DTO;


namespace Library_API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var MappingConfig = new MapperConfiguration(
                
                config => {

                    config.CreateMap<UsersPerson, UsersPersonDTO>().ReverseMap();


                });

            return MappingConfig;
        }       
    }
}
