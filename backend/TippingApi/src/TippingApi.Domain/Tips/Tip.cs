using TippingApi.Domain.Abstractions;

namespace TippingApi.Domain.Tips;

public class Tip : Entity
{
    private Tip(Guid id, decimal amount, DateTime date)
        : base(id)
    {
        Amount = amount;
        Date = date;
    }

    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime WeekStart => Date;


    public static Tip Create(decimal amount, DateTime date)
    {
        return new Tip(Guid.NewGuid(), amount, date);
    }

    public static Tip CreateWeekly(DateTime weekStart, decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Tip amount cannot be negative.", nameof(amount));
        }

        return new Tip(Guid.NewGuid(), amount, weekStart);
    }
}
