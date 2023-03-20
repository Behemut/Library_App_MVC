using AutoMapper;
using Library_API.Models;
using Library_API.Models.DTO;
using Domain;

namespace Library_API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var MappingConfig = new MapperConfiguration(
                
                config => {

                    //config.CreateMap<UsersPerson, UsersPersonDTO>().ReverseMap();
                    //config.CreateMap<Authors, AuthorsDTO>().ReverseMap();
                    //config.CreateMap<Books, BooksDTO>().ReverseMap();
                    //config.CreateMap<Borrows, BorrowsDTO>().ReverseMap();
                    //config.CreateMap<Genres, GenresDTO>().ReverseMap();



                    config.CreateMap<APITokenDTO, APITokenDTO>().ReverseMap();



                });

            return MappingConfig;
        }       
    }
}
