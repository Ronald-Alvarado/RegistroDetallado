using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroDetallado.Entidades
{
    public class TelefonosDetalle 
    {
        [Key]
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public String TipoTelefono { get; set; }
        public String Telefono { get; set; }

        //[ForeignKey("PersonaId")]
        public virtual Personas Personas { get; set; }

        public TelefonosDetalle()
        {
            Id = 0;
            PersonaId = 0;
            TipoTelefono = String.Empty;
            Telefono = String.Empty;
        }
    }

    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public String Nombre { get; set; }
        public String Cedula { get; set; }
        public String Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [ForeignKey("PersonaId")]
        public virtual List<TelefonosDetalle> Telefonos { get; set; }

        public Personas()
        {
            PersonaId = 0;
            Nombre = String.Empty;
            Cedula = String.Empty;
            Direccion = String.Empty;
            FechaNacimiento = DateTime.Now;

            Telefonos = new List<TelefonosDetalle>();
        }
    }
}
