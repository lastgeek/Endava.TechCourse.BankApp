namespace Endava.TechCourse.BankApp.Domain.Common
{
    internal class BaseEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime TimeStap { get; } = DateTime.Now;
    }
}