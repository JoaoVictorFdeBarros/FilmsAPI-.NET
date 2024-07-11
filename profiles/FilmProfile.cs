using AutoMapper;
using filmsApi.Data.Dtos;
using filmsApi.Models;

namespace filmsApi.Profiles;

public class FilmProfile:Profile{

    public FilmProfile(){
        CreateMap<CreateFilmDto, Film>();
        CreateMap<UpdateFilmDto, Film>();
        CreateMap<Film, UpdateFilmDto>();
        CreateMap<Film,ReadFilmDto>();
    }
}