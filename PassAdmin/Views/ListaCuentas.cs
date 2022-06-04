using PassAdmin.Model;
using PassAdmin.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace PassAdmin.Views
{
    public partial class ListaCuentas : Form
    {
        List<Cuenta> cuentas;
        public int id;
        Cuenta SelectedCuenta;
        public ListaCuentas(int id)
        {
            InitializeComponent();
            this.id = id;
            GetData(id);
        }
        private void GetData(int id)
        {
            SqLiteDbContext db = new SqLiteDbContext();
            cuentas = db.GetAllCuentas(id);
            for (int i = 0; i < cuentas.Count; i++)
            {
                String[] items = new string[] { cuentas[i].ID.ToString(), cuentas[i].NombreSitio, cuentas[i].URL, cuentas[i].Usuario };
                this.dataGridView1.Rows.Add(items);
            }
        }
        public void Refresh()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();
            SqLiteDbContext db = new SqLiteDbContext();
            cuentas = db.GetAllCuentas(this.id);
            for (int i = 0; i < cuentas.Count; i++)
            {
                String[] items = new string[] { cuentas[i].ID.ToString(), cuentas[i].NombreSitio, cuentas[i].URL, cuentas[i].Usuario };
                this.dataGridView1.Rows.Add(items);
            }
        }
        private void GetData(List<Cuenta> _cuentas)
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();
            SqLiteDbContext db = new SqLiteDbContext();
            for (int i = 0; i < _cuentas.Count; i++)
            {
                String[] items = new string[] { _cuentas[i].ID.ToString(), _cuentas[i].NombreSitio, _cuentas[i].URL, _cuentas[i].Usuario };
                this.dataGridView1.Rows.Add(items);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string texto = (sender as TextBox).Text;
            List<Cuenta> tmp = cuentas.Where(s => s.Nota.Contains(texto) || s.URL.Contains(texto)).ToList();
            GetData(tmp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCuenta frmCuenta = new AddCuenta(this);
            frmCuenta.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            SelectedCuenta = cuentas[index];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedCuenta == null)
            {
                MessageBox.Show(this, "No se ha seleccionado ningun registro", "Error", MessageBoxButtons.OK);
                return;
            }
            AddCuenta frmCuenta = new AddCuenta(this, SelectedCuenta);
            frmCuenta.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SelectedCuenta == null)
            {
                MessageBox.Show(this, "No se ha seleccionado ningun registro", "Error", MessageBoxButtons.OK);
                return;
            }
            SqLiteDbContext db = new SqLiteDbContext();
            var resp = db.EliminarCuenta(SelectedCuenta);
            if (resp.IsSuccess)
            {
                Refresh();
            }
            else
            {
                MessageBox.Show(this,resp.Message,"Error", MessageBoxButtons.OK);
            }
        }
    }
}
