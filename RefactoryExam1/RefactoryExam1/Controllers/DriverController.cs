using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RefactoryExam1.Models;

namespace RefactoryExam1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly DriversContext _context;

        public DriverController(DriversContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(RequestDriver request)
        {
            var Drivers = request.data.attributes;
            var date = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            Drivers.created_at = (long)date;
            Drivers.updated_at = (long)date;
            _context.Drivers.Add(Drivers);
            _context.SaveChanges();
            return Ok(new { message = "success add data", status = true, data = Drivers });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Drivers.OrderBy(x => x.id);
            return Ok(new { message = "success retrieve data", status = true, data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _context.Drivers.First(x => x.id == id);
                return Ok(new { message = "success retrieve data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutById(int id, RequestDriver request)
        {
            var products = request.data.attributes;
            try
            {
                var result = _context.Drivers.First(x => x.id == id);
                result.full_name = products.full_name;
                result.updated_at = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
                _context.SaveChanges();
                return Ok(new { message = "success update data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                var result = _context.Drivers.First(x => x.id == id);
                _context.Drivers.Remove(result);
                _context.SaveChanges();
                return Ok(new { message = "success delete data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }

    public class RequestDriver
    {
        public DataDriver data { get; set; }
    }

    public class DataDriver
    {
        public Drivers attributes { get; set; }
    }
}
