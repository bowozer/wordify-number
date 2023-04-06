// See https://aka.ms/new-console-template for more information
using WordifyNumberApp;

bool stop = false;

Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
{
    e.Cancel = true;
    stop = true;
};

while (!stop)
{
    Console.WriteLine("Please input number: ");

    string? line = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(line)) continue;

    bool success = decimal.TryParse(line, out decimal value);

    if (!success || value < 0)
    {
        Console.WriteLine($"'{line}' is not a positive decimal value.");
        continue;
    }

    if (line!.Length > ((int)ThreeNumberPosition.Quintillions * 3))
    {
        Console.WriteLine($"Cannot process '{line}' due to over Quantillions value.");
        continue;
    }

    Console.WriteLine("Number to wordify: {0:C2}", Math.Truncate(value * 100) / 100);
    Console.WriteLine("Hit any key to proceed");
    Console.ReadKey();

    var wordifyNumber = new WordifyNumber();

    string words = wordifyNumber.Wordify(line!);

    Console.WriteLine();
    Console.WriteLine("Wordified Number: " + words);

    Console.WriteLine();
    Console.Write("Again? [Y] Yes, [n] No: ");
    ConsoleKeyInfo info = Console.ReadKey();
    if (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Y)
    {
        stop = true;
    }

    Console.WriteLine();
}
