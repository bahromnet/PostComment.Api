using Microsoft.Net.Http.Headers;
using System.Security.Cryptography;

namespace PostComment.Api.Middlewares;

public class ETagMiddleware
{
    private readonly RequestDelegate _next;

    public ETagMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var response = context.Response;
        var originalStrem = response.Body;

        using (var ms = new MemoryStream())
        {
            response.Body = ms;
            await _next(context);

            if (IsEtagSupported(response))
            {
                byte[] buffer = ms.ToArray();
                string cheksum = HashString(buffer);

                response.Headers[HeaderNames.ETag] = cheksum;

                if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var etag) && cheksum == etag)
                {
                    response.StatusCode = StatusCodes.Status304NotModified;
                    return;
                }

            }
            ms.Position = 0;
            await ms.CopyToAsync(originalStrem);
        }
    }

    private static bool IsEtagSupported(HttpResponse response)
    {
        if (response.StatusCode != StatusCodes.Status200OK) return false;
        if (response.Headers.ContainsKey(HeaderNames.ETag)) return false;

        return true;
    }

    private static string HashString(byte[] textBytes)
    {
        // Uses SHA256 to create the hash
        using (var sha = new SHA256Managed())
        {
            byte[] hashBytes = sha.ComputeHash(textBytes);

            // Convert back to a string, removing the '-' that BitConverter adds
            string hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", String.Empty);

            return hash;
        }
    }


}
public static class ETagMiddlewareHandleExtensions
{
    public static void UseETagger(this IApplicationBuilder app)
    {
        app.UseMiddleware<ETagMiddleware>();
    }

}

/*public class ETagMiddleware
{
    private readonly RequestDelegate _next;

    public ETagMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsynk(HttpContext context)
    {
        //if (context.Request.Method == "")
        //{
            var response = context.Response;
            var originalStream = response.Body;

            using var ms = new MemoryStream();
            response.Body = ms;

            await _next(context);

            if (isEtagSupported(response))
            {
                string checksum = CalclateHash(ms);
                if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var etag) && checksum == etag)
                {
                    response.StatusCode = StatusCodes.Status304NotModified;
                    return;
                }
                response.Headers[HeaderNames.ETag] = checksum;
            }
            ms.Position = 0;
            await ms.CopyToAsync(originalStream);
        //}
        //else await _next(context);
    }

    private static bool isEtagSupported(HttpResponse response)
    {
        if (response.StatusCode != StatusCodes.Status200OK)
        {
            return false;
        }
        if (response.Body.Length > 20 * 1024)
        {
            return false;
        }
        if (response.Headers.ContainsKey(HeaderNames.ETag))
        {
            return false;
        }
        return true;
    }

    private static string CalclateHash(MemoryStream ms)
    {
        string checkSum = "";

        using (var algo = SHA1.Create())
        {
            ms.Position = 0;
            byte[] bytes = algo.ComputeHash(ms);
            checkSum = $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";
        }
        return checkSum;
    }
}

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseETagger(this IApplicationBuilder app)
    {
         return app.UseMiddleware<ETagMiddleware>();
    }
}*/
