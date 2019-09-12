namespace Business
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class ParseService
    {
        public static void RunParseFile(string file)
        {
            (int lineNumber, int[] badLines) = ParseFileAsync(file).Result;

            Console.WriteLine();
            Console.WriteLine($"Line with maximum number of elements is: {lineNumber}.");
            Console.WriteLine();
            Console.WriteLine($"Lines with non-numberic characters (\"Bad lines\"): {string.Join(",", badLines)}.");
            Console.ReadKey();
        }

        public static async Task<(int lineNumber, int[] badLines)> ParseFileAsync(string file)
        {
            List<Task> tasks = new List<Task>();

            int numberOfLineElements = 0;
            int lineWithMaxAmountOfElements = 0;

            Stopwatch stopwatch = Stopwatch.StartNew();

            ConcurrentQueue<int> badLinesConcurrentQueue = new ConcurrentQueue<int>();

            string[] lines = File.ReadAllLines(file);

            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    int lineIndex = i;

                    int lineNumber = i + 1;

                    tasks.Add(Task.Run(() =>
                    {
                        string[] lineSplit = lines[lineIndex].Split(",");

                        if (numberOfLineElements < lineSplit.Length)
                        {
                            numberOfLineElements = lineSplit.Length;

                            lineWithMaxAmountOfElements = lineNumber;
                        }

                        if (Regex.Matches(lines[lineIndex], @"[^0-9.,]\D+").Count > 0)
                        {
                            badLinesConcurrentQueue.Enqueue(lineNumber);
                        }
                    }));
                }

                await Task.WhenAll(tasks);
            }
            catch(Exception ex)
            {
                Console.Write($"Something went wrong on parse file: {ex.Message}.");
            }

            stopwatch.Stop();

            int[] badLines = badLinesConcurrentQueue.ToArray();

            Console.Write($"Time execution on parse file {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine();

            return (lineWithMaxAmountOfElements, badLines);
        }
    }
}
