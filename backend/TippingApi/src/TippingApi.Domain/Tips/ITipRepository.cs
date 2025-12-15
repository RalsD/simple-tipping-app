namespace TippingApi.Domain.Tips;

public interface ITipRepository
{
    Task<IEnumerable<Tip>> GetAllAsync();

    Task<Tip?> GetByIdAsync(Guid id);

    Task AddAsync(Tip tip);

    Task UpdateAsync(Tip tip);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<Tip>> GetTipsForWeekAsync(DateTime weekStart, DateTime weekEnd);

    Task<decimal> GetWeeklyTotalAsync(DateTime weekStart, DateTime weekEnd);
}
