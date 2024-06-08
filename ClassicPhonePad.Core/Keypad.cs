using System.Text;
using System.Text.RegularExpressions;

namespace ClassicPhonePad.Core;

public interface IButton
{
    char Decode(int times);
}

public class Button : IButton
{
    private char[] Characters { get; }

    public Button(char[] characters)
    {
        Characters = characters;
    }
    public char Decode(int times) => Characters[(times - 1) % Characters.Length];
}

public interface IKeyPad
{
    void AddButton(char number, IButton button);
    KeyValuePair<char, int>[] Encode(string input);
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
            var button = _buttons[number.Key];
            tokens.Add(button.Decode(number.Value));
        }

        return tokens.ToArray();
    }
}
