using PokemonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonConsumer
{
    public class PokemonWorker
    {
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public void DoWork()
        {
            Console.WriteLine("GetAll:");
            List<Pokemon>? pokemons = GetAll();
            foreach (var pokemon in pokemons)
            {
                Console.WriteLine(pokemon);
            }
            Pokemon newPokemon = new Pokemon()
            {
                level = 23,
                name = "Hej"

            };
            Console.WriteLine("Post:");
            Pokemon new1Pokemon = PostPokemon(newPokemon);
            Console.WriteLine(new1Pokemon);

            Console.WriteLine("Put");
            Pokemon pokemonToUpdate = new Pokemon
            {
                Id = 1,
                name = "Halløj",
                level = 15
            };

            Pokemon updatedPokemon = PutPokemon(pokemonToUpdate);
            Console.WriteLine(updatedPokemon);

            Console.WriteLine("Delete");
            Pokemon deletedPokemon = DeletePokemon(new1Pokemon.Id);
            Console.WriteLine(deletedPokemon);
            
        }


        public List<Pokemon>? GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(
                    "https://pokemonapi20230216124134.azurewebsites.net/" +
                    "api/pokemons").Result;
                string json = response.Content.ReadAsStringAsync().Result;
                List<Pokemon>? list = JsonSerializer.Deserialize<List<Pokemon>>(json, options);
                return list;
            }
        }

        public Pokemon? PostPokemon(Pokemon newPokemon)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonContent serializedIPostPokemon = JsonContent.Create(newPokemon);
                HttpResponseMessage response = client.PostAsync("https://pokemonapi20230216124134.azurewebsites.net/" +
                        "api/pokemons", serializedIPostPokemon).Result;
                string json = response.Content.ReadAsStringAsync().Result;
                Pokemon? PostedPokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
                return PostedPokemon;
            }
        }

        public Pokemon? PutPokemon(Pokemon updatedPokemon)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonContent serializedIPutPokemon = JsonContent.Create(updatedPokemon);
                HttpResponseMessage response = client.PutAsync("https://pokemonapi20230216124134.azurewebsites.net/" +
                        "api/pokemons/" + updatedPokemon.Id, serializedIPutPokemon).Result;
                string json = response.Content.ReadAsStringAsync().Result;
                Pokemon? PutPokemon = JsonSerializer.Deserialize<Pokemon?>(json, options);
                return PutPokemon;
            }
        }   

        public Pokemon? DeletePokemon(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync("https://pokemonapi20230216124134.azurewebsites.net/" +
                        "api/pokemons/" + Id).Result;
                string json = response.Content.ReadAsStringAsync().Result;
                Pokemon? DeletePokemon = JsonSerializer.Deserialize<Pokemon?>(json, options);
                return DeletePokemon;
            }
        }
    }
}
