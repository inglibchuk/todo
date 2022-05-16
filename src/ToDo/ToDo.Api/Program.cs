using ToDo.Core;
using ToDo.Data;
using ToDo.Service;
using ToDo.Service.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton(typeof(IRepository<>), typeof(JustInMemoryRepository<>));
builder.Services.AddSingleton<IRepository<TodoTask>, TodoTaskRepository>();
builder.Services.AddSingleton<IRepository<Taxonomy>, TaxonomyRepository>();
builder.Services.AddScoped<ITaskValidationService, TaskValidationService>();
builder.Services.AddScoped<ITodoListService, TodoListService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
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

app.UsePathBase(new PathString("/api"));
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200"));
app.UseAuthorization();

app.MapControllers();


app.Run();
