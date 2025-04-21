using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using dominio;
using negocio;

namespace TP2Grupo1B
{
    public partial class ListadoArticulo : Form
    {
        private List<Producto> listaArticulos;

        private int imagenActual = 0;
        public ListadoArticulo()
        {
            InitializeComponent();
        }

        private void CargarImagen(string imagen)
        {

            try
            {
                pbxImagen.Load(imagen);

            }
            catch
            {
                pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
            }
        }

        private void dgvListadoArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListadoArticulos.CurrentRow == null) return;

            Producto seleccionado = (Producto)dgvListadoArticulos.CurrentRow.DataBoundItem;

            if (seleccionado.Imagenes != null && seleccionado.Imagenes.Count > 0)
                CargarImagen(seleccionado.Imagenes[0].UrlImagen);
            else
            {
                imagenActual = 0;
                pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
            }
        }

        private void ListadoArticulo_Load(object sender, EventArgs e)
        {
            ProductoNegocio listado = new ProductoNegocio();
            listaArticulos = listado.listar();

            dgvListadoArticulos.DataSource = listaArticulos;

            if (dgvListadoArticulos.Columns.Contains("Imagenes"))
                dgvListadoArticulos.Columns["Imagenes"].Visible = false;

            if (listaArticulos != null && listaArticulos.Count > 0)
            {
                Producto primerArticulo = listaArticulos[0];

                if (primerArticulo.Imagenes != null && primerArticulo.Imagenes.Count > 0)
                    CargarImagen(primerArticulo.Imagenes[0].UrlImagen);

            }
            else
            {
                pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
                MessageBox.Show("No hay artículos para mostrar.");
            }
        }

       

        private void btnSiguienteImagen_Click(object sender, EventArgs e)
        {
            Producto seleccionado = (Producto)dgvListadoArticulos.CurrentRow.DataBoundItem;

            if (seleccionado.Imagenes.Count > 0)
            {
                imagenActual = (imagenActual + 1) % seleccionado.Imagenes.Count;
                CargarImagen(seleccionado.Imagenes[imagenActual].UrlImagen);
            }
        }

        
    }
}
