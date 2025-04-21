using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TP2Grupo1B
{
    public partial class AgregarImagen : Form
    {
        public string UrlIngresada { get; private set; }
        public AgregarImagen()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string url = txtUrlImagen.Text.Trim();

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute) &&
                (url.EndsWith(".jpg") || url.EndsWith(".png") || url.EndsWith(".jpeg") || url.EndsWith(".gif")))
            {
                UrlIngresada = url;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("URL inválida. Asegurate que sea una imagen válida (jpg, png, jpeg, gif).");
            }

        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            CargarImagen(txtUrlImagen.Text); 
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
