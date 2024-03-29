using Server.Hubs;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();

            builder.Services.AddSignalR();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAntiforgery();

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
            app.MapFallback(context =>
            {
                context.Response.Redirect("/");
                return Task.CompletedTask;
            });

            app.MapHub<FileHub>("/filehub");

            app.Run();
        }
    }
}