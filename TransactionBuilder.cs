using System;

namespace UndoRedoSystem
{
    public class TransactionBuilder
    {
        private TransactionCommand _command;
        private UndoRedoHelper _helper;

        public TransactionBuilder(UndoRedoHelper helper, string name)
        {
            _helper = helper;
            _command = new TransactionCommand(name);
        }

        public TransactionBuilder AddDo(Action action)
        {
            _command.AddDo(action);
            return this;
        }

        public TransactionBuilder AddUndo(Action action)
        {
            _command.AddUndo(action);
            return this;
        }

        public void Commit()
        {
            _helper.ExecuteCommand(_command);
        }
    }
}