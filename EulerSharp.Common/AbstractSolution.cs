using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerSharp.Common
{
    public abstract class AbstractSolution
    {
        /// <summary>
        /// Abstract method that returns an object that is the solution to the problem
        /// </summary>
        /// <returns>The answer to the given Project Euler problem</returns>
        public abstract object Solve();
    }
}
