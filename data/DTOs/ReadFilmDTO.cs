using System.ComponentModel.DataAnnotations;
namespace filmsApi.Data.Dtos;

public class ReadFilmDto{
    public string Title {get;set;} = "";
    public int Length {get;set;}
    public string Genre {get;set;} = "";

    public DateTime ReadTime{get;set;} = DateTime.Now;
}