﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    /// <summary>
    /// Каталог товаров
    /// </summary>
    public interface IProductData
    {
        /// <summary>
        /// Получить все секции
        /// </summary>
        /// <returns>Перечисление секций каталога</returns>
        IEnumerable<Section> GetSections();


        /// <summary>
        /// Получить все бренды
        /// </summary>
        /// <returns>Перечисление брендов каталога</returns>
        IEnumerable<Brand> GetBrands();

        /// <summary>
        /// Получить товары
        /// </summary>
        /// <param name="Filter">Критерии поиска/фильтрации</param>
        /// <returns>Искомые товары из каталога товаров</returns>
        IEnumerable<Product> GetProducts(ProductFilter Filter = null);


        /// <summary>Получить товар по идентификатору</summary>
        /// <param name="id">Идентификатор требуемого товара</param>
        /// <returns>Товар</returns>
        Product GetProductById(int id);
    }
}