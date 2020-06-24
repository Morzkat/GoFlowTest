using GoFlow.Mastermind;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GoFlow.Test
{
    public class MastermindTest
    {
        List<CodePeg> _codePegsToGuess;
        GoFlow.Mastermind.Mastermind _mastermind;

        [SetUp]
        public void Setup()
        {
            _codePegsToGuess = new List<CodePeg>() { CodePeg.Black, CodePeg.Black, CodePeg.Green, CodePeg.White };
            _mastermind = new GoFlow.Mastermind.Mastermind(_codePegsToGuess);
        }

        [Test]
        public void Mastermind_First_Test_Case()
        {
            var guessCodePegs = new List<CodePeg>() { CodePeg.Black, CodePeg.Black, CodePeg.Green, CodePeg.White }; //Black, Black, Black, Black 
            var expectedResult = new List<ResultPeg>() { ResultPeg.Black, ResultPeg.Black, ResultPeg.Black, ResultPeg.Black };

            var result = _mastermind.GetHints(guessCodePegs);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Mastermind_Second_Test_Case()
        {
            var guessCodePegs = new List<CodePeg>() { CodePeg.Green, CodePeg.White, CodePeg.Black, CodePeg.Black }; //White, White, White, White 
            var expectedResult = new List<ResultPeg>() { ResultPeg.White, ResultPeg.White, ResultPeg.White, ResultPeg.White };

            var result = _mastermind.GetHints(guessCodePegs);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Mastermind_Third_Test_Case()
        {
            var guessCodePegs = new List<CodePeg>() { CodePeg.Red, CodePeg.Black, CodePeg.Black, CodePeg.White }; //Black, Black, White, None   
            var expectedResult = new List<ResultPeg>() { ResultPeg.White, ResultPeg.Black, ResultPeg.None, ResultPeg.Black };

            var result = _mastermind.GetHints(guessCodePegs);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Mastermind_Fourth_Test_Case()
        {
            var guessCodePegs = new List<CodePeg>() { CodePeg.Green, CodePeg.Black, CodePeg.Black, CodePeg.White }; //White, White, Black, Black
            var expectedResult = new List<ResultPeg>() { ResultPeg.White, ResultPeg.White, ResultPeg.Black, ResultPeg.Black };

            var result = _mastermind.GetHints(guessCodePegs);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Mastermind_Fifth_Test_Case()
        {
            var guessCodePegs = new List<CodePeg>() { CodePeg.Red, CodePeg.White, CodePeg.Black, CodePeg.White }; //Black, White, None, None
            var expectedResult = new List<ResultPeg>() { ResultPeg.None, ResultPeg.White, ResultPeg.Black, ResultPeg.None };

            var result = _mastermind.GetHints(guessCodePegs).ToList();

            Assert.AreEqual(expectedResult, result);
        }
    }
}