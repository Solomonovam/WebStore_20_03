using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Упорядочивемая сущность
    /// </summary>
    public interface IOrderEntity : IBaseEntity
    {
        /// <summary>
        /// Порядковый номер
        /// </summary>
        int Order { get; set; }
    }
}
