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
        public static Point cuadricula_normal(int ancho, int alto)
        {
            Random azar = new Random();

            int x = buscar_multiplo(ancho);
            int y = buscar_multiplo(alto);

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
