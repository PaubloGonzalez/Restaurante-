using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LN_WEB.Entities
{
    public class UsuarioEnt
    {
        [Key]
        public long IdUsuario { get; set; }

        [DisplayName("Correo")]
        [MaxLength(50)]
        public string CorreoElectronico { get; set; }

        [DisplayName("Password")]
        [MaxLength(20)]
        public string Contrasenna { get; set; }

        [DisplayName("Identificacion")]
        [MaxLength(20)]
        public string Identificacion { get; set; }

        [DisplayName("Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string ContrasennaNueva { get; set; }
        public string ConfirmarContrasennaNueva { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
        public int TipoCedula { get; set; }
    }
}