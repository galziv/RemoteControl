using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace RemoteControl.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            var webServerPath = Path.Combine(Environment.CurrentDirectory, "WebServer");

#if DEBUG
            webServerPath = Path.Combine(Environment.CurrentDirectory, @"..\..\WebServer");
#endif

            config.SuppressDefaultHostAuthentication();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;


            var physicalFileSystem = new PhysicalFileSystem(webServerPath); // debug

            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };

            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] { "index.html" };

            app.UseFileServer(options);
        }
    }
}
