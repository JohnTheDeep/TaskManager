using DataMaster.Models;

namespace DataMaster
{
    public interface IDataMasterClient
    {
        Task<(IEnumerable<T> items, int total)> GetList<T>(Dictionary<string, string>? queryParams = null, int? skip = null, int? take = null) where T : BaseEntity;
        Task<T?> GetById<T>(Guid id) where T : BaseEntity;
    }
}
