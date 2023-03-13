using Microsoft.EntityFrameworkCore;
using PokemonLibrary;

namespace PokemonAPI.Context
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> 
            options) : base(options) { }

        public DbSet<Pokemon> pokemons { get; set;}
        

        

    }
}
