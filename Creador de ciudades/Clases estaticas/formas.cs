using Creador_de_ciudades.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            informacion.g = Graphics.FromImage(bmp);

            Point a = informacion.punto_origen;
            Point b = new Point(a.X + informacion.ancho_forma * 100, a.Y);
            Point c = new Point(a.X, a.Y + informacion.alto_forma * 100); 
            Point d = new Point(a.X + informacion.ancho_forma * 100, a.Y + informacion.alto_forma * 100);
          
            //Se dibuja la pared
            Brush brocha_pared = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Point[] rect_pared = {a,b,d,c};
            rect_pared = Herramienta.rotar_lista_puntos(rect_pared.ToList(),informacion.grados,informacion.punto_medio).ToArray();
            informacion.g.FillPolygon(brocha_pared,rect_pared);

            
            //Se dibuja el suelo
            a = new Point(informacion.punto_origen.X + informacion.grosor_pared,informacion.punto_origen.Y + informacion.grosor_pared);
            b = new Point(a.X + informacion.ancho_forma * 100 - informacion.grosor_pared * 2, a.Y);
            c = new Point(a.X , a.Y + informacion.alto_forma * 100 - informacion.grosor_pared * 2);
            d = new Point(a.X + informacion.ancho_forma * 100 - informacion.grosor_pared * 2, a.Y + informacion.alto_forma * 100 - informacion.grosor_pared * 2);
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209,209,135));
            Point[] rect_suelo = { a, b, d, c };
            rect_suelo = Herramienta.rotar_lista_puntos(rect_suelo.ToList(), informacion.grados, informacion.punto_medio).ToArray();
            informacion.g.FillPolygon(brocha_suelo,rect_suelo);

            //Prueba  
            informacion.b = Herramienta.rotarpunto(informacion.punto_origen, informacion.punto_medio, informacion.grados);
            informacion.c = Herramienta.rotarpunto(informacion.d, informacion.punto_medio, informacion.grados);
            informacion.g.DrawLine(new Pen(Color.Black, 10), informacion.b, informacion.c);
            //Prueba

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
        }

    }
}
