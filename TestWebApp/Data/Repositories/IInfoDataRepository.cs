using System.Linq.Expressions;
using TestRestApplication.Models;

namespace TestWebApp.Data.Repositories;

public interface IInfoDataRepository
{
    Task<List<Info>> GetAll(Expression<Func<Info, bool>>? filter = null, bool isTracked = true, int pageNumber = 1, int pageSize=5);
    Task SaveInfoData(IEnumerable<Info> data);
    Task<int> GetTotalCount();
    Task Save();
}