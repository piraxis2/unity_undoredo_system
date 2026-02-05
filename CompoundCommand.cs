using System.Collections.Generic;

namespace UndoRedoSystem
{
    public class CompoundCommand : ICommand
    {
        private List<ICommand> _commands = new List<ICommand>();
        public string Name { get; private set; }

        public CompoundCommand(string name = "Compound Command")
        {
            Name = name;
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void Execute()
        {
            foreach (var cmd in _commands)
            {
                cmd.Execute();
            }
        }

        public void Undo()
        {
            // Undo in reverse order
            for (int i = _commands.Count - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }
    }
}