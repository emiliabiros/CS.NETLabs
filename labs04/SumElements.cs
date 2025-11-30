class SumElements
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int m = int.Parse(Console.ReadLine());

        Random random = new Random();

        var matrix = Enumerable.Range(0, n)
            .Select(_ =>
                Enumerable.Range(0, m)
                    .Select(_ => random.Next(1, 101))
                    .ToList()
            )
            .ToList();

        long sum = matrix
            .SelectMany(row => row)
            .Sum(x => (long)x);

        Console.WriteLine(sum);

        string result = string.Join(
            "\n",
            matrix.Select(row => string.Join(" ", row))
        );

        Console.WriteLine(result);
    }
}