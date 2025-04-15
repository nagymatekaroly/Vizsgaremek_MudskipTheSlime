using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudskipApi;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🔧 HttpClient beállítása a sütik kezeléséhez


await builder.Build().RunAsync();

// 🔧 FetchWithCredentialsHandler definiálása
public class FetchWithCredentialsHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // ❗️Fontos: ezt nem a C# oldalon fogja használni, hanem a JS fetch API fogja lekezelni
        throw new NotImplementedException("Use JS interop or configure fetch() via Blazor extensions for withCredentials support.");
    }
}
