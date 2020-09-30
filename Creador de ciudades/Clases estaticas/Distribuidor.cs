using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Creador_de_ciudades.Clases
{
    static class Distribuidor
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

        public static void rotar_grafico(Graphics e, int grados, int ancho, int alto) 
        {
            e.TranslateTransform(ancho/2, alto/2);
            e.RotateTransform(grados);
            e.TranslateTransform(- ancho / 2, - alto / 2);
        }

        public static Point rotar_punto(Point objetivo, int grados)
        {
            double angle = grados * (Math.PI / 180);
            Double s = Math.Sin(angle); // angle is in radians
            Double c = Math.Cos(angle); // angle is in radians
            Double xnew = objetivo.X * c - objetivo.Y * s;
            Double ynew = objetivo.X * s + objetivo.Y * c;

            return new Point((int)xnew, (int)ynew);
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
