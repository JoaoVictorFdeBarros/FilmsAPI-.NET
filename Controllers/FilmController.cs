using Microsoft.AspNetCore.Mvc;
using filmsApi.Models;
namespace filmsApi.Controllers;
using System;
using AutoMapper;
using filmsApi.Data;
using filmsApi.Data.Dtos;
using Microsoft.AspNetCore.JsonPatch;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private FilmContext context;
    private IMapper mapper;

    public FilmController(FilmContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }


/// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddFilm([FromBody] CreateFilmDto filmDto)
    {

        Film film = mapper.Map<Film>(filmDto);
        context.Films.Add(film);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, film);
    }

/// <summary>
    /// Retorna os filmes do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso retorno seja feito com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadFilmDto> GetFilms([FromQuery] int skip = 0, int take = 10)
    {
        return mapper.Map<List<ReadFilmDto>>(context.Films.Skip(skip).Take(take));
    }

    /// <summary>
    /// Retorna um filme do banco de dados pelo Id
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso retorno seja feito com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult GetFilmById(Guid id)
    {
        var film = context.Films.FirstOrDefault(film => film.Id == id);

        if (film == null)
        {
            return NotFound();
        }
        var filmDto = mapper.Map<ReadFilmDto>(film);
        return Ok(filmDto);

    }
    
    /// <summary>
    /// Atualiza um filme do banco de dados pelo Id
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização seja feita com sucesso</response>
    [HttpPut("{id}")]
    public IActionResult UpdateFilm(Guid id, [FromBody] UpdateFilmDto filmDto)
    {
        var film = context.Films.FirstOrDefault(film => film.Id == id);

        if (film == null)
        {
            return NotFound();
        }
        mapper.Map(filmDto, film);
        context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualiza um campo de um filme do banco de dados pelo Id
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização seja feita com sucesso</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateFilmPatch(Guid id, JsonPatchDocument<UpdateFilmDto> patch)
    {
        var film = context.Films.FirstOrDefault(film => film.Id == id);

        if (film == null)
        {
            return NotFound();
        }

        var UpdateFilm = mapper.Map<UpdateFilmDto>(film);

        patch.ApplyTo(UpdateFilm, ModelState);

        if(!TryValidateModel(UpdateFilm)){
            return ValidationProblem(ModelState);
        }

        mapper.Map(UpdateFilm, film);
        context.SaveChanges();
        return NoContent();

    }
    /// <summary>
    /// Remove um filme do banco de dados pelo Id
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteFilm(Guid id){
        var film = context.Films.FirstOrDefault(film => film.Id == id);

        if (film == null)
        {
            return NotFound();
        }

        context.Remove(film);
        context.SaveChanges();
        return NoContent();

    }

}