using System;
using System.Web;
using AutoMapper;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using ServiceApp.Infrastructure.DI;

namespace ServiceApp
{
    public class Global : HttpApplication
    {

        static IWindsorContainer _container;

        protected void Application_Start(Object sender, EventArgs e)
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfiles("ServiceApp");
            });

            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            _container.Install(new WindsorInstaller());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}