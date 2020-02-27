using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RefactoryExam1.Models;

namespace RefactoryExam1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductController(ProductsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(RequestProduct request)
        {
            var products = request.data.attributes;
            var date = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            products.created_at = (long)date;
            products.updated_at = (long)date;
            _context.products.Add(products);
            _context.SaveChanges();
            return Ok(new { message = "success add data", status = true, data = products });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.products.OrderBy(x => x.id);
            return Ok(new { message = "success retrieve data", status = true, data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _context.products.First(x => x.id == id);
                return Ok(new { message = "success retrieve data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutById(int id, RequestProduct request)
        {
            var products = request.data.attributes;
            try
            {
                var result = _context.products.First(x => x.id == id);
                result.name = products.name;
                result.price = products.price;
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
                var result = _context.products.First(x => x.id == id);
                _context.products.Remove(result);
                _context.SaveChanges();
                return Ok(new { message = "success delete data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }

    public class RequestProduct
    {
        public DataProduct data { get; set; }
    }

    public class DataProduct
    {
        public Products attributes { get; set; }
    }
}
