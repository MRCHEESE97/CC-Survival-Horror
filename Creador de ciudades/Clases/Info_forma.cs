﻿
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Creador_de_ciudades.Clases
{
    //Esta clase almacena la información casa forma se esta de cualquier tipo
    public class Info_forma
    {
        public int ancho_lienzo;
        public int alto_lienzo;
        public int ancho_forma;
        public int alto_forma;
        public int resp_ancho_forma;
        public int resp_alto_forma;
        public int grosor_pared;
        public Point po;
        //po es el punto_origen
        public Point a,b,c,d;
        public Point punto_medio;
        public Point nuevo_origen;
        public int columna_cuadrada_med;
        public int columna_redonda_med;
        public int col_prox;
        public Rectangle espacio_forma;
        public Rectangle espacio_elevador;
        public bool ubicacion_pb = true;
        public Point origen_elevador;
        public int mover_ascensor;
        //Solo escala constante variable hace uso de esta variable
        public int pisos_reales;
        public int grados;
        public float distancia_entre_columnas;
        public Graphics g;
        public List<Point> area_puntos;
        public TrackBar posibilidad;
        public TrackBar distancia;
        public bool rotar_activo;
        public bool pegar_casas;
        public int ancho_puerta;
        public int cant_puerta;
        //Despues de dibujar las casas obtengo su forma en esta variable
        public List<Point> contorno;
        //Esta variable solo se usa cuando "Combinar formas" esté activo
        public string forma;
        public string vano_ventana;
        



        public Info_forma(int Ancho_lienzo, int Alto_Lienzo, int Ancho_forma, int Alto_forma, int Grosor_pared, Point Punto_origen, Point Nuevo_origen, int Columna_cuadrada_valor, int Columna_redonda_valor, int Pisos_reales, int Grados, float Distancia_entre_columnas, int Mover_ascensor, bool Rotar, TrackBar Posibilidad, TrackBar Distancia, bool Pegar_casas, string Vano_ventana, String Forma, int Col_prox, int Cant_puerta) 
        {
            ancho_lienzo = Ancho_lienzo;
            alto_lienzo = Alto_Lienzo;
            ancho_forma = Ancho_forma;
            alto_forma = Alto_forma;
            grosor_pared = Grosor_pared;
            po = Punto_origen;
            punto_medio = centro();
            pegar_casas = Pegar_casas;
            grados = Grados;
            a = A();
            b = B();
            c = C();
            d = D();           
            nuevo_origen = Nuevo_origen;
            columna_cuadrada_med = Columna_cuadrada_valor;
            columna_redonda_med = Columna_redonda_valor;
            col_prox = Col_prox;
            espacio_forma = rectangulo();
            pisos_reales = Pisos_reales;
            distancia_entre_columnas = Distancia_entre_columnas;
            mover_ascensor = Mover_ascensor;
            rotar_activo = Rotar;
            area_puntos = area();
            posibilidad = Posibilidad;
            distancia = Distancia;
            cant_puerta = Cant_puerta;
            forma = Forma;
            vano_ventana = Vano_ventana;
        }
        // Toda figura geometrica tendrá un limite para que no haya una interseccion con otras, la forma de este limite será un rectangulo
        private Rectangle rectangulo()
        {
          Rectangle limite = new Rectangle(po, new Size(ancho_forma * 100, alto_forma * 100));
          return limite;
        }

        public List<Point> area()
        {
            List<Point> recolector = new List<Point>();
          
            recolector.AddRange(Herramienta.rotar_area_puntos(Herramienta.obtener_puntos_internos(po, ancho_forma, alto_forma,100), grados, punto_medio));
                       
            return recolector;
        }
        public void area_post()
        {
            area_puntos.Clear();
            area_puntos.AddRange(Herramienta.rotar_area_puntos(Herramienta.obtener_puntos_internos(po, ancho_forma, alto_forma,100), grados, punto_medio));        
        }
        private Point A()
        {
            Point a = po;
            if (pegar_casas) { a.X = a.X + 2; a.Y = a.Y + 2;}
            return Herramienta.rotarpunto_area(a, punto_medio, grados);
        }
        private Point B()
        {   
            Point b = new Point(po.X + ancho_forma * 100, po.Y);
            if (pegar_casas) { b.X = b.X - 2; b.Y = b.Y + 2; }
            return Herramienta.rotarpunto_area(b,punto_medio,grados);
        }
        private Point C()
        {
            Point c = new Point(po.X, po.Y + alto_forma * 100);
            if (pegar_casas) { c.X = c.X + 2; c.Y = c.Y - 2;}
           
            return Herramienta.rotarpunto_area(c, punto_medio, grados);
        }
        private Point D()
        {
            Point d = new Point(po.X + ancho_forma * 100, po.Y + alto_forma * 100);
            if (pegar_casas) { d.X = d.X - 2; d.Y = d.Y - 2; }
            return Herramienta.rotarpunto_area(d, punto_medio, grados);
        }
        private Point centro()
        {
            return new Point((po.X + (po.X + ancho_forma * 100))/2 , 
                             (po.Y + (po.Y + alto_forma * 100))/2);
        }
      

    }
}
