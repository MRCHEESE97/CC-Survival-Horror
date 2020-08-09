using System;
using System.Collections.Generic;
using System.Drawing;
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
            Bitmap bmp = (Bitmap)pintura.Image;
            Graphics g;
            g = Graphics.FromImage(bmp);
            Point punto_superior_izquierdo = informacion.punto_origen;
            Point punto_superior_derecho = new Point(informacion.punto_origen.X + informacion.ancho_forma * 100 - informacion.columna_cuadrada_valor, informacion.punto_origen.Y);
            Point punto_inferior_izquierdo = new Point(informacion.punto_origen.X, informacion.punto_origen.Y + informacion.alto_forma *100 - informacion.columna_cuadrada_valor);
            Point punto_inferior_derecho = new Point(informacion.punto_origen.X + informacion.ancho_forma * 100 - informacion.columna_cuadrada_valor, informacion.punto_origen.Y + informacion.alto_forma * 100 - informacion.columna_cuadrada_valor);

            Brush brocha_columna = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            Rectangle columna = new Rectangle(punto_superior_izquierdo, new Size(informacion.columna_cuadrada_valor, informacion.columna_cuadrada_valor));
            g.FillRectangle(brocha_columna, columna);
            columna = new Rectangle(punto_superior_derecho, new Size(informacion.columna_cuadrada_valor, informacion.columna_cuadrada_valor));
            g.FillRectangle(brocha_columna, columna);
            columna = new Rectangle(punto_inferior_izquierdo, new Size(informacion.columna_cuadrada_valor, informacion.columna_cuadrada_valor));
            g.FillRectangle(brocha_columna, columna);
            columna = new Rectangle(punto_inferior_derecho, new Size(informacion.columna_cuadrada_valor, informacion.columna_cuadrada_valor));
            g.FillRectangle(brocha_columna, columna);

        }
        private static void columna_circular(datos_forma informacion, PictureBox pintura)
        {

        }
      

    }
}
