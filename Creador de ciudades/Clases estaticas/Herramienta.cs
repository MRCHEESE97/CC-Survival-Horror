﻿using System;
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
                encontrado = azar.Next((int)minimo,(int)maximo);
                if (!(encontrado % multiplo == 0))
                {
                    return buscar_multiplo(minimo,maximo);
                }
                else
                {
                    return encontrado;
                }
            }        
            
            return new Point(x,y);
        }

        public static bool validar_interseccion( Rectangle uno, Rectangle dos)
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
                puntos[i] = rotarpunto(puntos[i],origen,angle);
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

    }
}