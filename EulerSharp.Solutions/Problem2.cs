using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EulerSharp.Common;

namespace EulerSharp.Solutions
{
    [EulerSolution(ProblemId = 2, Name = "Even Fibonacci numbers")]
    public class Problem2 : AbstractSolution
    {
        private const int SEARCH_LIMIT = 4000000;

        public override object Solve()
        {
            var fibNumbers = EnumerateFibonacci(SEARCH_LIMIT);
                
            var answer = fibNumbers.Where(x => x % 2 == 0).Sum();

            return answer;
        }

        private IEnumerable<int> EnumerateFibonacci(int limit)
        {
            if (limit <= 0)
                yield break;

            yield return 1;

            if (limit <= 1)
                yield break;

            yield return 2;

            var numberBeforeLast = 1;
            var lastNumber = 2;

            while (true)
            {
                var nextFibonacci = lastNumber + numberBeforeLast;
                numberBeforeLast = lastNumber;
                lastNumber = nextFibonacci;

                if (limit <= nextFibonacci)
                    yield break;

                yield return nextFibonacci;
            }
        }
    }
}
