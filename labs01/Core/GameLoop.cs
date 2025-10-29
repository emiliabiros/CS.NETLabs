using labs01.Models;
using labs01.World;

namespace labs01.Core
{
    public static class GameLoop
    {
        public static void Start(Hero hero)
        {
            var location = WorldFactory.CreateStartingLocation();
            var parser = new DialogParser(hero);

            while (true)
            {
                Console.Clear();
                ShowLocation(location);

                var input = char.ToUpper(Console.ReadKey(true).KeyChar);
                if (input == 'X') break;

                int index = input - '1';
                if (index >= 0 && index < location.Npcs.Count)
                    TalkTo(location.Npcs[index], parser);
                else
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa opcja.");
                    Thread.Sleep(1000);
                }
            }
        }

        private static void ShowLocation(Location location)
        {
            Console.WriteLine($"Znajdujesz się w {location.Name}. Co chcesz zrobić?\n");

            for (int i = 0; i < location.Npcs.Count; i++)
                Console.WriteLine($"\t[{i + 1}] Porozmawiaj z {location.Npcs[i].Name}");

            Console.WriteLine("\t[X] Wyjdź z gry");
            Console.Write("\nWybierz opcję: ");
        }

        private static void TalkTo(NonPlayerCharacter npc, DialogParser parser)
        {
            Console.Clear();
            var npcPart = npc.StartTalking();

            while (npcPart != null)
            {
                Console.Clear();
                Console.WriteLine($"{npc.Name}: {parser.ParseDialog(npcPart)}\n");

                if (npcPart.HeroResponses.Count == 0)
                {
                    Console.WriteLine("\n--- DIALOG ZAKOŃCZONY ---");
                    break;
                }

                for (int i = 0; i < npcPart.HeroResponses.Count; i++)
                {
                    var response = parser.ParseDialog(npcPart.HeroResponses[i]);
                    Console.WriteLine($"\t[{i + 1}] {response}");
                }

                Console.Write("\nWybór: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    int index = choice - 1;
                    if (index >= 0 && index < npcPart.HeroResponses.Count)
                        npcPart = npcPart.HeroResponses[index].NextNpcPart;
                    else
                    {
                        Console.WriteLine("\n[BŁĄD] Nieprawidłowy wybór.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowy format.");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do lokacji...");
            Console.ReadKey(true);
        }
    }
}