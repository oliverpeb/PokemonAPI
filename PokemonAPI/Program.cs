using Microsoft.EntityFrameworkCore;
using PokemonAPI;
using PokemonAPI.Context;
using PokemonAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.WithOrigins("http://minfedeside.dk")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

bool useSql = true;
if (useSql)
{
    var optionsBuilder = new DbContextOptionsBuilder<PokemonContext>();
    optionsBuilder.UseSqlServer(Secrets.ConnectionString);
    PokemonContext context = new PokemonContext(optionsBuilder.Options);
    builder.Services.AddSingleton<IPokemonsRepository>(new PokemonsRepositoryDB(context));

}
else
{
builder.Services.AddSingleton<IPokemonsRepository>(new PokemonsRepository());
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// Configure the HTTP request pipeline.
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();



app.Run();
