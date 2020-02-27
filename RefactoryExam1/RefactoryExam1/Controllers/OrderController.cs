using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RefactoryExam1.Models;

namespace RefactoryExam1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrdersContext _context;
        private readonly OrderItemsContext _itemsContext;

        public OrderController(OrdersContext context, OrderItemsContext itemsContext)
        {
            _context = context;
            _itemsContext = itemsContext;
        }

        [HttpPost]
        public ActionResult Post(RequestOrder request)
        {
            var date = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            var Order = request.data.attributes;
            var Orders = new Orders
            {
                user_id = Order.user_id,
                driver_id = Order.driver_id,
                Status = "Sending",
                created_at = (long)date,
                updated_at = (long)date
            };
            _context.Orders.Add(Orders);
            _context.SaveChanges();

            foreach (var od in Order.order_detail)
            {
                od.order_id = _context.Orders.ToList().Last().id;
                _itemsContext.OrderItems.Add(od);
            }

            _itemsContext.SaveChanges();
            return Ok(new { message = "success add data", data = new { attributes = Orders } });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Orders.OrderBy(x => x.id);
            return Ok(new { message = "success retrieve data", status = true, data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _context.Orders.First(x => x.id == id);
                return Ok(new { message = "success retrieve data", status = true, data = result });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutById(int id, RequestOrder request)
        {
            var products = request.data.attributes;
            try
            {
                var result = _context.Orders.First(x => x.id == id);
                result.Status = products.Status;
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
                var result = _context.Orders.First(x => x.id == id);
                _context.Orders.Remove(result);
                _context.SaveChanges();
                return Ok(new { message = "success delete data", data = new { attributes = result } });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public class Order : Orders
        {
            public List<OrderItems> order_detail { get; set; }
        }

        public class RequestOrder
        {
            public DataOrder data { get; set; }
        }

        public class DataOrder
        {
            public Order attributes { get; set; }
        }
    }
}
