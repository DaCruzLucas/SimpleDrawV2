using DrawServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddSingleton<DrawingService>();
var app = builder.Build();

app.MapHub<DrawHub>("/draw");
app.Run();
