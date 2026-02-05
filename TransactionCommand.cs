using System;
using System.Collections.Generic;

namespace UndoRedoSystem
{
    /// <summary>
    /// A command that groups multiple Do and Undo actions, similar to Godot's UndoRedo actions.
    /// </summary>
    public class TransactionCommand : ICommand
    {
        private List<Action> _doActions = new List<Action>();
        private List<Action> _undoActions = new List<Action>();
        public string Name { get; private set; }

        public TransactionCommand(string name)
        {
            Name = name;
        }

        public void AddDo(Action action)
        {
            _doActions.Add(action);
        }

        public void AddUndo(Action action)
        {
            _undoActions.Add(action);
        }

        public void Execute()
        {
            foreach (var action in _doActions)
            {
                action.Invoke();
            }
        }

        public void Undo()
        {
            // Execute undo actions in reverse order of how they were added
            for (int i = _undoActions.Count - 1; i >= 0; i--)
            {
                _undoActions[i].Invoke();
            }
        }
    }
}