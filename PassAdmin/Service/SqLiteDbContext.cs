using PassAdmin.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassAdmin.Service
{
    public class SqLiteDbContext
    {
        SQLiteConnection db;
        public SqLiteDbContext()
        {
            // Get an absolute path to the database file
            var databasePath = Path.Combine(Application.StartupPath, "MyData.db");

            if (!File.Exists(databasePath))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(databasePath);
                db = new SQLiteConnection(databasePath);
                db.CreateTable<Usuario>();
                db.CreateTable<Cuenta>();
            }
            db = new SQLiteConnection(databasePath);
        }
        public Response InsertUsuario(Usuario item)
        {
            try
            {
                db.Insert(item);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Registro insertado"
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
        public Response Login(Usuario item)
        {
            var query = db.Table<Usuario>().Where(s => s.Username.Equals(item.Username) && s.Password.Equals(item.Password));
            var result = query.ToList().FirstOrDefault();
            if (result == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Credenciales invalidas"
                };
            }
            return new Response
            {
                IsSuccess = true,
                Result = result
            };
        }
        public List<Cuenta> GetAllCuentas(int id)
        {
            return db.Table<Cuenta>().Where(s => s.UsuarioID.Equals(id)).ToList();
        }
        public Response CrearCuenta(Cuenta item)
        {
            try
            {
                db.Insert(item);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Cuenta agregada"
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
        public Response ActualizarCuenta(Cuenta item)
        {
            try
            {
                db.Update(item);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Cuenta actualizada"
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
        public Response EliminarCuenta(Cuenta item)
        {
            try
            {
                db.Delete(item);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Cuenta eliminada"
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
    }
}
