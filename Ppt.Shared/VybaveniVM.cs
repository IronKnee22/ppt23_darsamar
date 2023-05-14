using System.ComponentModel.DataAnnotations;

namespace Ppt.Shered.ViewModels
{
    public class VybaveniVM
    {
        [Required(ErrorMessage = "Pole \"{0}\" musí něco obsahovat")]
        [MinLength(5, ErrorMessage = "Pole \"{0}\" musí být alespoň {1} znaků")]
        [Display(Name = "Název")]
        public string Name { get; set; } = "";

        public Guid Id { get; set; }

        public DateTime BuyDate { get; set; }

        [CustomValidation(typeof(VybaveniVM), nameof(validation))]
        public DateTime LastRevision { get; set; }

        public bool IsRevisionNeed { get => LastRevision + new TimeSpan(730, 0, 0, 0) < DateTime.Now; }

        [Range(0, 10000000, ErrorMessage = "\"{0}\" musí být mezi {1} až {2}")]
        [Display(Name = "Cena")]
        public int Cena { get; set; }

        public VybaveniVM()
        {
            this.Name = "";
            Id = Guid.NewGuid();
            this.BuyDate = RandomDate(new DateTime(2002, 7, 15));
            //this.LastRevision = RandomDate(BuyDate); //tohle se musí dát aby se nám to generovalo v revizích
            this.Cena = Random.Shared.Next(10000000);
        }

        public static ValidationResult? validation(DateTime LastRev, ValidationContext validationContext)
        {
            var Vyb = (VybaveniVM)validationContext.ObjectInstance;

            if (LastRev < Vyb.BuyDate)
            {
                return new ValidationResult("Revize musí být až po koupi");
            }
            return ValidationResult.Success;
        }
        public VybaveniVM Copy()
        {
            VybaveniVM to = new();
            to.Name = Name;
            to.BuyDate = BuyDate;
            to.LastRevision = LastRevision;
            to.Cena = Cena;

            return to;
        }
        public void MapTo(VybaveniVM? to)
        {
            if (to == null) return;
            to.Name = Name;
            to.BuyDate = BuyDate;
            to.LastRevision = LastRevision;
            to.Cena = Cena;
        }

        public static List<VybaveniVM> VratRandSeznam()
        {
            List<VybaveniVM> ListVybaveni = new List<VybaveniVM>();
            for (int i = 0; i < Random.Shared.Next(5, 10); i++)
            {
                VybaveniVM vybavenivm = new VybaveniVM();

                vybavenivm.Name = RandomName();
                ListVybaveni.Add(vybavenivm);
            }
            return ListVybaveni;
        }

        public static DateTime RandomDate(DateTime since)
        {
            int between = (DateTime.Now - since).Days;

            since += new TimeSpan(Random.Shared.Next(0, between), 0, 0, 0);

            return since;
        }

        public static string RandomName()
        {
            string letters = "qwertzuiopasdfghjklyxcvbm";
            string Name = "";

            int lenght = Random.Shared.Next(5, 10);
            for (int i = 0; i < lenght; i++)
            {
                int rndpositon = Random.Shared.Next(letters.Length);
                Name += letters[rndpositon];
            }
            return Name;
        }
    }
}
