namespace API.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public List<Guid> AllGroupsId { get; set; } = new List<Guid>();
    }
}
