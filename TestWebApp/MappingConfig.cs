using AutoMapper;
using TestRestApplication.Models;
using TestWebApp.Models.Dto;

namespace TestWebApp;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Info, InfoDto>().ReverseMap();
    }
}