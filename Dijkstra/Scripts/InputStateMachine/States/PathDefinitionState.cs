using System;

namespace Dijkstra {
	public class PathDefinitionState : IInputState {
		const string S_PROMPT = "Please enter the ID of the start node.\n> ";
		const string S_RETRY = "That node doesn't exist.";

		const string F_PROMPT = "Please enter the ID of the finish node.\n> ";
		const string F_RETRY = "That node doesn't exist.";

		#region IInputState implementation
		public void Process(InputParser parser, GraphDefinition definition) {
			definition.StartNode = parser.GetNextInt(S_PROMPT, S_RETRY, 0, definition.NodeCount - 1);
			definition.EndNode = parser.GetNextInt(F_PROMPT, F_RETRY, 0, definition.NodeCount - 1);
		}

		public IInputState GetNextState() {
			return null;
		}
		#endregion
	}
}

