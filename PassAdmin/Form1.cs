using PassAdmin.Model;
using PassAdmin.Service;
using PassAdmin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassAdmin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqLiteDbContext db = new SqLiteDbContext();
            Usuario item = new Usuario
            {
                Username = this.textBox1.Text,
                Password = this.textBox2.Text
            };
            var resp = db.Login(item);
            if (resp.IsSuccess)
            {
                ListaCuentas frmLista = new ListaCuentas(((Usuario)resp.Result).ID);
                frmLista.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show(this, "Credenciales invalidas", "Error", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registro frmRegistro = new Registro();
            frmRegistro.Show();
        }
    }
}
