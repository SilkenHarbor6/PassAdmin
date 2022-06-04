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
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;
            username = this.textBox1.Text;
            password = this.textBox2.Text;
            SqLiteDbContext conn = new SqLiteDbContext();
            Usuario item = new Usuario
            {
                Username = username,
                Password = password
            };
            var resp = conn.InsertUsuario(item);
            if (resp.IsSuccess)
            {
                MessageBox.Show(this, resp.Message, "Exito", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(this, resp.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
