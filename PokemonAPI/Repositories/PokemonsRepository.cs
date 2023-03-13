using PokemonAPI;

using PokemonLibrary;
using System;


namespace PokemonAPI.Repositories

{
    public class PokemonsRepository : IPokemonsRepository
    {
        private int _nextID;
        private List<Pokemon> _pokemons;

        public PokemonsRepository()
        {
            _nextID = 1;
            _pokemons = new List<Pokemon>()
            {
                new Pokemon() {Id = _nextID++, name="Pikachu", level= 99, PokeDex= 25 },
                new Pokemon() {Id = _nextID++, name="Squirtle", level= 55, PokeDex= 26 },
                new Pokemon() {Id = _nextID++, name="Charizard", level= 28, PokeDex= 27 },
            };
        }

        public List<Pokemon> GetAll(int? amount, string? namefilter)
        {
            List<Pokemon> result = new List<Pokemon>(_pokemons);

            if (namefilter != null)
            {
                result = result.FindAll(pokemon => pokemon.name.Contains(namefilter,
                    StringComparison.InvariantCultureIgnoreCase));
            }

            if (amount != null)
            {
                int castAmount = (int)amount;
                return result.Take(castAmount).ToList();
            }

            return result;
        }

        public Pokemon Add(Pokemon newPokemon)
        {
            newPokemon.Validate();
            newPokemon.Id = _nextID++;
            _pokemons.Add(newPokemon);
            return newPokemon;
        }

        public Pokemon Delete(int id)
        {
            Pokemon foundPokemon = GetbyID(id);
            _pokemons.Remove(foundPokemon);
            return foundPokemon;
        }

        public Pokemon? GetbyID(int id)
        {
            return _pokemons.Find(x => x.Id == id);


        }

        public Pokemon? Update(int id, Pokemon updates)
        {
            updates.Validate();
            Pokemon? foundPokemon = GetbyID(id);
            if (foundPokemon == null)
            {
                return null;
            }
            foundPokemon.name = updates.name;
            foundPokemon.PokeDex = updates.PokeDex;
            foundPokemon.level = updates.level;
            return foundPokemon;
        }








    }
}
