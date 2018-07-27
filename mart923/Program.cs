using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace mart923
{
    class Program
    {

        public static IEnumerable<(int, string)> Frequencies(string[] word, int k)
        {
            var frs = word
                .GroupBy(s => s.ToUpper())          // convert all keys to uppercase
                .Select(g => (g.Count(), g.Key))    // get a tupple consisting of frequencies and key
                .OrderByDescending(kc => kc.Item1)  // sort frequencies by descending order
                .ThenBy(a => a.Item2)               // sort keys by aphabetical order
                .Take(k);
            return frs;

        }
        public static void Main(String[] args)
        {
            int k;
            try
            {
                string[] arguments = Environment.GetCommandLineArgs();
                var input = File.ReadAllText(arguments[1]);
                Console.WriteLine("Frequency:");
                Regex rgx = new Regex("[^A-Za-z]");
                var text = rgx.Replace(input, " ");
                Regex rgx1 = new Regex("\\s+");
                var words = rgx1.Replace(text, " ").Trim().Split(' ');
                try
                {
                    k = int.Parse(arguments[2]);
                    var frs = Frequencies(words, k);
                    //Array.ForEach(words, w => Console.WriteLine(w));
                    foreach ((int, string) f in frs)
                    {
                        Console.WriteLine("{0} {1}", f.Item1, f.Item2);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"^*** Error: (ex )", e);
                    Environment.ExitCode = 1;
                    k = 3;
                    var frs = Frequencies(words, k);
                    //Array.ForEach(words, w => Console.WriteLine(w));
                    foreach ((int, string) f in frs)
                    {
                        Console.WriteLine("{0} {1}", f.Item1, f.Item2);
                    }
                }
                finally
                {
                    //Console.WriteLine(frs);
                    Console.ReadKey();
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine($"^*** Error: (ex Message)", e);
                Environment.ExitCode = 1;
            }
        }
    }
}