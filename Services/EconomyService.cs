
using System.Text.Json;
using Ekonomikollen.Models;

namespace Ekonomikollen.Services;

public class EconomyService
{
    // Lista som innehåller alla registrerade transaktioner
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    // Sökväg till JSON-filen där transaktionerna sparas
    private readonly string filePath = Path.Combine(
        AppContext.BaseDirectory, "transactions.json");

    // Lägger till en ny transaktion
    public void AddTransaction(string type, decimal amount,
        string category, string description, DateTime date)
    {
        Transaction transaction = new Transaction();

    // Ger transaktionen ID 1 om listan är tom, annars högsta ID + 1
        transaction.Id = Transactions.Count == 0
            ? 1
            : Transactions.Max(t => t.Id) + 1;

    // Tilldelar transaktionen de uppgifter som användaren har angett
        transaction.Type = type;
        transaction.Amount = amount;
        transaction.Category = category;
        transaction.Description = description;
        transaction.Date = date;

        Transactions.Add(transaction);

        // Sparar listan direkt efter att en transaktion har lagts till
        Save();
    }

    // Skriver ut alla registrerade transaktioner
    public void ShowTransactions()
    {
        // Visar ett meddelande och avslutar metoden om listan är tom
        if (Transactions.Count == 0)
        {
            Console.WriteLine("Det finns inga transaktioner.");
            return;
        }

        // Går igenom listan och skriver ut varje transaktion
        foreach (Transaction transaction in Transactions)
        {
            PrintTransaction(transaction);
        }
    }

    // Skriver ut en enskild transaktion i ett bestämt format
    private void PrintTransaction(Transaction transaction)
    {
        Console.WriteLine(
            $"[{transaction.Id}] {transaction.Date:yyyy-MM-dd} | " +
            $"{transaction.Type} | {transaction.Amount:F2} kr | " +
            $"{transaction.Category} | {transaction.Description}");
    }

    // Beräknar och visar inkomster, utgifter och saldo
    public void ShowSummary()
    {
        decimal totalIncome = 0;
        decimal totalExpenses = 0;

     // Summerar inkomster och utgifter var för sig
        foreach (Transaction transaction in Transactions)
        {
            if (transaction.Type == "Inkomst")
            {
                totalIncome += transaction.Amount;
            }
            else if (transaction.Type == "Utgift")
            {
                totalExpenses += transaction.Amount;
            }
        }

     // Räknar ut saldot genom att dra av utgifterna från inkomsterna
        decimal balance = totalIncome - totalExpenses;

        Console.WriteLine("EKONOMISK SAMMANSTÄLLNING");
        Console.WriteLine("==========================");
        Console.WriteLine();

        Console.WriteLine($"Totala inkomster: {totalIncome:F2} kr");
        Console.WriteLine($"Totala utgifter:  {totalExpenses:F2} kr");
        Console.WriteLine("--------------------------");
        Console.WriteLine($"Kvarvarande saldo: {balance:F2} kr");
    }

    // Tar bort en transaktion med hjälp av dess ID
    public bool RemoveTransaction(int id)
    {
        // Söker efter transaktionen med LINQ och returnerar null om den saknas
        Transaction? transaction = Transactions.FirstOrDefault(
            t => t.Id == id);

        if (transaction == null)
        {
            return false;
        }

        Transactions.Remove(transaction);

       // Uppdaterar JSON-filen efter borttagningen
        Save();

      // Returnerar true när borttagningen lyckades
        return true;
    }

    // Söker bland transaktionernas kategorier och beskrivningar
    public void SearchTransactions(string searchTerm)
    {

     // Filtrerar listan med LINQ utan att skilja på små & stora bokstäver
        List<Transaction> results = Transactions
            .Where(t =>
                t.Category.Contains(
                    searchTerm, StringComparison.OrdinalIgnoreCase)
                ||
                t.Description.Contains(
                    searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("Inga transaktioner matchade sökningen.");
            return;
        }

        Console.WriteLine($"Antal träffar: {results.Count}");
        Console.WriteLine();

      // Skriver ut alla transaktioner som matchar sökordet
        foreach (Transaction transaction in results)
        {
            PrintTransaction(transaction);
        }
    }

    // Sparar transaktionerna i JSON-filen
    public void Save()
    {

      // Omvandlar listan till JSON och gör texten lättare att läsa
        string json = JsonSerializer.Serialize(
            Transactions,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(filePath, json);
    }

    // Läser in tidigare sparade transaktioner vid programstart
    public void Load()
    {

        // Kontrollerar att JSON-filet finns innan den läses in
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);


        // Omvandlar JSON till en lista eller skapar en tom lista vid null
            Transactions = JsonSerializer.Deserialize<List<Transaction>>(json)
                ?? new List<Transaction>();
        }
    }
}



