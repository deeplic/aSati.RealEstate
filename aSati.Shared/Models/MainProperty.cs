namespace aSati.Shared.Models
{
    public class MainProperty
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;

        public List<PropertyUnit> Units { get; set; } = new();
    }
}
