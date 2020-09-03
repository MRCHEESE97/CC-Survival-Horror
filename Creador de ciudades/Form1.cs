﻿using Creador_de_ciudades.Clases;
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
            int ancho = (Convert.ToInt32(ui_min_ancho_casa.Value) * 100) * Convert.ToInt32(ui_cantidad_casas.Value) * multiplicador;          
            return ancho;
        }

        private int alto_lienzo()
        {
            int multiplicador = 1; // el valor que agranda el lienzo con respecto al tamaño de las casas
            int alto = (Convert.ToInt32(ui_min_alto_casa.Value) * 100) * Convert.ToInt32(ui_cantidad_casas.Value) * multiplicador;
            return alto;
        }

     
        

        //Subsistema de dibujo
        //Objetivo: Dibujar los planos en todos los lienzos.

        private void dibujar()
        {
            Random azar = new Random();

            //Aqui calcularé el 20% del tamaño del lienzo, esto para que la ciudad tenga un limite y las formas no sobresalgan
            float c_ancho = ancho_lienzo() * (float)0.20;
            float c_alto = alto_lienzo() * (float)0.20;

            List<Info_forma> datos = new List<Info_forma>();

            //llenar lista de datos por casa

            for (int ubicacion_datos = 0; ubicacion_datos < ui_cantidad_casas.Value; ubicacion_datos++)
            {
                Info_forma info = new Info_forma
                (
                 ancho_lienzo(),
                 alto_lienzo(),
                 azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value)),
                 azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value)),
                 Convert.ToInt32(ui_grosor_pared.Value),
                 Distribuidor.cuadricula_normal(ancho_lienzo() - (int)c_ancho,alto_lienzo() - (int)c_alto, 100, 0, 0), //100 es el multiplo 
                 new Point(),
                 Convert.ToInt32(ui_columna_cuadrada_valor.Value),
                 Convert.ToInt32(ui_columna_redonda_valor.Value)
                );

                info.nuevo_alto_forma = info.alto_forma;
                info.nuevo_ancho_forma = info.ancho_forma;
                info.origen_elevador = Distribuidor.cuadricula_normal(info.punto_origen.X + info.ancho_forma * 100, info.punto_origen.Y + info.alto_forma * 100, 100, info.punto_origen.X, info.punto_origen.Y); //El ascensor aparece solo en los numeros multiplos de 100


                if (ubicacion_datos == 0)
                {
                    datos.Add(info);
                    continue;
                }
             
                bool interruptor = false;

                //Valida si existe interseccion entre casas
                for (int i = 0; i < datos.Count; i++)
                {              
                 if ( info.limite.IntersectsWith(datos[i].limite))
                    {
                        //Existe interseccion
                        interruptor = true;
                        break;
                    }                           
                }
                if (interruptor)
                {
                    ubicacion_datos--;
                    continue;
                }
                else 
                { 
                    // Si no se encontraron intersecciones agrega la info de la nueva forma
                    datos.Add(info); 
                }                     
            }

            //Encuentro el nombre del radiobutton de la forma que ha escogido el usuario

            String forma_seleccionada = ui_groupbox_forma_casas.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            //Pintar lienzos con los datos almacenados, dependiedo de la superposicion

            if (ui_superposicion_esc_fija.Checked == true)
            {
                for (int i = 0; i < ui_cantidad_pisos.Value; i++)
                {
                    for (int recorrer = 0; recorrer < ui_cantidad_casas.Value; recorrer++)
                    {
                        string nombre_page = "Planta " + i;
                        Formas.forma(forma_seleccionada, datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                        //Primero se guardan los nombre de los checkbox activo es una lista

                        List<String> nombres_checkbox = new List<string>();
                        foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                        {
                            if (c.Checked == true){nombres_checkbox.Add(c.Name);}
                        }

                        //Después de pintar las casas, se pintan los objetos
                        Objetos.objeto(nombres_checkbox,datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);
                        
                        //Esta variable es modificada una vez que PB se haya dibujado
                        datos[recorrer].ubicacion_pb = false;

                    }
                }
            }

            else if (ui_superposicion_esc_dec_cons.Checked == true || ui_superposicion_esc_dec_var.Checked == true) 
            {
                for (int i = 0; i < ui_cantidad_pisos.Value; i++)
                {

                    for (int recorrer = 0; recorrer < ui_cantidad_casas.Value; recorrer++)
                    {                                                           
                        if (i>0)
                        {
                            int valor_reduccion = 0;
                            if (ui_superposicion_rad_valor_fijo.Checked == true)
                            {
                                valor_reduccion = Convert.ToInt32(ui_superposicion_valor_fijo.Value);
                            }
                            else if(ui_superposicion_rad_valor_por_rango.Checked == true)
                            {
                                int limite = Math.Min(datos[recorrer].alto_forma, datos[recorrer].ancho_forma);
                                //Esto es para manejar la excepcion probar un break
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
                            datos[recorrer].nuevo_origen = new Point(datos[recorrer].punto_origen.X + ((valor_reduccion * 100) / 2), datos[recorrer].punto_origen.Y + ((valor_reduccion * 100) / 2));
                            datos[recorrer].punto_origen = datos[recorrer].nuevo_origen;
                            datos[recorrer].nuevo_ancho_forma = datos[recorrer].nuevo_ancho_forma - valor_reduccion;
                            datos[recorrer].nuevo_alto_forma  = datos[recorrer].nuevo_alto_forma - valor_reduccion;
                        }                      

                        if (datos[recorrer].ancho_forma > 2 && datos[recorrer].alto_forma > 2) 
                        {
                            string nombre_page = "Planta " + i;
                            Formas.forma(forma_seleccionada, datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                            //Después de pintar las casas, se pintan los objetos

                            //Primero se guardan los nombre de los checkbox activo es una lista

                            List<String> nombres_checkbox = new List<string>();
                            foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                            {
                                if (c.Checked == true) { nombres_checkbox.Add(c.Name); }
                            }

                            Objetos.objeto(nombres_checkbox, datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);
                           
                            //Esta variable es modificada una vez que PB se haya dibujado
                            datos[recorrer].ubicacion_pb = false;
                        }

                      
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // The top panel remains the same size when the form is resized.
            splitContainer1.FixedPanel= System.Windows.Forms.FixedPanel.Panel2;
        }
    }
}
