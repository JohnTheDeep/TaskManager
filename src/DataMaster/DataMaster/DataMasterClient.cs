using DataMaster.Core.Pagination;
using DataMaster.Models;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataMaster
{
    public class DataMasterClient : IDataMasterClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        private readonly Dictionary<Type, string> _urls = new()
        {
            [typeof(User)] = "/api/v1/Users"
        };

        public DataMasterClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            _jsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<(IEnumerable<T> items, int total)> GetList<T>(Dictionary<string, string>? queryParams = null, int? skip = null, int? take = null) where T : BaseEntity
        {
            var uri = _urls[typeof(T)];

            var allQueryParams = new Dictionary<string, string>();

            if (queryParams != null)
                foreach (var param in queryParams.Keys)
                    allQueryParams.Add(param, queryParams[param]);

            if (skip.HasValue) allQueryParams.Add(nameof(skip), skip.Value.ToString());
            if (take.HasValue) allQueryParams.Add(nameof(skip), take.Value.ToString());

            var query = await new FormUrlEncodedContent(allQueryParams).ReadAsStringAsync();

            if (!string.IsNullOrEmpty(query))
                uri += "?" + query;

            using var resp = await _httpClient.GetAsync(uri);
            var respBody = await resp.Content.ReadAsStringAsync();


            if (resp.IsSuccessStatusCode)
            {
                try
                {
                    var result = JsonSerializer.Deserialize<Page<T>>(respBody, _jsonOptions);
                    return (result!.Items, result.TotalCount);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Cannot parse: {respBody}", ex);
                }
            }

            int hash = _httpClient.GetHashCode();

            throw new DataMasterClientException($"[{hash}] Response code: {resp.StatusCode}" + Environment.NewLine +
                $"Response body: {respBody}", resp.StatusCode);
        }

        public async Task<T?> GetById<T>(Guid id) where T : BaseEntity
        {
            var uri = _urls[typeof(T)] + "/" + id;

            using var resp = await _httpClient.GetAsync(uri);
            var respBody = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
            {
                try
                {
                    var result = JsonSerializer.Deserialize<T>(respBody, _jsonOptions);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"[{_httpClient.GetHashCode()}] Cannot parse: {respBody}", ex);
                }
            }

            if (resp.StatusCode == HttpStatusCode.NotFound) return default;

            throw new DataMasterClientException($" Response code: {resp.StatusCode}" + Environment.NewLine +
                $"Response body: {respBody}", resp.StatusCode);
        }
    }
}
