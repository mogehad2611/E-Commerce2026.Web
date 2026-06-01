namespace DomainLayer.Models.IdentityModule
{
    public class Address
    {
        public int Id { get; set; }
        public string Fname { get; set; } = default!;
        public string Lname { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public AppUser User { get; set; } = default!;
        public string UserId { get; set; }
    }
}