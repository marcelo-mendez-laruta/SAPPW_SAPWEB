using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Comunes
{
    public class IdentificadorClienteNatural
    {
        /// <summary>
        /// Parte numérica del IDC del cliente.
        /// </summary>
        /// <required>true</required>
        /// <example>06036920</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo no puede estar vacío de no tener mandar 00.")]
        [MaxLength(8, ErrorMessage = "El campo solo puede tener una longitud de 8 carácter.")]
        public string idcNumero { get; set; }
        /// <summary>
        /// Tipo de documento del IDC del cliente.
        /// </summary>
        /// <example>Q</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo no puede estar vacío.")]
        [MaxLength(1, ErrorMessage = "El campo solo puede tener una longitud de 1 carácter.")]
        [RegularExpression(@"^[PQRYXZ]{1}$", ErrorMessage = "Solo se permite: Q, P, R, Y, X, Z")]
        public string idcTipo { get; set; }
        /// <summary>
        /// Extensión del documento IDC del cliente.
        /// </summary>
        /// <example>LP</example>
        [MaxLength(3, ErrorMessage = "El campo solo puede tener una longitud de 3 carácter.")]
        public string idcExtension { get; set; }
        /// <summary>
        /// Complemento del IDC del cliente si se trata de una Persona Natural, por defecto será “00”.
        /// </summary>
        /// <example>00</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo no puede estar vacío de no tener mandar 00.")]
        [MaxLength(2, ErrorMessage = "El campo solo puede tener una longitud de 2 carácter.")]
        public string idcComplemento { get; set; }
    }
}
