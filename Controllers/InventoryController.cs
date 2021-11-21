using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tracker.Models;

namespace Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {

        TrackerContext _context;
        public InventoryController(TrackerContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
           var inventories =  _context.Inventories.ToList();

            return Ok(inventories);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddNewInventory( [FromBody] Inventory inventory)
        {
            try
            {
                _context.Inventories.Add(inventory);


                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(inventory);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteInventory([FromRoute]int id)
        {

            Inventory item;
            try
            {
                item =  _context.Inventories.FirstOrDefault(x=>x.Id == id);

                _context.Inventories.Remove(item);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(item);
        }
    }
}
