using Beamable.Server;
using System.Threading.Tasks;

namespace Beamable.TestService
{
	public class Program
	{
		/// <summary>
		/// The entry point for the <see cref="TestService"/> service.
		/// </summary>
		public static async Task Main()
		{
			// inject data from the CLI.
			await MicroserviceBootstrapper.Prepare<TestService>();
			
			// run the Microservice code
			await MicroserviceBootstrapper.Start<TestService>();
		}
	}
}
