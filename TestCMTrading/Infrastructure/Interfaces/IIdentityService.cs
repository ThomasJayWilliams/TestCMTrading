using System.Threading.Tasks;
using TestCMTrading.Models;

namespace TestCMTrading.Infrastructure.Interfaces
{
	public interface IIdentityService
	{
		Task SignIn(Login login);
		Task RegisterUser(User user);
	}
}
