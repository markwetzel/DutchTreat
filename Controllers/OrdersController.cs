using System;
using System.Collections.Generic;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class OrdersController : ControllerBase {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper mapper;

        public OrdersController (IDutchRepository repository, ILogger<ProductsController> logger, IMapper mapper) {
            this._logger = logger;
            this.mapper = mapper;
            this._repository = repository;
        }

        [HttpGet]
        public IActionResult Get () {
            try {
                return Ok (mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>> (_repository.GetAllOrders ()));
            } catch (Exception ex) {
                _logger.LogError ($"Failed to get orders: {ex}");
                return BadRequest ("Failed to get orders");
            }
        }

        [HttpGet ("{id:int}")]
        public ActionResult<Order> Get (int id) {
            try {
                var order = _repository.GetOrderById (id);
                if (order != null) {
                    return Ok (mapper.Map<Order, OrderViewModel> (order));
                } else {
                    return NotFound ();
                }
            } catch (Exception ex) {
                _logger.LogError ($"Failed to get orders: {ex}");
                return BadRequest ("Failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post ([FromBody] OrderViewModel model) {
            try {
                if (ModelState.IsValid) {

                    var newOrder = mapper.Map<OrderViewModel, Order> (model);

                    if (newOrder.OrderDate == DateTime.MinValue) {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity (newOrder);

                    if (_repository.SaveAll ()) {
                        return Created ($"/api/orders/{newOrder.Id}", mapper.Map<Order, OrderViewModel> (newOrder));
                    }
                } else {
                    return BadRequest (ModelState);
                }
            } catch (Exception ex) {
                _logger.LogError ($"Failed to save a new order: { ex }");
            }
            return BadRequest ("Failed to save new order ");
        }
    }
}