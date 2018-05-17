using System;

namespace Dijkstra {
	public interface IInputState {
		void Process(InputParser parser, GraphDefinition definition);
		IInputState GetNextState();
	}
}

