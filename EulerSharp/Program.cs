using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using EulerSharp.Common;

namespace EulerSharp
{
    public class Program
    {
        private const string BREAK_SEQ = " = ";

        private static string _breakString;
        private static IList<SolutionType> _solutions;

        static void Main(string[] args)
        {
            WriteBreakLine();
            Console.WriteLine(" PROJECT EULER");
            WriteBreakLine();

            _solutions = GetAllSolutionTypes();
            Console.Write(" Loading solutions...");
            Console.WriteLine("Done!");

            StartInputLoop();
        }

        private static void WriteBreakLine()
        {
            if (string.IsNullOrWhiteSpace(_breakString))
            {
                var repeatCount = Console.WindowWidth / BREAK_SEQ.Length;

                var sb = new StringBuilder();

                for (int i = 0; i < repeatCount; i++)
                {
                    sb.Append(BREAK_SEQ);
                }

                _breakString = sb.ToString();
            }

            Console.WriteLine();
            Console.WriteLine(_breakString);
            Console.WriteLine();
        }

        private static IList<SolutionType> GetAllSolutionTypes()
        {
            var result = new List<SolutionType>();
            var solAssembly = Assembly.Load(new AssemblyName("EulerSharp.Solutions"));

            foreach (var t in solAssembly.GetTypes())
            {
                if (t.GetCustomAttributes(typeof(EulerSolutionAttribute), true).Length > 0)
                {
                    var attr = t.GetCustomAttributes(typeof(EulerSolutionAttribute), true).First() as EulerSolutionAttribute;

                    if (attr != null)
                    {
                        result.Add(new SolutionType()
                        {
                            ProblemId = attr.ProblemId,
                            Title = attr.Name,
                            Type = t
                        });
                    }
                }
            }

            return result;
        }

        private static void StartInputLoop()
        {
            var exit = false;

            WriteBreakLine();
            PrintPuzzleOptions();

            while (!exit)
            {
                var input = GetInput();

                int result;
                if (Int32.TryParse(input, out result))
                    ExecuteTask(result);
                else
                    exit = HandleOtherInput(input);
            }
        }

        private static void PrintPuzzleOptions()
        {
            foreach (var entry in _solutions)
            {
                PrintSolutionDetail(entry);
            }

            Console.WriteLine();
        }

        private static void PrintSolutionDetail(SolutionType entry)
        {
            Console.WriteLine(string.Format(" {0}) {1}", entry.ProblemId, entry.Title));
        }

        private static string GetInput()
        {
            while (true)
            {
                Console.Write(" Please select puzzle to run > ");
                var input = Console.ReadLine();

                int result;
                if (Int32.TryParse(input, out result) && _solutions.Any(x => x.ProblemId == result))
                {
                    return input;
                }

                var upperInput = input.ToUpperInvariant();
                var validChars = new string[] { "QUIT", "EXIT", "LIST" };

                if (validChars.Contains(upperInput))
                    return upperInput.Substring(0, 1);

                if (upperInput.Length == 1 && validChars.Any(x => x[0] == upperInput[0]))
                    return upperInput.Substring(0, 1);

                Console.WriteLine(" Sorry, input was unrecognised");
            }
        }

        private static void ExecuteTask(int puzzleId)
        {
            var type = _solutions.First(x => x.ProblemId == puzzleId).Type;

            WriteBreakLine();
            Console.Write(" Executing task {0}...");

            Task<object> workerTask = Task.Factory.StartNew(() =>
            {
                var instance = (AbstractSolution)Activator.CreateInstance(type);
                return instance.Solve();
            });

            //Start spin caret
            workerTask.Wait();
            //Stop spin caret

            if (workerTask.IsCompleted)
            {
                Console.WriteLine(" Solution is: {0}", workerTask.Result);
                return;
            }

            if (workerTask.IsCanceled)
            {
                Console.WriteLine(" Task was cancelled");
                return;
            }

            Console.WriteLine(" A failure occured during processing: {0}", workerTask.Exception.ToString());
        }

        private static bool HandleOtherInput(string input)
        {
            if (input == "Q" || input == "E")
                return true;

            PrintPuzzleOptions();

            return false;
        }
    }
}
