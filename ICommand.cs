namespace UndoRedoSystem
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}