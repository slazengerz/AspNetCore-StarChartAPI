using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Name=GetById,{id:int")]
        public IActionResult GetById(int id)
        {
            var celstObj = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);
            if (celstObj == null)
            {
                return NotFound();
            }

            celstObj.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();
            return Ok(celstObj);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celstObjs = _context.CelestialObjects.Where(c => c.Name == name).ToList();
            if (celstObjs.Count == 0)
            {
                return NotFound();
            }
            foreach (var celstObj in celstObjs)
            {
                celstObj.Satellites = _context.CelestialObjects.Where(c => c.Id == celstObj.Id).ToList();
            }
            return Ok(celstObjs);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.CelestialObjects);
        }
    }
}
