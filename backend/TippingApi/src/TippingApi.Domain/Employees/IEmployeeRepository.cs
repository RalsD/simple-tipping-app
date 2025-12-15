namespace TippingApi.Domain.Employees;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(Guid id);

    Task AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<Employee>> GetWithShiftsAsync(DateTime weekStart, DateTime weekEnd);
}

