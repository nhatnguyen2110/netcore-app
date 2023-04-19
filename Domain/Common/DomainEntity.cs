namespace Domain.Common
{
    public class DomainEntity<T>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DomainEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DomainEntity(T id) { Id = id; }
        public bool IsTransient()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return Id.Equals(default(T));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        public T Id { get; set; }
    }
}
