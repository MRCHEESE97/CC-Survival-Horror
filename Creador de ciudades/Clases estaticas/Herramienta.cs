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
    static class Herramienta
    {
        public static Point seleccionar_punto_cuadricula(int maximox, int maximoy, int multiplo, int minimox, int minimoy)
        {
            Random azar = new Random();

            int x = buscar_multiplo(minimox, maximox);
            int y = buscar_multiplo(minimoy, maximoy);

            int encontrado;

            int buscar_multiplo(float minimo, float maximo)
            {
                encontrado = azar.Next((int)minimo, (int)maximo);
                if (!(encontrado % multiplo == 0))
                {
                    return buscar_multiplo(minimo, maximo);
                }
                else
                {
                    return encontrado;
                }
            }

            return new Point(x, y);
        }

        public static bool validar_interseccion(Rectangle uno, Rectangle dos)
        {
            bool existe = false;
            if (uno.IntersectsWith(dos))
            {
                existe = true;
            }
            return existe;
        }

        public static List<Point> rotar_lista_puntos(List<Point> puntos, int angle, Point origen)
        {
            //Rota todos los puntos, con un mismo origen y angulo
            List<Point> puntos_a_rotar = new List<Point>();
            for (int i = 0; i < puntos.Count; i++)
            {
                puntos[i] = rotarpunto(puntos[i], origen, angle);
            }
            return puntos;
        }
        public static Point rotarpunto(Point P_rotar, Point P_ori, int angle)
        {
            double radians = (Math.PI / 180) * angle;
            double sin = Math.Sin(radians);
            double cos = Math.Cos(radians);

            // Translate point back to origin
            P_rotar.X -= P_ori.X;
            P_rotar.Y -= P_ori.Y;

            // Rotate point
            double xnew = P_rotar.X * cos - P_rotar.Y * sin;
            double ynew = P_rotar.X * sin + P_rotar.Y * cos;

            // Translate point back
            Point newPoint = new Point((int)xnew + P_ori.X, (int)ynew + P_ori.Y);
            return newPoint;
        }

        public static IEnumerable<Point> obtener_puntos_diagonal(int x0, int y0, int x1, int y1)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                int t;
                t = x0; // swap x0 and y0
                x0 = y0;
                y0 = t;
                t = x1; // swap x1 and y1
                x1 = y1;
                y1 = t;
            }
            if (x0 > x1)
            {
                int t;
                t = x0; // swap x0 and x1
                x0 = x1;
                x1 = t;
                t = y0; // swap y0 and y1
                y0 = y1;
                y1 = t;
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2;
            int ystep = (y0 < y1) ? 1 : -1;
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                yield return new Point((steep ? y : x), (steep ? x : y));
                error = error - dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
            yield break;
        }
        public static List<Point> calcular_lado(Point inicio, int longitud, string eje)
        {
            List<Point> puntos = new List<Point>();
            if (eje == "x")
            {
                for (int i = inicio.X; i <= inicio.X + (longitud * 100); i+= 100)
                {
                    puntos.Add(new Point(i,inicio.Y));
                }
                return puntos; 
            }
            else 
            {
                for (int i = inicio.Y; i <= inicio.Y + (longitud * 100); i += 100)
                {
                    puntos.Add(new Point(inicio.X, i));
                }
                return puntos;
            }
        }
        public static List<Point> obtener_puntos_internos(Point po, int ancho, int alto)
        {
            //Rota todos los puntos, con un mismo origen y angulo
            List<Point> puntos = new List<Point>();
            for (int i = po.X; i <= po.X + ancho * 100; i += 50)
            {
                for (int j = po.Y; j <= po.Y + alto * 100; j += 50)
                {
                    puntos.Add(new Point(i,j));
                }
            }
            return puntos;
        }

        public static int azar_par_o_impar(int min, int max, bool modo)   
        {

            Random azar = new Random();

            int encontrado = azar.Next(min, max);

            if (modo == true)
            {
                if (encontrado % 2 == 0)
                {
                    return azar_par_o_impar(min, max, modo);
                }
                else
                {
                    return encontrado;
                }
            }
            else
            {
                if (encontrado % 2 != 0)
                {
                    return azar_par_o_impar(min, max, modo);
                }
                else
                {
                    return encontrado;
                }
            }
            
        }


    }
}
