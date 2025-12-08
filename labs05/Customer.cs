using System.Reflection;
namespace labs05;

public class Customer
{
    private string _name;
    protected int _age;
    public bool IsPreferred;

    public Customer(string name)
    {
        if (string.IsNullOrEmpty(name)) 
            throw new ArgumentNullException("Customer name!");
        _name = name;
    }

    public string Name
    {
        get { return _name; }
    }

    public string Address { get; set; }
    public int SomeValue { get; set; }

    public int ImportantCalculation()
    {
        return 1000;
    }

    public void ImportantVoidMethod()
    {
    }

    public enum SomeEnumeration
    {
        ValueOne = 1,
        ValueTwo = 2
    }

    public class SomeNestedClass
    {
        private string _someString;
    }
}

class Program
{
    static void Main()
    {
        Type customerType = typeof(Customer);

        Console.WriteLine("==================== ZADANIE 1 ====================\n");
        Console.WriteLine("Pełna analiza klasy Customer za pomocą refleksji:\n");

        Console.WriteLine("Fields: ");
        FieldInfo[] fields = customerType.GetFields(
            BindingFlags.Public | BindingFlags.NonPublic | 
            BindingFlags.Instance | BindingFlags.Static);
        
        foreach (var field in fields)
        {
            string accessLevel = field.IsPublic ? "Public" : 
                                field.IsPrivate ? "Non Public" : "Protected";
            Console.WriteLine($"  - {accessLevel}: {field.FieldType.Name} {field.Name}");
        }

        Console.WriteLine("\nProperties: ");
        PropertyInfo[] properties = customerType.GetProperties();
        foreach (var prop in properties)
        {
            Console.WriteLine($"  - {prop.Name}");
        }

        Console.WriteLine("\nMethods: ");
        MethodInfo[] methods = customerType.GetMethods(
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        
        foreach (var method in methods)
        {
            if (!method.IsSpecialName)
            {
                Console.WriteLine($"  - {method.Name}");
            }
        }

        Console.WriteLine("\nNested types: ");
        Type[] nestedTypes = customerType.GetNestedTypes(
            BindingFlags.Public | BindingFlags.NonPublic);
        
        foreach (var nested in nestedTypes)
        {
            Console.WriteLine($"  - {nested.Name}");
        }

        Console.WriteLine("\nMembers: ");
        MemberInfo[] members = customerType.GetMembers(
            BindingFlags.Public | BindingFlags.NonPublic | 
            BindingFlags.Instance | BindingFlags.Static);
        
        foreach (var member in members)
        {
            Console.WriteLine($"  - {member.Name}");
        }

        Console.WriteLine("\n\n==================== ZADANIE 2 ====================\n");
        Console.WriteLine("Wyświetlanie właściwości za pomocą GetProperty:\n");

        Customer customer = new Customer("Jan Kowalski");
        customer.Address = "ul. Przykładowa 123";
        customer.SomeValue = 42;

        PropertyInfo[] allProperties = customerType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in allProperties)
        {
            Console.WriteLine($"Właściwość: {prop.Name}");
            Console.WriteLine($"  Typ: {prop.PropertyType.Name}");
            Console.WriteLine($"  Można odczytać: {prop.CanRead}");
            Console.WriteLine($"  Można zapisać: {prop.CanWrite}");
            
            if (prop.CanRead)
            {
                object value = prop.GetValue(customer);
                Console.WriteLine($"  Wartość: {value ?? "null"}");
            }
            
            Console.WriteLine();
        }

        Console.WriteLine("=== Użycie GetProperty dla konkretnych właściwości ===\n");

        PropertyInfo nameProperty = customerType.GetProperty("Name");
        if (nameProperty != null)
        {
            Console.WriteLine($"Pobrano właściwość: {nameProperty.Name}");
            Console.WriteLine($"Wartość: {nameProperty.GetValue(customer)}");
        }

        PropertyInfo addressProperty = customerType.GetProperty("Address");
        if (addressProperty != null)
        {
            Console.WriteLine($"\nPobrano właściwość: {addressProperty.Name}");
            Console.WriteLine($"Wartość przed zmianą: {addressProperty.GetValue(customer)}");
            
            addressProperty.SetValue(customer, "ul. Nowa 456");
            Console.WriteLine($"Wartość po zmianie: {addressProperty.GetValue(customer)}");
        }
    }
}