using Xunit;
using GASLanguageProcessor.FinalTypes;
using System.Collections.Generic;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests
{
    public class AddToListTest
    {
        [Fact]
        public void EvaluateExpressionAddToListPass()
        {
            var env = SharedTesting.RunInterpreter(
                "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
                "num x = 10;" +
                "list<num> l = <num>[1, 2, 3, 4, 5];" +
                "AddToList(5, l);"
            );

            var envV = env.Item1;
            var sto = env.Item2;

            Assert.Empty(env.Item5);

            var result = sto.LookUp(envV.LookUp("l").Value) as FinalList;
            var expected = new FinalList([ 1f, 2f, 3f, 4f, 5f, 5f ]);

            Assert.NotNull(result);
            Assert.IsType<FinalList>(result);
            Assert.Equal(expected.Values, result.Values);
        }

        [Fact]
        public void EvaluateExpressionAddToListWithEmptyListPass()
        {
            var env = SharedTesting.RunInterpreter(
                "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
                "num x = 10;" +
                "list<num> l = List<num>{};" +
                "AddToList(5, l);"
            );

            var envV = env.Item1;
            var sto = env.Item2;

            Assert.Empty(env.Item5);

            var result = sto.LookUp(envV.LookUp("l").Value) as FinalList;
            var expected = new FinalList([ 5f ]);

            Assert.NotNull(result);
            Assert.IsType<FinalList>(result);
            Assert.Equal(expected.Values, result.Values);
        }
    }
}
