﻿using Mapster;
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
            if (!_db.Vybavenis.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var vyb = new VybaveniVM(); 
                    vyb.Name = VybaveniVM.RandomName();
                    

                    _db.Vybavenis.Add(vyb.Adapt<Vybaveni>());
                    
                }
                _db.SaveChanges();
                foreach (var item in _db.Vybavenis) 
                {
                    var novaRevize = new Revize
                    {
                        DateTime = VybaveniVM.RandomDate(item.BuyDate),
                        VybaveniId = item.Id,
                        Name = item.Name,

                    };
                    _db.Revizes.Add(novaRevize);

                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
