namespace TippingApi.Domain.Shifts;

public interface IShiftRepository
{
    Task<IEnumerable<Shift>> GetAllAsync();

    Task<Shift?> GetByIdAsync(Guid id);

    Task AddAsync(Shift shift);

    Task UpdateAsync(Shift shift);

    Task DeleteAsync(Guid id);

    // Get shifts for a specific week
    Task<IEnumerable<Shift>> GetShiftsForWeekAsync(DateTime weekStart, DateTime weekEnd);

    // Get shifts for a specific employee within a week
    Task<IEnumerable<Shift>> GetShiftsForEmployeeAsync(Guid employeeId, DateTime weekStart, DateTime weekEnd);
}
