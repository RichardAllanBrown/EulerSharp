using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EulerSharp.Common;

namespace EulerSharp.Solutions
{
    [EulerSolution(ProblemId = 1, Name = "Multiples of 3 and 5")]
    public class Problem1 : AbstractSolution
    {
        private const int TOP_LIMIT = 1000;

        public override object Solve()
        {
            var total = 0;

            for (int i = 3; i < TOP_LIMIT; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                    total += i;
            }

            return total;
        }
    }
}
