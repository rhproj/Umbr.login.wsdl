using Newtonsoft.Json;
using Umbr2.Site.Models;

namespace Umbr2.Site.Services;

public static class CustomerAccountService
	{
		public static CustomerModel? Customer { get; private set; }

    public static void GetMember(string userInfo)
		{
			Customer = new();
			if (userInfo.Contains("EntityId"))
			{
				Customer = JsonConvert.DeserializeObject<CustomerModel>(userInfo);
			}
		}

		public static void LogoutMember()
		{
        Customer = null;
    }

}
