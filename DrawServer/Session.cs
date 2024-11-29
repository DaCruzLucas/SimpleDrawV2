using System.Drawing;

namespace DrawServer
{
    public class Session
    {
        public readonly string ConnectionId;

        public Stack<DrawAction> undo = new Stack<DrawAction>();
        public Stack<DrawAction> redo = new Stack<DrawAction>();

        public Point MousePos;

        public Session(string connectionId)
        {
            ConnectionId = connectionId;
        }

        public void ClearStacks()
        {
            undo.Clear();
            redo.Clear();
        }

        public void AddNewUndoAction(DrawAction action)
        {
            undo.Push(action);
            redo.Clear();
        }

        public void AddUndoAction(DrawAction action)
        {
            undo.Push(action);
        }

        public void AddRedoAction(DrawAction action)
        {
            redo.Push(action);
        }

        public DrawAction? PopUndoAction()
        {
            return undo.Count > 0 ? undo.Pop() : null;
        }

        public DrawAction? PopRedoAction()
        {
            return redo.Count > 0 ? redo.Pop() : null;
        }
    }
}