using Creador_de_ciudades.Clases;
using Creador_de_ciudades.Clases_estaticas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;



namespace Creador_de_ciudades
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //Subsistema TabControl
        //Objetivo: Esta parte del código instancia las paginas para el tabControl


        private void ui_cantidad_pisos_ValueChanged(object sender, EventArgs e)
        {
            crear_pages();
        }

       
        //Subsistema tamaño lienzos
        //Objetivo: Definir tamaños de los lienzos

        private int ancho_lienzo()
        {
            int multiplicador = 1; // el valor que agranda el lienzo con respecto al tamaño de las casas
            int media_ancho = (Convert.ToInt32(ui_min_ancho_casa.Value) + Convert.ToInt32(ui_max_ancho_casa.Value)/2);
            //int ancho = (media_ancho * 100) * Convert.ToInt32(ui_cantidad_casas.Value)* multiplicador;
            int ancho = (Convert.ToInt32(ui_min_ancho_casa.Value) * 100) * Convert.ToInt32(ui_cantidad_casas.Value) * multiplicador;
            return ancho;
        }

        private int alto_lienzo()
        {
            int multiplicador = 1; // el valor que agranda el lienzo con respecto al tamaño de las casas
            int media_alto = (Convert.ToInt32(ui_min_alto_casa.Value) + Convert.ToInt32(ui_max_alto_casa.Value) / 2);
            //int alto = (media_alto * 100) * Convert.ToInt32(ui_cantidad_casas.Value) * multiplicador;
            int alto = (Convert.ToInt32(ui_min_alto_casa.Value) * 100) * Convert.ToInt32(ui_cantidad_casas.Value) * multiplicador;
            return alto;
        }

        //Subsistema de datos para los Lienzos

        public struct datos_forma
        {
           public int ancho_lienzo;
           public int alto_lienzo;
           public Point punto_origen;
           public int ancho_forma;
           public int alto_forma;
           public int grosor_pared;
           public Point nuevo_origen;
        }

        //Subsistema de dibujo
        //Objetivo: Dibujar los planos en todos los lienzos.

        private void dibujar()
        {
            Random azar = new Random();

            List<Point> cuadricula;
            cuadricula= cuadriculas.cuadricula_normal(ancho_lienzo(),alto_lienzo());

            List<datos_forma> datos = new List<datos_forma>();

            //llenar lista de datos por casa

            for (int ubicacion_datos = 0; ubicacion_datos < ui_cantidad_casas.Value; ubicacion_datos++)
            {
                datos.Add(new datos_forma()
                {
                    ancho_lienzo = ancho_lienzo(),
                    alto_lienzo = alto_lienzo(),
                    punto_origen = cuadricula[azar.Next(0, cuadricula.Count)],
                    ancho_forma = azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value)),
                    alto_forma = azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value)),
                    grosor_pared = Convert.ToInt32(ui_grosor_pared.Value),
                    nuevo_origen = new Point()
                });         
            }

            //Encuentro el nombre del radiobutton de forma que ha escogido el usuario

            String forma_seleccionada = ui_groupbox_forma_casas.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            //Pintar lienzos con los datos almacenados

            if (ui_superposicion_esc_fija.Checked == true)
            {
                for (int i = 0; i < ui_cantidad_pisos.Value; i++)
                {
                    for (int recorrer = 0; recorrer < ui_cantidad_casas.Value; recorrer++)
                    {
                        string nombre_page = "Planta " + i;
                        formas.forma(forma_seleccionada, datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);
                    }
                }
            }

            else if (ui_superposicion_esc_dec_cons.Checked == true || ui_superposicion_esc_dec_var.Checked == true) 
            {
                for (int i = 0; i < ui_cantidad_pisos.Value; i++)
                {

                    for (int recorrer = 0; recorrer < ui_cantidad_casas.Value; recorrer++)
                    {
                        datos_forma modificar = datos[recorrer];                                      
                        if (i>0)
                        {
                            int valor_reduccion = 0;
                            if (ui_superposicion_rad_valor_fijo.Checked == true)
                            {
                                valor_reduccion = Convert.ToInt32(ui_superposicion_valor_fijo.Value);
                            }
                            else if(ui_superposicion_rad_valor_por_rango.Checked == true)
                            {
                                int limite = Math.Min(modificar.alto_forma, modificar.ancho_forma);
                                //Esto es para manejar la excepcion
                                if (limite < 0)
                                { limite = 0; }

                                int modo = 0;
                                if (ui_superposicion_esc_dec_cons.Checked == true)
                                {
                                    modo = 1;
                                }
                                else if (ui_superposicion_esc_dec_var.Checked == true)
                                {
                                    modo = 0;
                                }
                                valor_reduccion = azar.Next(modo, limite+1);
                            }
                            modificar.nuevo_origen = new Point(modificar.punto_origen.X + ((valor_reduccion * 100) / 2),modificar.punto_origen.Y + ((valor_reduccion * 100) / 2));
                            modificar.punto_origen = modificar.nuevo_origen;
                            modificar.ancho_forma = modificar.ancho_forma - valor_reduccion;
                            modificar.alto_forma  = modificar.alto_forma - valor_reduccion;
                        }
                        datos[recorrer] = modificar;
                        string nombre_page = "Planta " + i;
                        formas.forma(forma_seleccionada , datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);
                    }
                }
            }        
        }
              
        private void ui_construir_Click(object sender, EventArgs e)
        {
            crear_pages();
            dibujar();
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            //mostrar_lienzos();
        }

        private void crear_pages() 
        {
            TabControl.TabPages.Clear();

            int ancho = ancho_lienzo();
            int alto = alto_lienzo();

            for (int i = 0; i < ui_cantidad_pisos.Value; i++)
            {
                string titulo = "Planta " + (TabControl.TabCount).ToString();
                TabPage nueva_pagina = new TabPage(titulo);
                nueva_pagina.AutoScroll = true;
                nueva_pagina.BorderStyle = BorderStyle.Fixed3D;
                nueva_pagina.BackColor = Color.White;

                PictureBox nuevo_lienzo = new PictureBox();
                nuevo_lienzo.Name = "Planta "+ i;
                nuevo_lienzo.Size = new System.Drawing.Size(ancho, alto);
                nuevo_lienzo.Image = new Bitmap(ancho, alto);
                nueva_pagina.Controls.Add(nuevo_lienzo);

                TabControl.TabPages.Add(nueva_pagina);
            }           

        }

        private void ui_superposicion_rad_valor_fijo_CheckedChanged_1(object sender, EventArgs e)
        {
            ui_superposicion_valor_fijo.Enabled = ui_superposicion_rad_valor_fijo.Checked;
        }

        private void ui_superposicion_esc_fija_CheckedChanged(object sender, EventArgs e)
        {
            ui_groupBox_superposicion_modo.Enabled = !ui_superposicion_esc_fija.Checked;
        }

        private void guardarCiudadComoCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ciudades"));

            //En las siguientes lineas recolecto las imagenes de los TabControls para guardarlas         
            for (int i = 0; i < TabControl.TabCount; i++)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "( *.png) | *.png";
                dialog.FileName = "Planta " + i;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    PictureBox nueva_imagen = (PictureBox)TabControl.TabPages[i].Controls.Find("Planta " + i, true)[0];             
                    nueva_imagen.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}
