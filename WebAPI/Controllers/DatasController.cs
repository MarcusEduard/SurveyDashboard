using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
using Domain.Models;
using Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DatasController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DatasController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Data>> GetDatas()
        {
            return _dataService.GetDatas();
        }

        [HttpPost(Name = "SaveData")]
        public async Task<ActionResult> SaveDataAsync(Data data)
        {
            await _dataService.SaveDataAsync(data);
            return CreatedAtAction("SaveData", data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDataAsync(int id, Data data)
        {
            if (id != data.Id)
            {
                return BadRequest();
            }
            await _dataService.UpdateDataAsync(data);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveDataAsync(int id)
        {
            await _dataService.RemoveDataAsync(id);
            return NoContent();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class GreenHouseController : ControllerBase
    {
        private readonly EdataService _edataService;

        public GreenHouseController(EdataService edataService)
        {
            _edataService = edataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GreenHouse>>> GetAllGreenHouse()
        {
            return await _edataService.GetAllGreenHouseAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGreenhouse(GreenHouse greenhouse)
        {
            await _edataService.CreateGreenhouseAsync(greenhouse);
            return CreatedAtAction(nameof(GetGreenhouseById), new { id = greenhouse.GreenHouseId }, greenhouse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GreenHouse>> GetGreenhouseById(int id)
        {
            var greenhouse = await _edataService.GetGreenhouseByIdAsync(id);
            if (greenhouse == null)
            {
                return NotFound();
            }
            return greenhouse;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGreenHouse(List<GreenHouse> greenHouseList)
        {
            foreach (var greenhouse in greenHouseList)
            {
                await _edataService.UpdateGreenHouseAsync(greenhouse);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGreenhouse(int id)
        {
            var greenhouse = await _edataService.GetGreenhouseByIdAsync(id);
            if (greenhouse == null)
            {
                return NotFound();
            }

            await _edataService.DeleteGreenhouseAsync(id);

            return NoContent();
        }
    }


[Route("api/[controller]")]
[ApiController]
public class GreenHouseKController : ControllerBase
{
    private readonly EdataService _edataService;

    public GreenHouseKController(EdataService edataService)
    {
        _edataService = edataService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GreenHouseK>>> GetAllGreenHouseK()
    {
        return await _edataService.GetAllGreenHouseKAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateGreenhouseK(GreenHouseK greenhouseK)
    {
        await _edataService.CreateGreenhouseKAsync(greenhouseK);
        return CreatedAtAction(nameof(GetGreenhouseKById), new { id = greenhouseK.GreenHouseId }, greenhouseK);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GreenHouseK>> GetGreenhouseKById(int id)
    {
        var greenhouseK = await _edataService.GetGreenhouseKByIdAsync(id);
        if (greenhouseK == null)
        {
            return NotFound();
        }
        return greenhouseK;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGreenHouseK(List<GreenHouseK> greenHouseKList)
    {
        foreach (var greenhouseK in greenHouseKList)
        {
            await _edataService.UpdateGreenHouseKAsync(greenhouseK);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGreenhouseK(int id)
    {
        var greenhouseK = await _edataService.GetGreenhouseKByIdAsync(id);
        if (greenhouseK == null)
        {
            return NotFound();
        }

        await _edataService.DeleteGreenhouseKAsync(id);

        return NoContent();
    }
}

[Route("api/[controller]")]
[ApiController]
public class GreenHouseTController : ControllerBase
{
    private readonly EdataService _edataService;

    public GreenHouseTController(EdataService edataService)
    {
        _edataService = edataService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GreenHouseT>>> GetAllGreenHouseT()
    {
        var greenhouseTList = await _edataService.GetAllGreenHouseTAsync();
        return Ok(greenhouseTList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GreenHouseT>> GetGreenhouseTById(int id)
    {
        var greenhouseT = await _edataService.GetGreenhouseTByIdAsync(id);
        if (greenhouseT == null)
        {
            return NotFound();
        }
        return Ok(greenhouseT);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGreenhouseT(GreenHouseT greenhouseT)
    {
        await _edataService.CreateGreenhouseTAsync(greenhouseT);
        return CreatedAtAction(nameof(GetGreenhouseTById), new { id = greenhouseT.GreenHouseId }, greenhouseT);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGreenHouseT(List<GreenHouseT> greenHouseTList)
    {
        foreach (var greenhouseT in greenHouseTList)
        {
            await _edataService.UpdateGreenHouseTAsync(greenhouseT);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGreenhouseT(int id)
    {
        await _edataService.DeleteGreenhouseTAsync(id);
        return NoContent();
    }
}

    [Route("api/[controller]")]
    [ApiController]
    public class EdataController : ControllerBase
    {
        private readonly EdataService _edataService;

        public EdataController(EdataService edataService)
        {
            _edataService = edataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Edata>>> GetAllEdata()
        {
            return await _edataService.GetAllEdataAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdata(Edata edata)
        {
            await _edataService.CreateEdataAsync(edata);
            return CreatedAtAction(nameof(GetEdataById), new { id = edata.Environmentid }, edata);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Edata>> GetEdataById(int id)
        {
            var edata = await _edataService.GetEdataByIdAsync(id);
            if (edata == null)
            {
                return NotFound();
            }
            return edata;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEdata(List<Edata> edataList)
        {
            foreach (var edata in edataList)
            {
                await _edataService.UpdateEdataAsync(edata);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEdata(int id)
        {
            var edata = await _edataService.GetEdataByIdAsync(id);
            if (edata == null)
            {
                return NotFound();
            }

            await _edataService.DeleteEdataAsync(id);

            return NoContent();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class WaterController : ControllerBase
    {
        private readonly EdataService _edataService;

        public WaterController(EdataService edataService)
        {
            _edataService = edataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Water>>> GetAllWater()
        {
            return await _edataService.GetAllWaterAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWater(Water water)
        {
            await _edataService.CreateWaterAsync(water);
            return CreatedAtAction(nameof(GetWaterById), new { id = water.WaterId }, water);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Water>> GetWaterById(int id)
        {
            var water = await _edataService.GetWaterByIdAsync(id);
            if (water == null)
            {
                return NotFound();
            }
            return water;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWater(List<Water> waterList)
        {
            foreach (var water in waterList)
            {
                await _edataService.UpdateWaterAsync(water);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWater(int id)
        {
            var water = await _edataService.GetWaterByIdAsync(id);
            if (water == null)
            {
                return NotFound();
            }

            await _edataService.DeleteWaterAsync(id);

            return NoContent();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class WasteController : ControllerBase
    {
        private readonly EdataService _edataService;

        public WasteController(EdataService edataService)
        {
            _edataService = edataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Waste>>> GetAllWaste()
        {
            return await _edataService.GetAllWasteAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWaste(Waste waste)
        {
            await _edataService.CreateWasteAsync(waste);
            return CreatedAtAction(nameof(GetWasteById), new { id = waste.WasteId }, waste);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Waste>> GetWasteById(int id)
        {
            var waste = await _edataService.GetWasteByIdAsync(id);
            if (waste == null)
            {
                return NotFound();
            }
            return waste;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWaste(List<Waste> wasteList)
        {
            foreach (var waste in wasteList)
            {
                await _edataService.UpdateWasteAsync(waste);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaste(int id)
        {
            var waste = await _edataService.GetWasteByIdAsync(id);
            if (waste == null)
            {
                return NotFound();
            }

            await _edataService.DeleteWasteAsync(id);

            return NoContent();
        }
    }

   [Route("api/[controller]")]
    [ApiController]
    public class WaterKController : ControllerBase
{
    private readonly EdataService _edataService;

    public WaterKController(EdataService edataService)
    {
        _edataService = edataService;
    }

    [HttpGet]
    public async Task<ActionResult<List<WaterK>>> GetAllWaterK()
    {
        return await _edataService.GetAllWaterKAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WaterK>> GetWaterKById(int id)
    {
        var waterK = await _edataService.GetWaterKByIdAsync(id);
        if (waterK == null)
        {
            return NotFound();
        }
        return waterK;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWaterK(WaterK waterK)
    {
        await _edataService.CreateWaterKAsync(waterK);
        return CreatedAtAction(nameof(GetWaterKById), new { id = waterK.WaterId }, waterK);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWaterK(List<WaterK> waterKList)
    {
        foreach (var waterK in waterKList)
        {
            await _edataService.UpdateWaterKAsync(waterK);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaterK(int id)
    {
        await _edataService.DeleteWaterKAsync(id);
        return NoContent();
    }
}

    [Route("api/[controller]")]
[ApiController]
public class EdataKController : ControllerBase
{
    private readonly EdataService _edataService;

    public EdataKController(EdataService edataService)
    {
        _edataService = edataService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EdataK>>> GetAllEdataK()
    {
        return await _edataService.GetAllEdataKAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EdataK>> GetEdataKById(int id)
    {
        var edataK = await _edataService.GetEdataKByIdAsync(id);
        if (edataK == null)
        {
            return NotFound();
        }
        return edataK;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEdataK(EdataK edataK)
    {
        await _edataService.CreateEdataKAsync(edataK);
        return CreatedAtAction(nameof(GetEdataKById), new { id = edataK.Environmentid }, edataK);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEdataK(List<EdataK> edataKList)
    {
        foreach (var edataK in edataKList)
        {
            await _edataService.UpdateEdataKAsync(edataK);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEdataK(int id)
    {
        await _edataService.DeleteEdataKAsync(id);
        return NoContent();
    }
}


[Route("api/[controller]")]
[ApiController]
public class WasteTController : ControllerBase
{
    private readonly EdataService _edataService;

    public WasteTController(EdataService edataService)
    {
        _edataService = edataService;
    }

    [HttpGet]
    public async Task<ActionResult<List<WasteT>>> GetAllWasteT()
    {
        return await _edataService.GetAllWasteTAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WasteT>> GetWasteTById(int id)
    {
        var wasteT = await _edataService.GetWasteTByIdAsync(id);
        if (wasteT == null)
        {
            return NotFound();
        }
        return wasteT;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWasteT(WasteT wasteT)
    {
        await _edataService.CreateWasteTAsync(wasteT);
        return CreatedAtAction(nameof(GetWasteTById), new { id = wasteT.WasteId }, wasteT);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWasteT(List<WasteT> wasteTList)
    {
        foreach (var wasteT in wasteTList)
        {
            await _edataService.UpdateWasteTAsync(wasteT);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWasteT(int id)
    {
        await _edataService.DeleteWasteTAsync(id);
        return NoContent();
    }
}

[Route("api/[controller]")]
[ApiController]
public class WasteKController : ControllerBase
{
    private readonly EdataService _edataService;

    public WasteKController(EdataService edataService)
    {
        _edataService = edataService;
    }

    [HttpGet]
    public async Task<ActionResult<List<WasteK>>> GetAllWasteK()
    {
        return await _edataService.GetAllWasteKAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WasteK>> GetWasteKById(int id)
    {
        var wasteK = await _edataService.GetWasteKByIdAsync(id);
        if (wasteK == null)
        {
            return NotFound();
        }
        return wasteK;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWasteK(WasteK wasteK)
    {
        await _edataService.CreateWasteKAsync(wasteK);
        return CreatedAtAction(nameof(GetWasteKById), new { id = wasteK.WasteId }, wasteK);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWasteK(List<WasteK> wasteKList)
    {
        foreach (var wasteK in wasteKList)
        {
            await _edataService.UpdateWasteKAsync(wasteK);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWasteK(int id)
    {
        await _edataService.DeleteWasteKAsync(id);
        return NoContent();
    }
}

}