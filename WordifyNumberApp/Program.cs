// See https://aka.ms/new-console-template for more information
Console.WriteLine("Please input number: ");

string? line = Console.ReadLine();

bool success = long.TryParse(line, out long value);

if (!success)
{
    Console.WriteLine($"'{line}' is not a valid non-decimal value.");
    return;
}

Console.WriteLine("Number to wordify: {0:#,0}", value);
Console.WriteLine("Hit any key to proceed");
Console.ReadKey();

var wordifyNumber = new WordifyNumber(line!);

string words = wordifyNumber.Wordify();

Console.WriteLine();
Console.WriteLine("Wordify Number: " + words);

