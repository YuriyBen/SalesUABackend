using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesUA.Entities;
using SalesUA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Controllers
{
    [ApiController]
    [Route("api/shop")]
    public class ShopController:ControllerBase
    {
        private readonly ILogger<ShopController> _logger;
        private readonly SalesProjectContext _context;
        private readonly IMapper _mapper;

        public ShopController(ILogger<ShopController> logger, SalesProjectContext context ,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context =context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult AllShopsWithoutProducts()
        {
            try
            {
                List<Shop> listOfAllShops = _context.Shop.ToList();
                var shopsToReturn = _mapper.Map<IEnumerable<ShopDTO>>(listOfAllShops);
                return Ok(shopsToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all supermarkets : {ex}");
                return BadRequest();
            }
        }

       
    }
}
