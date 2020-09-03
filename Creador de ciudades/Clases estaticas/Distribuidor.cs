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
        public static Point cuadricula_normal(int maximox, int maximoy, int multiplo, int minimox, int minimoy)
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
        
    }
}
