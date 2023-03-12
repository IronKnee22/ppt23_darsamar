namespace Ppt23.Client.ViewModels
{
	public class VybaveniVM
	{
		Random random = new Random();
		public string Name { get; set; } = "";
		public DateTime BuyDate { get; set; }
		public DateTime LastRevision { get; set; }
		public bool IsRevisionNeed { get => LastRevision + new TimeSpan(730, 0, 0, 0) < DateTime.Now; }

		public VybaveniVM()
		{
			this.Name = RandomName();
			this.BuyDate = RandomDate(new DateTime(2002, 7, 15));
			this.LastRevision = RandomDate(BuyDate);
			

		}

		public static List<VybaveniVM> VratRandSeznam()
		{
			Random random = new Random();
			List<VybaveniVM> ListVybaveni = new List<VybaveniVM>();
			for (int i = 0; i < random.Next(5, 10); i++)
			{
				VybaveniVM vybavenivm = new VybaveniVM();

				ListVybaveni.Add(vybavenivm);
			}
			return ListVybaveni;

		}

		public DateTime RandomDate(DateTime since)
		{
			int between = (DateTime.Now - since).Days;
			since.AddDays(random.Next(between));


			return since;


		}

		public string RandomName()
		{


			string letters = "qwertzuiopasdfghjklyxcvbm";
			string Name = "";

			int lenght = random.Next(5, 10);
			for (int i = 0; i < lenght; i++)
			{
				int rndpositon = random.Next(letters.Length);

				Name += letters[rndpositon];
			}
			return Name;
		}
	}
}
