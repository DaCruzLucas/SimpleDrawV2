using DrawingLibrary;

namespace DrawServer
{
    public class DrawAction
    {
        public enum ActionType { Add, Remove, Modify, Color }

        public ActionType Type { get; }
        public DTOShape Shape { get; }
        public DTOShape? OldShape { get; }

        public DrawAction(ActionType type, DTOShape shape, DTOShape? oldShape = null)
        {
            Type = type;
            Shape = shape;
            OldShape = oldShape;
        }
    }
}
