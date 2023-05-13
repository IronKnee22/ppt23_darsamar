using Mapster;
using Microsoft.EntityFrameworkCore;
using Ppt.Api.Data;
using Ppt.Shared;
using Ppt.Shered.ViewModels;


var builder = WebApplication.CreateBuilder(args);
var corsAllowedOrigin = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SeedingData>();



builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins(corsAllowedOrigin)//👈
    .WithMethods("GET", "DELETE", "POST", "PUT")//👈 (musí být UPPERCASE)
    .AllowAnyHeader()
));

string? sqliteDbPath = builder.Configuration[nameof(sqliteDbPath)];

builder.Services.AddDbContext<PptDbContext>(opt => opt.UseSqlite($"FileName={sqliteDbPath}"));

var app = builder.Build();
app.UseCors();

app.Services.CreateScope().ServiceProvider.GetRequiredService<PptDbContext>().Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/vybaveni", ( PptDbContext db) =>
{
    Console.WriteLine(  $"Pocet vybavani v db: {db.Vybavenis.Count()}");
    db.Vybavenis.Select(x=>x.Adapt<VybaveniVM>()).ToList();
    return db.Vybavenis;
    
});

app.MapPost("/vybaveni", (VybaveniVM prichoziModel, PptDbContext db) => /*nové vybavení*/
{


    prichoziModel.Id = Guid.Empty;

    var en = prichoziModel.Adapt<Vybaveni>();

    //přidat do db.Vybavenis

    db.Vybavenis.Add(en);
    //uložit db (db.Save)

    db.SaveChanges();

    return en.Id;
});

app.MapDelete("/vybaveni/{Id}", (Guid Id, PptDbContext db) =>    /*smazání vybavení*/
{
    var vybranyModel = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
        return Results.NotFound("Položka nalezena");
    db.Vybavenis.Remove(vybranyModel);
    db.SaveChanges();

    return Results.Ok();
}
);

app.MapPut("/vybaveni/{Id}", (Vybaveni vyb, Guid Id, PptDbContext db) => /*update vybavení*/
{
    var vybranyModel = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
    {
        return Results.NotFound("Položka nalezena");
    }

    else
    {
        vyb.Id = Id;
        db.Vybavenis.Entry(vybranyModel).CurrentValues.SetValues(vyb);
        db.SaveChanges();
        return Results.Ok();
    }
});

app.MapGet("/vybaveni/{Id}", (Guid Id, PptDbContext db) =>   /*Pomocí ID získán jedno vybavení*/
{
    var nalezeny = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    return nalezeny;
});

app.MapGet("/revize/{text}", (string text, PptDbContext db ) => /*Bylo přidáno Ahoj jako testovaci*/
{
    var Revize = db.Revizes.ToList();
    var odpovidajiciRevize = Revize.Where(r => r.Name.Contains(text)).Adapt<List<RevizeVM>>();
    return odpovidajiciRevize;
   
});

await app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingData>().SeedData();

app.Run();

