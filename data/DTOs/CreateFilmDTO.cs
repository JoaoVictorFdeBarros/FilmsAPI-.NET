using System.ComponentModel.DataAnnotations;
namespace filmsApi.Data.Dtos;

public class CreateFilmDto{

    [Required(ErrorMessage = "Title required")]
    public string Title {get;set;} = "";
    [Required(ErrorMessage = "Length required")]
    [Range(70,500, ErrorMessage = "Valid length: 70 - 500 minutes")]
    public int Length {get;set;}
    [Required(ErrorMessage = "Genre required")]
    public string Genre {get;set;} = "";
}