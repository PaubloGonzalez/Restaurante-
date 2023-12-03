using System;

namespace LN_API.Entities
{
    public class UsuarioEnt
    {
        public long IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Image { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string ContrasennaNueva { get; set; }
        public string Token { get; set; }
        public int TipoCedula { get; set; }
    }
}