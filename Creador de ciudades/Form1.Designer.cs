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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ui_max_alto_casa = new System.Windows.Forms.NumericUpDown();
            this.ui_max_ancho_casa = new System.Windows.Forms.NumericUpDown();
            this.ui_min_alto_casa = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ui_min_ancho_casa = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ui_grosor_pared = new System.Windows.Forms.NumericUpDown();
            this.ui_cantidad_casas = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxInteriores = new System.Windows.Forms.GroupBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.ui_cantidad_pisos = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ui_quitar_todo = new System.Windows.Forms.Button();
            this.ui_construir = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBoxCasas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_max_alto_casa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_max_ancho_casa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_min_alto_casa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_min_ancho_casa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_grosor_pared)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_cantidad_casas)).BeginInit();
            this.TabControl.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_cantidad_pisos)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.groupBoxCasas.Controls.Add(this.groupBox1);
            this.groupBoxCasas.Controls.Add(this.label4);
            this.groupBoxCasas.Controls.Add(this.label3);
            this.groupBoxCasas.Controls.Add(this.ui_grosor_pared);
            this.groupBoxCasas.Controls.Add(this.ui_cantidad_casas);
            this.groupBoxCasas.Controls.Add(this.label2);
            this.groupBoxCasas.Location = new System.Drawing.Point(368, 117);
            this.groupBoxCasas.Name = "groupBoxCasas";
            this.groupBoxCasas.Size = new System.Drawing.Size(212, 266);
            this.groupBoxCasas.TabIndex = 4;
            this.groupBoxCasas.TabStop = false;
            this.groupBoxCasas.Text = "Propiedades de las casas";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ui_max_alto_casa);
            this.groupBox1.Controls.Add(this.ui_max_ancho_casa);
            this.groupBox1.Controls.Add(this.ui_min_alto_casa);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ui_min_ancho_casa);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 92);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Medidas casas";
            // 
            // ui_max_alto_casa
            // 
            this.ui_max_alto_casa.Location = new System.Drawing.Point(135, 62);
            this.ui_max_alto_casa.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.ui_max_alto_casa.Name = "ui_max_alto_casa";
            this.ui_max_alto_casa.Size = new System.Drawing.Size(47, 20);
            this.ui_max_alto_casa.TabIndex = 7;
            this.ui_max_alto_casa.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // ui_max_ancho_casa
            // 
            this.ui_max_ancho_casa.Location = new System.Drawing.Point(135, 34);
            this.ui_max_ancho_casa.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.ui_max_ancho_casa.Name = "ui_max_ancho_casa";
            this.ui_max_ancho_casa.Size = new System.Drawing.Size(47, 20);
            this.ui_max_ancho_casa.TabIndex = 6;
            this.ui_max_ancho_casa.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // ui_min_alto_casa
            // 
            this.ui_min_alto_casa.Location = new System.Drawing.Point(59, 62);
            this.ui_min_alto_casa.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.ui_min_alto_casa.Name = "ui_min_alto_casa";
            this.ui_min_alto_casa.Size = new System.Drawing.Size(47, 20);
            this.ui_min_alto_casa.TabIndex = 5;
            this.ui_min_alto_casa.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(132, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Máximo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Mínimo";
            // 
            // ui_min_ancho_casa
            // 
            this.ui_min_ancho_casa.Location = new System.Drawing.Point(59, 34);
            this.ui_min_ancho_casa.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.ui_min_ancho_casa.Name = "ui_min_ancho_casa";
            this.ui_min_ancho_casa.Size = new System.Drawing.Size(47, 20);
            this.ui_min_ancho_casa.TabIndex = 2;
            this.ui_min_ancho_casa.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Alto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ancho:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "cm.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Grosor pared:";
            // 
            // ui_grosor_pared
            // 
            this.ui_grosor_pared.Location = new System.Drawing.Point(118, 19);
            this.ui_grosor_pared.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.ui_grosor_pared.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.ui_grosor_pared.Name = "ui_grosor_pared";
            this.ui_grosor_pared.Size = new System.Drawing.Size(47, 20);
            this.ui_grosor_pared.TabIndex = 2;
            this.ui_grosor_pared.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // ui_cantidad_casas
            // 
            this.ui_cantidad_casas.Location = new System.Drawing.Point(118, 49);
            this.ui_cantidad_casas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ui_cantidad_casas.Name = "ui_cantidad_casas";
            this.ui_cantidad_casas.Size = new System.Drawing.Size(47, 20);
            this.ui_cantidad_casas.TabIndex = 1;
            this.ui_cantidad_casas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cantidad de casas:";
            // 
            // groupBoxInteriores
            // 
            this.groupBoxInteriores.Location = new System.Drawing.Point(586, 117);
            this.groupBoxInteriores.Name = "groupBoxInteriores";
            this.groupBoxInteriores.Size = new System.Drawing.Size(212, 266);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(342, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PB";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.ui_cantidad_pisos);
            this.groupBoxGeneral.Controls.Add(this.label1);
            this.groupBoxGeneral.Location = new System.Drawing.Point(368, 24);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(430, 87);
            this.groupBoxGeneral.TabIndex = 7;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "Propiedades de las plantas";
            // 
            // ui_cantidad_pisos
            // 
            this.ui_cantidad_pisos.Location = new System.Drawing.Point(118, 19);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cantidad de pisos:";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ui_quitar_todo);
            this.groupBox2.Controls.Add(this.ui_construir);
            this.groupBox2.Location = new System.Drawing.Point(368, 387);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 63);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // ui_quitar_todo
            // 
            this.ui_quitar_todo.Location = new System.Drawing.Point(234, 19);
            this.ui_quitar_todo.Name = "ui_quitar_todo";
            this.ui_quitar_todo.Size = new System.Drawing.Size(75, 23);
            this.ui_quitar_todo.TabIndex = 1;
            this.ui_quitar_todo.Text = "Quitar todo";
            this.ui_quitar_todo.UseVisualStyleBackColor = true;
            // 
            // ui_construir
            // 
            this.ui_construir.Location = new System.Drawing.Point(118, 19);
            this.ui_construir.Name = "ui_construir";
            this.ui_construir.Size = new System.Drawing.Size(75, 23);
            this.ui_construir.TabIndex = 0;
            this.ui_construir.Text = "Construir ";
            this.ui_construir.UseVisualStyleBackColor = true;
            this.ui_construir.Click += new System.EventHandler(this.ui_construir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 453);
            this.Controls.Add(this.groupBox2);
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
            this.groupBoxCasas.ResumeLayout(false);
            this.groupBoxCasas.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_max_alto_casa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_max_ancho_casa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_min_alto_casa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_min_ancho_casa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_grosor_pared)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_cantidad_casas)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_cantidad_pisos)).EndInit();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ui_cantidad_pisos;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown ui_cantidad_casas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ui_grosor_pared;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown ui_max_alto_casa;
        private System.Windows.Forms.NumericUpDown ui_max_ancho_casa;
        private System.Windows.Forms.NumericUpDown ui_min_alto_casa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown ui_min_ancho_casa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ui_quitar_todo;
        private System.Windows.Forms.Button ui_construir;
    }
}

