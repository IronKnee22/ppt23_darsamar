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

app.Services.CreateScope().ServiceProvider
  .GetRequiredService<PptDbContext>()
  .Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/vybaveni", (PptDbContext db) =>
{
    Console.WriteLine($"Pocet vybavani v db: {db.Vybavenis.Count()}");

    var vybaveni = db.Vybavenis
        .Select(x => x.Adapt<VybaveniVM>())
        .ToList();

    foreach (var v in vybaveni)
    {
        var nejnovejsiRevize = db.Revizes
            .Where(r => r.VybaveniId == v.Id)
            .OrderByDescending(r => r.DateTime)
            .FirstOrDefault();

        if (nejnovejsiRevize != null)
        {
            v.LastRevision = nejnovejsiRevize.DateTime;
        }
    }

    return vybaveni;
});

app.MapPost("/vybaveni", (VybaveniVM prichoziModel, PptDbContext db) => /*Nové vybavení*/
{
    

    var en = prichoziModel.Adapt<Vybaveni>();
    
    var novaRevize = new Revize()
    {
        DateTime = prichoziModel.LastRevision, 
        VybaveniId = en.Id,
        Name = en.Name,
        
    };
    
    prichoziModel.Id = Guid.Empty;
    db.Vybavenis.Add(en);
    db.SaveChanges();

    db.Revizes.Add(novaRevize);
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

app.MapPut("/vybaveni/{Id}", (VybaveniVM vyb, Guid Id, PptDbContext db) => /*update vybavení*/
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
        var novaRevize = new Revize
        {
            DateTime = vyb.LastRevision,
            VybaveniId = vyb.Id,
            Name = vyb.Name,
            
        };
        db.SaveChanges();

        db.Revizes.Add(novaRevize);
        db.SaveChanges();
        return Results.Ok();
    }
});

app.MapGet("/vybaveni/{Id}", (Guid Id, PptDbContext db) =>   /*Pomocí ID získán jedno vybavení a všechno co kněmu patří*/
{
    var nalezeny = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    return nalezeny;

});

app.MapGet("{Id}", (Guid Id, PptDbContext db) =>   /*Pomocí ID získáme z tabulky revizes všechny revize*/
{
    var nalezeny = db.Revizes.Where(r => r.VybaveniId == Id).ToList();
    return nalezeny;

});

app.MapGet("/revize", ( PptDbContext db ) => 
{
    var Revize = db.Revizes.ToList();
    
    return Revize;
   
});

await app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingData>().SeedData();

app.Run();

