using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Domain.Entities.Base
{
    public abstract class NamedEntity : BaseEntity, INamedEntity
    {
        /// <summary>Имя</summary>
        [Required/*, StringLength(250)*/]
        public string Name { get; set; }
    }
}
