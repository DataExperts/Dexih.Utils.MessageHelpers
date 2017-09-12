using Dexih.Utils.MessageHelpers;
using System;
using System.Diagnostics;

namespace PerformanceComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            var iterations = 10000;

            var stopwatch = new Stopwatch();

            stopwatch.Reset();
            stopwatch.Start();
            for(int i = 0; i< iterations; i++)
            {
                var returnValue = ReturnValuePassedTest();
            }
            stopwatch.Stop();
            Console.WriteLine($"ReturnValuePassed: {stopwatch.ElapsedTicks}");

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var returnValue = ReturnValueFailedTest();
            }
            stopwatch.Stop();
            Console.WriteLine($"ReturnValueFailed: {stopwatch.ElapsedTicks}");

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                try
                {
                    var returnValue = ReturnExceptionTest();
                } catch(Exception)
                {

                }
            }
            stopwatch.Stop();
            Console.WriteLine($"ReturnValueException: {stopwatch.ElapsedTicks}");

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                try
                {
                    var returnValue = ReturnNoExceptionTest();
                }
                catch (Exception)
                {

                }
            }
            stopwatch.Stop();
            Console.WriteLine($"ReturnValueException: {stopwatch.ElapsedTicks}");

            Console.ReadKey();
        }

        public static ReturnValue<int> ReturnValueFailedTest()
        {
            return new ReturnValue<int>(false, "I failed", null);
        }

        public static ReturnValue<int> ReturnValuePassedTest()
        {
            return new ReturnValue<int>(true, 5);
        }

        public static int ReturnExceptionTest()
        {
            throw new Exception("I failed");
        }

        public static int ReturnNoExceptionTest()
        {
            return 5;
        }

    }
}
