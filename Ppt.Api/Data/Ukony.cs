namespace Ppt.Api.Data
{
    public class Ukony
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public DateTime RxecutionDate { get; set; }
        public Guid VybaveniUkonyId { get; set; }
        public VybaveniUkony VybaveniUkony { get; set; } = null!;
    }
}
