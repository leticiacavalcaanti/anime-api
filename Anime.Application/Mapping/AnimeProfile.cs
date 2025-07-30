using AutoMapper;
using AnimeApp.Domain.Entities;
using AnimeApp.Application.DTOs;

namespace AnimeApp.Application.Mapping;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<Anime, AnimeDTO>().ReverseMap();
    }
}