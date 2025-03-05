using Book2Glow.Service.Extension;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add controller for swagger 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var configuration = builder.Configuration;
string connexionString = configuration["ConnectionStrings:DataModelDevContext"];
builder.Services.AddServices(connexionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
