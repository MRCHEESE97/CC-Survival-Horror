using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Creador_de_ciudades.Clases
{
    static class cuadriculas
    {
        public static Point cuadricula_normal(int ancho_lienzo, int alto_lienzo)
        {
            Random azar = new Random();

            //Aqui se define el tamaño de la cuadricula, siempre será 1/4 del tamaño del lienzo

            float ancho_cuadricula = ancho_lienzo/(float)1.3;
            float alto_cuadricula = alto_lienzo/(float)1.3;

            int x = buscar_multiplo(ancho_cuadricula);
            int y = buscar_multiplo(alto_cuadricula);

            int encontrado;

            int buscar_multiplo(float a)
            {
                encontrado = azar.Next(0,(int)a);
                if (!(encontrado % 10 == 0))
                {
                    return buscar_multiplo(a);
                }
                else
                {
                    return encontrado;
                }
            }        
            
            return new Point(x,y);
        }
        
    }
}
