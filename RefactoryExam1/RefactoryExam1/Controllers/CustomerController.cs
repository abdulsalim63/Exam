using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RefactoryExam1.Models;

namespace RefactoryExam1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomersContext _context;

        public CustomerController(CustomersContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(RequestCustomer request)
        {
            var customer = request.data.attributes;
            var date = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            customer.created_at = (long)date;
            customer.updated_at = (long)date;
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok(new { message = "success add data", status = true, data = customer });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Customers.OrderBy(x => x.id);
            return Ok(new { message = "success retrieve data", status = true, data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _context.Customers.First(x => x.id == id);
                return Ok(new { message = "success retrieve data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutById(int id, RequestCustomer request)
        {
            var customer = request.data.attributes;
            try
            {
                var result = _context.Customers.First(x => x.id == id);
                result.full_name = customer.full_name;
                result.username = customer.username;
                result.email = customer.email;
                result.phone_number = customer.phone_number;
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
                var result = _context.Customers.First(x => x.id == id);
                _context.Customers.Remove(result);
                _context.SaveChanges();
                return Ok(new { message = "success delete data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }

    public class RequestCustomer
    {
        public DataCustomer data { get; set; }
    }

    public class DataCustomer
    {
        public Customers attributes { get; set; }
    }
}