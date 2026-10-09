// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    // Removes the item the user sees as number 1, 2, 3 ...
   public void RemoveAt(int number)
    {
        if (number >= 1 && number <= items.Count)
        {
            items.RemoveAt(number - 1);
        }
        else
        {
        Console.WriteLine("Det finns ingen vara med det numret.");
        }
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
            Console.WriteLine("Listan är sparad.");
        }

        catch (IOException)
        {
            Console.WriteLine("Det gick inte att spara listan.");
        }

        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Du har inte behörighet att spara filen.");
        }
}
    // Reads the file back into the list.
    public void Load()
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Ingen sparad lista hittades. En ny lista startas.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(';');

                if (parts.Length != 2 || !int.TryParse(parts[0], out int price))
                {
                    Console.WriteLine("En felaktig rad i filen hoppades över.");
                    continue;
                }

                items.Add(new Item(parts[1].Trim(), price));
            }
        }

        catch (IOException)
        {
            Console.WriteLine("Det gick inte att läsa filen.");
        }
        
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Du har inte behörighet att läsa filen.");
        }
    }
}
