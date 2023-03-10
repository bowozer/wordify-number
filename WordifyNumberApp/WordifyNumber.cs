public class WordifyNumber
{
    private string _numberText;

    public WordifyNumber(string numberText)
    {
        bool success = long.TryParse(numberText, out _);
        if (!success) throw new ArgumentException($"Cannot wordify ${_numberText} because it is not a valid non-decimal value.");

        _numberText = numberText;
    }

    internal string[] BuildArrayOfThree()
    {
        return new string[]
        {
            "123",
            "345"
        };
    }

    public string Wordify()
    {
        /** 
         * todo-bowo: 
         * create array list of three
         * 
         * iterate every array list of three
         *   use WordifyHundredOrLess to convert to word
         *   
         */



        return "one";
    }

    public string WordifyHundredOrLess(string numberText)
    {
        return "two";
    }
}