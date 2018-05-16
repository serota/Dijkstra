using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra {
    class Driver {
        const string V_PROMPT = "Verbose Mode? [y/N]\n> ";

        const string N_PROMPT = "Please enter a number of nodes to generate.\n> ";
        const string N_RETRY = "The number of nodes must be a positive integer.";

        const string W_PROMPT = "Please enter the maximum possible edge weight.\n> ";
        const string W_RETRY = "The maximum weight must be a positive integer.";

        const string P_PROMPT = "Please enter the probability of each possible edge existing.\n> ";
        const string P_RETRY = "The probability must be between 0 and 1.";

        const string S_PROMPT = "Please enter the ID of the start node.\n> ";
        const string S_RETRY = "That node doesn't exist.";

        const string F_PROMPT = "Please enter the ID of the finish node.\n> ";
        const string F_RETRY = "That node doesn't exist.";

        const string YN_RETRY = "Input must be a yes or no answer.";
        const string INT_RETRY = "Input must be an integer.";
        const string DOUBLE_RETRY = "Input must be a decimal number.";
        const string BELLIGERENT = "User can't follow directions.";

        const int ALLOWANCE = 5;
        static int attempt = 0;

        static bool v = false;
        static int n = 0,
            w = 0,
            s = 0,
            f = 0;
        static double p = 0;

        static void Main(string[] args) {
            try {
                v = NextYN(V_PROMPT);
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
                s = NextInt(S_PROMPT);
                attempt++;
            } while (s < 0 || s >= n);

            attempt = 0;
            do {
                RetriesCheck(F_RETRY);
                f = NextInt(F_PROMPT);
                attempt++;
            } while (f < 0 || f >= n);
        }

        static void CollectGraphInputs() {
            attempt = 0;
            do {
                RetriesCheck(N_RETRY);
                n = NextInt(N_PROMPT);
                attempt++;
            } while (n <= 0);

            attempt = 0;
            do {
                RetriesCheck(W_RETRY);
                w = NextInt(W_PROMPT);
                attempt++;
            } while (w <= 0);

            attempt = 0;
            do {
                RetriesCheck(P_RETRY);
                p = NextDouble(P_PROMPT);
                attempt++;
            } while (p < 0 || p > 1);
        }

        static void RetriesCheck(string notice) {
            if (attempt >= ALLOWANCE) {
                throw new IOException(BELLIGERENT);
            }

            if (attempt > 0)
                Console.WriteLine(notice);
        }

        static bool NextYN(string prompt) {
            bool output = false;

            Console.Write(prompt);
            while (!YNParse(Console.ReadLine(), out output)) {
                if (attempt >= ALLOWANCE) {
                    throw new IOException(BELLIGERENT);
                }

                Console.WriteLine();
                Console.WriteLine(YN_RETRY);
                Console.Write(prompt);
                attempt++;
            }
            Console.WriteLine();

            return output;
        }

        static int NextInt(string prompt) {
            int output = 0;

            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out output)) {
                if (attempt >= ALLOWANCE) {
                    throw new IOException(BELLIGERENT);
                }
                Console.WriteLine();
                Console.WriteLine(INT_RETRY);
                Console.Write(prompt);
                attempt++;
            }
            Console.WriteLine();

            return output;
        }

        static double NextDouble(string prompt) {
            double output = 0;

            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out output)) {
                if (attempt >= ALLOWANCE) {
                    throw new IOException(BELLIGERENT);
                }
                Console.WriteLine();
                Console.WriteLine(DOUBLE_RETRY);
                Console.Write(prompt);
                attempt++;
            }
            Console.WriteLine();

            return output;
        }

        static bool YNParse(string s, out bool b) {
            string[] affirmative = { "y", "yes", "t", "true", "on", "1" };
            string[] negative = { "", "n", "no", "f", "false", "off", "0" };

            string sanitized = s.ToLower().Trim();

            if (affirmative.Contains(sanitized)) {
                b = true;
                return true;
            }
            else if (negative.Contains(sanitized)) {
                b = false;
                return true;
            }
            else {
                b = false;
                return false;
            }
        }
    }
}
