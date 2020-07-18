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
    static class formas
    {    
        public static void forma(RadioButton seleccion_forma, datos_forma datos, PictureBox lienzo)
        {
            if (seleccion_forma.Checked == true)
            { 
                rectangulo(datos, lienzo); 
            }
        }

        private static void rectangulo(datos_forma informacion, PictureBox pintura)
        {
            Pen pincel = new Pen(Color.Black,informacion.grosor_pared);
            Bitmap bmp = (Bitmap)pintura.Image;
            Graphics g;
            g = Graphics.FromImage(bmp);
            Rectangle r = new Rectangle(informacion.punto_origen,new Size(informacion.ancho_forma * 100,informacion.alto_forma * 100));
            g.DrawRectangle(pincel,r);
            pintura.Image = bmp;
            pintura.Refresh();
                   
        }


    }
}
