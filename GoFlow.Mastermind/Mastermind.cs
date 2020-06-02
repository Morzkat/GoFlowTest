using System.Collections.Generic;

namespace GoFlow.Mastermind
{
    enum CodePeg
    {
        Black,
        Blue,
        Green,
        Red,
        White,
        Yellow,
    }

    enum ResultPeg
    {
        Black,
        White,
        None
    }

    class Mastermind
    {
        private List<CodePeg> codes;

        public Mastermind(List<CodePeg> codes)
        {
            this.codes = codes;
        }

        public IEnumerable<ResultPeg> GetHints(List<CodePeg> guessPegs)
        {
            var totalGuessPegs = guessPegs.Count;
            var hints = new Dictionary<int, ResultPeg>();
            for (int i = 0; i < totalGuessPegs; i++)
            {
                var hint = GetHint(guessPegs[i], i, hints);
                hints.Add(hint.Key, hint.Value);
            }
            return hints.Values.Randomize();
        }

        public KeyValuePair<int, ResultPeg> GetHint(CodePeg codePeg, int codePegIndex, Dictionary<int, ResultPeg> hints)
        {
            var totalCodesPegs = codes.Count;
            int i;
            for (i = 0; i < totalCodesPegs; i++)
            {
                if (codePeg == codes[i] && !hints.ContainsKey(i))
                {
                    if (codePegIndex == i)
                        return new KeyValuePair<int, ResultPeg>(i, ResultPeg.Black);

                    return new KeyValuePair<int, ResultPeg>(i, ResultPeg.White);
                }
            }
            return new KeyValuePair<int, ResultPeg>(codePegIndex + i, ResultPeg.None);
        }
    }
}