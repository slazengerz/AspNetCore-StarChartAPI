using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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

        [HttpGet("{id:int}",Name= "GetById")]
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

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var items = _context.CelestialObjects;
        //    foreach(var item in items)
        //    {
        //        item.Satellites = _context.CelestialObjects.Where(c => c.Id == item.Id).ToList();
        //    }
        //    return Ok(_context.CelestialObjects);
        //}

        //[HttpPost("[FromBody]CelestialObject")]
        //public IActionResult Create(CelestialObject  celstObj)
        //{
        //    _context.CelestialObjects.Add(celstObj);
        //    _context.SaveChanges();
        //    return CreatedAtRoute("GetById", new { id = celstObj.Id }, celstObj);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id,CelestialObject celstObj)
        //{
        //    var item = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);
        //    if(item == null)
        //    {
        //        return NotFound();
        //    }

        //    item.Name = celstObj.Name;
        //    item.OrbitalPeriod = celstObj.OrbitalPeriod;
        //    item.OrbitedObjectId = celstObj.OrbitedObjectId;
        //    item.Satellites = celstObj.Satellites;

        //    _context.SaveChanges();

        //    return NoContent();
        //}

        //[HttpPatch("{id}/{name}")]
        //public IActionResult RenameObject(int id,string name)
        //{
        //    var item = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);
        //    if(item == null)
        //    {
        //        return NotFound();
        //    }
        //    item.Name = name;
        //    _context.SaveChanges();
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var item = _context.CelestialObjects.Where(c => c.Id == id || c.OrbitedObjectId==id).ToList();
        //    if (item.Count==0)
        //    {
        //        return NotFound();
        //    }
        //    _context.RemoveRange(item);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
    }
}
