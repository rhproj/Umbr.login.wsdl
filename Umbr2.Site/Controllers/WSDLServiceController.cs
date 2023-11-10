using IICUTechService;
using Microsoft.AspNetCore.Mvc;
using Umbr2.Site.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.Cms.Web.Website.Controllers;

namespace Umbr2.Site.Controllers;

public class WSDLServiceController : SurfaceController
{
    public WSDLServiceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider)
                : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider){}
     
    public async Task<IActionResult> HandleLogin([Bind(Prefix = "loginModel")] LoginModel model) 
    {
        try
        {
            ICUTechClient techClient = new ICUTechClient();

            string IPs = NetworkService.GetIPAddress();
            LoginResponse response = await techClient.LoginAsync(model.Username, model.Password, IPs);
             
				var customerJson = response?.@return;

            CustomerAccountService.GetMember(customerJson);

				return Redirect("/MyAccount");
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public IActionResult HandleLogout()
    {
        CustomerAccountService.LogoutMember();
        return Redirect("/");
    }
}
