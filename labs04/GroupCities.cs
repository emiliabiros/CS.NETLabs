class GroupCities
{
    static void Main()
    {
        List<string> miasta = new List<string>();
        string? wejscie;

        while (true)
        {
            wejscie = Console.ReadLine();

            if (wejscie != null && wejscie.Trim().Equals("X", StringComparison.OrdinalIgnoreCase))
                break;

            if (!string.IsNullOrWhiteSpace(wejscie))
                miasta.Add(wejscie.Trim());
        }

        while (true)
        {
            char litera = Console.ReadLine()[0];

            if (litera == 'X' || litera == 'x')
                break;

            var miastaNaLitere = miasta
                .GroupBy(m => m[0])
                .OrderBy(g => g.Key)
                .Where(g => char.ToUpper(g.Key) == char.ToUpper(litera))
                .SelectMany(g => g)
                .OrderBy(m => m)
                .ToList();

            Console.WriteLine(
                miastaNaLitere.Any()
                    ? string.Join(", ", miastaNaLitere)
                    : "PUSTE"
            );
        }
    }
}