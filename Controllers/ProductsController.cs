using System;
using System.Collections.Generic;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class ProductsController : ControllerBase {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController (IDutchRepository repository, ILogger<ProductsController> logger) {
            this._logger = logger;
            this._repository = repository;
        }

        [ProducesResponseType (200)]
        [ProducesResponseType (400)]
        public ActionResult<IEnumerable<Product>> Get () {

            try {
                return Ok (_repository.GetAllProducts ());

            } catch (Exception ex) {
                _logger.LogError ($"Failed to get products: {ex}");
                return BadRequest ("Failed to get products");
            }
        }
    }
}