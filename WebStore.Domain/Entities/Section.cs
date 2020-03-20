using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;


namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Секция товаров
    /// </summary>
    public class Section : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }

        /// <summary>
        /// Id родительской секции
        /// </summary>
        public int? ParentId { get; set; }
    }
}
