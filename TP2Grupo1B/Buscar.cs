using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TP2Grupo1B
{
    public partial class Buscar : Form
    {
        private List<Producto> listaArticulos;

        public Buscar()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string Marca = cboMarca.Text;
            string Categoria = cboCategoria.Text;
            decimal? precio = null; 

            if (!string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                string precioTexto = txtPrecio.Text.Replace(",", ".");

                decimal valor;
                bool esValido = decimal.TryParse(precioTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out valor); 

                if (esValido)
                {
                    precio = valor;
                }
                else
                {
                    MessageBox.Show("Ingrese un precio válido (por ejemplo: 123.45)");
                    return;
                }
            }



            // Ejecutar la búsqueda con los parámetros validados
            List<Producto> articulos = new List<Producto>();
            articulos.AddRange(negocio.buscar(codigo, nombre, descripcion, Marca, Categoria, precio));
            dataGridViewListado.DataSource = articulos;
            if (dataGridViewListado.Columns.Contains("Imagenes"))
                dataGridViewListado.Columns["Imagenes"].Visible = false;
            if (articulos == null || articulos.Count == 0)
            {
                MessageBox.Show("Realice una busqueda con valores existentes");
                return;
            }



        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            cboMarca.SelectedIndex = -1;
            cboCategoria.SelectedIndex = -1;
            txtPrecio.Text = "";
            dataGridViewListado.DataSource = "";
            pbxImagen.Image= TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;

        }

        private void dataGridViewListado_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewListado.CurrentRow == null) return;

            Producto seleccionado = (Producto)dataGridViewListado.CurrentRow.DataBoundItem;

            if (seleccionado.Imagenes != null && seleccionado.Imagenes.Count > 0)
            {
                imagenActual = 0;
                CargarImagen(seleccionado.Imagenes[imagenActual].UrlImagen);
            }
            else
            {
                imagenActual = 0;
                pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
            }
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

        private void Buscar_Load(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
                listaArticulos = negocio.listar(); // <- ESTA ES LA PARTE QUE FALTABA
                dataGridViewListado.DataSource = listaArticulos;

                if (dataGridViewListado.Columns.Contains("Imagenes"))
                    dataGridViewListado.Columns["Imagenes"].Visible = false;

                if (listaArticulos != null && listaArticulos.Count > 0)
                {
                    Producto primerArticulo = listaArticulos[0];
                    dataGridViewListado.ClearSelection();
                    dataGridViewListado.Rows[0].Selected = true;

                    if (primerArticulo.Imagenes != null && primerArticulo.Imagenes.Count > 0)
                    {
                        imagenActual = 0;
                        CargarImagen(primerArticulo.Imagenes[imagenActual].UrlImagen);
                    }
                    else
                    {
                        imagenActual = 0;
                        pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
                    }
                }
                else
                {
                    pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
                    MessageBox.Show("No hay artículos para mostrar.");
                }

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                MarcaNegocio marcaNegocio = new MarcaNegocio();

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.SelectedIndex = -1;
                cboCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto seleccionado;

            try
            {
                if (dataGridViewListado.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccioná un producto para eliminar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "¿Estás seguro que quieres eliminar este producto?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    seleccionado = (Producto)dataGridViewListado.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.Id);

                    Cargar();
                    pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
                }
                else
                {
                    MessageBox.Show("El producto no ha sido eliminado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Cargar()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> articulos = negocio.listar(); // Obtener la lista de productos actualizada
            dataGridViewListado.DataSource = articulos;
            if (dataGridViewListado.Columns.Contains("UrlImagen"))
                dataGridViewListado.Columns["UrlImagen"].Visible = false; // Ocultar la columna de la imagen
        }

        private int imagenActual = 0;

        private void btnSiguienteimagen_Click(object sender, EventArgs e)
        {
            Producto seleccionado = (Producto)dataGridViewListado.CurrentRow.DataBoundItem;

            if (seleccionado.Imagenes.Count > 0)
            {
                imagenActual = (imagenActual + 1) % seleccionado.Imagenes.Count;
                CargarImagen(seleccionado.Imagenes[imagenActual].UrlImagen);
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (dataGridViewListado.CurrentRow == null)
            {
                MessageBox.Show("Por favor seleccione un artículo.");
                return;
            }
            AgregarImagen imagen = new AgregarImagen();
            var result = imagen.ShowDialog();
            if (result == DialogResult.OK)
            {
                string url = imagen.UrlIngresada;
                Producto seleccionado = (Producto)dataGridViewListado.CurrentRow.DataBoundItem;

                Imagen nueva = new Imagen { UrlImagen = url };
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                imagenNegocio.agregarImagen(seleccionado.Codigo, nueva);

                if (seleccionado.Imagenes == null)
                    seleccionado.Imagenes = new List<Imagen>();

                seleccionado.Imagenes.Add(nueva);

                if (seleccionado.Imagenes.Count == 1)
                {
                    imagenActual = 0;
                    CargarImagen(nueva.UrlImagen);
                }

                MessageBox.Show("Imagen agregada correctamente.");
            }
        }

        private void btnEliminarimagen_Click(object sender, EventArgs e)
        {
            if (dataGridViewListado.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un artículo primero.");
                return;
            }

            Producto seleccionado = (Producto)dataGridViewListado.CurrentRow.DataBoundItem;

            if (seleccionado.Imagenes == null || seleccionado.Imagenes.Count == 0)
            {
                MessageBox.Show("Este artículo no tiene imágenes para eliminar.");
                return;
            }

            Imagen imagenAEliminar = seleccionado.Imagenes[imagenActual];

            DialogResult respuesta = MessageBox.Show("¿Estás seguro que querés eliminar esta imagen?", "Eliminar imagen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    ImagenNegocio negocio = new ImagenNegocio();
                    negocio.eliminarImagen(imagenAEliminar.Id);

                    seleccionado.Imagenes.RemoveAt(imagenActual);

                    if (seleccionado.Imagenes.Count == 0)
                    {
                        pbxImagen.Image = TP2Grupo1B.Properties.Resources.istockphoto_1409329028_612x612;
                        imagenActual = 0;
                    }
                    else
                    {
                        imagenActual = imagenActual % seleccionado.Imagenes.Count;
                        CargarImagen(seleccionado.Imagenes[imagenActual].UrlImagen);
                    }

                    MessageBox.Show("Imagen eliminada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la imagen: " + ex.ToString());
                }
            }
        }
    }
}
