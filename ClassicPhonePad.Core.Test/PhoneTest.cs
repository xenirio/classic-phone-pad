using Moq;

namespace ClassicPhonePad.Core.Tests
{
    [TestClass]
    public class ClassicPhoneTests
    {
        private Mock<IKeyPad> _keypad;
        private Mock<IProcessor> _processor;
        private ClassicPhone _phone;

        [TestInitialize]
        public void Setup()
        {
            _keypad = new Mock<IKeyPad>();
            _processor = new Mock<IProcessor>();
            _phone = new ClassicPhone(_keypad.Object, _processor.Object);
        }


        [TestMethod]
        public void Press_InputMustEnd_WithHash()
        {
            // Arrange
            var input = "33";

            // Assert to throw exception
            Assert.ThrowsException<ArgumentException>(() => _phone.Press(input));
        }

        [TestMethod]
        public void Encode_MustBeDigitsOrStarOrHash()
        {
            // Arrange
            var input = "ABC*DEF**GH#";

            // Assert to throw exception
            Assert.ThrowsException<ArgumentException>(() => _phone.Press(input));
        }

        [TestMethod]
        public void Press_ShouldReturnDecodedInput()
        {
            // Arrange
            _keypad.Setup(k => k.Encode(It.IsAny<string>())).Returns([new KeyValuePair<char, int>('2', 1)]);
            _keypad.Setup(k => k.Parse(It.IsAny<KeyValuePair<char, int>[]>())).Returns(['A']);
            _processor.Setup(p => p.Decode(It.IsAny<char[]>())).Returns("A");

            // Act
            var result = _phone.Press("2#");

            // Assert
            Assert.AreEqual("A", result);
        }
    }
}