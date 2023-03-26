using Ppt.Shered.ViewModels;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
List<VybaveniVM> seznam = VybaveniVM.VratRandSeznam();

app.MapGet("/vybaveni", () =>
{
    return seznam;
});

app.MapPost("/vybaveni", (VybaveniVM prichoziModel) =>
{
    seznam.Insert(0, prichoziModel);
});





app.Run();

