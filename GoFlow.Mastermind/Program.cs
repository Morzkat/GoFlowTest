using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoFlow.Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game instructions: \nIf you introduce a no-listed number or a letter it will be converted to the default number which is 0. \nChoose the color using the numbers: ");
            PlayMasterMind();
        }

        public static void PlayMasterMind()
        {
            var attemps = 0;
            var positions = 4;
            var codePegs = (CodePeg[])Enum.GetValues(typeof(CodePeg));
            var codePegsToGuess = new List<CodePeg>();
            codePegsToGuess.AddRange(codePegs.Randomize().Take(positions));

            foreach (CodePeg codePeg in codePegs)
            {
                Console.WriteLine($"{codePeg}: {(int)codePeg}");
            }

            while (attemps < 10)
            {
                var guessCodePegs = new List<CodePeg>();

                for (int position = 1; position <= positions; position++)
                {
                    Console.WriteLine($"Select a color for the position {position}: ");
                    int.TryParse(Console.ReadLine(), out int guessCodePeg);
                    try
                    {
                        guessCodePegs.Add(codePegs[guessCodePeg]);
                    }
                    catch
                    {
                        guessCodePegs.Add(codePegs[0]);
                    }
                }

                Mastermind mastermind = new Mastermind(codePegsToGuess);
                var hints = mastermind.GetHints(guessCodePegs);
                var guessedPegs = 0;

                foreach (var hint in hints)
                {
                    if (hint == ResultPeg.Black)
                        guessedPegs++;

                    Console.WriteLine(hint);
                }

                if (guessedPegs == guessCodePegs.Count)
                {
                    Console.WriteLine($"Well done, you broke the code in {attemps} attemps.");
                    break;
                }

                attemps++;
            }
        }
    }
}
