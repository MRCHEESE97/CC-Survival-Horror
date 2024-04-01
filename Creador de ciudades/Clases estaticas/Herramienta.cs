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
using System.Runtime.CompilerServices;
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
                //Este if lo añadí por un error
                if (minimo>maximo)
                {
                    encontrado = azar.Next((int)maximo, (int)minimo+1);
                }
                else
                {
                    encontrado = azar.Next((int)minimo, (int)maximo+1);
                }
                
               //Esta es la verdadera funcion importante 
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

        public static List<Point> rotar_area_puntos(List<Point> puntos, int angle, Point origen)
        {
            //Rota todos los puntos, con un mismo origen y angulo

            for (int i = 0; i < puntos.Count; i++)
            {
                puntos[i] = rotarpunto_area(puntos[i], origen, angle);
            }
            return puntos;
        }
        public static Point rotarpunto_area(Point P_rotar, Point P_ori, int angle)
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
            xnew = Math.Truncate(xnew + P_ori.X);
            ynew = Math.Truncate(ynew + P_ori.Y);

            //Dado que PI es un numero irracional y los limitados decimales que se pueden usar generan impresicion
            //En las siguientes lineas añado una mejora, reemplazando las ultimas dos cifras por "0"


            string x = Convert.ToString(xnew);
            if (x.Length > 2)
            {
                x = x.Remove(x.Length - 2) + "00";
                xnew = Convert.ToInt32(x);
            }


            string y = Convert.ToString(ynew);
            if (y.Length > 2)
            {
                y = y.Remove(y.Length - 2) + "00";
                ynew = Convert.ToInt32(y);
            }



            // Translate point back
            Point newPoint = new Point((int)xnew, (int)ynew);
            //MessageBox.Show(Convert.ToString(newPoint));

            return newPoint;

        }
        public static List<Point> rotar_puntos_figuras(List<Point> puntos, int angle, Point origen)
        {
            //Rota todos los puntos, con un mismo origen y angulo

            for (int i = 0; i < puntos.Count; i++)
            {
                puntos[i] = rotarpunto_figura(puntos[i], origen, angle);
            }
            return puntos;
        }
        public static Point rotarpunto_figura(Point P_rotar, Point P_ori, int angle)
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
            xnew = xnew + P_ori.X;
            ynew = ynew + P_ori.Y;

            // Translate point back
            Point newPoint = new Point((int)xnew, (int)ynew);
            //MessageBox.Show(Convert.ToString(newPoint));

            return newPoint;

        }

        public static List<Point> calcular_lado(Point inicio, int longitud, string eje)
        {
            List<Point> puntos = new List<Point>();
            if (eje == "x")
            {
                for (int i = inicio.X; i <= inicio.X + (longitud * 100); i += 100)
                {
                    puntos.Add(new Point(i, inicio.Y));
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
        
        
        // NO SE USA, QUIZA EN EL FUTURO SI
        public static List<PointF> calcular_ladoF(PointF inicio, float longitud, string eje)
        {
            List<PointF> puntos = new List<PointF>();
            if (eje == "x")
            {
                for (float i = inicio.X; i <= inicio.X + (longitud *100); i += 100)
                {
                    puntos.Add(new PointF(i, inicio.Y));
                }
                return puntos;
            }
            else
            {
                for (float i = inicio.Y; i <= inicio.Y + (longitud * 100); i += 100)
                {
                    puntos.Add(new PointF(inicio.X, i));
                }
                return puntos;
            }
        }

        public static List<Point> obtener_puntos_internos(Point po, int ancho, int alto, int avance) // 1m = 10 avance
        {

            List<Point> puntos = new List<Point>();

            for (int i = po.X; i <= po.X + (ancho)  * 100; i += avance)
            {
                for (int j = po.Y; j <= po.Y + (alto) * 100; j += avance)
                {
                    puntos.Add(new Point(i, j));
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
        public static List<Point> obtener_coor_pixel_blancos(Bitmap bitmapImage)
        {
            Color pixelColor;
            List<Point> Blancos = new List<Point>();
            for (int y = 0; y < bitmapImage.Height; y += 100)
            {
                for (int x = 0; x < bitmapImage.Width; x += 100)
                {
                    
                    pixelColor = bitmapImage.GetPixel(x, y);
                    if (pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255)
                    {
                       // bitmapImage.SetPixel(x, y, Color.Red);   comentado 17/09/2023
                        Blancos.Add(new Point(x, y));
                    }
                }
            }
            return Blancos;
        }


        public static List<Point> obtener_coor_pixel_amarillo_interior(Bitmap bitmapImage, Point inicio, Point fin)
        {
            Color pixelColor;
            List<Point> amarillos = new List<Point>();
            for (int y = inicio.Y; y < fin.Y; y += 100)
            {
                for (int x = inicio.X; x < fin.X; x += 100)
                {   

                    if (fin.X > bitmapImage.Width || fin.Y > bitmapImage.Height)
                    {
                        amarillos.Add(new Point(x, y));
                        break;
                    }


                    pixelColor = bitmapImage.GetPixel(x, y);
                    if (pixelColor.R == 255 && pixelColor.G == 165 && pixelColor.B == 0)
                    {
                        // bitmapImage.SetPixel(x, y, Color.Red);  // comentado 17/09/2023
                        amarillos.Add(new Point(x, y));
                    }
                                   
                }
            }
            return amarillos;
        }

        public static List<Point> obtener_coor_pixel_negro_interior(Bitmap bitmapImage, Point inicio, Point fin)
        {
            Color pixelColor;
            List<Point> verdes = new List<Point>();
            for (int y = inicio.Y; y <= fin.Y - 100; y += 100)
            {
                for (int x = inicio.X; x <= fin.X - 100; x += 100)
                {
                    if (fin.X > bitmapImage.Width || fin.Y > bitmapImage.Height)
                    {
                        break;
                    }
                    pixelColor = bitmapImage.GetPixel(x, y); 
                    if (pixelColor.R == 0 && pixelColor.G == 0 && pixelColor.B == 0)
                    {
                        // bitmapImage.SetPixel(x, y, Color.Red);   //comentado 17/09/2023
                        verdes.Add(new Point(x, y));
                    }
                }
            }
            return verdes;
        }

        public static bool pixel_es_de_un_color(Point Z, Bitmap bitmapImage,int R,int G,int B)
        {
           
            Color color = bitmapImage.GetPixel(Z.X, Math.Abs(Z.Y));
            if (color.R == R && color.G == G && color.B == B)
            {
                return true;
            }
            else 
            {
                return false;
            }
           
        }

        public static int retornar_mayor(int a, int b)
        {
            if (a > b)
            {  return a;  }
            else
            {   return b; }
        }

        public static int cambiar_distancia_calle(int minimo, int maximo)
        {
            Random azar = new Random();
            return azar.Next(minimo, maximo + 1);
        }

        public static string orientacion_linea(Point p1, Point p2)
        {
            if (p1.X == p2.X)
            {
                return "Vertical";
            }
            else if (p1.Y == p2.Y)
            {
                return "Horizontal";
            }
            else
            {
                return "Otra";
            }

        }



    }
}
