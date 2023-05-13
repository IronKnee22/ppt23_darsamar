using Mapster;
using Ppt.Shered.ViewModels;
using System.Runtime.CompilerServices;

namespace Ppt.Api.Data
{
    public class SeedingData
    {
        PptDbContext? _db;

        public SeedingData(PptDbContext pptDbContext)
        {
            _db = pptDbContext;
        }


        public async Task SeedData()
        {
            if (_db == null)
                return;
            
            if (!_db.Vybavenis.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    var vyb = new VybaveniVM(); 
                    vyb.Name = VybaveniVM.RandomName();
                    
                    _db.Vybavenis.Add(vyb.Adapt<Vybaveni>());
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
