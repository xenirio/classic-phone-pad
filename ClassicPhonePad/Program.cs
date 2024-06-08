using System.Text;
using ClassicPhonePad.Core;

class Program
{
    private readonly ClassicPhone _phone;
    public Program()
    {
        var keypad = new Keypad();
        keypad.AddButton('1', new Button(['&', '\'', '(']));
        keypad.AddButton('2', new Button(['A', 'B', 'C']));
        keypad.AddButton('3', new Button(['D', 'E', 'F']));
        keypad.AddButton('4', new Button(['G', 'H', 'I']));
        keypad.AddButton('5', new Button(['J', 'K', 'L']));
        keypad.AddButton('6', new Button(['M', 'N', 'O']));
        keypad.AddButton('7', new Button(['P', 'Q', 'R', 'S']));
        keypad.AddButton('8', new Button(['T', 'U', 'V']));
        keypad.AddButton('9', new Button(['W', 'X', 'Y', 'Z']));
        keypad.AddButton('0', new Button([' ']));
        keypad.AddButton('*', new Button(['*']));
        keypad.AddButton('#', new Button(['#']));

        var processor = new Processor();
        _phone = new ClassicPhone(keypad, processor);
    }

    public static string OldPhonePad(string input)
    {
        var program = new Program();
        return program._phone.Press(input);
    }

    static void execute(string input)
    {
        try
        {
            Console.WriteLine($"OldPhonePad(\"{input}\") => output: {OldPhonePad(input)}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"OldPhonePad(\"{input}\") => error: {e.Message}");
        }
    }

    static void Main(string[] args)
    {
        // Run examples
        var examples = new[]
        {
            "33#",
            "227*#",
            "4433555 555666#",
            "8 88777444666*664#",
            "ABC*DEFG**#"
        };
        Console.WriteLine("Running examples:");
        for (int i = 0; i < examples.Length; i++)
        {
            execute(examples[i]);
        }

        // Allow user to try their own input
        ConsoleKeyInfo keyInfo;
        char keyChar;

        Console.WriteLine("\nPress Ctrl+C to exit.");
        while (true)
        {
            var token = new StringBuilder();
            Console.Write("\nEnter number sequence, confirm with \"#\": ");
            while (true)
            {
                keyInfo = Console.ReadKey(intercept: true);
                keyChar = keyInfo.KeyChar;

                // Handle backspace key press by removing the last character
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (token.Length > 0)
                    {
                        token.Length--;
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    token.Append(keyChar);
                    Console.Write(keyChar);
                }

                // Break loop if user confirms input with #
                if (keyChar == '#')
                {
                    Console.WriteLine();
                    break;
                }
            }

            var input = token.ToString();
            execute(input);
        }
    }
}
