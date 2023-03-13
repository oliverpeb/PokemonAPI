namespace PokemonLibrary
{
    public class Pokemon
    {
        public int Id { get; set; } //Ikke null

        public string? name { get; set; } //Ikke null, minimum længde 2

        public int level { get; set; } // 1-99

        public int PokeDex { get; set; } // Positivt > 0

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Pokemon)) return false;
            Pokemon pokemon = (Pokemon)obj;
            if (pokemon.Id != Id) return false;
            if (pokemon.name != name) return false;
            if (pokemon.level != level) return false;
            if (pokemon.PokeDex != PokeDex) return false;
            return true;
        }

        public override string ToString()
        {
            return $"Name: {name} Level: {level} Id: {Id}";
        }

        /*
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        */

        public void ValidateID()
        {
            if (Id == null)
            {
                throw new ArgumentException("Cannot return null");
            }

        }

        public void ValidateName()
        {
            if (name == null)
                throw new ArgumentException("Name cannot be null");
            if (name.Length < 2)
                throw new ArgumentException("Name cannot be less than 2");

        }

        public void ValidateLevel()
        {
            if (level < 1 || level > 99)
                throw new ArgumentOutOfRangeException("Level has to be between 1 and 99");
        }

        public void ValidatePokeDex()
        {
            if (PokeDex < 0)
                throw new ArgumentException("The PokeDex number has to be positive");
        }

        public void Validate()
        {
            ValidateName();
            ValidateID();
            ValidateLevel();
            ValidatePokeDex();
        }
    }
}

