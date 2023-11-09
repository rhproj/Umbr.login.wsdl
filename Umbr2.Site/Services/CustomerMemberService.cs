using Newtonsoft.Json;
using Umbr2.Site.Models;

namespace Umbr2.Site.Services
{
	public static class CustomerMemberService
	{
		public static CustomerModel? Customer { get; private set; }
		//public static bool IsLoggedIn { get; }

        public static void GetMember(string userInfo)
		{
			Customer = new();
			if (userInfo.Contains("EntityId"))
			{
				Customer = JsonConvert.DeserializeObject<CustomerModel>(userInfo);
			}
		}
	}
}
