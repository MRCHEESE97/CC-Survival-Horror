/*  Copyright (c) 2020 José Bravo <galillo1997@gmail.com>
    This file is part of creador de ciudades.

    Creador de ciudades is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see<https://www.gnu.org/licenses/>.
*/

using Creador_de_ciudades.Clases;
using Creador_de_ciudades.Clases_estaticas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
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

   
        //Sistema de dibujo
        //Objetivo: Dibujar los planos en todos los lienzos.

        private void dibujar()
        {   
            Random azar = new Random();
            //Variable para busqueda de origen
            int x_ori = Convert.ToInt32(ui_min_ancho_casa.Value) * 100, y_ori = Convert.ToInt32(ui_min_alto_casa.Value) * 100;

            List<Info_forma> datos = new List<Info_forma>();
            List<Info_calle> datos_calles = new List<Info_calle>();

            //Subsitema #1: calculo de area ciudad

            //Calculo del margen del area de dibujo, para que las formas no sobresalgan
            int margen_ancho = Convert.ToInt32(ui_max_ancho_casa.Value) * 100 ;
            int margen_alto = Convert.ToInt32(ui_max_alto_casa.Value) * 100 ;

            //-------

            int ancho_lienzo = 0, alto_lienzo = 0;

            List<int> anchos = new List<int>();
            List<int> altos = new List<int>();

            for (int i = 0; i < ui_cantidad_casas.Value; i++)
            {
                anchos.Add(azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1));
                altos.Add(azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1));

                if (anchos[i] > altos[i])
                { ancho_lienzo = ancho_lienzo + anchos[i] * 50; }
                else if (anchos[i] < altos[i])
                { alto_lienzo = alto_lienzo + altos[i] * 50; }
                else if (anchos[i] == altos[i])
                { // Si son iguales. se alternan
                    if (i % 2 == 0)
                    {
                        ancho_lienzo = ancho_lienzo + anchos[i] * 50;
                    }
                    else if (i % 2 != 0)
                    {
                        alto_lienzo = alto_lienzo + altos[i] * 50;
                    }
                }
            }


            float por_ancho = (float)(ancho_lienzo * (Convert.ToInt32(ui_porcentaje_sin_casas.Value) * 0.01));           
            ancho_lienzo = (int)(ancho_lienzo + por_ancho);
            ancho_lienzo = ancho_lienzo + margen_ancho;

            float por_alto = (float)(alto_lienzo * (Convert.ToInt32(ui_porcentaje_sin_casas.Value) * 0.01));
            alto_lienzo = (int)(alto_lienzo + por_alto);
            alto_lienzo = alto_lienzo + margen_alto;

            //Mostrar area de ciudad

            ui_label_m2.Text = Convert.ToString((ancho_lienzo/100 * alto_lienzo/100));

            //Llamada a la función que crea los lienzos

            crear_pages_area_casas(ancho_lienzo,alto_lienzo);

            //Pintar el fondo del picture box

            PictureBox pic = (PictureBox)TabControl.TabPages[0].Controls.Find("Planta 0", true)[0];
            Graphics fondo = Graphics.FromImage(pic.Image);
            Brush brocha_fondo = new SolidBrush(Color.FromArgb(0, 255, 0));
            fondo.FillRectangle(brocha_fondo, new Rectangle(new Point(0,0), new Size(ancho_lienzo, alto_lienzo)));


            //Subsistema # 2 creación de calles

            int largo_calle_ver = alto_lienzo / 100;
            int largo_calle_hor = ancho_lienzo / 100;

            Brush brocha_calle = new SolidBrush(Color.FromArgb(88,88,88));
            Pen brocha_vereda = new Pen(Color.White, 100);
            List<Point> guardado_de_puntos = new List<Point>();
            List<Info_calle> calles = new List<Info_calle>();

            //Obtiene la info para las calles y dibuja las veredas
            
            for (int i = 0; i < Convert.ToInt32(ui_cantidad_calles.Value); i++)
            {   
                int s = azar.Next(0,2);
                Info_calle nv;
                Point punto_cuadricula = Herramienta.seleccionar_punto_cuadricula(ancho_lienzo - margen_ancho, alto_lienzo - margen_alto, 100, Convert.ToInt32(ui_min_ancho_casa.Value) * 100, Convert.ToInt32(ui_min_alto_casa.Value) * 100);

                if (guardado_de_puntos.Contains(punto_cuadricula))
                {
                    i--;
                    continue;
                }
                else
                {
                    PictureBox pintura = (PictureBox)TabControl.TabPages[0].Controls.Find("Planta 0", true)[0];
                    Graphics c = Graphics.FromImage(pintura.Image);

                    if (i % 2 == 0)
                    { // Calle vertical
                        nv = new Info_calle(6, largo_calle_ver, punto_cuadricula);
                        //c.FillRectangle(brocha_calle, nv.po.X, nv.po.Y, nv.ancho_forma * 100, nv.alto_forma * 100);             
                        c.DrawLine(brocha_vereda, nv.po.X + 50, nv.po.Y, nv.po.X + 50, nv.po.Y + nv.alto_forma * 100);
                        c.DrawLine(brocha_vereda, nv.po.X - 50 + nv.ancho_forma * 100, nv.po.Y, nv.po.X - 50 + nv.ancho_forma * 100, nv.po.Y + nv.alto_forma * 100);
                        datos_calles.Add(nv);
                    }
                    else
                    { // Calle horizontal
                        nv = new Info_calle(largo_calle_hor, 6, punto_cuadricula);
                        //c.FillRectangle(brocha_calle, nv.po.X, nv.po.Y, nv.ancho_forma * 100, nv.alto_forma * 100);
                        c.DrawLine(brocha_vereda, nv.po.X, nv.po.Y + 50, nv.po.X + nv.ancho_forma * 100, nv.po.Y + 50);
                        c.DrawLine(brocha_vereda, nv.po.X, nv.po.Y - 50 + nv.alto_forma * 100, nv.po.X - 50 + nv.ancho_forma * 100, nv.po.Y -50 + nv.alto_forma * 100);
                        datos_calles.Add(nv);
                    }                  
                    calles.Add(nv);
                    guardado_de_puntos.Add(punto_cuadricula);
                }
            }

            //Dibujo las calles

            for (int i = 0; i < Convert.ToInt32(ui_cantidad_calles.Value); i++) 
            {
                PictureBox pintura = (PictureBox)TabControl.TabPages[0].Controls.Find("Planta 0", true)[0];
                Graphics c = Graphics.FromImage(pintura.Image);
                if (i % 2 == 0)
                { // Calle vertical
                    c.FillRectangle(brocha_calle, calles[i].po.X + 100, calles[i].po.Y, (calles[i].ancho_forma - 2) * 100, calles[i].alto_forma * 100);
                }
                else
                { // Calle horizontal
                    c.FillRectangle(brocha_calle, calles[i].po.X, calles[i].po.Y + 100, calles[i].ancho_forma * 100, (calles[i].alto_forma - 2) * 100);
                }              
            }


            //Subsistema # 3 de recoleccion de datos: casas

            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();


            for (int ubicacion_datos = 0; ubicacion_datos < ui_cantidad_casas.Value; ubicacion_datos++)
            {
                if (cronometro.ElapsedMilliseconds >= 50000)
                {
                    MessageBox.Show("Superó el tiempo limite ", "Operación cancelada",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            

                //Guardo en una variable el valor para los grados
                int grados = 0;
                if (ui_checkbox_girar.Checked) { grados = azar.Next(0, 361); }
                else 
                { 
                    int seleccionar = azar.Next(0, 4);
                    switch (seleccionar) 
                    { 
                        case 0: grados = 90;
                            break;
                        case 1: grados = 180;
                            break;
                        case 2: grados = 270;
                            break;
                        case 3: grados = 360;
                            break;
                    }
                }

                List<String> nombres_de_formas = new List<string>();
                nombres_de_formas.Add("ui_forma_casa_rectangular");
                //nombres_de_formas.Add("ui_forma_casa_hexagonal");
                nombres_de_formas.Add("ui_forma_casa_deformada");
                nombres_de_formas.Add("ui_forma_casa_deformada_chaflan");

                String vano_ventana_seleccionado = ui_group_box_vanos_ventanas.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

                //En caso de que una de las casas no encaje y pasaron mas de 10 segundos cambiara su tamaño

                if (cronometro.ElapsedMilliseconds > 30000) 
                {
                 anchos[ubicacion_datos] = azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1);
                 altos[ubicacion_datos] = azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1);
                }

                //Subsistema 3.1 seleccion de punto origen segun la distribución

                Point origen = new Point();

                String distribucion_seleccionado = ui_group_box_distribucion.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

                switch (distribucion_seleccionado)
                {
                    case "ui_distribucion_aleatoria":
                        origen = Herramienta.seleccionar_punto_cuadricula(ancho_lienzo - margen_ancho, alto_lienzo - margen_alto, 100, Convert.ToInt32(ui_min_ancho_casa.Value) * 100, Convert.ToInt32(ui_min_alto_casa.Value) * 100);
                        //100 es el multiplo
                        break;
                    case "ui_distribucion_columnas":
                        if(y_ori >= alto_lienzo - Convert.ToInt32(ui_max_alto_casa.Value) * 100)
                        {
                            x_ori = x_ori + 100;
                            y_ori = Convert.ToInt32(ui_min_alto_casa.Value) * 100;
                        }
                        origen = new Point(x_ori, y_ori);
                        y_ori = y_ori + 100;
                        break;
                    case "ui_distribucion_filas":
                        if (x_ori >= ancho_lienzo - Convert.ToInt32(ui_max_ancho_casa.Value) * 100)
                        {
                            x_ori = Convert.ToInt32(ui_min_ancho_casa.Value) * 100;
                            y_ori = y_ori + 100;
                        }
                        origen = new Point(x_ori, y_ori);
                        x_ori = x_ori + 100;
                        break;
                }

                //Aqui empieza la recollecion de la informacion para las casas

                Info_forma info = new Info_forma
                (
                 ancho_lienzo,
                 alto_lienzo,
                 anchos[ubicacion_datos],
                 altos[ubicacion_datos],
                 azar.Next(Convert.ToInt32(ui_min_grosor_pared.Value), Convert.ToInt32(ui_max_grosor_pared.Value)),
                 origen, //origen de la forma (casa) 
                 new Point(),
                 azar.Next(Convert.ToInt32(ui_pilar_cubico_med_min.Value), Convert.ToInt32(ui_pilar_cubico_med_max.Value)),
                 azar.Next(Convert.ToInt32(ui_pilar_round_med_min.Value), Convert.ToInt32(ui_pilar_round_med_max.Value)),
                 azar.Next(1, Convert.ToInt32(ui_cantidad_pisos.Value) + 1),
                 grados,
                 azar.Next(10, 20), // Este numero se multiplica por el valor de la columna Ej 3 espacio = 90 (30CM)
                 azar.Next(1, 5),
                 ui_checkbox_girar.Checked,
                 Posibilidad,
                 Distancia,
                 ui_pegar_casas.Checked,
                 vano_ventana_seleccionado,
                 nombres_de_formas[azar.Next(0,nombres_de_formas.Count)],
                 azar.Next(Convert.ToInt32(ui_pilar_prox_min.Value), Convert.ToInt32(ui_pilar_prox_max.Value)),
                 azar.Next(Convert.ToInt32(ui_vano_puerta_cant_min.Value), Convert.ToInt32(ui_vano_puerta_cant_max.Value))
                );

                info.resp_alto_forma = info.alto_forma;
                info.resp_ancho_forma = info.ancho_forma;

              
                bool interruptor = false;              

                //Valida si existe interseccion entre casas
                for (int i = 0; i < datos.Count; i++)
                {              
                 if (info.area_puntos.Intersect(datos[i].area_puntos).Any())
                    {
                        //Existe interseccion
                        interruptor = true;
                        info = null;
                        break;
                    }                           
                }
                //Valida si existe interseccion entre casas en calles

                if (info != null)
                {
                    for (int i = 0; i < datos_calles.Count; i++)
                    {
                        if (info.area_puntos.Intersect(datos_calles[i].area_puntos).Any())
                        {
                            //Existe interseccion
                            interruptor = true;
                            info = null;
                            break;
                        }
                    }
                }
                if (interruptor)
                {
                    ubicacion_datos--;
                    continue;
                }
                else
                {
                    // Si no se encontraron intersecciones agrega la info de forma
                    datos.Add(info);

                    if (ui_objetos_elevador.Checked == true)
                    {
                        //Valida que el elevador este dentro del espacio de la forma
                        bool encontrado = false;
                        do
                        {
                            info.origen_elevador = Herramienta.seleccionar_punto_cuadricula(info.po.X + info.ancho_forma * 100, info.po.Y + info.alto_forma * 100, 100, info.po.X, info.po.Y);
                            info.espacio_elevador = new Rectangle(info.origen_elevador.X, info.origen_elevador.Y, 2 * 100, 2 * 100);
                            Rectangle resultado = Rectangle.Intersect(info.espacio_elevador, info.espacio_forma);
                            if (resultado == info.espacio_elevador)
                            { encontrado = true; }
                        } while (encontrado == false);
                    }

                }     
            }
           
            //Subsistema #4 superposiciones
            //Encuentro el nombre del radiobutton de la forma que ha escogido el usuario

            String forma_seleccionada = ui_groupbox_forma_casas.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            //Pintar lienzos con los datos almacenados, dependiedo de la superposicion

            if (ui_superposicion_esc_cons.Checked == true)
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
            else if (ui_superposicion_esc_cons_var.Checked == true)
            {
                for (int i = 0; i < ui_cantidad_pisos.Value; i++)
                {
                    for (int recorrer = 0; recorrer < ui_cantidad_casas.Value; recorrer++)
                    {
                        if (datos[recorrer].pisos_reales > 0)
                        {
                            string nombre_page = "Planta " + i;
                            Formas.forma(forma_seleccionada, datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                            //Primero se guardan los nombre de los checkbox activo es una lista

                            List<String> nombres_checkbox = new List<string>();
                            foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                            {
                                if (c.Checked == true) { nombres_checkbox.Add(c.Name); }
                            }

                            //Después de pintar las casas, se pintan los objetos
                            Objetos.objeto(nombres_checkbox, datos[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                            //Esta variable es modificada una vez que PB se haya dibujado
                            datos[recorrer].ubicacion_pb = false;
                        }
                        datos[recorrer].pisos_reales = datos[recorrer].pisos_reales - 1;
                    }
                }
            }
            else if (ui_superposicion_piramidal.Checked == true) 
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
                                valor_reduccion = azar.Next(modo, limite+1);
                            }
                            datos[recorrer].nuevo_origen = new Point(datos[recorrer].po.X + ((valor_reduccion * 100) / 2), datos[recorrer].po.Y + ((valor_reduccion * 100) / 2));
                            datos[recorrer].po = datos[recorrer].nuevo_origen;
                            datos[recorrer].ancho_forma = datos[recorrer].ancho_forma - valor_reduccion;
                            datos[recorrer].alto_forma  = datos[recorrer].alto_forma - valor_reduccion;
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
            MessageBox.Show("Completado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
              
        private void ui_construir_Click(object sender, EventArgs e)
        {
            crear_pages();
            //Llamo al metodo de dibujo por medio de un hilo
            dibujar();
           
        }

       

       
        private void crear_pages() 
        {
            TabControl.TabPages.Clear();


            for (int i = 0; i < ui_cantidad_pisos.Value; i++)
            {
                string titulo = "Planta " + (TabControl.TabCount).ToString();
                TabPage nueva_pagina = new TabPage(titulo);
                nueva_pagina.AutoScroll = true;
                nueva_pagina.BorderStyle = BorderStyle.Fixed3D;
                nueva_pagina.BackColor = Color.White;
                

                PictureBox nuevo_lienzo = new PictureBox();
                nuevo_lienzo.Name = "Planta "+ i;
                
                nueva_pagina.Controls.Add(nuevo_lienzo);
                TabControl.TabPages.Add(nueva_pagina);
            }           

        }
        private void crear_pages_area_casas(int ancho, int alto)
        {
            TabControl.TabPages.Clear();

            for (int i = 0; i < ui_cantidad_pisos.Value; i++)
            {
                string titulo = "Planta " + (TabControl.TabCount).ToString();
                TabPage nueva_pagina = new TabPage(titulo);
                nueva_pagina.AutoScroll = true;
                nueva_pagina.BorderStyle = BorderStyle.Fixed3D;
                nueva_pagina.BackColor = Color.White;
                


                PictureBox nuevo_lienzo = new PictureBox();
                nuevo_lienzo.Name = "Planta " + i;

                nueva_pagina.Controls.Add(nuevo_lienzo);
                nuevo_lienzo.Size = new System.Drawing.Size(tabPage1.Size.Width, tabPage1.Size.Height);
                nuevo_lienzo.SizeMode = PictureBoxSizeMode.StretchImage;
                nuevo_lienzo.Dock = DockStyle.Fill;

                Bitmap bmp = new Bitmap(ancho, alto);
                nuevo_lienzo.Image = bmp; 
                TabControl.TabPages.Add(nueva_pagina);
            }

        }

        private void ui_superposicion_rad_valor_fijo_CheckedChanged_1(object sender, EventArgs e)
        {
            ui_superposicion_valor_fijo.Enabled = ui_superposicion_rad_valor_fijo.Checked;
        }

        private void ui_superposicion_esc_fija_CheckedChanged(object sender, EventArgs e)
        {
            ui_groupBox_superposicion_modo.Enabled = !ui_superposicion_esc_cons.Checked;
        }

        private void guardarCiudadComoCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void ui_superposicion_esc_cons_var_CheckedChanged(object sender, EventArgs e)
        {
            ui_groupBox_superposicion_modo.Enabled = !ui_superposicion_esc_cons_var.Checked;
        }

        private void ui_quitar_todo_Click(object sender, EventArgs e)
        {
            ui_label_m2.Text = "----";
        }

        //Validación de minimos y maximos
        private void ui_min_grosor_pared_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ui_min_grosor_pared.Value) > Convert.ToInt32(ui_max_grosor_pared.Value))
            {
                MessageBox.Show("Valor invalido");
                ui_min_grosor_pared.Value = ui_max_grosor_pared.Value;
            }
        }

        private void ui_max_grosor_pared_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ui_max_grosor_pared.Value) < Convert.ToInt32(ui_min_grosor_pared.Value))
            {
                MessageBox.Show("Valor invalido");
                ui_max_grosor_pared.Value = ui_min_grosor_pared.Value;
            }
        }
        private void ui_min_ancho_casa_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ui_min_ancho_casa.Value) > Convert.ToInt32(ui_max_ancho_casa.Value))
            {
                MessageBox.Show("Valor invalido");
                ui_min_ancho_casa.Value = ui_max_ancho_casa.Value;
            }
        }

        private void ui_max_ancho_casa_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ui_max_ancho_casa.Value) < Convert.ToInt32(ui_min_ancho_casa.Value))
            {
                MessageBox.Show("Valor invalido");
                ui_max_ancho_casa.Value = ui_min_ancho_casa.Value;
            }
        }

        private void ui_min_alto_casa_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ui_min_alto_casa.Value) > Convert.ToInt32(ui_max_alto_casa.Value))
            {
                MessageBox.Show("Valor invalido");
                ui_min_alto_casa.Value = ui_max_alto_casa.Value;
            }
        }

        private void ui_max_alto_casa_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ui_max_alto_casa.Value) < Convert.ToInt32(ui_min_alto_casa.Value))
            {
                MessageBox.Show("Valor invalido");
                ui_max_alto_casa.Value = ui_min_alto_casa.Value;
            }
        }

    }
}
