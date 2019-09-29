using System;

namespace OreonsApi.core.customexceptions
{
    public class ProductConflictException : Exception
    {
        /// <summary>
        /// Exceção que ocorre quando algum campo único do Produto está conflitando 
        /// com outro Produto que já existe.
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        public ProductConflictException(string message) : base(message) { }
    }
}
