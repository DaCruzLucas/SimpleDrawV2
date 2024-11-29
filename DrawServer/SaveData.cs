using DrawingLibrary;

namespace DrawServer
{
    public class SaveData
    {
        public int Id { get; set; }
        public Dictionary<int, DTOShape>? Shapes { get; set; }

        public SaveData(int id, Dictionary<int, DTOShape> shapes)
        {
            Id = id;
            Shapes = shapes;
        }
    }
}