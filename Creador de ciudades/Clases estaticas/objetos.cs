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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Creador_de_ciudades.Form1;

namespace Creador_de_ciudades.Clases_estaticas
{
    static class Objetos
    {
        
        public static Random azar = new Random();
        public static void seleccionados(List<String> seleccion_objeto, Info_forma datos, PictureBox lienzo, int poblacion)   
        {
            for (int i = 0; i < seleccion_objeto.Count ; i++)
            {
                //POBLACION SI ES ALEATORIA
                if (poblacion == 100)
                {
                    poblacion =  azar.Next(0, 99);
                }

                int numerin = azar.Next(0,99);
                if (numerin <= poblacion)
                {
                    //A PARTIR DE AQUI SE EMPIEZAN A DIBUJAR TODOS

                    String nombre_objeto = seleccion_objeto[i];

                    if (nombre_objeto.Equals("ui_objetos_columna_cuadrada"))
                    {
                        columna_cuadrada(datos, lienzo);
                    }
                    else if (nombre_objeto.Equals("ui_objetos_columna_redonda"))
                    {
                        columna_circular(datos, lienzo);
                    }
                    else if (nombre_objeto.Equals("ui_objetos_ventana"))
                    {
                        if (datos.vano_ventana.Equals("ui_objetos_ventana_ale"))
                        {
                            ventanas(datos, lienzo);
                        }
                        else if (datos.vano_ventana.Equals("ui_objetos_ventana_binaria"))
                        {
                            ventanas_binarias(datos, lienzo);
                        }
                        else if (datos.vano_ventana.Equals("ui_objetos_ventana_total"))
                        {
                            ventanas_totales(datos, lienzo);
                        }
                        else if (datos.vano_ventana.Equals("ui_objetos_ventana_todos"))
                        {
                            int s = azar.Next(0, 3);
                            if (s == 0)
                            { ventanas_binarias(datos, lienzo); }
                            else if (s == 1)
                            { ventanas_binarias(datos, lienzo); }
                            else if (s == 2)
                            { ventanas_totales(datos, lienzo); }
                        }
                    }

                    else if (nombre_objeto.Equals("ui_objetos_puerta") && datos.ubicacion_pb)
                    {
                        puerta(datos, lienzo);
                    }

                    else if (nombre_objeto.Equals("ui_objetos_elevador"))
                    {
                        elevador(datos, lienzo);
                    }
                }
                else 
                {
                    continue;
                }
               
            }          
        }
        private static void puerta(Info_forma informacion, PictureBox pintura)
        {
            //Por limitaciones tecnicas no se puede usar decimales para la puerta
            //debido a eso tendré que fijar una medida 1,2,3 m maximo
            int cantidad = 0;
            List<Point> borrador = new List<Point>();

            do {

                Pen puerta = new Pen(Color.Purple, informacion.grosor_pared);
                

                int ubicacion_punto = azar.Next(0, informacion.contorno.Count - informacion.ancho_puerta - 1);

                Point punto_inicio;
                Point punto_fin;


                // se define el inicio y el fin
                punto_inicio = informacion.contorno[ubicacion_punto];
                punto_fin = informacion.contorno[ubicacion_punto + 1];



                informacion.g.DrawLine(puerta, punto_inicio, punto_fin);
               

                //Para que no se dibuje otro objeto encima, tendré que borrar los puntos
                borrador.Add(punto_inicio);
                borrador.Add(punto_fin);

             

                cantidad = cantidad + 1;
            } while (cantidad != informacion.cant_puerta);

            informacion.contorno = informacion.contorno.Except(borrador).ToList();
        }
        private static void columna_cuadrada(Info_forma info, PictureBox pintura)
        {

            Brush dibujar = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            int mitad_col = info.columna_cuadrada_med / 2;

            for (int i = 0; i < info.contorno.Count - info.col_prox; i+=info.col_prox)
            {
                Point origen_columna = new Point(info.contorno[i].X - mitad_col, info.contorno[i].Y - mitad_col);
                Rectangle columna = new Rectangle(origen_columna,new Size(info.columna_cuadrada_med,info.columna_cuadrada_med));
                info.g.FillRectangle(dibujar,columna);
            }
           
        }
        private static void columna_circular(Info_forma info, PictureBox pintura)
        {
            
            Brush dibujar = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            int mitad_col = info.columna_redonda_med / 2;

            for (int i = 0; i < info.contorno.Count - info.col_prox; i+=info.col_prox)
            {
                Point origen_columna = new Point(info.contorno[i].X - mitad_col, info.contorno[i].Y - mitad_col);
                Rectangle columna = new Rectangle(origen_columna, new Size(info.columna_redonda_med, info.columna_redonda_med));
                info.g.FillEllipse(dibujar, columna);
            }
        }

        private static void ventanas(Info_forma informacion, PictureBox pintura)
        {

            Pen ventana = new Pen(Color.Blue, informacion.grosor_pared);
            List<bool> marcar = new List<bool>();

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                int x = azar.Next(0,2);
                if (x == 0)
                { marcar.Add(false); }
                else if (x == 1)
                { marcar.Add(true); }        
            }

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                if (marcar[i])
                {
                    informacion.g.DrawLine(ventana, informacion.contorno[i], informacion.contorno[i+1]);                  
                }       
            }         
        }
        private static void ventanas_binarias(Info_forma informacion, PictureBox pintura)
        {

            Pen ventana = new Pen(Color.Blue, informacion.grosor_pared);
            List<bool> marcar = new List<bool>();

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {              
                if (i % 2 == 0)
                { marcar.Add(false); }
                else 
                { marcar.Add(true); }
            }

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                if (marcar[i])
                {
                    informacion.g.DrawLine(ventana, informacion.contorno[i], informacion.contorno[i + 1]);
                }
            }
        }
        private static void ventanas_totales(Info_forma informacion, PictureBox pintura)
        {

            Pen ventana = new Pen(Color.Blue, informacion.grosor_pared);
            List<bool> marcar = new List<bool>();

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {            
               marcar.Add(true); 
            }

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                if (marcar[i])
                {
                    informacion.g.DrawLine(ventana, informacion.contorno[i], informacion.contorno[i + 1]);
                }
            }
        }

        private static void elevador(Info_forma informacion, PictureBox pintura)
        {
           /*

            //Aqui se dibuja la pared
            Brush brocha_pared = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Rectangle pared = new Rectangle(informacion.origen_elevador, new Size(2 * 100, 2 * 100));
            informacion.g.FillRectangle(brocha_pared, pared);

            Point punto_origen_suelo = new Point(informacion.origen_elevador.X + informacion.grosor_pared, informacion.origen_elevador.Y + informacion.grosor_pared);

            //Aqui se dibuja el agujero del suelo
            int ancho_suelo = 2 * 100 - informacion.grosor_pared * 2;
            int alto_suelo = 2 * 100 - informacion.grosor_pared * 2;
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent);

            if (informacion.ubicacion_pb)
            {
                brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209, 209, 135));
            }
            else 
            {
                //Cambiando el modo de composicion paso a modo forzado de pintado. Predeterminado: SourceOver
                informacion.g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            }
            
            informacion.g.FillRectangle(brocha_suelo, suelo);

            //Aqui cambia el punto para que el agujero se desplace, vuelve a dibujar otro agujero
            switch (informacion.mover_ascensor)
            {
                case 1: punto_origen_suelo.Y = punto_origen_suelo.Y - informacion.grosor_pared; break;
                case 2: punto_origen_suelo.X = punto_origen_suelo.X + informacion.grosor_pared; break;
                case 3: punto_origen_suelo.Y = punto_origen_suelo.Y + informacion.grosor_pared; break;
                case 4: punto_origen_suelo.X = punto_origen_suelo.X - informacion.grosor_pared; break;
            }
            suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            
            if (informacion.ubicacion_pb)
            {  brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209, 209, 135));}
            else { brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent); }
            
            informacion.g.FillRectangle(brocha_suelo, suelo); */
        }

    }
}
