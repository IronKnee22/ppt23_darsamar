namespace Ppt23.Client.ViewModels
{
    public class VybaveniVM
    {
        public string Name { get; set; } = "";
		//public bool IsNeedRevision { get => ;}

		public bool IsNameJAck { get => Name=="Jack"; }

		public static List<VybaveniVM> VratRandSeznam(int pocet)
        {
            return new List<VybaveniVM>() { new VybaveniVM() { Name = "da" }, new VybaveniVM() { Name = "df"} };
        }
    }
}
