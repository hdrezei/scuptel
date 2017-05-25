using System;

namespace ScupTel.Domain.Core.Helpers
{
    public static class TestConsoleWriterHelper
    {
        private static object _lockObject = new object();

        public static void PrintTestClass(string testClassName)
        {
            lock (_lockObject)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine();
                Console.WriteLine(" {0}", testClassName);
            }
        }

        public static void PrintTestName(string testName)
        {
            lock (_lockObject)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.WriteLine(" * {0}", testName);
            }
        }

        public static void PrintAssertResult(string assert)
        {
            lock (_lockObject)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("     - {0}", assert);
            }
        }

        public static void TestPassed()
        {
            lock (_lockObject)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("       Passed");
                Console.WriteLine();
            }
        }
    }
}
