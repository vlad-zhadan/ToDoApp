using ToDoApp.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using ToDoApp.BLL.Mapping.Task;
using ToDoApp.DAL.Repositories.Interfaces.Base;
using ToDoApp.DAL.Repositories.Realizations.Base;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<ToDoAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ToDoAppDb")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(currentAssemblies);
});

builder.Services.AddAutoMapper(typeof(TaskProfile));
builder.Services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
