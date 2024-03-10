namespace ClinicManagementSystem.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
    }
}
