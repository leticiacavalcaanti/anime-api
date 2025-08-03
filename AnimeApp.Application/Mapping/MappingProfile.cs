using AutoMapper;
using AnimeApp.Domain.Entities;
using AnimeApp.Application.DTOs;

namespace AnimeApp.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AnimeRequest, Anime>().ReverseMap();
        CreateMap<Anime, AnimeDTO>().ReverseMap();
    }
}