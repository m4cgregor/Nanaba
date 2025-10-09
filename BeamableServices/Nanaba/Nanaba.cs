using Beamable.Server;

namespace Beamable.Server
{
	/// <summary>
	/// This class represents the existence of the Nanaba database.
	/// Use it for type safe access to the database.
	/// <code>
	/// var db = await Storage.GetDatabase&lt;Nanaba&gt;();
	/// </code>
	/// </summary>
	[StorageObject("Nanaba")]
	public class Nanaba : MongoStorageObject
	{
		
	}
}
