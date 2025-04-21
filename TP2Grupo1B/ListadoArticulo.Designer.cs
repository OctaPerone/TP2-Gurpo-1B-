namespace TP2Grupo1B
{
    partial class ListadoArticulo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvListadoArticulos = new System.Windows.Forms.DataGridView();
            this.btnSiguienteImagen = new System.Windows.Forms.Button();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.lblListadoArticulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListadoArticulos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListadoArticulos
            // 
            this.dgvListadoArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgvListadoArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListadoArticulos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvListadoArticulos.Location = new System.Drawing.Point(51, 121);
            this.dgvListadoArticulos.MultiSelect = false;
            this.dgvListadoArticulos.Name = "dgvListadoArticulos";
            this.dgvListadoArticulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListadoArticulos.Size = new System.Drawing.Size(623, 318);
            this.dgvListadoArticulos.TabIndex = 3;
            this.dgvListadoArticulos.SelectionChanged += new System.EventHandler(this.dgvListadoArticulos_SelectionChanged);
            // 
            // btnSiguienteImagen
            // 
            this.btnSiguienteImagen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSiguienteImagen.Location = new System.Drawing.Point(751, 385);
            this.btnSiguienteImagen.Name = "btnSiguienteImagen";
            this.btnSiguienteImagen.Size = new System.Drawing.Size(167, 30);
            this.btnSiguienteImagen.TabIndex = 7;
            this.btnSiguienteImagen.Text = "Siguiente imagen";
            this.btnSiguienteImagen.UseVisualStyleBackColor = true;
            this.btnSiguienteImagen.Click += new System.EventHandler(this.btnSiguienteImagen_Click);
            // 
            // pbxImagen
            // 
            this.pbxImagen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbxImagen.Location = new System.Drawing.Point(717, 154);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(245, 225);
            this.pbxImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagen.TabIndex = 6;
            this.pbxImagen.TabStop = false;
            // 
            // lblListadoArticulo
            // 
            this.lblListadoArticulo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblListadoArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListadoArticulo.Location = new System.Drawing.Point(310, 42);
            this.lblListadoArticulo.Name = "lblListadoArticulo";
            this.lblListadoArticulo.Size = new System.Drawing.Size(318, 37);
            this.lblListadoArticulo.TabIndex = 37;
            this.lblListadoArticulo.Text = "Listado de Articulos ";
            // 
            // ListadoArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1000, 480);
            this.Controls.Add(this.lblListadoArticulo);
            this.Controls.Add(this.btnSiguienteImagen);
            this.Controls.Add(this.pbxImagen);
            this.Controls.Add(this.dgvListadoArticulos);
            this.MinimumSize = new System.Drawing.Size(992, 489);
            this.Name = "ListadoArticulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LstadoArticulo";
            this.Load += new System.EventHandler(this.ListadoArticulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListadoArticulos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvListadoArticulos;
        private System.Windows.Forms.PictureBox pbxImagen;
        private System.Windows.Forms.Button btnSiguienteImagen;
        private System.Windows.Forms.Label lblListadoArticulo;
    }
}