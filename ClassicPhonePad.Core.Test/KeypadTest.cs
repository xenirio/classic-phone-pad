namespace ClassicPhonePad.Core.Tests
{
    [TestClass]
    public class KeypadTests
    {
        private IKeyPad _keypad;

        [TestInitialize]
        public void Setup()
        {
            _keypad = new Keypad();
            _keypad.AddButton('1', new Button(['&', '\'', '(']));
            _keypad.AddButton('2', new Button(['A', 'B', 'C']));
            _keypad.AddButton('3', new Button(['D', 'E', 'F']));
            _keypad.AddButton('4', new Button(['G', 'H', 'I']));
            _keypad.AddButton('5', new Button(['J', 'K', 'L']));
            _keypad.AddButton('6', new Button(['M', 'N', 'O']));
            _keypad.AddButton('7', new Button(['P', 'Q', 'R', 'S']));
            _keypad.AddButton('8', new Button(['T', 'U', 'V']));
            _keypad.AddButton('9', new Button(['W', 'X', 'Y', 'Z']));
            _keypad.AddButton('0', new Button([' ']));
            _keypad.AddButton('*', new Button(['*']));
            _keypad.AddButton('#', new Button(['#']));
        }

        [TestMethod]
        public void Encode_ShouldReturnCorrectNumbers()
        {
            // Arrange
            var input = "227*#";

            // Act
            var result = _keypad.Encode(input);

            // Assert
            var expected = new[]
            {
                new KeyValuePair<char, int>('2', 2),
                new KeyValuePair<char, int>('7', 1),
                new KeyValuePair<char, int>('*', 1),
                new KeyValuePair<char, int>('#', 1)
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encode_ShouldReturnCorrectNumbers_WhenInputHasSpace()
        {
            // Arrange
            var input = "4433555 555666#";

            // Act
            var result = _keypad.Encode(input);

            // Assert
            var expected = new[]
            {
                new KeyValuePair<char, int>('4', 2),
                new KeyValuePair<char, int>('3', 2),
                new KeyValuePair<char, int>('5', 3),
                new KeyValuePair<char, int>('5', 3),
                new KeyValuePair<char, int>('6', 3),
                new KeyValuePair<char, int>('#', 1)
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Encode_ShouldReturnCorrectNumbers_WhenInputHasSpaces()
        {
            // Arrange
            var input = "4433555 55 5666#";

            // Act
            var result = _keypad.Encode(input);

            // Assert
            var expected = new[]
            {
                new KeyValuePair<char, int>('4', 2),
                new KeyValuePair<char, int>('3', 2),
                new KeyValuePair<char, int>('5', 3),
                new KeyValuePair<char, int>('5', 2),
                new KeyValuePair<char, int>('5', 1),
                new KeyValuePair<char, int>('6', 3),
                new KeyValuePair<char, int>('#', 1)
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Parse_ShouldReturnCorrectTokens()
        {
            // Arrange
            var numbers = new[]
            {
                new KeyValuePair<char, int>('2', 1),
                new KeyValuePair<char, int>('3', 2),
                new KeyValuePair<char, int>('4', 3)
            };

            // Act
            var result = _keypad.Parse(numbers);

            // Assert
            var expected = "AEI".ToCharArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Parse_ShouldReturnCorrectTokens_WithStar()
        {
            // Arrange
            var numbers = new[]
            {
                new KeyValuePair<char, int>('2', 2),
                new KeyValuePair<char, int>('7', 1),
                new KeyValuePair<char, int>('*', 1),
            };

            // Act
            var result = _keypad.Parse(numbers);

            // Assert
            var expected = "BP*".ToCharArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Parse_ShouldReturnTokens_WithHash()
        {
            // Arrange
            var numbers = new[]
            {
                new KeyValuePair<char, int>('2', 2),
                new KeyValuePair<char, int>('7', 1),
                new KeyValuePair<char, int>('#', 1),
            };

            // Act
            var result = _keypad.Parse(numbers);

            // Assert
            var expected = "BP#".ToCharArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Parse_ShouldReturnTokens_WithStartAndHash()
        {
            // Arrange
            var numbers = new[]
            {
                new KeyValuePair<char, int>('2', 2),
                new KeyValuePair<char, int>('7', 1),
                new KeyValuePair<char, int>('*', 1),
                new KeyValuePair<char, int>('#', 1),
            };

            // Act
            var result = _keypad.Parse(numbers);

            // Assert
            var expected = "BP*#".ToCharArray();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}