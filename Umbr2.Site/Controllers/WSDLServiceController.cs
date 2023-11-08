﻿using IICUTechService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Umbr2.Site.Models;
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
{
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

                var customerJson = response.@return;

                if (!customerJson.Contains("EntityId"))
                {
                    await Console.Out.WriteLineAsync("TryAgain!");
                    //TempData["failed"] = "Wrong Credentials";
                    return RedirectToCurrentUmbracoUrl();
                }

                await Console.Out.WriteLineAsync("Success!");

                var customer = JsonConvert.DeserializeObject<CustomerModel>(customerJson);

                return Redirect("/MyAccount");
                //return Redirect("/user-page");
                //return RedirectToPage("/user-page", customer);
            }
            catch (Exception) //паснуть ошибку на страницу
            {
                throw;
            }
        }
    }
}
