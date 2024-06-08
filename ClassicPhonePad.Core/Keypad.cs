using System.Text;
using System.Text.RegularExpressions;

namespace ClassicPhonePad.Core;

public interface IButton
{
    // Decode the button press based on the number of times pressed
    char Decode(int times);
}

public class Button : IButton
{
    private char[] Characters { get; }

    public Button(char[] characters)
    {
        Characters = characters;
    }
    public char Decode(int times)
    {
        // Return null if times is less than or equal to 0
        if (times <= 0)
            return '\0';

        // Return the character based on the number of times pressed
        return Characters[(times - 1) % Characters.Length];
    }
}

public interface IKeyPad
{
    // Add a button to the keypad with a number
    void AddButton(char number, IButton button);

    // Encode the input into a list of number and count pairs
    KeyValuePair<char, int>[] Encode(string input);

    // Parse the list of number and count pairs into a list of characters
    char[] Parse(KeyValuePair<char, int>[] numbers);
}

public class Keypad : IKeyPad
{
    private readonly IDictionary<char, IButton> _buttons;

    public Keypad()
    {
        _buttons = new Dictionary<char, IButton>();
    }

    public void AddButton(char number, IButton button)
    {
        _buttons.Add(number, button);
    }

    public KeyValuePair<char, int>[] Encode(string input)
    {
        // Extract input by match any digit, *, or # that repeats 1-3 times
        var tokens = Regex.Matches(input, @"(\d|\*|\#)\1{0,2}");
        return tokens.Select(t => new KeyValuePair<char, int>(t.Value[0], t.Value.Length)).ToArray();
    }

    public char[] Parse(KeyValuePair<char, int>[] numbers)
    {
        var tokens = new List<char>();
        foreach (var number in numbers)
        {
            // Throw an exception if the button is not found
            if (!_buttons.ContainsKey(number.Key))
                throw new ArgumentException($"Button {number.Key} not found");

            // Decode the button press based on the number of times pressed
            var button = _buttons[number.Key];
            var symbol = button.Decode(number.Value);
            if (symbol != '\0')
                tokens.Add(symbol);
        }

        return tokens.ToArray();
    }
}
