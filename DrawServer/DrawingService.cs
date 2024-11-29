using DrawingLibrary;
using System.Drawing;
using System.Text.Json;

namespace DrawServer
{
    public class DrawingService
    {
        public Dictionary<int, DTOShape> Shapes = new Dictionary<int, DTOShape>();

        public Dictionary<string, Session> Users = new Dictionary<string, Session>();

        private int Id = 0;

        public DrawingService() { }

        // SESSIONS
        public void AddUser(string connectionId)
        {
            Users.Add(connectionId, new Session(connectionId));
        }
        public void RemoveUser(string connectionId)
        {
            Users.Remove(connectionId);
        }

        public void UpdateMousePos(string connectionId, int x, int y)
        {
            if (Users.ContainsKey(connectionId))
            {
                Users[connectionId].MousePos = new Point(x, y);
            }
        }

        public void AddNewUndoAction(string connectionId, DrawAction action)
        {
            Users[connectionId].AddNewUndoAction(action);
        }
        public void AddUndoAction(string connectionId, DrawAction action)
        {
            Users[connectionId].AddUndoAction(action);
        }
        public void AddRedoAction(string connectionId, DrawAction action)
        {
            Users[connectionId].AddRedoAction(action);
        }

        public DrawAction? UndoLastAction(string connectionId)
        {
            return Users[connectionId].PopUndoAction();
        }
        public DrawAction? RedoLastAction(string connectionId)
        {
            return Users[connectionId].PopRedoAction();
        }

        // SHAPES
        public void SaveJSON()
        {
            var data = new SaveData(Id, Shapes);

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("data.json", json);
        }
        public void LoadJSON()
        {
            if (File.Exists("data.json"))
            {
                var json = File.ReadAllText("data.json");
                var data = JsonSerializer.Deserialize<SaveData>(json);

                if (data != null && data.Shapes != null)
                {
                    Shapes.Clear();

                    Id = data.Id;
                    Shapes = data.Shapes;

                    foreach (var user in Users.Values)
                    {
                        user.ClearStacks();
                    }
                }
            }
        }

        public int GetNextId()
        {
            Id++;
            return Id;
        }

        public void AddShape(DTOShape dto)
        {
            Shapes[dto.Id] = dto;
            //Shapes.Add(dto.Id, dto);
        }
        public void RemoveShape(int id)
        {
            Shapes.Remove(id);
        }
        public void ModifyShape(DTOShape dto)
        {
            Shapes[dto.Id] = dto;
        }

        public List<DTOShape> GetShapes()
        {
            return Shapes.Values.ToList();
        }
    }
}
