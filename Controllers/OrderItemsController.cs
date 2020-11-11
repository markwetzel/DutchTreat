using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers {
    [Route ("/api/orders/{orderId}/items")]
    public class OrderItemsController : Controller {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController (IDutchRepository repository, ILogger<OrderItemsController> logger, IMapper mapper) {
            this._repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get (int orderId) {
            var order = _repository.GetOrderById (orderId);
            if (order != null) {
                return Ok (mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>> (order.Items));
            } else {
                return NotFound ();
            }
        }

        [HttpGet ("{id}")]
        public IActionResult Get (int orderId, int id) {
            var order = _repository.GetOrderById (orderId);
            if (order != null) {
                var item = order.Items.Where (i => i.Id == id).FirstOrDefault ();
                if (item != null) {
                    return Ok (mapper.Map<OrderItem, OrderItemViewModel> (item));
                }
            }
            return NotFound ();
        }
    }
}