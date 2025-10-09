using Beamable.Common;
using MongoDB.Driver;

namespace Beamable.Server
{
	public static class NanabaDataExtension
	{
		/// <summary>
		/// Get an authenticated MongoDB instance for NanabaData
		/// </summary>
		/// <returns></returns>
		public static Promise<IMongoDatabase> NanabaDataDatabase(
			this IStorageObjectConnectionProvider provider)
			=> provider.GetDatabase<NanabaData>();

		/// <summary>
		/// Gets a MongoDB collection from NanabaData by the requested name, and uses the given mapping class.
		/// If you don't want to pass in a name, consider using <see cref="NanabaDataCollection{TCollection}()"/>
		/// </summary>
		/// <param name="name">The name of the collection</param>
		/// <typeparam name="TCollection">The type of the mapping class</typeparam>
		/// <returns>When the promise completes, you'll have an authorized collection</returns>
		public static Promise<IMongoCollection<TCollection>> NanabaDataCollection<TCollection>(
			this IStorageObjectConnectionProvider provider, string name)
			where TCollection : StorageDocument
			=> provider.GetCollection<NanabaData, TCollection>(name);

		/// <summary>
		/// Gets a MongoDB collection from NanabaData by the requested name, and uses the given mapping class.
		/// If you want to control the collection name separate from the class name, consider using <see cref="NanabaDataCollection{TCollection}(string)"/>
		/// </summary>
		/// <param name="name">The name of the collection</param>
		/// <typeparam name="TCollection">The type of the mapping class</typeparam>
		/// <returns>When the promise completes, you'll have an authorized collection</returns>
		public static Promise<IMongoCollection<TCollection>> NanabaDataCollection<TCollection>(
			this IStorageObjectConnectionProvider provider)
			where TCollection : StorageDocument
			=> provider.GetCollection<NanabaData, TCollection>();
	}
}
