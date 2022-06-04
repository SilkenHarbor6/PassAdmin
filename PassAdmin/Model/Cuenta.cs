using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassAdmin.Model
{
    public class Cuenta
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        [Indexed]
        public int UsuarioID { get; set; }
        public string NombreSitio { get; set; }
        public string URL { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nota { get; set; }

    }
}
