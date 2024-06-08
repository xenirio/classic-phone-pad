using System.Text.RegularExpressions;

namespace ClassicPhonePad.Core;

public class ClassicPhone {
    private readonly IKeyPad _keypad;
    private readonly IProcessor _processor;

    public ClassicPhone(IKeyPad keypad, IProcessor processor)
    {
        _keypad = keypad;
        _processor = processor;
    }

    public string Press(string input)
    {
        // Validate input need to be numeric or * or # only
        if (!Regex.IsMatch(input, @"^[\d\*\#\s]+$"))
            throw new ArgumentException("Input must be numeric or * or # only.");

        // Validate input need to end with hash
        if (!input.EndsWith("#"))
            throw new ArgumentException("Input must end with #.");

        // Encode the input into a list of number and count pairs, then parse the list of characters
        var numbers = _keypad.Encode(input);
        var tokens = _keypad.Parse(numbers);

        // Decode the list of characters into a string
        return _processor.Decode(tokens);
    }
}
