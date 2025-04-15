using System.Net;


namespace MudskipApp;

public static class ApiSession
{
    public static readonly CookieContainer Cookies = new();
    public static readonly HttpClient Client = new(new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = Cookies
    });
}
