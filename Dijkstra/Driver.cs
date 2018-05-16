using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra {
    class Driver {
        const String V_PROMPT = "Verbose Mode? [y/N]\n> ";
        const String V_RETRY = "Please answer yes or no.";

        const String N_PROMPT = "Please enter a number of nodes to generate.\n> ";
        const String N_RETRY = "The number of nodes must be a positive integer.";

        const String W_PROMPT = "Please enter the maximum possible edge weight.\n> ";
        const String W_RETRY = "The maximum weight must be a positive integer.";

        const String P_PROMPT = "Please enter the probability of each possible edge existing.\n> ";
        const String P_RETRY = "The probability must be between 0 and 1.";

        const String S_PROMPT = "Please enter the ID of the start node.\n> ";
        const String S_RETRY = "That node doesn't exist.";

        const String F_PROMPT = "Please enter the ID of the finish node.\n> ";
        const String F_RETRY = "That node doesn't exist.";

        const String INT_RETRY = "Input must be an integer.";
        const String DOUBLE_RETRY = "Input must be a decimal number.";
        const String BELLIGERENT = "User can't follow directions.";

        const int ALLOWANCE = 5;
        static int attempt = 0;

        static bool v = false;
        static int n = 0,
            w = 0,
            s = 0,
            f = 0;
        static double p = 0;

        static void Main(String[] args) {
            try {
                v = VerbosePrompt();
            }
            catch (IOException e) {
                Console.WriteLine(e.Message + "  Exiting.");
                Environment.Exit(0);
            }

            try {
                CollectGraphInputs();
            }
            catch (IOException e) {
                Console.WriteLine(e.Message + "  Exiting.");
                Environment.Exit(0);
            }

            WeightedGraph graph = WeightedGraph.GenerateRandomWeightedGraph(n, w, p);
            graph.Verbose = v;

            Console.WriteLine(graph);

            try {
                CollectPathInputs();
            }
            catch (IOException e) {
                Console.WriteLine(e.Message + "  Exiting.");
                Environment.Exit(0);
            }

            Path path = graph.ShortestPath(s, f);

            Console.WriteLine();

            if (path.Nodes.Count == 0) {
                Console.WriteLine($"No path exists from {s} to {f}.");
            }
            else {
                Console.WriteLine($"The shortest path from {s} to {f} is: \n{path}");
            }

            Console.ReadLine();
        }

        static void CollectPathInputs() {
            attempt = 0;
            do {
                RetriesCheck(S_RETRY);
                NextInt(S_PROMPT, out s);
                attempt++;
            } while (s < 0 || s >= n);

            attempt = 0;
            do {
                RetriesCheck(F_RETRY);
                NextInt(F_PROMPT, out f);
                attempt++;
            } while (f < 0 || f >= n);
        }

        static void CollectGraphInputs() {
            attempt = 0;
            do {
                RetriesCheck(N_RETRY);
                NextInt(N_PROMPT, out n);
                attempt++;
            } while (n <= 0);

            attempt = 0;
            do {
                RetriesCheck(W_RETRY);
                NextInt(W_PROMPT, out w);
                attempt++;
            } while (w <= 0);

            attempt = 0;
            do {
                RetriesCheck(P_RETRY);
                NextDouble(P_PROMPT, out p);
                attempt++;
            } while (p < 0 || p > 1);
        }

        static bool VerbosePrompt() {
            attempt = 0;
            while (true) {
                RetriesCheck(V_RETRY);

                Console.Write(V_PROMPT);
                string answer = Console.ReadLine();

                try {
                    return YNParse(answer);
                } catch (FormatException e) { }

                attempt++;
            }
        }

        static bool YNParse(string s) {
            string[] affirmative = { "y", "yes", "t", "true", "on", "1" };
            string[] negative = { "", "n", "no", "f", "false", "off", "0" };

            string sanitized = s.ToLower().Trim();

            if (affirmative.Contains(sanitized)) {
                return true;
            }
            else if (negative.Contains(sanitized)) {
                return false;
            }
            else {
                throw new FormatException("String was not recognized as a valid y/n answer.");
            }
        }

        static void RetriesCheck(String notice) {
            if (attempt >= ALLOWANCE) {
                throw new IOException(BELLIGERENT);
            }

            if (attempt > 0)
                Console.WriteLine(notice);
        }

        static void NextInt(String prompt, out int input) {
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out input)) {
                if (attempt >= ALLOWANCE) {
                    throw new IOException(BELLIGERENT);
                }
                Console.WriteLine();
                Console.WriteLine(INT_RETRY);
                Console.Write(prompt);
                attempt++;
            }
            Console.WriteLine();
        }

        static void NextDouble(String prompt, out double input) {
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out input)) {
                if (attempt >= ALLOWANCE) {
                    throw new IOException(BELLIGERENT);
                }
                Console.WriteLine();
                Console.WriteLine(DOUBLE_RETRY);
                Console.Write(prompt);
                attempt++;
            }
            Console.WriteLine();
        }
    }
}
