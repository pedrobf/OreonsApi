using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OreonsApi.Models
{
    public class CategoryDTO
    {
        //[Required(ErrorMessage = "Id é obrigatório")]
        //public Guid? Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(70, MinimumLength = 1, ErrorMessage = "O campo Descrição deve conter no mínimo 1 e no máximo 70 caracteres.")]
        public string Description { get; set; }
        public IEnumerable<SubCategoryDTO> ChildrensCategory { get; set; }
    }
}
