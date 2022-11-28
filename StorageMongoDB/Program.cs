using MongoDB.Driver;
using StorageMongoDB.Data;
using StorageMongoDB.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionStringMongoDb = builder.Configuration["DatabaseOptions:ConnectionStrings:MongoDB"];
string mongoDbName = builder.Configuration["DatabaseOptions:DataBaseName"];

var mongoClient = new MongoClient(connectionStringMongoDb);
var mongoCollection = mongoClient.GetDatabase(mongoDbName);

builder.Services.AddSingleton(mongoClient.GetDatabase(mongoDbName));


builder.Services.AddScoped<IRepoFile, RepoFile>();
builder.Services.AddScoped<IFileManager, FileManager>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
