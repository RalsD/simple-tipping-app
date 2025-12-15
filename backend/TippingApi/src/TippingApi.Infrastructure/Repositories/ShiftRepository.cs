using Microsoft.EntityFrameworkCore;
using TippingApi.Domain.Shifts;
using TippingApi.Infrastructure;

internal sealed class ShiftRepository : IShiftRepository
{
    private readonly ApplicationDbContext _context;

    public ShiftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shift>> GetAllAsync()
    {
        return await _context.Shifts
            .Include(s => s.Employee)
            .ToListAsync();
    }

    public async Task<Shift?> GetByIdAsync(Guid id)
    {
        return await _context.Shifts
            .Include(s => s.Employee)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Shift shift)
    {
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Shift shift)
    {
        _context.Shifts.Update(shift);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        if (shift != null)
        {
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Shift>> GetShiftsForWeekAsync(DateTime weekStart, DateTime weekEnd)
    {
        return await _context.Shifts
            .Include(s => s.Employee)
            .Where(s => s.Date >= weekStart && s.Date < weekEnd)
            .ToListAsync();
    }

    public async Task<IEnumerable<Shift>> GetShiftsForEmployeeAsync(Guid employeeId, DateTime weekStart, DateTime weekEnd)
    {
        return await _context.Shifts
            .Include(s => s.Employee)
            .Where(s => s.Employee.Id == employeeId && s.Date >= weekStart && s.Date < weekEnd)
            .ToListAsync();
    }
}
