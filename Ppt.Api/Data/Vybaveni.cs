using Ppt.Shered.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ppt.Api.Data
{
    public class Vybaveni
    {
        //tohle je třída pro tabulku a jsou zde proměné které tabulku obsahuje
        
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public DateTime BuyDate { get; set; }

        public int Cena { get; set; }
        public List<Revize> Revizes { get; set; } = new();




    }
}
