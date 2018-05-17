using System;
using System.Linq;
using System.IO;

namespace Dijkstra {
	public class InputParser {
		private const string BELLIGERENT = "User can't follow directions.";
		const string YN_RETRY = "Input must be a yes or no answer.";
		const string INT_RETRY = "Input must be an integer.";
		const string DOUBLE_RETRY = "Input must be a decimal number.";

		private int _allowedAttempts;

		public InputParser(int allowedAttempts) {
			this._allowedAttempts = allowedAttempts;
		}

		public bool GetNextYN(string prompt, string retry) {
			bool output = false;
			int attempt = 0;

			Console.Write(prompt);
			while (!YN_TryParse(Console.ReadLine(), out output)) {
				if (attempt >= this._allowedAttempts) {
					throw new IOException(BELLIGERENT);
				}

				Console.WriteLine();
				Console.WriteLine(retry);
				Console.Write(prompt);
				attempt++;
			}
			Console.WriteLine();

			return output;
		}

		public int GetNextInt(string prompt, string retry, int minValue = int.MinValue, int maxValue = int.MaxValue) {
			int output = 0;
			int attempt = 0;

			Console.Write(prompt);
			while (true) {
				bool invalidFormat = !int.TryParse(Console.ReadLine(), out output);
				bool outOfBounds = (output < minValue || output > maxValue);

				if (!invalidFormat && !outOfBounds) break;
				if (attempt >= this._allowedAttempts) throw new IOException(BELLIGERENT);

				Console.WriteLine();
				Console.WriteLine(invalidFormat ? INT_RETRY : retry);
				Console.Write(prompt);

				attempt++;
			}
			Console.WriteLine();

			return output;
		}

		public double GetNextDouble(string prompt, string retry, double minValue = double.MinValue, double maxValue = double.MaxValue) {
			double output = 0;
			int attempt = 0;

			Console.Write(prompt);
			while (true) {
				bool invalidFormat = !double.TryParse(Console.ReadLine(), out output);
				bool outOfBounds = (output < minValue || output > maxValue);

				if (!invalidFormat && !outOfBounds) break;
				if (attempt >= this._allowedAttempts) throw new IOException(BELLIGERENT);

				Console.WriteLine();
				Console.WriteLine(invalidFormat ? DOUBLE_RETRY : retry);
				Console.Write(prompt);
				attempt++;
			}
			Console.WriteLine();

			return output;
		}

		#region Helper
		private bool YN_TryParse(string s, out bool b) {
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
		#endregion
	}
}

