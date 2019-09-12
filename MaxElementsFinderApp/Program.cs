namespace MaxElementsFinderApp
{
    using Business;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string filePath = args[0];

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    ParseService.RunParseFile(filePath);
                }
            }
            else
            {
                Console.WriteLine("Please, specify the file you want to parse to find a line with a maximum number of elements.");

                string filePath = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    ParseService.RunParseFile(filePath);
                }
            }
        }
    }
}
