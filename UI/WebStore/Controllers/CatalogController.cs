﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebStore.Domain.DTO.Products;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using  WebStore.Services.Mapping;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private const string __PageSize = "PageSize";

        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;
        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _ProductData = ProductData;
            _Configuration = Configuration;
        }
        public IActionResult Shop(int? SectionId, int? BrandId, int Page = 1)
        {
            var page_size = int.TryParse(_Configuration[__PageSize], out var size) ? size : (int?)null;
            //var page_size = int.TryParse(_Configuration["PageSize"], out var size) ? size : (int?)null;

            var filter = new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = page_size
            };
            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.Products.Select(ProductMapping.FromDTO).Select(ProductMapping.ToView).OrderBy(p => p.Order),
                PageViewModel = new PageViewModel
                {
                    PageSize = page_size ?? 0,
                    PageNumber = Page,
                    TotalItems = products.TotalCount
                }
            });
        }
        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO().ToView());
        }

        #region API

        public IActionResult GetFilteredItems(int? SectionId, int? BrandId, int Page)
        {
            var products =
                GetProducts(SectionId, BrandId, Page)
                   .Select(ProductMapping.FromDTO)
                   .Select(ProductMapping.ToView)
                   .OrderBy(p => p.Order);
            return PartialView("Partial/_FeaturesItems", products);
        }

        private IEnumerable<ProductDTO> GetProducts(int? SectionId, int? BrandId, int Page) =>
            _ProductData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = int.Parse(_Configuration[__PageSize])
            })
            .Products;

        #endregion
    }
}