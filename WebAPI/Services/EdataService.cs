using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class EdataService
    {
        private readonly ESGContext _context;

        public EdataService(ESGContext context)
        {
            _context = context;
        }

        public async Task<List<Edata>> GetAllEdataAsync()
        {
            return await _context.EModels.ToListAsync();
        }

        public async Task<Edata> GetEdataByIdAsync(int id)
        {
            return await _context.EModels.FindAsync(id);
        }

        public async Task CreateEdataAsync(Edata edata)
        {
            _context.EModels.Add(edata);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEdataAsync(Edata edata)
        {
            _context.Entry(edata).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEdataAsync(int id)
        {
            var edata = await _context.EModels.FindAsync(id);
            if (edata != null)
            {
                _context.EModels.Remove(edata);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<EdataK>> GetAllEdataKAsync()
        {
            return await _context.EModelsK.ToListAsync();
        }

        public async Task<EdataK> GetEdataKByIdAsync(int id)
        {
            return await _context.EModelsK.FindAsync(id);
        }

        public async Task CreateEdataKAsync(EdataK edataK)
        {
            _context.EModelsK.Add(edataK);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEdataKAsync(EdataK edataK)
        {
            _context.Entry(edataK).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEdataKAsync(int id)
        {
            var edataK = await _context.EModelsK.FindAsync(id);
            if (edataK != null)
            {
                _context.EModelsK.Remove(edataK);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GreenHouseK>> GetAllGreenHouseKAsync()
        {
            return await _context.GHModelsK.ToListAsync();
        }

        public async Task<GreenHouseK> GetGreenhouseKByIdAsync(int id)
        {
            return await _context.GHModelsK.FindAsync(id);
        }

        public async Task CreateGreenhouseKAsync(GreenHouseK greenhouseK)
        {
            _context.GHModelsK.Add(greenhouseK);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGreenHouseKAsync(GreenHouseK greenhouseK)
        {
            _context.GHModelsK.Update(greenhouseK);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGreenhouseKAsync(int id)
        {
            var greenhouseK = await _context.GHModelsK.FindAsync(id);
            if (greenhouseK != null)
            {
                _context.GHModelsK.Remove(greenhouseK);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GreenHouseT>> GetAllGreenhouseTAsync()
        {
            return await _context.GHModelsT.ToListAsync();
        }

        public async Task<GreenHouseT> GetGreenhouseTByIdAsync(int id)
        {
            return await _context.GHModelsT.FindAsync(id);
        }

        public async Task CreateGreenhouseTAsync(GreenHouseT greenhouseT)
        {
            _context.GHModelsT.Add(greenhouseT);
            await _context.SaveChangesAsync();
        }

        public async Task CreateGreenhouseAsync(GreenHouse greenhouse)
        {
            _context.GHModels.Add(greenhouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGreenhouseTAsync(GreenHouseT greenhouseT)
        {
            _context.Entry(greenhouseT).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGreenHouseAsync(GreenHouse greenhouse)
        {
            _context.Entry(greenhouse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<GreenHouseT>> GetAllGreenHouseTAsync()
        {
            return await _context.GHModelsT.ToListAsync();
        }

        public async Task<List<GreenHouse>> GetAllGreenHouseAsync()
        {
            return await _context.GHModels.ToListAsync();
        }

        public async Task<GreenHouse> GetGreenhouseByIdAsync(int id)
        {
            return await _context.GHModels.FindAsync(id);
        }

        public async Task CreateGreenhousTeAsync(GreenHouseT greenhouseT)
        {
            _context.GHModelsT.Add(greenhouseT);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGreenHouseTAsync(GreenHouseT greenhouseT)
        {
            _context.GHModelsT.Update(greenhouseT);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGreenhouseTAsync(int id)
        {
            var greenhouseT = await _context.GHModelsT.FindAsync(id);
            if (greenhouseT != null)
            {
                _context.GHModelsT.Remove(greenhouseT);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGreenhouseAsync(int id)
        {
            var greenhouse = await _context.GHModels.FindAsync(id);
            if (greenhouse != null)
            {
                _context.GHModels.Remove(greenhouse);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateWaterAsync(Water water)
        {
            _context.Entry(water).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<WasteK>> GetAllWasteKAsync()
        {
            return await _context.WModelsK.ToListAsync();
        }

        public async Task<WasteK> GetWasteKByIdAsync(int id)
        {
            return await _context.WModelsK.FindAsync(id);
        }

        public async Task CreateWasteKAsync(WasteK wasteK)
        {
            _context.WModelsK.Add(wasteK);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWasteKAsync(WasteK wasteK)
        {
            _context.Entry(wasteK).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWasteKAsync(int id)
        {
            var wasteK = await _context.WModelsK.FindAsync(id);
            if (wasteK != null)
            {
                _context.WModelsK.Remove(wasteK);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<WasteT>> GetAllWasteTAsync()
        {
            return await _context.WModelsT.ToListAsync();
        }

        public async Task<WasteT> GetWasteTByIdAsync(int id)
        {
            return await _context.WModelsT.FindAsync(id);
        }

        public async Task CreateWasteTAsync(WasteT wasteT)
        {
            _context.WModelsT.Add(wasteT);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWasteTAsync(WasteT wasteT)
        {
            _context.Entry(wasteT).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWasteTAsync(int id)
        {
            var wasteT = await _context.WModelsT.FindAsync(id);
            if (wasteT != null)
            {
                _context.WModelsT.Remove(wasteT);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Water>> GetAllWaterAsync()
        {
            return await _context.WaModels.ToListAsync();
        }

        public async Task<Water> GetWaterByIdAsync(int id)
        {
            return await _context.WaModels.FindAsync(id);
        }

        public async Task CreateWaterAsync(Water water)
        {
            _context.WaModels.Add(water);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWater(Water water)
        {
            _context.Entry(water).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWaterAsync(int id)
        {
            var water = await _context.WaModels.FindAsync(id);
            if (water != null)
            {
                _context.WaModels.Remove(water);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<WaterK>> GetAllWaterKAsync()
        {
            return await _context.WaModelsK.ToListAsync();
        }

        public async Task<WaterK> GetWaterKByIdAsync(int id)
        {
            return await _context.WaModelsK.FindAsync(id);
        }

        public async Task CreateWaterKAsync(WaterK waterK)
        {
            _context.WaModelsK.Add(waterK);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWaterKAsync(WaterK waterK)
        {
            _context.Entry(waterK).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWaterKAsync(int id)
        {
            var waterK = await _context.WaModelsK.FindAsync(id);
            if (waterK != null)
            {
                _context.WaModelsK.Remove(waterK);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Waste>> GetAllWasteAsync()
        {
            return await _context.WModels.ToListAsync();
        }

        public async Task<Waste> GetWasteByIdAsync(int id)
        {
            return await _context.WModels.FindAsync(id);
        }

        public async Task CreateWasteAsync(Waste waste)
        {
            _context.WModels.Add(waste);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWasteAsync(Waste waste)
        {
            _context.Entry(waste).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWasteAsync(int id)
        {
            var waste = await _context.WModels.FindAsync(id);
            if (waste != null)
            {
                _context.WModels.Remove(waste);
                await _context.SaveChangesAsync();
            }
        }
    }
}
