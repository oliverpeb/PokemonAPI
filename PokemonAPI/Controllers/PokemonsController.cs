using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Repositories;
using PokemonAPI.Controllers;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Cors;
using PokemonLibrary;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    //URI er egentlig api/Pokemons
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private IPokemonsRepository _repository;

        public PokemonsController(IPokemonsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> GetAll(
            [FromHeader] int? amount,
            [FromQuery] string? namefilter, 
            [FromQuery] int? minlevel)
        {
            List<Pokemon> result = _repository.GetAll(amount,namefilter);
            if (result.Count < 1)
            {
                return NoContent(); // NotFound er også ok
            }
            Response.Headers.Add("TotalAmount", "" + result.Count());
            return Ok(result);
        }


        // GET: api/<PokemonsController>
        [HttpGet("{id}")]
        public Pokemon Get(int id)
        {
            Pokemon? foundPokemon = _repository.GetbyID(id);
            return foundPokemon;
        }



        // POST api/<PokemonsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Pokemon> Post([FromBody] Pokemon newPokemon)
        {
            try
            {

                Pokemon createdPokemon = _repository.Add(newPokemon);
                return Created($"api/pokemons/" + createdPokemon.Id, createdPokemon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // PUT api/<PokemonsController>/5
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public ActionResult<Pokemon> Put(int id, [FromBody] Pokemon updates)
        {
            try
            {
                Pokemon? foundPokemon = _repository.Update(id, updates);
                if (foundPokemon == null)
                {
                    return NotFound();
                }
                return Ok(foundPokemon);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);

            }





        }

        // DELETE api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public ActionResult<Pokemon> Delete(int id)
        {

            Pokemon? deletedPokemon = _repository.Delete(id);
            if (deletedPokemon == null)
            {
                return NotFound(id);
            }
            return Ok(deletedPokemon);
        }







    }
}



