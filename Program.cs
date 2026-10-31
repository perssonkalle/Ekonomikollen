
using Ekonomikollen.Services;
 // Skapar serviceklassen som hanterar programmets transaktioner
EconomyService economy = new EconomyService();

// Läser in tidigare sparade transaktioner vid ny uppstart
economy.Load();

bool running = true;
 
 // Huvudloopen fortsätter tills användaren väljer att avsluta
while (running)
{
    Console.Clear();

    Console.WriteLine("========== EKONOMIKOLLEN ==========");
    Console.WriteLine();
    Console.WriteLine("1. Lägg till inkomst");
    Console.WriteLine("2. Lägg till utgift");
    Console.WriteLine("3. Visa alla transaktioner");
    Console.WriteLine("4. Visa ekonomisk sammanställning");
    Console.WriteLine("5. Ta bort transaktion");
    Console.WriteLine("6. Sök transaktioner");
    Console.WriteLine();
    Console.WriteLine("0. Avsluta");
    Console.WriteLine();
    Console.Write("Välj ett alternativ: ");

    string choice = Console.ReadLine()?.Trim() ?? "";
    
    // Anropar rätt funktion beroende på användarens menyval
    switch (choice)
    {
        case "1":
            AddTransaction("Inkomst");
            break;

        case "2":
            AddTransaction("Utgift");
            break;

        case "3":
            Console.Clear();
            Console.WriteLine("ALLA TRANSAKTIONER");
            Console.WriteLine();

            economy.ShowTransactions();
            Pause();
            break;

        case "4":
            Console.Clear();
            economy.ShowSummary();
            Pause();
            break;

        case "5":
            RemoveTransaction();
            break;

        case "6":
            SearchTransactions();
            break;

        case "0":
            running = false;
            break;

        default:
            Console.WriteLine("Felaktigt menyval. Försök igen.");
            Pause();
            break;
    }
}

Console.WriteLine("Programmet avslutas.");


// Lägger till en inkomst eller utgift med samma metod
void AddTransaction(string type)
{
    Console.Clear();
    Console.WriteLine($"LÄGG TILL {type.ToUpper()}");
    Console.WriteLine();

    decimal amount;

    // Kontrollerar att beloppet är ett giltigt tal, större än 0
    while (true)
    {
        Console.Write("Ange belopp: ");

        if (decimal.TryParse(Console.ReadLine(), out amount)
            && amount > 0)
        {
            break;
        }

        Console.WriteLine("Ange ett giltigt belopp större än 0.");
    }

    Console.Write("Ange kategori: ");
    string category = Console.ReadLine()?.Trim() ?? "";

    // Kräver att användaren anger en kategori innan programmet fortsätter
    while (string.IsNullOrWhiteSpace(category))
    {
        Console.Write("Kategorin får inte vara tom. Försök igen: ");
        category = Console.ReadLine()?.Trim() ?? "";
    }

    Console.Write("Ange beskrivning: ");
    string description = Console.ReadLine()?.Trim() ?? "";

    // Sätter datumet automatiskt till dagens datum
    DateTime date = DateTime.Today;

    // Skickar uppgifterna till serviceklassen som registrerar transaktionen
    economy.AddTransaction(type, amount, category,
        description, date);

    Console.WriteLine();
    Console.WriteLine("Transaktionen har lagts till!");

    Pause();
}


// Tar bort en transaktion med hjälp av dess ID
void RemoveTransaction()
{
    Console.Clear();
    Console.WriteLine("TA BORT TRANSAKTION");
    Console.WriteLine();

    // Avbryter om det inte finns några transaktioner att ta bort
    if (economy.Transactions.Count == 0)
    {
        Console.WriteLine("Det finns inga transaktioner att ta bort.");
        Pause();
        return;
    }

    economy.ShowTransactions();

    Console.WriteLine();
    Console.Write("Ange ID på transaktionen som ska tas bort: ");

    string input = Console.ReadLine() ?? "";

    // Kontrollerar att användaren har angett ett giltigt heltal
    if (!int.TryParse(input, out int id))
    {
        Console.WriteLine("Fel! Du måste ange ett heltal.");
        Pause();
        return;
    }

    // Visar om transaktionen kunde hittas och tas bort
    if (economy.RemoveTransaction(id))
    {
        Console.WriteLine("Transaktionen har tagits bort!");
    }
    else
    {
        Console.WriteLine("Det finns ingen transaktion med detta ID.");
    }

    Pause();
}


// Söker efter transaktioner utifrån kategori eller beskrivning
void SearchTransactions()
{
    Console.Clear();
    Console.WriteLine("SÖK TRANSAKTIONER");
    Console.WriteLine();

    Console.Write("Ange kategori eller sökord: ");
    string searchTerm = Console.ReadLine()?.Trim() ?? "";

    // Avbryter sökningen om användaren inte skriver något
    if (string.IsNullOrWhiteSpace(searchTerm))
    {
        Console.WriteLine("Du måste ange ett sökord.");
        Pause();
        return;
    }

    Console.WriteLine();
    economy.SearchTransactions(searchTerm);

    Pause();
}


// Pausar programmet så att användaren har tid att läsa resultatet
void Pause()
{
    Console.WriteLine();
    Console.Write("Tryck Enter för att fortsätta...");
    Console.ReadLine();
}




