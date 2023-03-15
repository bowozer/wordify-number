using System;
using WordifyNumberApp;
using Xunit;

namespace WordifyNumberTests
{
    public class WordifyNumber_Tests
    {
        private readonly WordifyNumber _wordifyNumber;

        public WordifyNumber_Tests()
        {
            _wordifyNumber = new WordifyNumber();
        }

        [Theory]
        [InlineData("0", new string[] { "0" })]
        [InlineData("12", new string[] { "12" })]
        [InlineData("123", new string[] { "123" })]
        [InlineData("4123", new string[] { "4", "123" })]
        [InlineData("45123", new string[] { "45", "123" })]
        [InlineData("456123", new string[] { "456", "123" })]
        [InlineData("7456000", new string[] { "7", "456", "000" })]
        public void BuildArrayOfThree_VariousCases_ReturnAsExpected(string numberText, string[] expectedArrayOf3)
        {
            // Act
            string[] arrayOf3 = _wordifyNumber.BuildArrayOfThree(numberText);

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
        public void WordifyHundredOrLess_VariousCases_ReturnsAsExpected(string hundredText, string expectedWords)
        {
            // Act
            string words = _wordifyNumber.WordifyHundredOrLess(hundredText);

            // Assert
            Assert.Equal(expectedWords, words);
        }

        [Theory]
        [InlineData("0", "zero")]
        [InlineData("0000", "zero")]
        [InlineData("1", "one")]
        [InlineData("17", "seventeen")]
        [InlineData("73", "seventy three")]
        [InlineData("430", "four hundred and thirty")]
        [InlineData("1000", "one thousand")]
        [InlineData("1001", "one thousand and one")]
        [InlineData("1082", "one thousand and eighty two")]
        [InlineData("2195", "two thousand one hundred and ninety five")]
        [InlineData("22000", "twenty two thousand")]
        [InlineData("22002", "twenty two thousand and two")]
        [InlineData("345000", "three hundred and forty five thousand")]
        [InlineData("345003", "three hundred and forty five thousand and three")]
        [InlineData("4000000", "four million")]
        [InlineData("4000004", "four million and four")]
        [InlineData("4005000", "four million and five thousand")]
        [InlineData("4005006", "four million and five thousand and six")]
        [InlineData("4207000", "four million two hundred and seven thousand")]
        [InlineData("5000000000", "five billion")]
        [InlineData("6000000000000", "six trillion")]
        [InlineData("7000000000000000", "seven quadrillion")]
        [InlineData("8000000000000000000", "eight quintillion")]
        [InlineData("9876543210123456789", "nine quintillion eight hundred and seventy six quadrillion five hundred and forty three trillion two hundred and ten billion one hundred and twenty three million four hundred and fifty six thousand seven hundred and eighty nine")]
        [InlineData("999999999999999999999", "nine hundred and ninety nine quintillion nine hundred and ninety nine quadrillion nine hundred and ninety nine trillion nine hundred and ninety nine billion nine hundred and ninety nine million nine hundred and ninety nine thousand nine hundred and ninety nine")]
        public void Wordify_VariousCases_ReturnAsExpected(string numberText, string expectedWords)
        {
            // Act
            string words = _wordifyNumber.Wordify(numberText);

            // Assert
            Assert.Equal(expectedWords, words);
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("a")]
        public void Wordify_NotPositiveDecimalValue_ThrowsArgumentException(string numberText)
        {
            Assert.Throws<ArgumentException>(() => _wordifyNumber.Wordify(numberText));
        }

        [Fact]
        public void Wordify_OverQuantillions_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _wordifyNumber.Wordify("1000000000000000000000"));
        }
    }
}