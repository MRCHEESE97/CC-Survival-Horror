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

        public static SolidBrush Brocha = new SolidBrush(Color.Orange);
        public static void forma( String seleccion_forma, Info_forma datos, PictureBox lienzo)
        {
            if (seleccion_forma.Equals("ui_forma_casa_rectangular"))
            { 
                rectangulo(datos, lienzo); 
            }
            else if (seleccion_forma.Equals("ui_forma_casa_deformada"))
            {
                rectangulo_deformado(datos, lienzo, 1);
            }
            else if (seleccion_forma.Equals("ui_forma_casa_deformada_chaflan"))
            {
                rectangulo_deformado(datos, lienzo, 2);
            }
            else if (seleccion_forma.Equals("ui_forma_casa_hexagonal"))
            {
                hexagono(datos, lienzo);
            }
            else if (seleccion_forma.Equals("ui_forma_casa_combinar"))
            {
                combinar(datos, lienzo);
            }
           
        }


      

        private static void combinar(Info_forma informacion, PictureBox pintura)
        {   
            forma(informacion.forma, informacion, pintura);
        }
        private static void hexagono(Info_forma informacion, PictureBox pintura)
        {
           
            informacion.g = Graphics.FromImage((Bitmap)pintura.Image);
            Point[] shape = new Point[6];
            Pen borde = new Pen(Color.Black, informacion.grosor_pared);
            int[] array = { informacion.ancho_forma, informacion.alto_forma };
            int r = array.Min() * 100; 

            //Create 6 points
            for (int a = 0; a < 6; a++)
            {
                shape[a] = new Point(
                    (int)(informacion.po.X + r * (float)Math.Cos(a * 60 * Math.PI / 180)),
                    (int)(informacion.po.Y + r * (float)Math.Sin(a * 60 * Math.PI / 180)));
            }
            informacion.contorno = shape.ToList();
            informacion.g.FillPolygon(Brocha, shape);
            informacion.g.DrawPolygon(borde, shape);
            
            
        }
        private static void rectangulo(Info_forma inf, PictureBox pintura)
        {                     
            
            inf.g = Graphics.FromImage((Bitmap)pintura.Image);
            List<Point> rectangulo = new List<Point>();
          
            // Se usan 4 listas para cada lado
            List<Point> lado_superior = Herramienta.calcular_lado(inf.po,inf.ancho_forma,"x");
            List<Point> lado_izquierdo = Herramienta.calcular_lado(inf.po, inf.alto_forma, "y");
            List<Point> lado_derecho = Herramienta.calcular_lado(new Point(inf.po.X + inf.ancho_forma * 100, inf.po.Y), inf.alto_forma, "y");
            List<Point> lado_inferior = Herramienta.calcular_lado(new Point(inf.po.X, inf.po.Y + inf.alto_forma * 100), inf.ancho_forma, "x");

            rectangulo.AddRange(lado_superior);
            rectangulo.AddRange(lado_derecho);
            lado_inferior.Reverse();
            rectangulo.AddRange(lado_inferior);
            lado_izquierdo.Reverse();
            rectangulo.AddRange(lado_izquierdo);

            //Elimino los puntos repetidos
            rectangulo = rectangulo.Distinct().ToList();

            //Se dibuja la pared
            Pen borde = new Pen(Color.Black, inf.grosor_pared);

            rectangulo = Herramienta.rotar_puntos_figuras(rectangulo,0,inf.punto_medio);

            //Despues de rotar guardo los puntos en el objeto
            inf.contorno = rectangulo;

            inf.g.FillPolygon(Brocha, rectangulo.ToArray());
            inf.g.DrawPolygon(borde, rectangulo.ToArray());

            pintura.Refresh();
        }
      
        private static void rectangulo_deformado(Info_forma info, PictureBox pintura, int modo)
        {
            
            info.g = Graphics.FromImage((Bitmap)pintura.Image);

            Random random = new Random();
            /* Está función la desarrollé en la primera versión en el 2019, 
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
            Pen contorno = new Pen(Color.Black,info.grosor_pared);

            // Se usan 4 listas para cada lado
            List<Point> lado_arriba = new List<Point>();
            List<Point> lado_derecho = new List<Point>();
            List<Point> lado_izquierdo = new List<Point>();
            List<Point> lado_abajo = new List<Point>();

            /*
            Una variable llamada "desplazar hacia el centro" determina si 2 puntos se mueven.
            si es cualquier número a excepción de 0 no moverá los vertices
            si es cero los vertices serán atraidos al centro 
             __________
            |        __|
            |       |__   <--- "ej: desplazar_al_centro = 0"
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
                    // Deformacion alterada, otro añadido el 23/11/2023
                    if (info.def_alt)
                    {
                        //distancia_des_alto = random.Next(1, 10);
                        desplazar_al_centro = random.Next(0, 10);
                    }
                    //---------------------------------------------
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
                    // Deformacion alterada, otro añadido el 23/11/2023
                    if (info.def_alt) 
                    {
                        //distancia_des_alto = random.Next(1, 10);
                        desplazar_al_centro = random.Next(0, 10);
                    }                    
                    //---------------------------------------------
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
                    // Deformacion alterada, otro añadido el 23/11/2023
                    if (info.def_alt)
                    {
                        //distancia_des_alto = random.Next(1, 10);
                        desplazar_al_centro = random.Next(0, 10);
                    }
                    //---------------------------------------------
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
                    // Deformacion alterada, otro añadido el 23/11/2023
                    if (info.def_alt)
                    {
                        //distancia_des_alto = random.Next(1, 10);
                        desplazar_al_centro = random.Next(0, 10);
                    }
                    //---------------------------------------------
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
            //irregular = Herramienta.rotar_puntos_figuras(irregular, info.grados, info.punto_medio);

            //Guardo los puntos en el objeto
            info.contorno = irregular;
            info.g.FillPolygon(Brocha, irregular.ToArray());
            info.g.DrawPolygon(contorno, irregular.ToArray());

            pintura.Refresh();
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
