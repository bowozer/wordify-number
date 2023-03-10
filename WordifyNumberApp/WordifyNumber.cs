using System.Text;

namespace WordifyNumberApp
{
    public class WordifyNumber
    {
        private const char ZERO = '0';
        private const char SPACE = ' ';
        private const string AND = "and";
        private const string TEN = "ten";

        private static readonly Dictionary<string, string> correctionDictionary = new()
        {
            { "fivety", "fifty" },
            { "fourty", "forty" },
            { "threety", "thirty" },
            { "twoty", "twenty" },
            { "onety", TEN }
        };

        private string _numberText;

        public WordifyNumber(string numberText)
        {
            bool success = long.TryParse(numberText, out _);
            if (!success) throw new ArgumentException($"Cannot wordify ${_numberText} because it is not a valid non-decimal value.");

            _numberText = numberText;
        }

        internal string[] BuildArrayOfThree()
        {
            int lengthMod3 = _numberText.Length % 3;

            var arrayOf3List = new List<string>();
            if (lengthMod3 > 0)
            {
                string firstText = _numberText.Substring(0, lengthMod3);
                arrayOf3List.Add(firstText);
            }

            for (int i = lengthMod3; i < _numberText.Length; i += 3)
            {
                string threeCharText = _numberText.Substring(i, 3);
                arrayOf3List.Add(threeCharText);
            }

            return arrayOf3List.ToArray();
        }

        public string Wordify()
        {
            if (_numberText == ZERO.ToString()) return UnitNumber.Zero.ToString().ToLower();

            string[] numberArrayOf3 = BuildArrayOfThree();



            /** 
             * todo-bowo: 
             * *create array list of three
             * 
             * iterate every array list of three
             *   use WordifyHundredOrLess to convert to word
             *   dont forget to add 'and' if the next is only zeroes
             */



            return "one";
        }

        internal string WordifyHundredOrLess(string hundredText)
        {
            if (hundredText.Length > 0 && hundredText.Length > 3) throw new ArgumentException("Requires a number string with length 3 or less.");

            hundredText = hundredText.PadLeft(3, ZERO);

            var wordBuilder = new StringBuilder();
            string tensText = string.Empty;

            for (int i = 0; i < hundredText.Length; i++)
            {
                NumberPosition pos = (NumberPosition)(hundredText.Length - i);

                if (hundredText[i] == ZERO && pos != NumberPosition.Unit) continue;

                UnitNumber currentNumberEnum = (UnitNumber)char.GetNumericValue(hundredText[i]);
                string currentNumberText = currentNumberEnum.ToString().ToLower();

                if (pos == NumberPosition.Hundreds && currentNumberEnum > UnitNumber.Zero)
                {
                    // hundreds
                    wordBuilder.Append($"{currentNumberText} hundred {AND}");
                    wordBuilder.Append(SPACE);
                }
                else if (pos == NumberPosition.Tens && currentNumberEnum > UnitNumber.Zero)
                {
                    // tens
                    tensText = Correction($"{currentNumberText}ty");
                    if (tensText != TEN)
                    {
                        wordBuilder.Append(tensText);
                        wordBuilder.Append(SPACE);
                    }
                }
                else
                {
                    if (tensText == TEN && currentNumberEnum == UnitNumber.Zero)
                    {
                        wordBuilder.Append(tensText);
                    }
                    else if (currentNumberEnum != UnitNumber.Zero)
                    {
                        if (tensText == TEN)
                        {
                            string numberText = WordifyTeens(currentNumberEnum);
                            wordBuilder.Append(numberText);
                        }
                        else
                        {
                            wordBuilder.Append(currentNumberText);
                        }
                    }
                }
            }

            string words = wordBuilder.ToString().TrimEnd();

            if (words.Length > AND.Length)
            {
                string lastAndLetters = words.Substring(words.Length - AND.Length, AND.Length);
                if (lastAndLetters == AND)
                {
                    words = words.Substring(0, words.Length - AND.Length);
                }
            }

            return words.TrimEnd();
        }

        private static string WordifyTeens(UnitNumber numberEnum)
        {
            string numberText;
            switch (numberEnum)
            {
                case UnitNumber.One:
                    numberText = "eleven";
                    break;
                case UnitNumber.Two:
                    numberText = "twelve";
                    break;
                case UnitNumber.Three:
                    numberText = "thirteen";
                    break;
                case UnitNumber.Five:
                    numberText = "fifteen";
                    break;
                default:
                    numberText = numberEnum.ToString().ToLower() + "teen";
                    break;
            }

            return numberText;
        }

        private static string Correction(string numberText)
        {
            if (correctionDictionary.ContainsKey(numberText))
            {
                return correctionDictionary[numberText];
            }

            return numberText;
        }
    }
}