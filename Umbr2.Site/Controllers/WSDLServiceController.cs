using IICUTechService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using StackExchange.Profiling.Internal;
using Umbr2.Site.Models;
using Umbr2.Site.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;

namespace Umbr2.Site.Controllers
{//WSDLoginController
	public class WSDLServiceController : SurfaceController
    {//?
        public WSDLServiceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
                ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider)
                    : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider){}
         
        public async Task<IActionResult> HandleLogin([Bind(Prefix = "loginModel")] LoginModel model) //<IActionResult>
        {
            //await Console.Out.WriteLineAsync($"Success! {model.Username} : {model.Password}");
            try
            {
                ICUTechClient techClient = new ICUTechClient();
                //techClient.RegisterNewCustomerAsync(,)

                //LoginRequest = new LoginRequest();

                LoginResponse response = await techClient.LoginAsync(model.Username, model.Password, "192.0.0.1");//Credential.UserName, Credential.Password, Credential.IPs);  //LoginRequest.UserName, LoginRequest.Password, LoginRequest.IPs);    //("admin","password","localhost");      //("meme", "123", "192.0.0.1");
                 
				var customerJson = response?.@return;


    //            if (customerJson == null)
    //            {
				//	TempData["message"] = "Failed to get login response from the web service";
				//	return Redirect("/MyAccount");
				//}
    //            else if (!customerJson.Contains("EntityId"))
    //            {
    //                await Console.Out.WriteLineAsync("TryAgain!");
    //                TempData["message"] = "Wrong Credentials";
				//	return Redirect("/MyAccount");
				//	//return RedirectToCurrentUmbracoUrl();
				//}

                CustomerMemberService.GetMember(customerJson);
				//TempData["message"] = "Welcome";
				//var customer = JsonConvert.DeserializeObject<CustomerModel>(customerJson);


				//TempData["Customer"] = customerJson;
				return Redirect("/MyAccount");
				
            }
            catch (Exception) //паснуть ошибку на страницу
            {
                throw;
            }
        }
    }
}
