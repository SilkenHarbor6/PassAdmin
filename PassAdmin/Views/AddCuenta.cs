using PassAdmin.Model;
using PassAdmin.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassAdmin.Views
{
    public partial class AddCuenta : Form
    {
        ListaCuentas parent;
        bool isNew=true;
        int id;
        bool isPasswordHidden=true;
        public AddCuenta(ListaCuentas parent,Cuenta item=null)
        {
            InitializeComponent();
            this.button3.Text = "Guardar";
            this.parent = parent;
            if (item!=null)
            {
                this.textBox1.Text = item.NombreSitio;
                this.textBox2.Text = item.URL;
                this.textBox3.Text = item.Usuario;
                this.textBox4.Text = item.Password;
                this.textBox5.Text = item.Nota;
                isNew = false;
                id = item.ID;
                this.button3.Text = "Actualizar";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cuenta cuenta = new Cuenta
            {
                NombreSitio = this.textBox1.Text,
                URL = this.textBox2.Text,
                Usuario = this.textBox3.Text,
                Password = this.textBox4.Text,
                Nota = this.textBox5.Text,
                UsuarioID = parent.id
            };
            SqLiteDbContext db = new SqLiteDbContext();
            Response resp;
            if (isNew)
            {
                resp = db.CrearCuenta(cuenta);

            }
            else
            {
                cuenta.ID=this.id;
                resp = db.ActualizarCuenta(cuenta);
            }
            if (resp.IsSuccess)
            {
                MessageBox.Show(this, resp.Message, "Exito", MessageBoxButtons.OK);
                parent.Refresh();
            }
            else
            {
                MessageBox.Show(this, resp.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isPasswordHidden)
            {
                this.textBox4.PasswordChar = '\0';
                isPasswordHidden= false;
            }
            else
            {
                this.textBox4.PasswordChar = '*';
                isPasswordHidden = true;
            }
            
        }
    }
}
