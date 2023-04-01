using Ppt.Shered.ViewModels;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//někde za definicí proměnné app




builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins("https://localhost:1111")
    .WithMethods("GET","DELETE")
    .AllowAnyHeader()
));
var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
List<VybaveniVM> SeznamVybaveni = VybaveniVM.VratRandSeznam();

app.MapGet("/vybaveni", () =>
{
    return SeznamVybaveni;
});

app.MapPost("/vybaveni", (VybaveniVM prichoziModel) =>
{
    prichoziModel.Id = Guid.NewGuid();
    SeznamVybaveni.Insert(0, prichoziModel);
});

app.MapDelete("/vybaveni/{Id}", (Guid Id) =>
{
    var vybranyModel = SeznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
        return Results.NotFound("Položka nalezena");
    SeznamVybaveni.Remove(vybranyModel);
    return Results.Ok();
}
);

app.MapPut("/vybaveni/{Id}", (VybaveniVM upravenyModel, Guid Id) =>
{
    var vybranyModel = SeznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
    {
        return Results.NotFound("Položka nalezena");
    }

    else
    {
        upravenyModel.Id = Id;
        int index = SeznamVybaveni.IndexOf(vybranyModel);

        SeznamVybaveni.Remove(vybranyModel);
        SeznamVybaveni.Insert(index, upravenyModel);

        return Results.Ok();
    }
});

app.MapGet("/vybaveni/{Id}", (Guid Id) =>
{
    VybaveniVM? nalezeny = SeznamVybaveni.SingleOrDefault(x => x.Id == Id);
    return nalezeny;
});

 




app.Run();

