class Squares
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        var squares = Enumerable.Range(1, n)
            .Where(x => x != 5 && x != 9 && (x % 2 == 1 || x % 7 == 0))
            .Select(x => x * x)
            .OrderBy(x => x);

        Console.WriteLine(squares.Sum());
        Console.WriteLine(squares.Count());
        Console.WriteLine(squares.First());
        Console.WriteLine(squares.Last());
        Console.WriteLine(squares.ElementAt(2));
    }
}