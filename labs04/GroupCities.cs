class GroupCities
{
    static void Main()
    {
        List<string> cities = new List<string>();
        string? input;

        while (true)
        {
            input = Console.ReadLine();

            if (input != null && input.Trim().Equals("X", StringComparison.OrdinalIgnoreCase))
                break;

            if (!string.IsNullOrWhiteSpace(input))
                cities.Add(input.Trim());
        }

        while (true)
        {
            char letter = Console.ReadLine()[0];

            if (letter == 'X' || letter == 'x')
                break;

            var citiesStartingWith = cities
                .GroupBy(m => m[0])
                .OrderBy(g => g.Key)
                .Where(g => char.ToUpper(g.Key) == char.ToUpper(letter))
                .SelectMany(g => g)
                .OrderBy(m => m)
                .ToList();

            Console.WriteLine(
                citiesStartingWith.Any()
                    ? string.Join(", ", citiesStartingWith)
                    : "PUSTE"
            );
        }
    }
}