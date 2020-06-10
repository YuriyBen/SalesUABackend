﻿using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesUA.Entities;
using SalesUA.Helpers;
using SalesUA.Helpers.ATB;
using SalesUA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Controllers
{
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly SalesProjectContext _context;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, SalesProjectContext context, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("api/shops/{shopId}/products")]
        public IActionResult Get(int shopId)
        {

            try
            {
                var ShopById = _context.Shop.FirstOrDefault(x => x.Id == shopId);
                var listOfProductsByShopId = ShopById.Product.ToList();

                var getProductsByParsing = ParsingPagesUsingShopId.GetListOfProductViaShopTitle(ShopById.Title);  //ParsingATBpage.Parsing();

                return Ok(_mapper.Map<IEnumerable<ProductDTO>>(getProductsByParsing));
            }
            catch (Exception ex)
            {
                string shopTitle = _context.Shop.Where(s => s.Id == shopId).Select(x => x.Title).FirstOrDefault();
                _logger.LogError($"Failed to get all products in {shopTitle} : {ex}");
                return BadRequest();
            }

        }

    }
}
