namespace labs01.Models
{
    public class Hero
    {
        public string Name { get; }
        public EHeroClass Class { get; }

        public Hero(string name, EHeroClass heroClass)
        {
            Name = name;
            Class = heroClass;
        }
    }
}