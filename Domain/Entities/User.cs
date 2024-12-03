namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string ExternalId { get; set; } = $"user_{Guid.NewGuid()}";

        public string UserName { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;

        public int EntityId { get; set; }

        public DateTime RegisterAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; } = null;
        public DateTime? DeleteAt { get; set; } = null;

        public User() { }

        public void SetUpdateAt()
        {
            UpdateAt = DateTime.Now;
        }
    }
}
