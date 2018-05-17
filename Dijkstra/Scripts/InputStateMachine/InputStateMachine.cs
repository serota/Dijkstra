using System;
using System.IO;

namespace Dijkstra {
	public class InputStateMachine {
		private IInputState _currentState;
		private InputParser _parser;

		private const int ALLOWED_ATTEMPTS = 5;

		public InputStateMachine() {
			this._currentState = new VerboseModeState();
			this._parser = new InputParser(ALLOWED_ATTEMPTS);
		}

		public GraphDefinition BuildGraphDefinition() {
			GraphDefinition definition = new GraphDefinition();

			while (this._currentState != null) {
				try {
					this._currentState.Process(this._parser, definition);
				}
				catch (IOException e) {
					Console.WriteLine(e.Message + "  Exiting.");
					Environment.Exit(0);
				}

				this._currentState = this._currentState.GetNextState();
			}

			return definition;
		}
	}
}

