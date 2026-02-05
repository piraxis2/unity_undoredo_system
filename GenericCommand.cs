using System;

namespace UndoRedoSystem
{
    public class GenericCommand : ICommand
    {
        private Action _executeAction;
        private Action _undoAction;

        public GenericCommand(Action executeAction, Action undoAction)
        {
            _executeAction = executeAction;
            _undoAction = undoAction;
        }

        public void Execute()
        {
            _executeAction?.Invoke();
        }

        public void Undo()
        {
            _undoAction?.Invoke();
        }
    }
}