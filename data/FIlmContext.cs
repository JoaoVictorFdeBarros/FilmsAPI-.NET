using Microsoft.EntityFrameworkCore;
using filmsApi.Models;
namespace filmsApi.Data;

public class FilmContext:DbContext {
    public FilmContext(DbContextOptions<FilmContext> options) :base(options){
        Films = Set<Film>();
    }
    
    public DbSet<Film>  Films{get;set;}

}