using Beamable.Server;

namespace Beamable.TestService
{
	[Microservice("TestService")]
	public partial class TestService : Microservice
	{
		[ClientCallable]
		public int Add(int a, int b)
		{
			return a + b;
		}
	}
}
