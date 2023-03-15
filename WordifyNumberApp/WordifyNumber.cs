using System.Text;

namespace WordifyNumberApp
{
    // todo-bowo: add "dollars and cents"
    public class WordifyNumber
    {
        private const char ZERO = '0';
        private const char SPACE = ' ';
        private const string AND = "and";
        private const string TEN = "ten";

        private static readonly Dictionary<string, string> correctionDictionary = new()
        {
            { ThreeNumberPosition.Quintillions.ToString(), "quintillion" },
            { ThreeNumberPosition.Quadrillions.ToString(), "quadrillion" },
            { ThreeNumberPosition.Trillions.ToString(), "trillion" },
            { ThreeNumberPosition.Billions.ToString(), "billion" },
            { ThreeNumberPosition.Millions.ToString(), "million" },
            { ThreeNumberPosition.Thousands.ToString(), "thousand" },
            { ThreeNumberPosition.Hundreds.ToString(), "hundred" },
            { "eightty", "eighty" },
            { "fivety", "fifty" },
            { "fourty", "forty" },
            { "threety", "thirty" },
            { "twoty", "twenty" },
            { "eightteen", "eighteen" },
            { "fiveteen", "fifteen" },
            { "threeteen", "thirteen" },
            { "twoteen", "twelve" },
            { "oneteen", "eleven" },
            { "onety", TEN }
        };

        public WordifyNumber()
        {
        }

        internal string[] BuildArrayOfThree(string numberText)
        {
            int lengthMod3 = numberText.Length % 3;

            var arrayOf3List = new List<string>();
            if (lengthMod3 > 0)
            {
                string firstText = numberText.Substring(0, lengthMod3);
                arrayOf3List.Add(firstText);
            }

            for (int i = lengthMod3; i < numberText.Length; i += 3)
            {
                string threeCharText = numberText.Substring(i, 3);
                arrayOf3List.Add(threeCharText);
            }

            return arrayOf3List.ToArray();
        }

        public string Wordify(string numberText)
        {
            bool success = decimal.TryParse(numberText, out decimal parsedNum);
            if (!success || parsedNum < 0) throw new ArgumentException($"Cannot wordify ${numberText} because it is not a valid positive decimal value.");

            string[] threeNumberArray = BuildArrayOfThree(numberText);
            if (threeNumberArray.Length > (int)ThreeNumberPosition.Quintillions) throw new InvalidOperationException("cannot process passed Quintillions");

            var wordBuilder = new StringBuilder();
            for (int i = 0; i < threeNumberArray.Length; i++)
            {
                string current3Number = threeNumberArray[i];

                ThreeNumberPosition pos = (ThreeNumberPosition)(threeNumberArray.Length - i);

                string hundredOrLessWord = WordifyHundredOrLess(current3Number);
                wordBuilder.Append(hundredOrLessWord);
                if (!string.IsNullOrEmpty(hundredOrLessWord)) wordBuilder.Append(SPACE);

                if (!string.IsNullOrEmpty(hundredOrLessWord) && pos > ThreeNumberPosition.Hundreds)
                {
                    string numberWord = Correction(pos.ToString());
                    wordBuilder.Append(numberWord);
                    wordBuilder.Append(SPACE);

                    // since from here the position is thousands and up, there should be at least one last three numbers.
                    string next3Number = threeNumberArray[i + 1];
                    if (next3Number.First() == ZERO)
                    {
                        wordBuilder.Append(AND);
                        wordBuilder.Append(SPACE);
                    }
                }
            }

            string words = TrimNumberWords(wordBuilder);

            if (string.IsNullOrEmpty(words)) return OnesNumber.Zero.ToString().ToLower();
            return words;
        }

        internal string WordifyHundredOrLess(string hundredText)
        {
            const int HUNDREDS_LENGTH = 3;

            if (hundredText.Length > 0 && hundredText.Length > HUNDREDS_LENGTH) throw new ArgumentException("Requires a string of hundreds number or below.");

            hundredText = hundredText.PadLeft(HUNDREDS_LENGTH, ZERO);

            var wordBuilder = new StringBuilder();
            string tensText = string.Empty;

            for (int i = 0; i < hundredText.Length; i++)
            {
                HundredOrLessPosition pos = (HundredOrLessPosition)(hundredText.Length - i);

                if (hundredText[i] == ZERO && pos != HundredOrLessPosition.Ones) continue;

                OnesNumber currentNumberEnum = (OnesNumber)char.GetNumericValue(hundredText[i]);
                string currentNumberText = currentNumberEnum.ToString().ToLower();

                if (pos == HundredOrLessPosition.Hundreds && currentNumberEnum > OnesNumber.Zero)
                {
                    // hundreds
                    wordBuilder.Append(currentNumberText);
                    wordBuilder.Append(SPACE);
                    wordBuilder.Append(Correction(pos.ToString()));
                    wordBuilder.Append(SPACE);
                    wordBuilder.Append(AND);
                    wordBuilder.Append(SPACE);
                }
                else if (pos == HundredOrLessPosition.Tens && currentNumberEnum > OnesNumber.Zero)
                {
                    // tens
                    tensText = Correction($"{currentNumberText}ty");
                    if (tensText != TEN)
                    {
                        // todo-bowo: can we use dash to separate tens and ones? e.g forty-five, twenty-three
                        wordBuilder.Append(tensText);
                        wordBuilder.Append(SPACE);
                    }
                }
                else
                {
                    if (tensText == TEN && currentNumberEnum == OnesNumber.Zero)
                    {
                        wordBuilder.Append(tensText);
                    }
                    else if (currentNumberEnum != OnesNumber.Zero)
                    {
                        if (tensText == TEN)
                        {
                            string numberText = currentNumberEnum.ToString().ToLower() + "teen";
                            numberText = Correction(numberText);
                            wordBuilder.Append(numberText);
                        }
                        else
                        {
                            wordBuilder.Append(currentNumberText);
                        }
                    }
                }
            }

            string words = TrimNumberWords(wordBuilder);
            return words;
        }

        private static string Correction(string numberText)
        {
            if (correctionDictionary.ContainsKey(numberText))
            {
                return correctionDictionary[numberText];
            }

            return numberText;
        }

        private static string TrimNumberWords(StringBuilder wordBuilder)
        {
            string words = wordBuilder.ToString().TrimEnd();

            string lastAndWord = SPACE + AND;
            if (words.Length >= lastAndWord.Length)
            {
                // remove "and" word if it is at the last chars.
                string lastAndLetters = words.Substring(words.Length - lastAndWord.Length, lastAndWord.Length);
                if (lastAndLetters == lastAndWord)
                {
                    words = words.Substring(0, words.Length - lastAndWord.Length);
                }
            }

            return words;
        }
    }
}