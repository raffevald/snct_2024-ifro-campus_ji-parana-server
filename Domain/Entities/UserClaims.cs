namespace Domain.Entities
{
    public class UserClaims
    {
        public int UserId { get; set; }
        public string UserClaimExternalId { get; set; } = string.Empty;

        public string ClaimType { get; set; } = string.Empty;
        public string ClaimValue { get; set; } = string.Empty;
        public string ClaimExternalId { get; set; } = string.Empty;

        public DateTime UserClaimRegisterAt { get; set; }
        public DateTime? UserClaimUpdateAt { get; set; }
        public DateTime? UserClaimDeleteAt { get; set; }
    }
}
