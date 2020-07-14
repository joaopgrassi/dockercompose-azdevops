using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlogApp.Api.Tests
{
    /// <summary>
	/// Helper class to convert a <see cref="StringContent"/> into a JsonContent
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class JsonContent<T> : StringContent
    {
        public JsonContent(T entity)
            : base(JsonConvert.SerializeObject(entity))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
