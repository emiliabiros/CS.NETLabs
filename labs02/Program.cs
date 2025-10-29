using labs02.Models;

namespace labs02
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("PARLIAMENT SIMULATION");
            Console.Write("enter number of members: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                n = 10;
                Console.WriteLine("invalid number, using default: 10");
            }

            var parliament = new Parliament();

            for (int i = 1; i <= n; i++)
            {
                var member = new Mop($"MP{i}");
                member.Subscribe(parliament);
                parliament.AddMember(member);
            }

            Console.WriteLine("\ncommands:");
            Console.WriteLine("START [topic] – begin voting");
            Console.WriteLine("END – finish voting and show results");
            Console.WriteLine("EXIT – quit\n");

            while (true)
            {
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                string command = parts[0].ToUpperInvariant();
                string arg = parts.Length > 1 ? parts[1] : null;

                switch (command)
                {
                    case "START":
                        parliament.StartVoting(arg ?? "<topic>");
                        break;

                    case "END":
                        await Task.Delay(1200);
                        parliament.EndVoting();
                        break;

                    case "EXIT":
                        return;

                    default:
                        Console.WriteLine("unknown command");
                        break;
                }
            }
        }
    }
}