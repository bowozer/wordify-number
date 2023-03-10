using Xunit;

namespace WordifyNumberTests
{
    public class WordifyNumber_Tests
    {
        private readonly WordifyNumber _wordifyNumber;

        public WordifyNumber_Tests()
        {
            _wordifyNumber = new WordifyNumber(string.Empty);
        }

        [Fact]
        public void BuildArrayOfThree_Case_Should()
        {
            Assert.True(true);
        }
    }
}