using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creador_de_ciudades.Clases
{
    static class cuadriculas
    {
        public static List<Point> cuadricula_normal(int ancho_lienzo, int alto_lienzo)
        {
            List<Point> cuadricula = new List<Point>();

            //Aqui se define el tamaño de la cuadricula, siempre será 1/4 del tamaño del lienzo

            float ancho_cuadricula = ancho_lienzo/(float)1.2;
            float alto_cuadricula = alto_lienzo/(float)1.2;

            //Creando la cuadricula de puntos

            for (int x = 0; x < ancho_cuadricula; x+=10)
            {
                for (int y = 0; y < alto_cuadricula; y+=10)
                {
                    cuadricula.Add(new Point(x,y));
                }
            }

            return cuadricula;
        }
        
    }
}
