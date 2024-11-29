using DrawingLibrary;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using System.Numerics;
using System.Security.Cryptography;

namespace DrawServer
{
    public class DrawHub : Hub
    {
        private readonly DrawingService ds;

        public DrawHub(DrawingService drawingService)
        {
            ds = drawingService;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Client " + Context.ConnectionId + " connecté");

            ds.AddUser(Context.ConnectionId);

            List<DTOShape> shapes = ds.GetShapes();

            await Clients.Caller.SendAsync("ReceiveLoadShapes", shapes);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("Client " + Context.ConnectionId + " déconnecté");

            ds.RemoveUser(Context.ConnectionId);

            await Clients.All.SendAsync("ReceiveDisconnect", Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateMousePos(int x, int y)
        {
            ds.UpdateMousePos(Context.ConnectionId, x, y);

            await Clients.All.SendAsync("ReceiveUpdateMousePos", Context.ConnectionId, x, y);
        }

        public void SaveData()
        {
            Console.WriteLine("Save Shapes");

            ds.SaveJSON();
        }

        public async Task LoadData()
        {
            Console.WriteLine("Load Shapes");

            ds.LoadJSON();

            List<DTOShape> shapes = ds.GetShapes();

            await Clients.All.SendAsync("ReceiveLoadShapes", shapes);
        }

        public async Task PasteShape(DTOShape dto)
        {
            Console.WriteLine("Added Shape : " + dto.Id);

            ds.AddShape(dto);
            ds.AddNewUndoAction(Context.ConnectionId, new DrawAction(DrawAction.ActionType.Add, dto));

            await Clients.All.SendAsync("ReceiveAddShape", dto);
        }

        public async Task AddShape(DTOShape dto)
        {
            Console.WriteLine("Added Shape : " + dto.Id);

            ds.AddShape(dto);

            await Clients.All.SendAsync("ReceiveAddShape", dto);
        }

        public async Task RemoveShape(DTOShape dto)
        {
            Console.WriteLine("Removed Shape : " + dto.Id);

            ds.RemoveShape(dto.Id);
            ds.AddNewUndoAction(Context.ConnectionId, new DrawAction(DrawAction.ActionType.Remove, dto));

            await Clients.All.SendAsync("ReceiveRemoveShape", dto.Id);
        }

        public async Task ModifyShape(DTOShape dto)
        {
            ds.ModifyShape(dto);

            await Clients.All.SendAsync("ReceiveModifyShape", dto);
        }

        public void AddShapeOnRelease(DTOShape dto)
        {
            ds.AddNewUndoAction(Context.ConnectionId, new DrawAction(DrawAction.ActionType.Add, dto));
        }

        public async Task RemoveShapeOnRelease(DTOShape dto)
        {
            Console.WriteLine("Removed Shape : " + dto.Id);

            ds.RemoveShape(dto.Id);

            await Clients.All.SendAsync("ReceiveRemoveShape", dto.Id);
        }

        public void ModifyShapeOnRelease(DTOShape dto, DTOShape dto2)
        {
            ds.AddNewUndoAction(Context.ConnectionId, new DrawAction(DrawAction.ActionType.Modify, dto, dto2));
        }

        public async Task UndoAction()
        {
            try
            {
                DrawAction? action = ds.UndoLastAction(Context.ConnectionId);
                switch (action?.Type)
                {
                    case DrawAction.ActionType.Add:
                        ds.RemoveShape(action.Shape.Id);
                        ds.AddRedoAction(Context.ConnectionId, action);
                        await Clients.All.SendAsync("ReceiveRemoveShape", action.Shape.Id);
                        break;

                    case DrawAction.ActionType.Remove:
                        ds.AddShape(action.Shape);
                        ds.AddRedoAction(Context.ConnectionId, action);
                        await Clients.All.SendAsync("ReceiveAddShape", action.Shape);
                        break;

                    case DrawAction.ActionType.Modify:
                        if (action.OldShape != null)
                        {
                            ds.ModifyShape(action.OldShape);
                            ds.AddRedoAction(Context.ConnectionId, action);
                            await Clients.All.SendAsync("ReceiveModifyShape", action.OldShape);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DrawHub.UndoAction :" + e.Message);
                Console.ResetColor();
            }
        }

        public async Task RedoAction()
        {
            DrawAction? action = ds.RedoLastAction(Context.ConnectionId);
            switch (action?.Type)
            {
                case DrawAction.ActionType.Add:
                    ds.AddShape(action.Shape);
                    ds.AddUndoAction(Context.ConnectionId, action);
                    await Clients.All.SendAsync("ReceiveAddShape", action.Shape);
                    break;

                case DrawAction.ActionType.Remove:
                    ds.RemoveShape(action.Shape.Id);
                    ds.AddUndoAction(Context.ConnectionId, action);
                    await Clients.All.SendAsync("ReceiveRemoveShape", action.Shape.Id);
                    break;

                case DrawAction.ActionType.Modify:
                    ds.ModifyShape(action.Shape);
                    ds.AddUndoAction(Context.ConnectionId, action);
                    await Clients.All.SendAsync("ReceiveModifyShape", action.Shape);
                    break;
            }
        }

        public int GetNextId()
        {
            return ds.GetNextId();
        }
    }
}
