using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TestRestApplication.Data;
using TestRestApplication.Models;

namespace TestWebApp.Data.Repositories;

public class InfoDataRepository : IInfoDataRepository
{
    private readonly ApplicationDbContext _db;

    public InfoDataRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Info>> GetAll(Expression<Func<Info, bool>>? filter = null, bool isTracked = true,
        int pageNumber = 1, int pageSize = 5)
    {
        IQueryable<Info> query = _db.Infos;
        if (!isTracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.OrderBy(d => d.SerialNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task SaveInfoData(IEnumerable<Info> data)
    {
        await _db.Database.ExecuteSqlAsync($"delete from dbo.infos");
        await _db.AddRangeAsync(data);
        await Save();
    }

    public async Task<int> GetTotalCount()
    {
        return await _db.Infos.CountAsync();
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}