using AutoMapper;
using AnimeApp.Domain.Entities;
using AnimeApp.Application.DTOs;

namespace AnimeApp.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Anime, AnimeDTO>().ReverseMap();
    }
}