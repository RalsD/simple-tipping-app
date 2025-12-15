using Microsoft.EntityFrameworkCore;
using TippingApi.Domain.Tips;
using TippingApi.Infrastructure;

internal sealed class TipRepository : ITipRepository
{
    private readonly ApplicationDbContext _context;

    public TipRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tip>> GetAllAsync()
    {
        return await _context.Tips.ToListAsync();
    }

    public async Task<Tip?> GetByIdAsync(Guid id)
    {
        return await _context.Tips.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(Tip tip)
    {
        _context.Tips.Add(tip);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tip tip)
    {
        _context.Tips.Update(tip);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var tip = await _context.Tips.FindAsync(id);
        if (tip != null)
        {
            _context.Tips.Remove(tip);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Tip>> GetTipsForWeekAsync(DateTime weekStart, DateTime weekEnd)
    {
        return await _context.Tips
            .Where(t => t.Date >= weekStart && t.Date < weekEnd)
            .ToListAsync();
    }

    public async Task<decimal> GetWeeklyTotalAsync(DateTime weekStart, DateTime weekEnd)
    {
        return await _context.Tips
            .Where(t => t.Date >= weekStart && t.Date < weekEnd)
            .SumAsync(t => t.Amount);
    }
}
