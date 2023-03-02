/*  Copyright (c) 2020 José Bravo <galillo1997@hotmail.com>
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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Creador_de_ciudades
{   
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // The top panel remains the same size when the form is resized.
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
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
            //Cronometro de progress bar
            Stopwatch cronometro_proceso = new Stopwatch();
            cronometro_proceso.Start();



            Random azar = new Random();
            //Variable para busqueda de origen
            int x_ori = Convert.ToInt32(ui_min_ancho_casa.Value) * 100, y_ori = Convert.ToInt32(ui_min_alto_casa.Value) * 100;

            List<Info_forma> lista_casas = new List<Info_forma>();
            List<Composicion_calle> lista_comp_calles = new List<Composicion_calle>();
            List<Point> lista_puntos_calles = new List<Point>();

            //Subsitema #1: calculo de area ciudad

            //Calculo del margen del area de dibujo, para que las formas no sobresalgan
            int margen_ancho = Convert.ToInt32(ui_max_ancho_casa.Value) * 100 ;
            int margen_alto = Convert.ToInt32(ui_max_alto_casa.Value) * 100 ;

            //-------

            int ancho_lienzo = 0, alto_lienzo = 0, area = 0;

            List<int> anchos = new List<int>();
            List<int> altos = new List<int>();

            for (int i = 0; i < ui_cantidad_casas.Value; i++)
            {
                anchos.Add(azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1));
                altos.Add(azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1));
            }
            for (int x = 0; x < ui_cantidad_casas.Value; x++)
            {
                int precalculo = (anchos[x] * altos[x]) * 100;
                area = area + precalculo;
            }

            ancho_lienzo = alto_lienzo = (int)Math.Sqrt(area) * 10;

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

            //Pintado en el fondo del picture box

            PictureBox primer_nivel = (PictureBox)TabControl.TabPages[0].Controls.Find("Planta 0", true)[0];
            Graphics fondo = Graphics.FromImage(primer_nivel.Image);
            Brush brocha_fondo = new SolidBrush(Color.DarkGreen);
            fondo.FillRectangle(brocha_fondo, new Rectangle(new Point(0,0), new Size(ancho_lienzo, alto_lienzo)));


            //Subsistema # 2 creación de calles

            if (ui_calle_cuadricula.Checked == true)
            {        
                //Se dibujan las veredas (Calle base)
                int dist_entre_cll = Convert.ToInt32(ui_espacio_calles.Value) * 100;

                for (int y= dist_entre_cll; y < alto_lienzo ; y += dist_entre_cll)
                {
                    int ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value));
                    int ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value));
                    lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100), new Pen(Color.FromArgb(88,88,88), ancho_calle * 100), new Point(0, y), new Point(ancho_lienzo, y)));                
                }
                for (int x = dist_entre_cll; x < ancho_lienzo; x += dist_entre_cll)
                {
                    int ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value));
                    int ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value));
                    lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100), new Pen(Color.FromArgb(88,88,88), ancho_calle * 100), new Point(x, 0), new Point(x, alto_lienzo)));
                }
                for (int i = 0; i < lista_comp_calles.Count; i++)
                {
                    fondo.DrawLine(lista_comp_calles[i].calle_base, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                    primer_nivel.Refresh();
                }

                //Subsistema # 2.1 Deteccion de pixeles blancos "Pixeles de linea base"
                lista_puntos_calles = Herramienta.obtener_coor_pixel_blancos((Bitmap)primer_nivel.Image);

                //Se dibujan las calles
                for (int i = 0; i < lista_comp_calles.Count; i++)
                {
                    fondo.DrawLine(lista_comp_calles[i].calle, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                    primer_nivel.Refresh();
                }
            }
            else if (ui_calle_incompleta.Checked == true)
            {
                //Se dibujan las veredas (Calle base)
                int dist_entre_cll = Convert.ToInt32(ui_espacio_calles.Value) * 100;
                int longitud_x = ancho_lienzo / dist_entre_cll;
                int longitud_y = alto_lienzo / dist_entre_cll;
               //Pen dash_street = new Pen(Color.Yellow,20);
               //dash_street.DashStyle = DashStyle.Dash;

                for (int y = dist_entre_cll; y < alto_lienzo; y += dist_entre_cll)
                {
                    int ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value));
                    int ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value));
                    lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100), new Pen(Color.FromArgb(88,88,88), ancho_calle * 100), new Point(azar.Next(0, longitud_x - 1) * dist_entre_cll, y), new Point(azar.Next(3, longitud_x + 2) * dist_entre_cll, y)));
                }
                for (int x = dist_entre_cll; x < ancho_lienzo; x += dist_entre_cll)
                {
                    int ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value));
                    int ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value));
                    lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100), new Pen(Color.FromArgb(88,88,88), ancho_calle * 100), new Point(x, azar.Next(0, longitud_y - 1)), new Point(x, azar.Next(3, longitud_y + 2) * dist_entre_cll)));
                }
                for (int i = 0; i < lista_comp_calles.Count; i++)
                {
                    fondo.DrawLine(lista_comp_calles[i].calle_base, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                    primer_nivel.Refresh();
                }

                //Subsistema # 2.1 Deteccion de pixeles blancos "Pixeles de linea base"
                lista_puntos_calles = Herramienta.obtener_coor_pixel_blancos((Bitmap)primer_nivel.Image);

                //Se dibujan las calles
                for (int i = 0; i < lista_comp_calles.Count; i++)
                {
                    fondo.DrawLine(lista_comp_calles[i].calle, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                    //fondo.DrawLine(dash_street, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                    primer_nivel.Refresh();
                }
            }
            else if (ui_calle_curvilineal.Checked == true)
            {

            }
            else if (ui_calle_callejones.Checked == true)
            {

            }



            //MessageBox.Show(Convert.ToString(lista_puntos_calles.Count));

            
            //Subsistema # 3 de recoleccion de datos: casas

            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();

          
       
            for (int ubicacion_datos = 0; ubicacion_datos < ui_cantidad_casas.Value; ubicacion_datos++)
            {
                //Actualización del progress bar #1

                barra.Value = (int)cronometro_proceso.Elapsed.TotalSeconds;

                

                if (cronometro.ElapsedMilliseconds >= Convert.ToInt32( ui_tiempo_espera.Value) * 1000)
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

                if (cronometro.ElapsedMilliseconds > Convert.ToInt32(ui_tiempo_espera.Value) * 1000 - (Convert.ToInt32(ui_tiempo_espera.Value) * 1000 * 0.1)) //Espera el 90% del tiempo de respuesta
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
                        if(y_ori >= (alto_lienzo - Convert.ToInt32(ui_max_alto_casa.Value) * 100) - 400)
                        {
                            x_ori = x_ori + 100;
                            y_ori = Convert.ToInt32(ui_min_alto_casa.Value) * 100;
                        }
                        origen = new Point(x_ori, y_ori);
                        y_ori = y_ori + 100;
                        break;
                    case "ui_distribucion_filas":
                        if (x_ori >= (ancho_lienzo - Convert.ToInt32(ui_max_ancho_casa.Value) * 100) - 400)
                        {
                            x_ori = Convert.ToInt32(ui_min_ancho_casa.Value) * 100;
                            y_ori = y_ori + 100;
                        }
                        origen = new Point(x_ori, y_ori);
                        x_ori = x_ori + 100;
                        break;
                }

                //Aqui empieza la recollecion de la informacion para las casas

                Info_forma nueva_casa = new Info_forma
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
                 azar.Next(1, Convert.ToInt32(ui_cantidad_pisos.Value) + 1), //Pisos_reales
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

                nueva_casa.resp_alto_forma = nueva_casa.alto_forma;
                nueva_casa.resp_ancho_forma = nueva_casa.ancho_forma;

              
                //Subsistema 3.2 #Filtros

                bool interruptor = false;

                //Verificar si existe interseccion entre casas
                //Esta verificación me deja una gran leccion 29/11/20 :)  ME REFERIA AL FOR EN PARALELO <3

                for (int x = 0; x < lista_casas.Count; x++)
                {
                    Parallel.For(0, nueva_casa.area_puntos.Count - 1, (i, state) => 
                    {
                        if (lista_casas[x].area_puntos.Contains(nueva_casa.area_puntos[i]))
                        {
                            //Existe interseccion
                            interruptor = true;
                            state.Break();                         
                        }
                    });
                }
                
                
                //Verifica si existe interseccion entre casas y calles

                if (nueva_casa != null)
                {           
                    Parallel.For(0, nueva_casa.area_puntos.Count - 1, (i, state) =>
                    {
                        if (lista_puntos_calles.Contains(nueva_casa.area_puntos[i]))
                        {
                            //Existe interseccion
                            interruptor = true;
                            state.Break();
                        }
                    });            
                } 

                if (interruptor)
                {
                    ubicacion_datos--;
                    continue;
                }
                else
                {
                    // Si no se encontraron intersecciones agrega la info de forma
                    //nueva_casa.area_post();
                    lista_casas.Add(nueva_casa);

                    if (ui_objetos_elevador.Checked == true)
                    {
                        //Valida que el elevador este dentro del espacio de la forma
                        bool encontrado = false;
                        do
                        {
                            nueva_casa.origen_elevador = Herramienta.seleccionar_punto_cuadricula(nueva_casa.po.X + nueva_casa.ancho_forma * 100, nueva_casa.po.Y + nueva_casa.alto_forma * 100, 100, nueva_casa.po.X, nueva_casa.po.Y);
                            nueva_casa.espacio_elevador = new Rectangle(nueva_casa.origen_elevador.X, nueva_casa.origen_elevador.Y, 2 * 100, 2 * 100);
                            Rectangle resultado = Rectangle.Intersect(nueva_casa.espacio_elevador, nueva_casa.espacio_forma);
                            if (resultado == nueva_casa.espacio_elevador)
                            { encontrado = true; }
                        } while (encontrado == false);
                    }

                }     
            }
           
            //Subsistema #4 superposiciones
            //Encuentro el nombre del radiobutton de la forma que ha escogido el usuario

            String forma_seleccionada = ui_groupbox_forma_casas.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

           

            //FUNCIONES DE SUPERPOSICIONES

            void superposicion_con(int recorrer, int i) //i es el iterado del piso, recorrer el iterador de una casa
            {
                string nombre_page = "Planta " + i;
                Formas.forma(forma_seleccionada, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                //Primero se guardan los nombre de los checkbox activo es una lista

                List<String> nombres_checkbox = new List<string>();
                foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                {
                    if (c.Checked == true) { nombres_checkbox.Add(c.Name); }
                }

                //Después de pintar las casas, se pintan los objetos
                Objetos.seleccionados(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                //Esta variable es modificada una vez que PB se haya dibujado
                lista_casas[recorrer].ubicacion_pb = false;


                //Actualización del progress bar #1

                barra.Value = (int)cronometro_proceso.Elapsed.TotalSeconds;

            }

            void superposicion_pir(int recorrer, int i) {
                if (i > 0)
                {
                    int valor_reduccion = 0;
                    if (ui_superposicion_rad_valor_fijo.Checked == true)
                    {
                        valor_reduccion = Convert.ToInt32(ui_superposicion_valor_fijo.Value);
                    }
                    else if (ui_superposicion_rad_valor_por_rango.Checked == true)
                    {
                        int limite = Math.Min(lista_casas[recorrer].alto_forma, lista_casas[recorrer].ancho_forma);
                        //Esto es para manejar la excepcion probar un break
                        if (limite < 0)
                        { limite = 0; }

                        int modo = 0;
                        valor_reduccion = azar.Next(modo, limite + 1);
                    }
                    lista_casas[recorrer].nuevo_origen = new Point(lista_casas[recorrer].po.X + ((valor_reduccion * 100) / 2), lista_casas[recorrer].po.Y + ((valor_reduccion * 100) / 2));
                    lista_casas[recorrer].po = lista_casas[recorrer].nuevo_origen;
                    lista_casas[recorrer].ancho_forma = lista_casas[recorrer].ancho_forma - valor_reduccion;
                    lista_casas[recorrer].alto_forma = lista_casas[recorrer].alto_forma - valor_reduccion;
                }

                if (lista_casas[recorrer].ancho_forma > 1 && lista_casas[recorrer].alto_forma > 1)  // Solo se dibuja si es mayor a 1 el ancho o el alto
                {
                    string nombre_page = "Planta " + i;
                    Formas.forma(forma_seleccionada, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                    //Después de pintar las casas, se pintan los objetos

                    //Primero se guardan los nombre de los checkbox activo es una lista

                    List<String> nombres_checkbox = new List<string>();
                    foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                    {
                        if (c.Checked == true) { nombres_checkbox.Add(c.Name); }
                    }

                    Objetos.seleccionados(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                    //Esta variable es modificada una vez que PB se haya dibujado
                    lista_casas[recorrer].ubicacion_pb = false;
                }


                //Actualización del progress bar #1

                barra.Value = (int)cronometro_proceso.Elapsed.TotalSeconds;
            }



            //4.2 Pintar lienzos con los datos almacenados, dependiedo de la superposicion

           
            for (int i = 0; i < ui_cantidad_pisos.Value; i++)
            {
                for (int recorrer = 0; recorrer < ui_cantidad_casas.Value; recorrer++)
                {
                    if (ui_superposicion_esc_cons.Checked == true)
                    {

                        if (ui_quitar_algunos_pisos.Checked)
                        {
                            if (lista_casas[recorrer].pisos_reales > 0)
                            {
                                superposicion_con(recorrer, i);
                            }

                            lista_casas[recorrer].pisos_reales = lista_casas[recorrer].pisos_reales - 1; // disminuye un piso

                        }
                        else
                        {
                            superposicion_con(recorrer, i);
                        }
                    }
                    if (ui_superposicion_piramidal.Checked == true) //este modo puede hacer parecer que quitar algunos pisos parezca activo
                    {
                        if (ui_quitar_algunos_pisos.Checked)
                        {
                            if (lista_casas[recorrer].pisos_reales > 0)
                            {
                                superposicion_pir(recorrer, i);
                            }

                            lista_casas[recorrer].pisos_reales = lista_casas[recorrer].pisos_reales - 1; // disminuye un piso

                        }
                        else
                        {
                            superposicion_pir(recorrer, i); ;
                        }
                       
                    }
                    //4.1 QUITA PISOS ALEATORIAMENTE EN LAS DISTINTAS CASAS
                }
            }
            
            
           
            MessageBox.Show("Completado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            barra.Value = 0;
           
        }
              
        private void ui_construir_Click(object sender, EventArgs e)
        {
            barra.Maximum = Convert.ToInt32(ui_tiempo_espera.Value) + 10; // probar mañana
            crear_pages();
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

        private void splitContainer1_MouseHover(object sender, EventArgs e)
        {
            splitContainer1.Cursor = Cursors.Cross;
        }

        private void splitContainer1_MouseLeave(object sender, EventArgs e)
        {
            splitContainer1.Cursor = Cursors.Hand;
        }

        private void ui_calle_cuadricula_CheckedChanged(object sender, EventArgs e)
        {
            ui_cantidad_calles.Enabled = !ui_calle_cuadricula.Checked;
        }
    }
}
