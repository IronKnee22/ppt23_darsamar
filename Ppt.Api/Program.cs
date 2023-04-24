using Ppt.Shared;
using Ppt.Shered.ViewModels;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins(builder.Configuration["AllowedOrigins"])
    .WithMethods("GET","DELETE","PUT","POST") /*použité endpoity*/
    .AllowAnyHeader()
));

var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
List<VybaveniVM> SeznamVybaveni = VybaveniVM.VratRandSeznam();  //základní gerenování seznamu
List<RevizeVM> SeznamRevizi = RevizeVM.VratRandSeznam(100);

app.MapGet("/vybaveni", () =>
{
    return SeznamVybaveni;
});

app.MapPost("/vybaveni", (VybaveniVM prichoziModel) => /*nové vybavení*/
{
    prichoziModel.Id = Guid.NewGuid();
    SeznamVybaveni.Insert(0, prichoziModel);
});

app.MapDelete("/vybaveni/{Id}", (Guid Id) =>    /*smazání vybavení*/
{
    var vybranyModel = SeznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
        return Results.NotFound("Položka nalezena");
    SeznamVybaveni.Remove(vybranyModel);
    return Results.Ok();
}
);

app.MapPut("/vybaveni/{Id}", (VybaveniVM upravenyModel, Guid Id) => /*update vybavení*/
{
    var vybranyModel = SeznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
    {
        return Results.NotFound("Položka nalezena");
    }

    else
    { 
        int index = SeznamVybaveni.IndexOf(vybranyModel);

        SeznamVybaveni.Remove(vybranyModel);
        SeznamVybaveni.Insert(index, upravenyModel);

        return Results.Ok(upravenyModel);
    }
});

app.MapGet("/vybaveni/{Id}", (Guid Id) =>   /*Pomocí ID získán jedno vybavení*/
{
    VybaveniVM? nalezeny = SeznamVybaveni.SingleOrDefault(x => x.Id == Id);
    return nalezeny;
});

app.MapGet("/revize/{text}", (string text) =>
{
    var filtrovaneRevize = SeznamRevizi.Where(x => x.Name.Contains(text)).ToList();
    return Results.Ok(filtrovaneRevize);
});

app.Run();

