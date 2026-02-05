using System.Collections.Generic;
using UnityEngine;

namespace UndoRedoSystem
{
    /// <summary>
    /// Manages the Undo/Redo history.
    /// Can be instantiated multiple times to create separate Undo/Redo contexts.
    /// </summary>
    public class UndoRedoHelper
    {
        // LinkedList allows efficient removal from both ends (Deque behavior)
        private LinkedList<ICommand> _undoList = new LinkedList<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();

        private int _maxHistorySize;

        public UndoRedoHelper(int maxHistorySize = 50)
        {
            _maxHistorySize = maxHistorySize;
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoList.AddLast(command);
            _redoStack.Clear();

            if (_undoList.Count > _maxHistorySize)
            {
                _undoList.RemoveFirst(); // Remove oldest
            }
        }

        public TransactionBuilder CreateTransaction(string name = "Transaction")
        {
            return new TransactionBuilder(this, name);
        }

        public void Undo()
        {
            if (_undoList.Count > 0)
            {
                ICommand command = _undoList.Last.Value;
                _undoList.RemoveLast();
                command.Undo();
                _redoStack.Push(command);
            }
            else
            {
                Debug.LogWarning("UndoRedoHelper: Nothing to undo.");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                ICommand command = _redoStack.Pop();
                command.Execute();
                _undoList.AddLast(command);
            }
            else
            {
                Debug.LogWarning("UndoRedoHelper: Nothing to redo.");
            }
        }

        public void ClearHistory()
        {
            _undoList.Clear();
            _redoStack.Clear();
        }
        
        public bool CanUndo => _undoList.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;
    }
}