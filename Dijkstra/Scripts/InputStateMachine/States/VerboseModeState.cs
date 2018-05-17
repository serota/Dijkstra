using System;
using System.IO;

namespace Dijkstra {
	public class VerboseModeState : IInputState {
		private const string V_PROMPT = "Verbose Mode? [y/N]\n> ";
		private const string YN_RETRY = "Input must be a yes or no answer.";

		#region IInputState implementation
		public void Process(InputParser parser, GraphDefinition definition) {
			definition.IsVerbose = parser.GetNextYN(V_PROMPT, YN_RETRY);
		}

		public IInputState GetNextState() {
			return new GraphDefinitionState();
		}
		#endregion
	}
}
