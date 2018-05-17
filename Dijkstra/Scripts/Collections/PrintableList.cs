using System;
using System.Collections.Generic;

namespace Dijkstra {
	public class PrintableList<T> : List<T> {
		public override string ToString() {
			char[] trimmings = { ',', ' ' };
			string output = "{";

			foreach (Object obj in this) {
				output += $"{obj.ToString()}, ";
			}

			output = output.TrimEnd(trimmings) + "}";
			return output;
		}
	}
}

