using labs01.Models;

namespace labs01.Core
{
    public class Game
    {
        private readonly string _title;
        private Hero _activeHero;

        public Game(string title)
        {
            _title = title;
        }

        public void Run()
        {
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Witaj w grze {_title}");
                Console.WriteLine("\t[1] Nowa gra");
                Console.WriteLine("\t[X] Wyjdź");
                Console.Write("\nWybierz opcję: ");

                string input = Console.ReadLine()?.ToUpper().Trim() ?? string.Empty;
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        _activeHero = HeroCreator.CreateHero();
                        GameLoop.Start(_activeHero);
                        break;
                    case "X":
                        Console.WriteLine("Zamykanie programu...");
                        return;
                    default:
                        Console.WriteLine("[BŁĄD] Nieprawidłowa opcja. Spróbuj ponownie...");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
    }
}