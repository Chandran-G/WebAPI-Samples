using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Swashbuckle.Application;
using Autofac;
using Autofac.Integration.WebApi;
using Account.Repository.Contract;
using Account.Repository;
using System.Reflection;
using Account.ServiceLayer;
using Account.ServiceLayer.Contract;
using System.Web.Http.Cors;

[assembly: OwinStartup("WebApi", typeof(Account.API.App_Start.ApiStartup))]

namespace Account.API.App_Start
{
    public class ApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            WebApiConfig.Register(httpConfig);
           

            var builder = new ContainerBuilder();

            builder.RegisterType<AccountRepository>().As<IAccountRepository>().SingleInstance();
            builder.RegisterType<AccountService>().As<IAccountService>();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            httpConfig.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Account.API");
                c.RootUrl(req =>
                {
                    return SwaggerDocsConfig.DefaultRootUrlResolver(req) + req.RequestUri.AbsolutePath.Remove(req.RequestUri.AbsolutePath.IndexOf("/swagger"));
                });
            }).EnableSwaggerUi();

            var diContainer = builder.Build();
            
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(diContainer);
            httpConfig.EnableCors(new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true });
            app.UseAutofacWebApi(httpConfig);
            app.UseWebApi(httpConfig);
            
        }

      


    }
}