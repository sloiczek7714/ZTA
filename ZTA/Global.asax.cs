using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ZTA
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteConfig.Register(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer(new ZtaDBContexSeeder());
        }
    }
}