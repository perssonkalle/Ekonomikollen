EKONOMIKOLLEN

En konsolapplikation utvecklad i C# och .NET som en projektuppgift i kursen Programmering i C#.NET vid Mittuniversitetet.

Programmet används för att hålla rewda på inkomster och utgifter och ger användaren en enkel överblick över sin ekonomi.

#Funktioner 

* Registrera inkomster och utgifter med belopp, kategori och beskrivning.

* Visa alla registrerade transaktioner.

* Visa aktuellt saldo, totala inkomster och utgifter.

* Söka efter transaktioner utifrån kategori eller beskrivning.

* Ta bort transaktioner med hjälp av ID.

* Spara och läsa in transaktioner automatiskt via en JSON-fil.

* Kontrollera vanliga fel vid användarinmatning.


Programmet är utvecklats med C# och .NET och använder bland annat objektorienterad programmering, listor, LINQ och Json-seralisering.

Projektet består huvudsakligen av tre filer: 

* Program.cs - hanterar menyn och användarinmatningen.

* Transaction.cs - innehåller datamodellen för en transaktion.

* EconomyService.cs - hanterar transaktioner, beräkningar, sökning och filhantering. 


# Starta programmet

.NET SDK behöver vara installerat.

* Öppna en terminal i projektmappen och kör: 

             dotnet run


Programmet startar i konsolen och visar en meny där du kan välja mellan de olika funktionerna.

Transaktionerna sparas lokalt i filen transactions.json och läses in igen när programmet startas.





