/*  Copyright (c) 2020 José Bravo <galillo1997@gmail.com>
    This file is part of creador de ciudades.

    Creador de ciudades is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see<https://www.gnu.org/licenses/>.
*/
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
            else if (seleccion_forma.Equals("ui_forma_casa_deformada"))
            {
                rectangulo_deformado(datos, lienzo, 1);
            }
            else if (seleccion_forma.Equals("ui_forma_casa_deformada_chaflan"))
            {
                rectangulo_deformado(datos, lienzo, 2);
            }            
        }

        private static void rectangulo(Info_forma informacion, PictureBox pintura)
        {                     
            Bitmap bmp = (Bitmap)pintura.Image;
            informacion.g = Graphics.FromImage(bmp);

            Point a = informacion.po;
            Point b = new Point(a.X + informacion.ancho_forma * 100, a.Y);
            Point c = new Point(a.X, a.Y + informacion.alto_forma * 100); 
            Point d = new Point(a.X + informacion.ancho_forma * 100, a.Y + informacion.alto_forma * 100);

            //Se dibuja la pared
            Pen borde = new Pen(Color.Black, informacion.grosor_pared);
            Brush brocha_pared = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209, 209, 135));
            Point[] rect_pared = {a,b,d,c};
            rect_pared = Herramienta.rotar_lista_puntos(rect_pared.ToList(),informacion.grados,informacion.punto_medio).ToArray();

            //Despues de rotar guardo los puntos en el objeto
            informacion.contorno = rect_pared.ToList();

            informacion.g.FillPolygon(brocha_pared, rect_pared);
            informacion.g.DrawPolygon(borde,rect_pared);
           

            //Prueba  
            informacion.b = Herramienta.rotarpunto(informacion.a, informacion.punto_medio, informacion.grados);
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
            Rectangle pared = new Rectangle(informacion.po, new Size(informacion.ancho_forma * 100, informacion.alto_forma * 100));
            g.FillEllipse(brocha_pared, pared);

            //Aqui se dibuja el suelo
            Point punto_origen_suelo = new Point(informacion.po.X + informacion.grosor_pared, informacion.po.Y + informacion.grosor_pared);
            int ancho_suelo = informacion.ancho_forma * 100 - informacion.grosor_pared * 2;
            int alto_suelo = informacion.alto_forma * 100 - informacion.grosor_pared * 2;
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209, 209, 135));
            g.FillEllipse(brocha_suelo, suelo);
        }
        private static void rectangulo_deformado(Info_forma info, PictureBox pintura, int modo)
        {
            Bitmap bmp = (Bitmap)pintura.Image;
            info.g = Graphics.FromImage(bmp);

            Random random = new Random();
            /* Está función la desarrollé en la primera versión hace 2 años, 
               parametros originales que recibia la función:
               float x, float y, float ancho, float alto, int tipo, List<PointF> pi*/
            int inicioy, iniciox, ultimay, cierrex;
            int x = info.po.X;
            int y = info.po.Y;
            int ancho = info.ancho_forma;
            int alto = info.alto_forma;
            int contador = 1;
            int distancia_des_ancho = (int)Math.Truncate(alto * 0.30) + 1, distancia_des_alto = (int)Math.Truncate(ancho * 0.30) + 1;
            int hund_izq = 0, hund_der = info.ancho_lienzo;
            Point point1 = new Point();
            Point point2 = new Point();
            SolidBrush brocha = new SolidBrush(Color.FromArgb(211, 209, 133));
            Pen contorno = new Pen(Color.Black,info.grosor_pared);

            // Se usan 4 listas para cada lado
            List<Point> lado_arriba = new List<Point>();
            List<Point> lado_derecho = new List<Point>();
            List<Point> lado_izquierdo = new List<Point>();
            List<Point> lado_abajo = new List<Point>();

            /*
            Una variable llamada "desplazar hacia el centro" determina si 2 puntos se mueven.
            si es cualquier número a excepción de 0 no moverá el vertice
            si es cero el vertice será atraido al centro 
             __________
            |        __|
            |       |__   <--- "desplazar_al_centro = 0"
            |          |
            |__________|          
            */

            int conteo = 0, desplazar_al_centro = 0;
            int random_profundidad = 0;

            //Ancho superior     
            while (contador <= ancho)
            {
                /*Esta parte fue agregada para controlar la distancia de deformacion 
                 y la posibilidad*/
                if (conteo == 0)
                {
                    conteo = 1;
                    random_profundidad = random.Next(1, distancia_des_ancho);
                    desplazar_al_centro = random.Next(0, info.posibilidad.Value);
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                else
                {
                    conteo = conteo + 1;
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                //-------------------------------------------
                if (desplazar_al_centro != 0)
                {
                    if (contador == 1)
                    {
                        point1.X = x;
                        point1.Y = y;
                        point2.X = (100 * contador) + x;
                        point2.Y = point1.Y;
                    }
                    else
                    {
                        point1.X = point2.X;
                        point1.Y = y;
                        point2.X = (100 * contador) + x;
                        point2.Y = point1.Y;
                    }
                }
                else
                {
                    if (contador == 1)
                    {
                        point1.X = x;
                        point1.Y = y + (random_profundidad * 100);
                        point2.X = (100 * contador) + x;
                        point2.Y = point1.Y;
                    }
                    else
                    {
                        point1.X = point2.X;
                        point1.Y = y + (random_profundidad * 100);
                        point2.X = (100 * contador) + x;
                        point2.Y = point1.Y;
                    }
                }
                contador = contador + 1;
                lado_arriba.Add(point1);
                lado_arriba.Add(point2);
            }
            // Guarda la Y del primer segmento
            inicioy = lado_arriba[0].Y;
            // Guarda la Y del ultimo segmento

         
            ultimay = lado_arriba[lado_arriba.Count - 1].Y;
            contador = distancia_des_ancho;
            conteo = 0;
            //lado derecho
            while (contador <= alto)
            {
                int punto_inicial_x = (100 * ancho) + x;
                //Esta parte fue agragada para la distancia
                if (conteo == 0)
                {
                    conteo = 1;
                    random_profundidad = random.Next(1, distancia_des_alto);
                    desplazar_al_centro = random.Next(0, info.posibilidad.Value);
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                else
                {
                    conteo = conteo + 1;
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                //-------------------------------------------
                if (contador == distancia_des_ancho)
                {
                    point1.X = punto_inicial_x;
                    point1.Y = ultimay;
                    point2.X = point1.X;
                    point2.Y = (100* contador) + y;
                }
                else
                {
                    if (desplazar_al_centro != 0)
                    {
                        point1.X = punto_inicial_x;
                        point1.Y = point2.Y;
                        point2.X = point1.X;
                        point2.Y = (100 * contador) + y;
                    }
                    else
                    {
                        point1.X = punto_inicial_x - (random_profundidad * 100);
                        point1.Y = point2.Y;
                        point2.X = point1.X;
                        point2.Y = (100 * contador) + y;
                    }
                }
                if (point2.X <= hund_der)
                { hund_der = point2.X - 100; }

                contador = contador + 1;
                lado_derecho.Add(point1);
                lado_derecho.Add(point2);
            }

            iniciox = lado_derecho[lado_derecho.Count - 1].X;
            cierrex = iniciox;

            contador = distancia_des_ancho;

            //lado izquierdo

            while (contador <= alto)
            {
                if (conteo == 0)
                {
                    conteo = 1;
                    random_profundidad = random.Next(1, distancia_des_alto);
                    desplazar_al_centro = random.Next(0, info.posibilidad.Value);
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                else
                {
                    conteo = conteo + 1;
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                //-------------------------------------------
                if (contador == distancia_des_ancho)
                {
                    point1.X = x;
                    point1.Y = inicioy;
                    point2.X = point1.X;
                    point2.Y = (100 * contador) + y;
                }
                else
                {
                    if (desplazar_al_centro != 0)
                    {
                        point1.X = x;
                        point1.Y = point2.Y;
                        point2.X = point1.X;
                        point2.Y = (100 * contador) + y;
                    }
                    else
                    {
                        point1.X = x + (random_profundidad * 100);
                        point1.Y = point2.Y;
                        point2.X = point1.X;
                        point2.Y = (100 * contador) + y;
                    }
                }
                if (point2.X >= hund_izq)
                { hund_izq = point2.X + 100; }

                contador = contador + 1;
                lado_izquierdo.Add(point1);
                lado_izquierdo.Add(point2);
            }

            iniciox = lado_izquierdo[lado_izquierdo.Count - 1].X;
            ultimay = lado_izquierdo[lado_izquierdo.Count - 1].Y;

            contador = distancia_des_alto;
            conteo = 0;

            //Ancho inferior

            while (contador <= ancho + 1)
            {
                int punto_inicial_y = (100 * alto) + y;
                //Esta parte fue agragada para la distancia
                if (conteo == 0)
                {
                    conteo = 1;
                    random_profundidad = random.Next(1, distancia_des_ancho);
                    desplazar_al_centro = random.Next(0, info.posibilidad.Value);
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                else
                {
                    conteo = conteo + 1;
                    if (conteo == info.distancia.Value)
                    { conteo = 0; }
                }
                //-------------------------------------------
                if (point2.X >= hund_der - 100)
                {
                    point1.X = point2.X;
                    point1.Y = punto_inicial_y;
                    point2.X = cierrex;
                    point2.Y = point1.Y;
                    lado_abajo.Add(point1);
                    lado_abajo.Add(point2);
                    break;
                }
                if (contador == distancia_des_alto)
                {
                    point1.X = iniciox;
                    point1.Y = punto_inicial_y;
                    point2.X = hund_izq;
                    point2.Y = point1.Y;
                }
                else
                {
                    if (desplazar_al_centro != 0)
                    {
                        point1.X = point2.X;
                        point1.Y = punto_inicial_y;
                        point2.X = (100 * contador) + x;
                        point2.Y = point1.Y;
                    }
                    else
                    {
                        point1.X = point2.X;
                        point1.Y = punto_inicial_y - (random_profundidad * 100);
                        point2.X = (100 * contador) + x;
                        point2.Y = point1.Y;
                    }
                }
                contador = contador + 1;
                lado_abajo.Add(point1);
                lado_abajo.Add(point2);
            }

            // Tipo de deformacion 2 
            if (modo == 2)
            {
                //deformacion b, tiene un problema de estetica: en ocasiones los vertices de las esquinas desaparecen, en las siguientes lineas lo soluciono
                deformar_lados(lado_arriba);
                deformar_lados(lado_abajo);
                deformar_lados(lado_derecho);
                deformar_lados(lado_izquierdo);
            }

            List<Point> irregular = new List<Point>();

            for (int c = 0; c < lado_arriba.Count; c++)
            { irregular.Add(lado_arriba[c]); }
            for (int c = 0; c < lado_derecho.Count; c++)
            { irregular.Add(lado_derecho[c]); }
            for (int c = lado_abajo.Count - 1; c >= 0; c--)
            { irregular.Add(lado_abajo[c]); }
            for (int c = lado_izquierdo.Count - 1; c >= 0; c--)
            { irregular.Add(lado_izquierdo[c]); }

            //Limpio de la lista los elementos duplicados
            irregular = irregular.Distinct().ToList();

            //Se rotan los puntos 
            irregular = Herramienta.rotar_lista_puntos(irregular, info.grados, info.punto_medio);

            //Guardo los puntos en el objeto
            info.contorno = irregular;

            info.g.FillPolygon(brocha, irregular.ToArray());
            info.g.DrawPolygon(contorno, irregular.ToArray());
            
        }
        static void deformar_lados(List<Point> lado)
        {
            Point v1 = lado[0];
            Point v2 = lado[lado.Count - 1];
            bool duplicado = false;
            double distancia = 0;
            List<int> direcciones_unicas = new List<int>();
            List<Point> puede_quitarse = new List<Point>();

            //Este for se encarga de seleccionar las direcciones de los valores que no se repiten
            for (int c = 0; c < lado.Count(); c++)
            {
                for (int i = 0; i < lado.Count(); i++)
                {
                    if (i == c)
                    {
                        continue;
                    }
                    else
                    {
                        if (lado[c] == lado[i])
                        {
                            duplicado = true;
                            break;
                        }
                        else
                        {
                            duplicado = false;
                        }
                    }
                }
                if (duplicado == false)
                {
                    direcciones_unicas.Add(c);
                }
            }
            //este for guarda en una lista las direcciones de los puntos que se pueden quitar de los unicos
            for (int i = 0; i < direcciones_unicas.Count - 1; i += 2)
            {
                if (!(direcciones_unicas[i] + 1 == direcciones_unicas[i + 1]))
                {
                    puede_quitarse.Add(lado[direcciones_unicas[i]]);
                    puede_quitarse.Add(lado[direcciones_unicas[i + 1]]);
                }
            }
            if (puede_quitarse.Count != 0)
            {
                int tomar_vertice = 0; Random random = new Random();
                for (int i = 0; i < puede_quitarse.Count; i += 2)
                {
                    distancia = Math.Pow(puede_quitarse[i].X - puede_quitarse[i + 1].X, 2) + Math.Pow(puede_quitarse[i].Y - puede_quitarse[i + 1].Y, 2);
                    distancia = Math.Sqrt(distancia);
                    if (distancia <= 100 * 2 + 10)
                    {
                        tomar_vertice = random.Next(0, 2);
                        if (tomar_vertice == 0)
                        {
                            lado.Remove(puede_quitarse[i + 1]);
                        }
                        else
                        { lado.Remove(puede_quitarse[i]); }
                    }
                    else
                    {
                        lado.Remove(puede_quitarse[i]);
                        lado.Remove(puede_quitarse[i + 1]);
                    }
                }
            }
            lado.Insert(0, v1);
            lado.Add(v2);
        }
    }
}
