using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ppt23.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var apiUri = builder.Configuration["ApiUri"];
if (apiUri == null) throw new ArgumentNullException(nameof(apiUri));
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUri) });



await builder.Build().RunAsync();
