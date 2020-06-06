namespace Creador_de_ciudades
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxCasas = new System.Windows.Forms.GroupBox();
            this.groupBoxInteriores = new System.Windows.Forms.GroupBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ui_cantidad_pisos = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_cantidad_pisos)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.temaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(803, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // temaToolStripMenuItem
            // 
            this.temaToolStripMenuItem.Name = "temaToolStripMenuItem";
            this.temaToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.temaToolStripMenuItem.Text = "Tema";
            // 
            // groupBoxCasas
            // 
            this.groupBoxCasas.Location = new System.Drawing.Point(368, 157);
            this.groupBoxCasas.Name = "groupBoxCasas";
            this.groupBoxCasas.Size = new System.Drawing.Size(212, 293);
            this.groupBoxCasas.TabIndex = 4;
            this.groupBoxCasas.TabStop = false;
            this.groupBoxCasas.Text = "Propiedades de las casas";
            // 
            // groupBoxInteriores
            // 
            this.groupBoxInteriores.Location = new System.Drawing.Point(586, 157);
            this.groupBoxInteriores.Name = "groupBoxInteriores";
            this.groupBoxInteriores.Size = new System.Drawing.Size(212, 293);
            this.groupBoxInteriores.TabIndex = 5;
            this.groupBoxInteriores.TabStop = false;
            this.groupBoxInteriores.Text = "Propiedades de los interiores";
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Location = new System.Drawing.Point(12, 24);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(350, 426);
            this.TabControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(342, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PB";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(370, 388);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.ui_cantidad_pisos);
            this.groupBoxGeneral.Controls.Add(this.label1);
            this.groupBoxGeneral.Location = new System.Drawing.Point(368, 24);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(430, 127);
            this.groupBoxGeneral.TabIndex = 7;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "Propiedades de las plantas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cantidad de pisos:";
            // 
            // ui_cantidad_pisos
            // 
            this.ui_cantidad_pisos.Location = new System.Drawing.Point(116, 20);
            this.ui_cantidad_pisos.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ui_cantidad_pisos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ui_cantidad_pisos.Name = "ui_cantidad_pisos";
            this.ui_cantidad_pisos.Size = new System.Drawing.Size(47, 20);
            this.ui_cantidad_pisos.TabIndex = 1;
            this.ui_cantidad_pisos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ui_cantidad_pisos.ValueChanged += new System.EventHandler(this.ui_cantidad_pisos_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 453);
            this.Controls.Add(this.groupBoxGeneral);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.groupBoxInteriores);
            this.Controls.Add(this.groupBoxCasas);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_cantidad_pisos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temaToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxCasas;
        private System.Windows.Forms.GroupBox groupBoxInteriores;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ui_cantidad_pisos;
    }
}

