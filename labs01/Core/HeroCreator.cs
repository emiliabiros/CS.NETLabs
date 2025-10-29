using System.Text.RegularExpressions;
using labs01.Models;

namespace labs01.Core
{
    public static class HeroCreator
    {
        public static Hero CreateHero()
        {
            string name = GetHeroName();
            EHeroClass heroClass = GetHeroClass(name);

            Console.Clear();
            Console.WriteLine($"{heroClass} {name} wyrusza na przygodę!");
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey(true);

            return new Hero(name, heroClass);
        }

        private static string GetHeroName()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Podaj nazwę bohatera (tylko litery, min. 2 znaki): ");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (input.Length >= 2 && !Regex.IsMatch(input, @"[^a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]"))
                    return input;

                Console.WriteLine("\n[BŁĄD] Imię musi mieć min. 2 litery i zawierać tylko znaki alfabetu.");
                Thread.Sleep(2000);
            }
        }

        private static EHeroClass GetHeroClass(string heroName)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Witaj {heroName}, wybierz klasę bohatera:\n");

                foreach (EHeroClass heroClass in Enum.GetValues(typeof(EHeroClass)))
                {
                    if (heroClass == EHeroClass.NieWybrano) continue;
                    Console.WriteLine($"\t[{(int)heroClass}] {heroClass}");
                }

                Console.Write("\nWybór: ");
                if (int.TryParse(Console.ReadLine(), out int choice) &&
                    Enum.IsDefined(typeof(EHeroClass), choice) &&
                    choice != (int)EHeroClass.NieWybrano)
                {
                    return (EHeroClass)choice;
                }

                Console.WriteLine("\n[BŁĄD] Nieprawidłowy wybór. Spróbuj ponownie...");
                Thread.Sleep(1500);
            }
        }
    }
}