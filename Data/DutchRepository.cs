using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data {
    public class DutchRepository : IDutchRepository {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;
        public DutchRepository (DutchContext context, ILogger<DutchRepository> logger) {
            this._logger = logger;
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts () {
            _logger.LogInformation ("GetAllProducts was called");
            try {

                return _context.Products.OrderBy (p => p.Title)
                    .ToList ();
            } catch (Exception ex) {
                _logger.LogError ($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory (string category) {
            return _context.Products.Where (p => p.Category == category).ToList ();
        }

        public bool SaveAll () {
            return _context.SaveChanges () > 0;
        }
    }
}