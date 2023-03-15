// See https://aka.ms/new-console-template for more information
using WordifyNumberApp;

Console.WriteLine("Please input number: ");

string? line = Console.ReadLine();

bool success = decimal.TryParse(line, out decimal value);

if (!success || value < 0)
{
    Console.WriteLine($"'{line}' is not a positive decimal value.");
    return;
}

if (line!.Length > ((int)ThreeNumberPosition.Quintillions * 3))
{
    Console.WriteLine($"Cannot process '{line}' due to over Quantillions value.");
    return;
}

Console.WriteLine("Number to wordify: {0:#,0}", value);
Console.WriteLine("Hit any key to proceed");
Console.ReadKey();

var wordifyNumber = new WordifyNumber();

string words = wordifyNumber.Wordify(line!);

Console.WriteLine();
Console.WriteLine("Wordify Number: " + words);

