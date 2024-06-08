namespace ClassicPhonePad.Core.Tests
{
    [TestClass]
    public class ProcessorTests
    {
        [TestMethod]
        public void Decode_ShouldReturnCorrectResult_WhenInputContainsOnlyOperators()
        {
            // Arrange
            var processor = new Processor();
            var input = "*".ToCharArray();

            // Act
            var result = processor.Decode(input);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Decode_ShouldReturnCorrectResult_WhenInputContainsOperatorsAndOperands()
        {
            // Arrange
            var processor = new Processor();
            var input = "AB*C".ToCharArray();

            // Act
            var result = processor.Decode(input);

            // Assert
            Assert.AreEqual("AC", result);
        }

        [TestMethod]
        public void Decode_ShouldReturnCorrectResult_WhenInputContainsMultipleOperators()
        {
            // Arrange
            var processor = new Processor();
            var input = "A*B*C*D".ToCharArray();

            // Act
            var result = processor.Decode(input);

            // Assert
            Assert.AreEqual("D", result);
        }

        [TestMethod]
        public void Decode_ShouldReturnCorrectResult_WhenInputContainsMultipleAdjacentOperators()
        {
            // Arrange
            var processor = new Processor();
            var input = "ABC*DEFG**HIJK*L".ToCharArray();

            // Act
            var result = processor.Decode(input);

            // Assert
            Assert.AreEqual("ABDEHIJL", result);
        }

        [TestMethod]
        public void Decode_ShouldReturnCorrectResult_WhenInputContainEndOperator()
        {
            // Arrange
            var processor = new Processor();
            var input = "BP*#".ToCharArray();

            // Act
            var result = processor.Decode(input);

            // Assert
            Assert.AreEqual("B", result);
        }
    }
}