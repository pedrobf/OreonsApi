using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OreonsApi.Models
{
    public class ProductDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(70, MinimumLength = 1, ErrorMessage = "O campo Nome deve conter no mínimo 1 e no máximo 70 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O campo descrição deve conter no mínimo 1 e no máximo 100 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo preço é obrigatório.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Preço inválido.")]
        public decimal SellingPrice { get; set; }

        [Required(ErrorMessage = "Id Categoria é obrigatório")]
        public string CategoryId { get; set; }
        public string ChildrenCategoryId { get; set; }
        public ImageDTO Images { get; set; }
    }
}
