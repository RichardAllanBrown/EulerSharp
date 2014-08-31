using System;
using System.Linq;
using System.Collections.Generic;
using EulerSharp.Common;

namespace EulerSharp.Solutions
{
    [EulerSolution(ProblemId = 3, Name = "Largest prime factor")]
    public class Problem3 : AbstractSolution
    {
        private static long TARGET_NUMBER = 600851475143;

        public override object Solve()
        {
            var factors = new List<long>();
            var remaining = TARGET_NUMBER;

            while (3 < remaining)
            {
                var factor = Primes(remaining / 2 + 1).FirstOrDefault(x => remaining % x == 0);

                if (factor != 0)
                {
                    factors.Add(factor);
                    remaining = remaining / factor;
                }
                else
                {
                    factors.Add(remaining);
                    remaining = 0;
                }
            }

            return factors.OrderByDescending(x => x).First();
        }

        private IEnumerable<long> Primes(long limit)
        {
            yield return 2;
            
            var candidate = 3;

            while (candidate <= limit)
            {
                if (IsPrime(candidate))
                {
                    yield return candidate;
                }

                candidate += 2;
            }
        }

        private bool IsPrime(long number)
        {
            for (long i = 3; i < number / 2; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}
