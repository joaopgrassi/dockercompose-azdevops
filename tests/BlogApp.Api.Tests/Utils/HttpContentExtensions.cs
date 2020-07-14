using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogApp.Api.Tests
{
    /// <summary>
	/// Extension methods around <see cref="HttpContent"/>
	/// </summary>
	public static class HttpContentExtensions
    {
        /// <summary>
        /// Reads as <see cref="HttpContent"/> as JSON string and deserializes to <typeparam name="TResult"></typeparam>
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="content">The content.</param>
        public static async Task<TResult> ReadAsJsonAsync<TResult>(this HttpContent content)
        {
            try
            {
                var responseString = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(responseString);
            }
            catch (Exception ex)
            {
                var rawJson = await content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception($"Failed to deserialize response:{ rawJson }", ex);
            }
        }
    }
}
