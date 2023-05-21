namespace Ppt.Api.Data
{
    public class VybaveniUkony
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public List<Ukony> Ukonys { get; set; } = new();
    }
}
