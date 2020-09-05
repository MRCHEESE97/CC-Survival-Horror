using Creador_de_ciudades.Clases;
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
    static class Formas
    {    
        public static void forma( String seleccion_forma, Info_forma datos, PictureBox lienzo)
        {
            if (seleccion_forma.Equals("ui_forma_casa_rectangular"))
            { 
                rectangulo(datos, lienzo); 
            }
            else if(seleccion_forma.Equals("ui_forma_casa_cilindrica"))
            {
                cilindro(datos, lienzo);
            }

        }

        private static void rectangulo(Info_forma informacion, PictureBox pintura)
        {                     
            Bitmap bmp = (Bitmap)pintura.Image;
            Graphics g;
            g = Graphics.FromImage(bmp);

            //Aqui se dibuja la pared
            Brush brocha_pared = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Rectangle pared = new Rectangle(informacion.punto_origen,new Size(informacion.ancho_forma * 100,informacion.alto_forma * 100));
            g.FillRectangle(brocha_pared,pared);

            //Aqui se dibuja el suelo
            Point punto_origen_suelo = new Point(informacion.punto_origen.X + informacion.grosor_pared,informacion.punto_origen.Y + informacion.grosor_pared);
            int ancho_suelo = informacion.ancho_forma * 100 - informacion.grosor_pared * 2;
            int alto_suelo = informacion.alto_forma * 100 - informacion.grosor_pared * 2;
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209,209,135));
            g.FillRectangle(brocha_suelo, suelo);

            pintura.Image = bmp;
        }
        private static void cilindro(Info_forma informacion, PictureBox pintura) 
        {
            Bitmap bmp = (Bitmap)pintura.Image;
            Graphics g;
            g = Graphics.FromImage(bmp);

            //Aqui se dibuja la pared
            Brush brocha_pared = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Rectangle pared = new Rectangle(informacion.punto_origen, new Size(informacion.ancho_forma * 100, informacion.alto_forma * 100));
            g.FillEllipse(brocha_pared, pared);

            //Aqui se dibuja el suelo
            Point punto_origen_suelo = new Point(informacion.punto_origen.X + informacion.grosor_pared, informacion.punto_origen.Y + informacion.grosor_pared);
            int ancho_suelo = informacion.ancho_forma * 100 - informacion.grosor_pared * 2;
            int alto_suelo = informacion.alto_forma * 100 - informacion.grosor_pared * 2;
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209, 209, 135));
            g.FillEllipse(brocha_suelo, suelo);

            pintura.Image = bmp;
        }

    }
}
