using System;

namespace Dijkstra {
	public class GraphDefinitionState : IInputState {
		private const string N_PROMPT = "Please enter a number of nodes to generate.\n> ";
		private const string N_RETRY = "The number of nodes must be a positive integer.";

		private const string W_PROMPT = "Please enter the maximum possible edge weight.\n> ";
		private const string W_RETRY = "The maximum weight must be a positive integer.";

		private const string P_PROMPT = "Please enter the probability of each possible edge existing.\n> ";
		private const string P_RETRY = "The probability must be between 0 and 1.";

		#region IInputState implementation
		public void Process(InputParser parser, GraphDefinition definition) {
			definition.NodeCount = parser.GetNextInt(N_PROMPT, N_RETRY, 1);
			definition.EdgeWeight = parser.GetNextInt(W_PROMPT, W_RETRY, 1);
			definition.EdgeProbability = parser.GetNextDouble(P_PROMPT, P_RETRY, 0, 1);
		}

		public IInputState GetNextState() {
			return new PathDefinitionState();
		}
		#endregion
	}
}

