/*  Copyright (c) 2020 José Bravo <galil******@hotmail.com>
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

/*PUNTOS A TENER EN CUENTA.
 A) ES MEJOR QUITAR EL SUELO (VERDE) EN EL MOTOR GRAFICO Y REEMPLAZARLO POR UN TERRAIN.
 B) LAS CASAS DEBEN SER REUBICADAS DEBIDO A LA ESTETICA DEL JUEGO Y LOS GUSTOS DEL DISEÑADOR DEL JUEGO.
 C) SE DEBE COMPROBAR QUE LOS PISOS (AMARILLOS) NO TENGAN LAS NORMALES INVERTIDAS EN BLENDER.
 */



using Aspose.Svg.ImageVectorization;
using Creador_de_ciudades.Clases;
using Creador_de_ciudades.Clases_estaticas;
using SkiaSharp;
using Svg;
using System;
using System.Collections;   
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;


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
            int x_ori =  100, y_ori = 100;

            List<Info_forma> lista_casas = new List<Info_forma>();
            List<Composicion_calle> lista_comp_calles = new List<Composicion_calle>();
            List<Point> lista_puntos_calles = new List<Point>();

            List<Point> lista_puntos_origen = new List<Point>(); // Esta lista sirve para que los puntos de origen de una forma, no se repitan. 

            //Estas varias pertencen a distribución
            bool h_o_v = true;
            int respaldo_x_ori = 0, respaldo_y_ori = 0;

            int avance_en_x = 100;  //100 es un metro
            int avance_en_y = 100;


            //Subsitema #1: calculo de area ciudad

            //Calculo del margen del area de dibujo, para que las formas no sobresalgan
            int margen_ancho = Convert.ToInt32(ui_max_ancho_casa.Value) * 100 ;
            int margen_alto = Convert.ToInt32(ui_max_alto_casa.Value) * 100 ;

            //-------

            int ancho_lienzo = 0, alto_lienzo = 0, area = 0;


            //Se llenan los altos y los anchos de las casas

            List<int> anchos = new List<int>();
            List<int> altos = new List<int>();

            for (int i = 0; i < ui_cantidad_casas.Value; i++)
            {
                //if (ui_ambos.Checked)
                //{
                //    anchos.Add(azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1));
                //    altos.Add(azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1));
                //}
                //else
                if (ui_solo_pares.Checked)
                {
                    int anchito = 0, altito = 0;
                    do
                    {
                        anchito = azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1);
                        altito = azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1);
                    }
                    while (anchito % 2 != 0 || altito % 2 != 0);
                    anchos.Add(anchito);
                    altos.Add(altito);
                }
                else if (ui_solo_impares.Checked)
                {
                    int anchito = 0, altito = 0;
                    do
                    {
                        anchito = azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1);
                        altito = azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1);
                    }
                    while (anchito % 2 == 0 || altito % 2 == 0);
                    anchos.Add(anchito);
                    altos.Add(altito);
                }
                else if (ui_multiplo_de.Checked)
                {
                    int anchito = 0, altito = 0;
                    do
                    {
                        anchito = azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1);
                        altito = azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1);
                    }
                    while (anchito % Convert.ToInt32(ui_multiplo_valor.Value) != 0 || altito % Convert.ToInt32(ui_multiplo_valor.Value) != 0);
                    anchos.Add(anchito);
                    altos.Add(altito);
                }
                else if(ui_normal.Checked)
                {   
                    // 11/12/2023
                    int anchito = 0, altito = 0;
                    do
                    {
                        anchito = azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1);
                        altito = azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1);
                    }
                    while (Math.Abs(anchito - altito) <= ui_dis_anch_alt.Value);
                    anchos.Add(anchito);
                    altos.Add(altito);

                }     
            }


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

            crear_pages_area_casas(ancho_lienzo, alto_lienzo);

            //Pintado en el fondo del picture box

            PictureBox primer_nivel = (PictureBox)TabControl.TabPages[0].Controls.Find("Planta 0", true)[0];
            Graphics fondo = Graphics.FromImage(primer_nivel.Image);
            Brush brocha_fondo = new SolidBrush(Color.DarkGreen);
            fondo.FillRectangle(brocha_fondo, new Rectangle(new Point(0, 0), new Size(ancho_lienzo, alto_lienzo)));
            primer_nivel.Refresh();



            //Subsistema # 2 creación de calles

            int dist_entre_cll = 0;

            if (ui_quitar_calles.Checked == false)
            {

                do
                {
                    fondo.FillRectangle(brocha_fondo, new Rectangle(new Point(0, 0), new Size(ancho_lienzo, alto_lienzo)));
                    primer_nivel.Refresh();

                    lista_comp_calles.Clear();

                    if (ui_calle_diagonal.Checked)
                    {
                        int ancho_calle = 0;
                        int ancho_vereda =0;
                        do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                        do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);
 

                        Point a, b;
                        int lado = azar.Next(0, 2);
                        if (lado == 0)
                        {
                            a = new Point(0, 0);
                            b = new Point(ancho_lienzo, alto_lienzo);
                        }
                        else
                        {
                            a = new Point(ancho_lienzo, 0);
                            b = new Point(0, alto_lienzo);
                        }

                        lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), a, b));
                        lista_puntos_calles = Herramienta.obtener_coor_pixel_blancos((Bitmap)primer_nivel.Image);
                    }

                    // aqui se define la lejania o distancia de una calle y otra, lo que indirectamente define el tamanio de una manzana
                    // Hay un detalle el ancho no es exacto debito a aque la linea pasa en medio de los puntos, ocupando la calle una parte de esa distancia por eso puse el 4
                    if (ui_autoajustar_dist_calles.Checked)
                    {
                        dist_entre_cll = (int)(Convert.ToInt32(ui_espacio_calles_maximo.Value)/1.5) * 100; // esta formula sola la obtuve con pruebas y la mas exacta
                    }
                    else
                    {
                        dist_entre_cll = Convert.ToInt32(Herramienta.retornar_mayor((int)ui_max_ancho_casa.Value, (int)ui_max_alto_casa.Value)) * 200;
                    }


                    if (ui_calle_cuadricula.Checked == true)
                    {

                        for (int y = dist_entre_cll; y < alto_lienzo; y += dist_entre_cll)
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);
                            lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), new Point(0, y), new Point(ancho_lienzo, y)));
                        }
                        for (int x = dist_entre_cll; x < ancho_lienzo; x += dist_entre_cll)
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);
                            lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), new Point(x, 0), new Point(x, alto_lienzo)));
                        }

                        //Se dibujan las veredas (Calle base)

                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {
                            fondo.DrawLine(lista_comp_calles[i].calle_base, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                            primer_nivel.Refresh();
                        }

                        //Subsistema # 2.1 Deteccion de pixeles blancos "Pixeles de la linea base"
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

                        int longitud_x = ancho_lienzo / dist_entre_cll; //DETERMINA EL ESPACIO ENTRE UNA CALLE Y OTRA 
                        int longitud_y = alto_lienzo / dist_entre_cll;
                        //Pen dash_street = new Pen(Color.Yellow,20);
                        //dash_street.DashStyle = DashStyle.Dash;

                        List <Point> desplz_diagonal = new List<Point>();


                        for (int y = 0; y < alto_lienzo; y += dist_entre_cll)  //LINEAS VERTICALES 
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);

                            desplz_diagonal.Add(new Point (azar.Next(0, dist_entre_cll), azar.Next(0, dist_entre_cll/2)));  

                            Point ini, fin;
                            do {
                                ini = new Point(azar.Next(0, ancho_lienzo), y + desplz_diagonal[desplz_diagonal.Count - 1 ].X);
                                fin = new Point(azar.Next(0, ancho_lienzo), y + desplz_diagonal[desplz_diagonal.Count - 1].Y);

                               } while (Math.Abs(ini.X - fin.X)<dist_entre_cll * 2);

                            lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), ini, fin));
                        }
                        for (int x = 0; x < ancho_lienzo; x += dist_entre_cll) //LINEAS HORIZONTALES  
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);

                            Point ini, fin;
                            do
                            {
                                ini = new Point(x + desplz_diagonal[desplz_diagonal.Count - 1].X, azar.Next(0, alto_lienzo));
                                fin = new Point(x + desplz_diagonal[desplz_diagonal.Count - 1].Y, azar.Next(0, alto_lienzo));

                            } while (Math.Abs(ini.Y - fin.Y) < dist_entre_cll * 2);

                            lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100),ini,fin));
                        }
                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {
                            fondo.DrawLine(lista_comp_calles[i].calle_base, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                            primer_nivel.Refresh();
                        }

                        //Subsistema # 2.1 Deteccion de pixeles blancos"
                        lista_puntos_calles = Herramienta.obtener_coor_pixel_blancos((Bitmap)primer_nivel.Image);

                        //Se dibujan las calles
                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {
                            fondo.DrawLine(lista_comp_calles[i].calle, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                            //fondo.DrawLine(dash_street, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                            primer_nivel.Refresh();
                        }
                    }
                    else if (ui_calle_incompleta_diags.Checked == true)
                    {
                        //20/12/2023
                       
                        //FILAS
                        for (int y = dist_entre_cll; y <= alto_lienzo; y += dist_entre_cll)
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);
                            if (ui_autoajustar_dist_calles.Checked == false)
                            {
                                dist_entre_cll = (Herramienta.cambiar_distancia_calle((int)ui_espacio_calles_minimo.Value, (int)ui_espacio_calles_maximo.Value)) * 100;
                            }


                            for (int x = dist_entre_cll; x <= ancho_lienzo; x += dist_entre_cll)
                            {
                                int poblacion = 70;
                                int numerin = azar.Next(0, 99);
                                if (numerin <= poblacion)
                                {
                                    int poblacion_diags = 30;
                                    int numerin_Az = azar.Next(0, 99);

                                    if (numerin_Az <= poblacion_diags) //30% probabilidad de calle diagonal
                                    {
                   
                                        Point ini, fin;

                                        if (azar.Next(0, 2) == 1)
                                        {
                                            ini = new Point(x + ancho_calle * 40 - ( (dist_entre_cll) + (ancho_calle * 100) / 2), y);
                                            fin = new Point(x - ancho_calle * 75, y - dist_entre_cll);
                                        }
                                        else
                                        {
                                            ini = new Point(x + ancho_calle * 75 - ( (dist_entre_cll) + (ancho_calle * 100) / 2), y - dist_entre_cll);
                                            fin = new Point(x - ancho_calle * 40 + (ancho_calle * 100) / 2, y );
                                        }

                                      

                                      //longitud máxima 
                                        lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), ini, fin));
                                    }
                                    else
                                    {
                                        lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), new Point(x - (100 + (dist_entre_cll) + (ancho_calle * 100) / 2), y), new Point(x + 200 + (ancho_calle * 100) / 2, y)));
                                    
                                    
                                    }
                                                                                                       
                                }
                            }
                        }
                        //COLUMNAS
                        for (int x = dist_entre_cll; x <= ancho_lienzo; x += dist_entre_cll)
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);

                            if (ui_autoajustar_dist_calles.Checked == false)
                            {
                                dist_entre_cll = (Herramienta.cambiar_distancia_calle((int)ui_espacio_calles_minimo.Value, (int)ui_espacio_calles_maximo.Value)) * 100;
                            }

                            for (int y = dist_entre_cll; y <= alto_lienzo; y += dist_entre_cll)
                            {
                                int poblacion = 70;
                                int numerin = azar.Next(0, 99);
                                if (numerin <= poblacion)
                                {
                                    lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), new Point(x, y - (100 + (dist_entre_cll) + (ancho_calle * 100) / 2)), new Point(x, y + 200 + (ancho_calle * 100) / 2)));
                                }
                            }
                        }

                        //Se dibujan las veredas (Calle base)

                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {
                            fondo.DrawLine(lista_comp_calles[i].calle_base, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);


                   

                        }

                        //Subsistema # 2.1 Deteccion de pixeles blancos "Pixeles de las lineas blancas"
                        lista_puntos_calles = Herramienta.obtener_coor_pixel_blancos((Bitmap)primer_nivel.Image);

                        //Se dibujan las calles
                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {

                            fondo.DrawLine(lista_comp_calles[i].calle, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                        }

                        //Se dibujan la vereda central 

                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {
                          


                            //vereda central
                            if (azar.Next(0, 2) == 1 && lista_comp_calles[i].calle_base.Width >1500)
                            {
                                int ajs = (int)(lista_comp_calles[i].calle_base.Width );

                                if (Herramienta.orientacion_linea(lista_comp_calles[i].inicio, lista_comp_calles[i].fin) == "Vertical")
                                {
                                    lista_comp_calles[i].inicio.Y = lista_comp_calles[i].inicio.Y + ajs;
                                    lista_comp_calles[i].fin.Y = lista_comp_calles[i].fin.Y - ajs;
                                }
                                else if (Herramienta.orientacion_linea(lista_comp_calles[i].inicio, lista_comp_calles[i].fin) == "Horizontal")
                                {                              
                                    lista_comp_calles[i].inicio.X = lista_comp_calles[i].inicio.X + ajs;
                                    lista_comp_calles[i].fin.X = lista_comp_calles[i].fin.X - ajs;
                                }

                                lista_comp_calles[i].calle_base.Width = 100;

                                fondo.DrawLine(lista_comp_calles[i].calle_base, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                            }

                        }


                        primer_nivel.Refresh();
                    }
                    else if (ui_calle_incompleta_v2.Checked == true)
                    {
                        //FILAS
                        for (int y = dist_entre_cll; y <= alto_lienzo; y += dist_entre_cll)
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);
                            if (ui_autoajustar_dist_calles.Checked == false)
                            {
                                dist_entre_cll = (Herramienta.cambiar_distancia_calle((int)ui_espacio_calles_minimo.Value, (int)ui_espacio_calles_maximo.Value)) * 100;
                            }


                            for (int x = dist_entre_cll; x <= ancho_lienzo; x += dist_entre_cll)
                            {
                                int poblacion = 60;
                                int numerin = azar.Next(0, 99);
                                if (numerin <= poblacion)
                                {
                                    lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), new Point(x - (100+(dist_entre_cll) + (ancho_calle*100)/2), y), new Point(x +200+ (ancho_calle*100)/2, y)));
                                }
                            }
                        }
                        //COLUMNAS
                        for (int x = dist_entre_cll; x <= ancho_lienzo; x += dist_entre_cll)
                        {
                            int ancho_calle = 0;
                            int ancho_vereda = 0;
                            do { ancho_calle = azar.Next(Convert.ToInt32(ui_min_ancho_calle.Value), Convert.ToInt32(ui_max_ancho_calle.Value) + 1); } while (ancho_calle % 2 != 0);
                            do { ancho_vereda = azar.Next(Convert.ToInt32(ui_min_ancho_ver.Value), Convert.ToInt32(ui_max_ancho_ver.Value) + 1) * 2; } while (ancho_vereda % 2 != 0);

                            if (ui_autoajustar_dist_calles.Checked == false)
                            {
                                dist_entre_cll = (Herramienta.cambiar_distancia_calle((int)ui_espacio_calles_minimo.Value, (int)ui_espacio_calles_maximo.Value)) * 100;
                            }

                            for (int y = dist_entre_cll; y <= alto_lienzo; y += dist_entre_cll)
                            {
                                int poblacion = 60;
                                int numerin = azar.Next(0, 99);
                                if (numerin <= poblacion)
                                {
                                    lista_comp_calles.Add(new Composicion_calle(new Pen(Color.White, (ancho_calle + ancho_vereda) * 100 + 1), new Pen(Color.FromArgb(88, 88, 88), ancho_calle * 100), new Point(x, y - (100+(dist_entre_cll)+(ancho_calle*100)/2)), new Point(x, y +200+(ancho_calle*100)/2)));
                                }
                            }
                        }

                        //Se dibujan las veredas (Calle base)

                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {
                            fondo.DrawLine(lista_comp_calles[i].calle_base, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                        }

                        //Subsistema # 2.1 Deteccion de pixeles blancos "Pixeles de las lineas blancas"
                        lista_puntos_calles = Herramienta.obtener_coor_pixel_blancos((Bitmap)primer_nivel.Image);

                        //Se dibujan las calles
                        for (int i = 0; i < lista_comp_calles.Count; i++)
                        {
                            
                            fondo.DrawLine(lista_comp_calles[i].calle, lista_comp_calles[i].inicio, lista_comp_calles[i].fin);
                        }
                        primer_nivel.Refresh();
                    }
                   
                    else if (ui_calle_curvilineal.Checked == true)
                    {

                    }
                    else if (ui_calle_callejones.Checked == true)
                    {

                    }
                } while (MessageBox.Show("¿Desea volver a generar calles?", "Sistema", MessageBoxButtons.YesNo) == DialogResult.Yes);

            }
            //MessageBox.Show(Convert.ToString(lista_puntos_calles.Count));


            //Subsistema # 3 de recoleccion de datos: casas

            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();

          
       
            for (int ubicacion_datos = 0; ubicacion_datos < ui_cantidad_casas.Value; ubicacion_datos++)
            {
                //Actualización del progress bar #1

                //barra.Value = (int)cronometro_proceso.Elapsed.TotalSeconds;
            

                if (cronometro.ElapsedMilliseconds >= Convert.ToInt32( ui_tiempo_espera.Value) * 1000)
                {
                    MessageBox.Show("Superó el tiempo limite ", "Operación cancelada",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }
            

                //Guardo en una variable el valor para los grados
                int grados = 0;
                if (ui_checkbox_girar.Checked)
                {
                    if (ui_checkbox_girar_ordenar.Checked)
                    {
                        int seleccionar = azar.Next(0, 2);  //casa gira o rota al azar dependiendo de esta variable
                        if (seleccionar == 0)
                        {
                            grados = azar.Next(0, 361);
                        }
                        else if (seleccionar == 1)
                        {

                            int selec = azar.Next(0, 4);
                            switch (selec)
                            {
                                case 0:
                                    grados = 90;
                                    break;
                                case 1:
                                    grados = 180;
                                    break;
                                case 2:
                                    grados = 270;
                                    break;
                                case 3:
                                    grados = 360;
                                    break;
                            }
                        }
                    }
                    else 
                    {
                        grados = azar.Next(0, 361);
                    }                                 
                }
                
                List<String> nombres_de_formas = new List<string>
                {
                    "ui_forma_casa_rectangular",
                    "ui_forma_casa_deformada",
                    "ui_forma_casa_deformada_chaflan"
                };

                String vano_ventana_seleccionado = ui_group_box_vanos_ventanas.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

                //En caso de que una de las casas no encaje y pasaron mas de 1 segundo cambiara su tamaño

                if (cronometro.ElapsedMilliseconds > Convert.ToInt32(ui_tiempo_espera.Value) * 1000 - (Convert.ToInt32(ui_tiempo_espera.Value) * 1000 * 0.01)) //Espera el 90% del tiempo de respuesta
                {
                 anchos[ubicacion_datos] = azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1);
                 altos[ubicacion_datos] = azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1);
                }

                //actualizacion de label info ubicaciones

                label45.Text = ubicacion_datos.ToString()+" de "+ui_cantidad_casas.Value.ToString();
                label45.Refresh();

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
                        if (y_ori >= (alto_lienzo - (Convert.ToInt32(ui_max_alto_casa.Value) * 100)))  // SI LLEGA A LOS LIMITES DE Y POR ESO EL >= EJ 20000
                        {
                            x_ori = x_ori + avance_en_x;
                            y_ori = Convert.ToInt32(ui_min_alto_casa.Value) * 100;
                        }
                        origen = new Point(x_ori, y_ori);
                        y_ori = y_ori + avance_en_y;   // antes de avance se usaba 100
                        break;
                    case "ui_distribucion_filas":
                        if (x_ori >= (ancho_lienzo - (Convert.ToInt32(ui_max_ancho_casa.Value) * 100)))
                        {
                            x_ori = Convert.ToInt32(ui_min_ancho_casa.Value) * 100;
                            y_ori = y_ori + avance_en_y;

                            if (y_ori >= (alto_lienzo - (Convert.ToInt32(ui_max_alto_casa.Value) * 100)))  // SI LLEGA A LOS LIMITES DE Y POR ESO EL >= EJ 20000
                            {
                             // MessageBox.Show(("ha alcanzado el limite" + ubicacion_datos.ToString()));
                                x_ori = 100;
                                y_ori = 100;

                                anchos = new List<int>();
                                altos = new List<int>();

                                for (int i = 0; i < ui_cantidad_casas.Value; i++)
                                {
                                    anchos.Add (azar.Next(Convert.ToInt32(ui_min_ancho_casa.Value), Convert.ToInt32(ui_max_ancho_casa.Value) + 1));
                                    altos.Add (azar.Next(Convert.ToInt32(ui_min_alto_casa.Value), Convert.ToInt32(ui_max_alto_casa.Value) + 1));
                                }

                                label48.Text = "Más de una";
                                
                            }
                        }
                        origen = new Point(x_ori, y_ori);
                        x_ori = x_ori + avance_en_x;
                        break;
                    case "ui_distribucion_alternable": // SOLO TOMA LO ANTERIOR Y LO ALTERNA AL LLEGAR LA FINAL, COLUMNA O FILA.

                        //bool h_o_v = true; esta variable estará al principio de esta funcion
                        if (h_o_v)
                        {
                            if (y_ori >= (alto_lienzo - (Convert.ToInt32(ui_max_alto_casa.Value) * 100)))
                            {
                                respaldo_x_ori = x_ori;
                                x_ori = x_ori + avance_en_x;
                                y_ori = respaldo_y_ori; // vuelve al origen
                                h_o_v = false;
                            }
                            origen = new Point(x_ori, y_ori);
                            y_ori = y_ori + avance_en_y;
                        }
                        else
                        {
                            if (x_ori >= (ancho_lienzo - (Convert.ToInt32(ui_max_ancho_casa.Value) * 100)))
                            {
                                respaldo_y_ori = y_ori;
                                y_ori = y_ori + avance_en_y;
                                x_ori = respaldo_x_ori;  //vuelve al origen
                                h_o_v = true;
                            }
                            origen = new Point(x_ori, y_ori);
                            x_ori = x_ori + avance_en_x;
                        }

                        break;
                }
                  
                //Aqui empieza la recoleccion de la informacion para las casas

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
                 10, // Este numero se multiplica por el valor de la columna Ej 3 espacio = 90 (30CM)
                 azar.Next(1, 5),
                 ui_checkbox_girar.Checked,
                 Probabilidad,
                 Distancia,
                 ui_pegar_casas.Checked,
                 vano_ventana_seleccionado,
                 nombres_de_formas[azar.Next(0,nombres_de_formas.Count)],
                 azar.Next(Convert.ToInt32(ui_pilar_prox_min.Value), Convert.ToInt32(ui_pilar_prox_max.Value)),
                 azar.Next(Convert.ToInt32(ui_vano_puerta_cant_min.Value), Convert.ToInt32(ui_vano_puerta_cant_max.Value)),
                 azar.Next(0, 99),
                 azar.Next(0, 99),
                 azar.Next(1, 5),
                 ui_deformacion_alterada.Checked
                );

                nueva_casa.resp_alto_forma = nueva_casa.alto_forma;
                nueva_casa.resp_ancho_forma = nueva_casa.ancho_forma;

                //Subsistema 3.2 #Filtro de puntos ocupados

                //Verifica si punto de origen ya apareció      <--------------> Solo en distribución aleatoria

                if (ui_distribucion_aleatoria.Checked)
                {
                    bool existe = false;

                    if (lista_puntos_origen.Contains(origen))
                    {
                        existe = true;
                    }
                    if (existe)
                    {
                        ubicacion_datos--;
                        continue;
                    }
                    else
                    {
                        lista_puntos_origen.Add(origen);
                    }
                }

               

                //Verifica si existe interseccion entre casas y calles
                bool interruptor = false;

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

                //añadido 20/11/2023

                if (interruptor)
                {
                    ubicacion_datos--;
                    continue;
                }

                //Verificar si existe interseccion entre casas
                //Esta verificación me deja una gran leccion 29/11/20 :)  

                if ( ui_montar_casas.Checked == false)  // añadí este if el 22/03/2023
                {
                    for (int x = lista_casas.Count - 1; x >= 0; x--)  // Empezando desde ultima casa, para aumentar la velocidad 
                    {
                        if (interruptor) //break cuando hay intersección en el bucle anterior 29/03/2023
                        {
                            break;
                        }
                        
                        Parallel.For(0, nueva_casa.area_puntos.Count - 1, (i,state) =>
                        {
                            if (lista_casas[x].area_puntos.Contains(nueva_casa.area_puntos[i]))
                            {
                                //Existe interseccion
                                interruptor = true;
                                state.Break();                              
                            }
                        });
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
                    //nueva_casa.area_post();
                    lista_casas.Add(nueva_casa);

                    //if (ui_objetos_elevador.Checked == true)
                    //{
                    //    //Valida que el elevador este dentro del espacio de la forma
                    //    bool encontrado = false;
                    //    do
                    //    {
                    //        nueva_casa.origen_elevador = Herramienta.seleccionar_punto_cuadricula(nueva_casa.po.X + nueva_casa.ancho_forma * 100, nueva_casa.po.Y + nueva_casa.alto_forma * 100, 100, nueva_casa.po.X, nueva_casa.po.Y);
                    //        nueva_casa.espacio_elevador = new Rectangle(nueva_casa.origen_elevador.X, nueva_casa.origen_elevador.Y, 2 * 100, 2 * 100);
                    //        Rectangle resultado = Rectangle.Intersect(nueva_casa.espacio_elevador, nueva_casa.espacio_forma);
                    //        if (resultado == nueva_casa.espacio_elevador)
                    //        { encontrado = true; }
                    //    } while (encontrado == false);
                    //}

                }
            }

            //Tomo la poblacion de los objetos 
             int Poblacion_objetos = 0;

            switch (ui_groupbox_poblacion_objetos.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name) 
            { 
                case "ui_poblacion_objetos_baja":
                    Poblacion_objetos = 33;
                    break;
                case "ui_poblacion_objetos_media":
                    Poblacion_objetos = 66;
                    break;
                case "ui_poblacion_objetos_alta":
                    Poblacion_objetos = 99;
                    break;
                case "ui_poblacion_objetos_aleatoria":
                    Poblacion_objetos = 100;
                    break;
            }


            //Subsistema #4 superposiciones
            //Encuentro el nombre del radiobutton de la forma que ha escogido el usuario

            String forma_seleccionada = ui_groupbox_forma_casas.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

           

            //FUNCIONES DE SUPERPOSICIONES
            //Superposicion constante mantiene las mismas medidas, lo que no puede conservar es la misma forma 

            void superposicion_con(int recorrer, int i) //i es el iterado del piso, recorrer el iterador de una casa
            {
                string nombre_page = "Planta " + i;

                List<String> nombres_checkbox = new List<string>();
                foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                {
                    if (c.Checked == true) { nombres_checkbox.Add(c.Name); }
                }
                
               
                Formas.forma(forma_seleccionada, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                //Primero se guardan los nombre de los checkbox activo es una lista

              

                //Después de pintar las casas, se pintan los objetos
                Objetos.seleccionados(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0],Poblacion_objetos, true);
                Objetos.seleccionados_primer_plano(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0], Poblacion_objetos, true);
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

                    List<String> nombres_checkbox = new List<string>();
                    foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                    {
                        if (c.Checked == true) { nombres_checkbox.Add(c.Name); }
                    }
                    

                    Formas.forma(forma_seleccionada, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                    //Después de pintar las casas, se pintan los objetos

                    //Primero se guardan los nombre de los checkbox activo es una lista

             
                    
                    Objetos.seleccionados(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0], Poblacion_objetos, true);
                    Objetos.seleccionados_primer_plano(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0], Poblacion_objetos, true);

                    //Esta variable es modificada una vez que PB se haya dibujado
                    lista_casas[recorrer].ubicacion_pb = false;
                }
                //Actualización del progress bar #1

                barra.Value = (int)cronometro_proceso.Elapsed.TotalSeconds;
            }


            void superposicion_alternable(int recorrer, int i) 
            {
                if (i > 0) // A PARTIR DEL PRIMER PISO EMPIEZA LA VARIACION 
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

                    if (i%2 != 0) // es impar  
                    {
                        lista_casas[recorrer].nuevo_origen = new Point(lista_casas[recorrer].po.X + ((valor_reduccion * 100) / 2), lista_casas[recorrer].po.Y + ((valor_reduccion * 100) / 2));
                        lista_casas[recorrer].po = lista_casas[recorrer].nuevo_origen;
                        lista_casas[recorrer].ancho_forma = lista_casas[recorrer].ancho_forma - valor_reduccion;
                        lista_casas[recorrer].alto_forma = lista_casas[recorrer].alto_forma - valor_reduccion;
                    }
                    else
                    {
                        lista_casas[recorrer].nuevo_origen = new Point(lista_casas[recorrer].po.X - ((valor_reduccion * 100) / 2), lista_casas[recorrer].po.Y - ((valor_reduccion * 100) / 2));
                        lista_casas[recorrer].po = lista_casas[recorrer].nuevo_origen;
                        lista_casas[recorrer].ancho_forma = lista_casas[recorrer].ancho_forma + valor_reduccion;
                        lista_casas[recorrer].alto_forma = lista_casas[recorrer].alto_forma + valor_reduccion;
                    }

                }

                if (lista_casas[recorrer].ancho_forma > 1 && lista_casas[recorrer].alto_forma > 1)  // Solo se dibuja si es mayor a 1 el ancho o el alto
                {
                    string nombre_page = "Planta " + i;

                    List<String> nombres_checkbox = new List<string>();
                    foreach (CheckBox c in ui_groupbox_objetos.Controls.OfType<CheckBox>())
                    {
                        if (c.Checked == true) { nombres_checkbox.Add(c.Name); }
                    }
                    
                    Formas.forma(forma_seleccionada, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0]);

                    //Después de pintar las casas, se pintan los objetos

                    //Primero se guardan los nombre de los checkbox activo es una lista

                    
                    Objetos.seleccionados(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0], Poblacion_objetos, true);
                    Objetos.seleccionados_primer_plano(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0], Poblacion_objetos, true);

                    //Esta variable es modificada una vez que PB se haya dibujado
                    lista_casas[recorrer].ubicacion_pb = false;
                }
                //Actualización del progress bar #1

                //barra.Value = (int)cronometro_proceso.Elapsed.TotalSeconds;

            }

            void superposicion_ascendente(int recorrer, int i)  // esta funcion es lo opuesto a piramidal 
            {
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
                    lista_casas[recorrer].nuevo_origen = new Point(lista_casas[recorrer].po.X - ((valor_reduccion * 100) / 2), lista_casas[recorrer].po.Y - ((valor_reduccion * 100) / 2));
                    lista_casas[recorrer].po = lista_casas[recorrer].nuevo_origen;
                    lista_casas[recorrer].ancho_forma = lista_casas[recorrer].ancho_forma + valor_reduccion;
                    lista_casas[recorrer].alto_forma = lista_casas[recorrer].alto_forma + valor_reduccion;
                }

                if (lista_casas[recorrer].ancho_forma != 0 && lista_casas[recorrer].alto_forma !=0)  // Solo se dibuja si el piso anterior existe por eso !=0
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
                    Objetos.seleccionados(nombres_checkbox, lista_casas[recorrer], (PictureBox)TabControl.TabPages[i].Controls.Find(nombre_page, true)[0], Poblacion_objetos, true);

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
                    if (ui_superposicion_piramidal.Checked == true) //este modo puede hacer parecer que "quitar algunos pisos" parezca activo
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
                    if (ui_superposicion_alternable.Checked == true) 
                    {
                        if (ui_quitar_algunos_pisos.Checked)
                        {
                            if (lista_casas[recorrer].pisos_reales > 0)
                            {
                                superposicion_alternable(recorrer, i);
                            }

                            lista_casas[recorrer].pisos_reales = lista_casas[recorrer].pisos_reales - 1; // disminuye un piso

                        }
                        else
                        {
                            superposicion_alternable(recorrer, i); ;
                        }

                    }
                    if (ui_superposicion_combinar.Checked == true)
                    {
                        int x = azar.Next(1, 4);
                        switch(x)
                        {
                          case 1:
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
                                break;
                          case 2:
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
                                break;
                           case 3:
                                if (ui_quitar_algunos_pisos.Checked)
                                {
                                    if (lista_casas[recorrer].pisos_reales > 0)
                                    {
                                        superposicion_alternable(recorrer, i);
                                    }

                                    lista_casas[recorrer].pisos_reales = lista_casas[recorrer].pisos_reales - 1; // disminuye un piso

                                }
                                else
                                {
                                    superposicion_alternable(recorrer, i); ;
                                }
                                break;
                            //case 4: //superposicion ascendente... no lo considero coherente y solo lo es para la combinacion
                            //    if (ui_quitar_algunos_pisos.Checked)
                            //    {
                            //        if (lista_casas[recorrer].pisos_reales > 0)
                            //        {
                            //            superposicion_ascendente(recorrer, i);
                            //        }

                            //        lista_casas[recorrer].pisos_reales = lista_casas[recorrer].pisos_reales - 1; // disminuye un piso

                            //    }
                            //    else
                            //    {
                            //        superposicion_ascendente(recorrer, i); ;
                            //    }
                            //    break; 

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
                label45.Text = "---";
                label48.Text = "1";
          
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

                //Añadido 05/01/2024
                Graphics fondo = Graphics.FromImage(bmp);
                Brush brocha_fondo = new SolidBrush(Color.White);
                fondo.FillRectangle(brocha_fondo, new Rectangle(new Point(0, 0), new Size(ancho, alto)));
                //*****************************************


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

            // para guardarlas En las siguientes lineas recolecto las imagenes de los TabControls


            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Seleccionar carpeta";
            dialog.Filter = "( *.png) | *.png";
            dialog.FileName = "NuevaCiudad";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                string path = System.IO.Path.GetDirectoryName(dialog.FileName);

                for (int i = 0; i < TabControl.TabCount; i++)
                {
                    string fileName = "Planta " + i + ".png";
                    string fullPath = Path.Combine(path, fileName);
                    PictureBox nueva_imagen = (PictureBox)TabControl.TabPages[i].Controls.Find("Planta " + i, true)[0];
                    nueva_imagen.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

                }

            }

            MessageBox.Show("Elementos guardados.");
             
        }


        private void emfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //for (int i = 0; i < TabControl.TabCount; i++)
            //{
            //    SaveFileDialog dialog = new SaveFileDialog();
            //    dialog.Filter = "( *.emf) | *.emf";
            //    dialog.FileName = "Planta " + i;

            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        PictureBox nueva_imagen = (PictureBox)TabControl.TabPages[i].Controls.Find("Planta " + i, true)[0];
            //        nueva_imagen.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Emf);
            //    }
            //}
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Seleccionar carpeta";
            dialog.Filter = "( *.png) | *.png";
            dialog.FileName = "NuevaCiudad";

            // Initialize an instance of the ImageVectorizer class
            var vectorizer = new ImageVectorizer
            {
                //optionally set configuration
                Configuration =
                        {
			                //optionally set path builder
                            PathBuilder = new BezierPathBuilder {
			                //optionally set trace smoother
                            TraceSmoother = new ImageTraceSmoother(1),
                                ErrorThreshold =  30,
                                MaxIterations = 50
                            },
                            ColorsLimit = 25,
                            LineWidth = 3
                        }
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                string path = System.IO.Path.GetDirectoryName(dialog.FileName);
                for (int i = 0; i < 4; i++)
                {

                    String nombre_png = "Planta " + i.ToString() + ".png";
                    nombre_png = Path.Combine(path, nombre_png);
                    String nombre_svg = "Planta_" + i.ToString() + ".svg";
                    nombre_svg = Path.Combine(path, nombre_svg);
                    // Vectorize image from the specified file
                    var document = vectorizer.Vectorize(nombre_png);
                    // Save vectorized image as SVG file 
                    document.Save(nombre_svg);
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

        private void ui_autoajustar_dist_calles_CheckedChanged(object sender, EventArgs e)
        {
            ui_espacio_calles_minimo.Enabled = !ui_autoajustar_dist_calles.Checked;
        }

        private void ui_checkbox_girar_CheckedChanged(object sender, EventArgs e)
        {
            ui_checkbox_girar_ordenar.Enabled = !ui_checkbox_girar.Checked;
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ui_distribucion_alternable_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void ui_montar_casas_CheckedChanged(object sender, EventArgs e)
        {
            ui_distribucion_aleatoria.Checked = ui_montar_casas.Checked;
        }

        private void ui_detener_Click(object sender, EventArgs e)
        {
            
        }

        private void ui_deformacion_alterada_CheckedChanged(object sender, EventArgs e)
        {
            if (ui_deformacion_alterada.Checked)
            {
                Probabilidad.Enabled = false;
                Distancia.Enabled = false;
            }
            else 
            {
                Probabilidad.Enabled = true;
                Distancia.Enabled = true;
            }
        }
    }
}
