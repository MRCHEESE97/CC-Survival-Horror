using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Creador_de_ciudades.Form1;

namespace Creador_de_ciudades.Clases_estaticas
{
    static class objetos
    {
        public static void objeto(List<String> seleccion_objeto, datos_forma datos, PictureBox lienzo)
        {
            for (int i = 0; i < seleccion_objeto.Count ; i++)
            {
                String nombre_objeto = seleccion_objeto[i];
                if (nombre_objeto.Equals("ui_objetos_columna_cuadrada"))
                {
                    columna_cuadrada(datos, lienzo);
                }
                else if (nombre_objeto.Equals("ui_objetos_columna_circular"))
                {
                    columna_circular(datos, lienzo);
                }
                else if (nombre_objeto.Equals("ui_objetos_puerta"))
                {
                    puerta(datos, lienzo);
                }
            }          
        }
        private static void puerta(datos_forma informacion, PictureBox pintura)
        { 
         
        }
        private static void columna_cuadrada(datos_forma informacion, PictureBox pintura)
        {

        }
        private static void columna_circular(datos_forma informacion, PictureBox pintura)
        {

        }
    }
}
