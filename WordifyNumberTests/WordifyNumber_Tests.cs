using WordifyNumberApp;
using Xunit;

namespace WordifyNumberTests
{
    public class WordifyNumber_Tests
    {
        private WordifyNumber? _wordifyNumber;

        [Theory]
        [InlineData("0", new string[] { "0" })]
        [InlineData("12", new string[] { "12" })]
        [InlineData("123", new string[] { "123" })]
        [InlineData("4123", new string[] { "4", "123" })]
        [InlineData("45123", new string[] { "45", "123" })]
        [InlineData("456123", new string[] { "456", "123" })]
        [InlineData("7456000", new string[] { "7", "456", "000" })]
        public void BuildArrayOfThree_VariousCases_ShouldReturnAsExpected(string numberText, string[] expectedArrayOf3)
        {
            // Arrange
            _wordifyNumber = new WordifyNumber(numberText);

            // Act
            string[] arrayOf3 = _wordifyNumber.BuildArrayOfThree();

            // Assert
            Assert.Equal(expectedArrayOf3.Length, arrayOf3.Length);

            for (int i = 0; i < expectedArrayOf3.Length; i++)
            {
                Assert.Equal(expectedArrayOf3[i], arrayOf3[i]);
            }
        }

        [Theory]
        [InlineData("0", "")]
        [InlineData("00", "")]
        [InlineData("000", "")]
        [InlineData("1", "one")]
        [InlineData("10", "ten")]
        [InlineData("11", "eleven")]
        [InlineData("12", "twelve")]
        [InlineData("13", "thirteen")]
        [InlineData("14", "fourteen")]
        [InlineData("15", "fifteen")]
        [InlineData("16", "sixteen")]
        [InlineData("20", "twenty")]
        [InlineData("21", "twenty one")]
        [InlineData("30", "thirty")]
        [InlineData("40", "forty")]
        [InlineData("50", "fifty")]
        [InlineData("60", "sixty")]
        [InlineData("62", "sixty two")]
        [InlineData("100", "one hundred")]
        [InlineData("204", "two hundred and four")]
        [InlineData("210", "two hundred and ten")]
        [InlineData("211", "two hundred and eleven")]
        [InlineData("213", "two hundred and thirteen")]
        [InlineData("217", "two hundred and seventeen")]
        [InlineData("320", "three hundred and twenty")]
        [InlineData("343", "three hundred and forty three")]
        [InlineData("374", "three hundred and seventy four")]
        public void WordifyHundredOrLess_VariousCases_ShouldReturnsAsExpected(string hundredText, string expectedWords)
        {
            // Arrange
            _wordifyNumber = new WordifyNumber("0");

            // Act
            string words = _wordifyNumber.WordifyHundredOrLess(hundredText);

            // Assert
            Assert.Equal(expectedWords, words);
        }

    }
}