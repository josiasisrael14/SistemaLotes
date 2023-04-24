namespace SistemaLotes.Models
{

    [Serializable]
    public class entidad
    {
        public String logeo { get; set; }
        public String logeoD { get; set; }
        public String contrazeña { get; set; }
        public byte[] imagen { get; set; }
        public String tipo { get; set; }
        public String nombre { get; set; }
        public int idetapas { get; set; }
        public String NombreEtapa { get; set; }
        public String Descripcion { get; set; }
        public int idlotes { get; set; }
        public String estado { get; set; }
        public String colindantes { get; set; }
        //public decimal PrecioContado { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal precioi { get; set; }
        public int LetrasPagar { get; set; }
        public int letras { get; set; }
        public byte[] ImagenLotes { get; set; }
        public byte[] imagenlotes1 { get; set; }
        public byte[] ImagenProyecto { get; set; }
        public byte[] voucher { get; set; }
       public byte[] foto { get; set; }
        public int idventas { get; set; }


        //public int preciocontado { get; set; }

        public decimal preciocontado { get; set; }
        public String nombrecliente { get; set; }
        public String nombrelotes { get; set; }


        public DateTime fechaventa { get; set; }
        public String fechaventaconversion { get; set; }

        public int idusuario { get; set; }

        public DateTime fechacreado { get; set; }

        public String etapa { get; set; }

        public int idcliente {get;set;}


        public int idventassepa { get; set; }

        //public int restante { get; set; }

        public decimal restante { get; set; }
        public DateTime fechaventaS { get; set; }

        public String nombretransaccion { get; set; }

        public String estadotransaccion { get; set; }

        public int idtransaccion { get; set; }

        public string NombreReniec { get; set; }

        public string Paterno { get; set; }
        public string Materno { get; set; }
        public int Dni { get; set; }
        public string codigoverificacion { get; set; }

        public string apellidosgeneral { get; set; }
        
        public int celular { get; set; }

        public string direccion { get; set; }

        //public int  letraspagar { get; set; }
        public decimal montopagar { get; set; }
        public int letrasrestante { get; set; }

        public int idtipo { get; set; }

        public int operacion { get; set; }
    }
}
