﻿using System;
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
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, ancho_suelo));
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209,209,135));
            g.FillRectangle(brocha_suelo, suelo);

            pintura.Image = bmp;
        }


    }
}
