﻿
using PokemonLibrary;

namespace PokemonAPI.Repositories
{
    public interface IPokemonsRepository
    {
        Pokemon Add(Pokemon newPokemon);
        Pokemon Delete(int id);
        List<Pokemon> GetAll(int? amount, string? namefilter);
        Pokemon? GetbyID(int id);
        Pokemon? Update(int id, Pokemon updates);
    }
}