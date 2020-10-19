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
        public static void objeto(List<String> seleccion_objeto, Info_forma datos, PictureBox lienzo)
        {
            for (int i = 0; i < seleccion_objeto.Count ; i++)
            {
                String nombre_objeto = seleccion_objeto[i];

                if (nombre_objeto.Equals("ui_objetos_ventana"))
                {
                    ventanas(datos, lienzo);
                }
                else if (nombre_objeto.Equals("ui_objetos_columna_cuadrada"))
                {
                    columna_cuadrada(datos, lienzo);
                }
                else if (nombre_objeto.Equals("ui_objetos_columna_redonda"))
                {
                    columna_circular(datos, lienzo);
                }
                else if (nombre_objeto.Equals("ui_objetos_elevador"))
                {
                    elevador(datos, lienzo);
                }
                else if (nombre_objeto.Equals("ui_objetos_puerta"))
                {
                    puerta(datos, lienzo); 
                }
            }          
        }
        private static void puerta(Info_forma informacion, PictureBox pintura)
        {
            //Por limitaciones de la API no se puede usar decimales para la puerta
            //debido a eso tendré que fijar una medida 1,2,3 m maximo
            int cantidad = 0;

            do {

                Pen puerta = new Pen(Color.Red, informacion.grosor_pared);
                Pen col_puerta = new Pen(Color.Black, informacion.columna_cuadrada_med);

                int ubicacion_punto = azar.Next(0, informacion.contorno.Count - informacion.ancho_puerta - 1);

                Point punto_inicio;
                Point punto_fin;
                int conteo = 0;
                do {
                    ubicacion_punto = ubicacion_punto + 1;

                    punto_inicio = informacion.contorno[ubicacion_punto];
                    punto_fin = informacion.contorno[ubicacion_punto + 1];
                    informacion.g.DrawLine(puerta, punto_inicio, punto_fin);
                    conteo = conteo + 1;
                } while (conteo != informacion.ancho_puerta);

                cantidad = cantidad + 1;
            } while (cantidad != informacion.cant_puerta);

            
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
            /*Brush brocha_columna = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Pen columnas = new Pen(Color.Green, informacion.grosor_pared);
           
            Point punto_origen_suelo = new Point(informacion.po.X + informacion.grosor_pared / 2, informacion.po.Y + informacion.grosor_pared / 2);
            int ancho_suelo = informacion.ancho_forma * 100 - informacion.grosor_pared;
            int alto_suelo = informacion.alto_forma * 100 - informacion.grosor_pared;
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            informacion.g.DrawRectangle(columnas, suelo);*/
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
