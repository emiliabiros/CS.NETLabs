namespace labs03;
public class Person
{
    public string Name;
    public string SecondName;

    public Person(string name, string secondName)
    {
        Name = name;
        SecondName = secondName;
    }

    public override string ToString()
    {
        return $"Name: {Name}, SecondName: {SecondName}";
    }
}

public class Program
{
    public static void Method1(Person p)
    {
        Console.WriteLine("\n--- Method 1 started (no ref/out) ---");
        Console.WriteLine($"Before change: {p}");
        p.Name = "Adam";
        p.SecondName = "Kowalski";
        Console.WriteLine($"After change: {p}");
    }
    public static void Method2(ref Person p)
    {
        Console.WriteLine("\n--- Method 2 started (with ref) ---");
        Console.WriteLine($"Before change: {p}");
        p = new Person("Piotr", "Zieliński");
        Console.WriteLine($"After change: {p}");
    }
    public static void Method3(Person p)
    {
        Console.WriteLine("\n--- Method 3 started (no ref/out, setting to null) ---");
        Console.WriteLine($"Before change: {p}");
        p = null;
        Console.WriteLine($"After change: {(p == null ? "null" : p.ToString())}");
    }

    public static void Method3_ref(ref Person p) 
    {
        Console.WriteLine("\n--- Method 3_ref started (with ref, setting to null) ---");
        Console.WriteLine($"Before change: {p}");
        p = null;
        Console.WriteLine($"After change: {(p == null ? "null" : p.ToString())}");
    }

    public static void Main(string[] args)
    {
        Person originalPerson = new Person("Jan", "Nowak");
        Console.WriteLine("Initial Object State: " + originalPerson);

        Method1(originalPerson);
        Console.WriteLine("\nAfter Method 1 (Name/SecondName modification):");
        Console.WriteLine($"Object in Main: {originalPerson}");

        Method2(ref originalPerson);
        Console.WriteLine("\nAfter Method 2 (New object assignment with ref):");
        Console.WriteLine($"Object in Main: {originalPerson}");

        Method3(originalPerson);
        Console.WriteLine("\nAfter Method 3 (Assignment to null without ref):");
        Console.WriteLine($"Object in Main: {originalPerson}");

        Method3_ref(ref originalPerson);
        Console.WriteLine("\nAfter Method 3_ref (Assignment to null with ref):");
        Console.WriteLine($"Object in Main: {(originalPerson == null ? "null" : originalPerson.ToString())}");
    }
}