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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static Creador_de_ciudades.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Creador_de_ciudades.Clases_estaticas
{
    static class Objetos
    {
        
        //NOTA: ESTA CLASE ES LLAMADA UNA VEZ POR CASA, LO QUE PERMITE ALTERAR LA "INFO_FORMA" ACTUAL

        public static Random azar = new Random();

        public static void seleccionados_primer_plano(List<String> seleccion_objeto, Info_forma datos, PictureBox lienzo, int poblacion, bool division)
        {

            //Lama a objetos exteriores: 

            for (int i = 0; i < seleccion_objeto.Count; i++)
            {

                string nombre_objeto = seleccion_objeto[i];


                //POBLACION SI ES ALEATORIA
                if (poblacion == 100)
                {
                    poblacion = azar.Next(0, 99);
                }

                int numerin = azar.Next(0, 99);
                if (numerin <= poblacion)
                {
                    //A PARTIR DE AQUI SE EMPIEZAN A DIBUJAR TODOS



                    if (nombre_objeto.Equals("ui_objetos_columna_cuadrada"))
                    {
                        columna_cuadrada(datos, lienzo);
                    }

                }
                else
                {
                    continue;
                }

            }


        }


        public static void seleccionados(List<String> seleccion_objeto, Info_forma datos, PictureBox lienzo, int poblacion, bool division)
        {

            //Lama a objetos exteriores: 

            for (int i = 0; i < seleccion_objeto.Count; i++)
            {

                    string nombre_objeto = seleccion_objeto[i];

                  
                    //POBLACION SI ES ALEATORIA
                    if (poblacion == 100)
                    {
                        poblacion = azar.Next(0, 99);
                    }

                    int numerin = azar.Next(0, 99);
                    if (numerin <= poblacion)
                    {
                        //A PARTIR DE AQUI SE EMPIEZAN A DIBUJAR TODOS


                        if (nombre_objeto.Equals("ui_objetos_columna_redonda"))
                        {
                            columna_circular(datos, lienzo);
                        }
                        else if (nombre_objeto.Equals("ui_objetos_ventana"))
                        {
                            if (datos.vano_ventana.Equals("ui_objetos_ventana_ale"))
                            {
                                ventanas(datos, lienzo);
                            }
                            else if (datos.vano_ventana.Equals("ui_objetos_ventana_binaria"))
                            {
                                ventanas_binarias(datos, lienzo);
                            }
                            else if (datos.vano_ventana.Equals("ui_objetos_ventana_total"))
                            {
                                ventanas_totales(datos, lienzo);
                            }
                            else if (datos.vano_ventana.Equals("ui_objetos_ventana_todos"))
                            {
                                int s = azar.Next(0, 3);
                                if (s == 0)
                                { ventanas_binarias(datos, lienzo); }
                                else if (s == 1)
                                { ventanas_binarias(datos, lienzo); }
                                else if (s == 2)
                                { ventanas_totales(datos, lienzo); }
                            }
                        }

                        else if (nombre_objeto.Equals("ui_objetos_puerta") && datos.ubicacion_pb)
                        {
                            puerta(datos, lienzo);
                        }


                        else if (nombre_objeto.Equals("ui_objetos_elevador"))
                        {
                            elevador(datos, lienzo);
                        }
                        else if (nombre_objeto.Equals("ui_objetos_data"))
                        {
                            data(datos, lienzo);
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
                if (division)
                {
                    //Llama a interiores:
                    divisiones(datos, lienzo);

                }

            //Lama a objetos zot, asc, esc: 

            for (int i = 0; i < seleccion_objeto.Count; i++)
            {

                string nombre_objeto = seleccion_objeto[i];

                //Objetos con poblacion especifica

                if (nombre_objeto.Equals("ui_objetos_puerta_zotano") && datos.ubicacion_pb && datos.prob_zot < 20) //si casa es 20... solo pb
                {
                    zotano(datos, lienzo);
                }

                if (nombre_objeto.Equals("ui_objetos_escalera")) // siempre y todos los pisos 
                {
                    if (datos.ubicacion_pb)
                    {
                        datos.origen_esc = Herramienta.seleccionar_punto_cuadricula(datos.d.X, datos.d.Y, 100, datos.a.X, datos.a.Y); // el mismo origen

                        if (datos.origen_esc.X >= datos.punto_medio.X && datos.origen_esc.Y <= datos.punto_medio.Y)   //der arriba
                        {
                            datos.origen_esc.X = datos.origen_esc.X - (3 * 100);
                        }
                        else if (datos.origen_esc.X >= datos.punto_medio.X && datos.origen_esc.Y >= datos.punto_medio.Y)   //der abajo
                        {
                            datos.origen_esc.Y = datos.origen_esc.Y - (3 * 100);
                            datos.origen_esc.X = datos.origen_esc.X - (3 * 100);
                        }
                    }
                    else if (datos.ubicacion_pb == false)
                    {
                        escalera(datos, lienzo);
                    }
                }

                if (nombre_objeto.Equals("ui_objetos_ascensor") && datos.prob_asc < 30) //si casa es 30... 
                {
                    if (datos.ubicacion_pb)
                    {
                        datos.origen_asc = Herramienta.seleccionar_punto_cuadricula(datos.d.X, datos.d.Y, 200, datos.a.X, datos.a.Y); // el mismo origen

                        if (datos.origen_asc.X >= datos.punto_medio.X && datos.origen_asc.Y <= datos.punto_medio.Y)   //der arriba
                        {
                            datos.origen_asc.X = datos.origen_asc.X - (2 * 100);
                        }
                        else if (datos.origen_asc.X >= datos.punto_medio.X && datos.origen_asc.Y >= datos.punto_medio.Y)   //der abajo
                        {
                            datos.origen_asc.Y = datos.origen_asc.Y - (2 * 100);
                            datos.origen_asc.X = datos.origen_asc.X - (2 * 100);
                        }
                    }
                    else if (datos.ubicacion_pb == false)
                    {
                        ascensor(datos, lienzo);
                    }
                
                }
                
            }



        }
        private static void puerta(Info_forma informacion, PictureBox pintura)
        {
            //Por limitaciones tecnicas no se puede usar decimales para la puerta
            //debido a eso tendré que fijar una medida 1,2,3 m maximo
            int cantidad = 0;
            List<Point> borrador = new List<Point>();

            do {

                Pen puerta = new Pen(Color.Purple, informacion.grosor_pared);
                Pen ensamble = new Pen(Color.Purple, informacion.grosor_pared/2);

                int ubicacion_punto = azar.Next(0, informacion.contorno.Count - 1);

                Point punto_inicio;
                Point punto_fin;

               


                // se define el inicio y el fin
                punto_inicio = informacion.contorno[ubicacion_punto];
                punto_fin = informacion.contorno[ubicacion_punto + 1];
               

                informacion.g.DrawLine(puerta, punto_inicio, punto_fin);
                Point a, b;

                // Ensamble 16/09/2023 al tener el problema de la abertura se me ocurrio emplear una tecnica del ensamble de madera
                bool orientacion = (punto_inicio.X == punto_fin.X) ? false : true;
                if (orientacion)
                {

                    a = new Point(punto_inicio.X - informacion.grosor_pared / 4, punto_inicio.Y + informacion.grosor_pared / 32);
                    b = new Point(punto_fin.X + informacion.grosor_pared / 4, punto_fin.Y + informacion.grosor_pared / 32);
                }
                else 
                {
                    a = new Point(punto_inicio.X + informacion.grosor_pared / 32, punto_inicio.Y - informacion.grosor_pared / 4);
                    b = new Point(punto_fin.X + informacion.grosor_pared / 32, punto_fin.Y + informacion.grosor_pared / 4);
                }

                informacion.g.DrawLine(ensamble, a, b);



                //Para que no se dibuje otro objeto encima, tendré que borrar los puntos
                borrador.Add(punto_inicio);
                borrador.Add(punto_fin);

             

                cantidad = cantidad + 1;
            } while (cantidad != informacion.cant_puerta);

            informacion.contorno = informacion.contorno.Except(borrador).ToList();
        }
        private static void columna_cuadrada(Info_forma info, PictureBox pintura)
        {

            Brush dibujar = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            int mitad_col = info.columna_cuadrada_med / 2;

            for (int i = 0; i < info.contorno.Count - info.col_prox; i+=info.col_prox)
            {
                Point origen_columna = new Point(info.contorno[i].X - mitad_col, info.contorno[i].Y - mitad_col);
                Rectangle columna = new Rectangle(origen_columna,new Size(info.columna_cuadrada_med,info.columna_cuadrada_med));
                info.g.FillRectangle(dibujar,columna);
            }
           
        }
        private static void columna_circular(Info_forma info, PictureBox pintura)
        {
            
            Brush dibujar = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            int mitad_col = info.columna_redonda_med / 2;

            for (int i = 0; i < info.contorno.Count - info.col_prox; i+=info.col_prox)
            {
                Point origen_columna = new Point(info.contorno[i].X - mitad_col, info.contorno[i].Y - mitad_col);
                Rectangle columna = new Rectangle(origen_columna, new Size(info.columna_redonda_med, info.columna_redonda_med));
                info.g.FillEllipse(dibujar, columna);
            }
        }

        private static void ventanas(Info_forma informacion, PictureBox pintura)
        {

            Pen ventana = new Pen(Color.Blue, informacion.grosor_pared);
            Pen ensamble = new Pen(Color.Blue, informacion.grosor_pared/2);
            List<bool> marcar = new List<bool>();

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                int x = azar.Next(0,2);
                if (x == 0)
                { marcar.Add(false); }
                else if (x == 1)
                { marcar.Add(true); }        
            }

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                if (marcar[i])
                {
                    informacion.g.DrawLine(ventana, informacion.contorno[i], informacion.contorno[i+1]);

                    Point a, b;
                    // Ensamble 16/09/2023 al tener el problema de la abertura se me ocurrio emplear la tecnica del ensamble de madera
                    bool orientacion = (informacion.contorno[i].X == informacion.contorno[i + 1].X) ? false : true;
                    if (orientacion)
                    {

                        a = new Point(informacion.contorno[i].X - informacion.grosor_pared / 4, informacion.contorno[i].Y + informacion.grosor_pared / 32);
                        b = new Point(informacion.contorno[i + 1].X + informacion.grosor_pared / 4, informacion.contorno[i + 1].Y + informacion.grosor_pared / 32);
                    }
                    else
                    {
                        a = new Point(informacion.contorno[i].X + informacion.grosor_pared / 32, informacion.contorno[i].Y - informacion.grosor_pared / 4);
                        b = new Point(informacion.contorno[i + 1].X + informacion.grosor_pared / 32, informacion.contorno[i + 1].Y + informacion.grosor_pared / 4);
                    }

                    informacion.g.DrawLine(ensamble, a, b);
                   

                }       
            }         
        }
        private static void ventanas_binarias(Info_forma informacion, PictureBox pintura)
        {

            Pen ventana = new Pen(Color.Blue, informacion.grosor_pared);
            List<bool> marcar = new List<bool>();

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {              
                if (i % 2 == 0)
                { marcar.Add(false); }
                else 
                { marcar.Add(true); }
            }

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                if (marcar[i])
                {
                    informacion.g.DrawLine(ventana, informacion.contorno[i], informacion.contorno[i + 1]);
                }
            }
        }
        private static void ventanas_totales(Info_forma informacion, PictureBox pintura)
        {

            Pen ventana = new Pen(Color.Blue, informacion.grosor_pared);
            List<bool> marcar = new List<bool>();

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {            
               marcar.Add(true); 
            }

            for (int i = 0; i < informacion.contorno.Count - 1; i++)
            {
                if (marcar[i])
                {
                    informacion.g.DrawLine(ventana, informacion.contorno[i], informacion.contorno[i + 1]);
                }
            }
        }

        private static void elevador(Info_forma informacion, PictureBox pintura)
        {
           /*
            //Aqui se dibuja la pared
            Brush brocha_pared = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Rectangle pared = new Rectangle(informacion.origen_elevador, new Size(2 * 100, 2 * 100));
            informacion.g.FillRectangle(brocha_pared, pared);

            Point punto_origen_suelo = new Point(informacion.origen_elevador.X + informacion.grosor_pared, informacion.origen_elevador.Y + informacion.grosor_pared);

            //Aqui se dibuja el agujero del suelo
            int ancho_suelo = 2 * 100 - informacion.grosor_pared * 2;
            int alto_suelo = 2 * 100 - informacion.grosor_pared * 2;
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent);

            if (informacion.ubicacion_pb)
            {
                brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209, 209, 135));
            }
            else 
            {
                //Cambiando el modo de composicion paso a modo forzado de pintado. Predeterminado: SourceOver
                informacion.g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            }
            
            informacion.g.FillRectangle(brocha_suelo, suelo);

            //Aqui cambia el punto para que el agujero se desplace, vuelve a dibujar otro agujero
            switch (informacion.mover_ascensor)
            {
                case 1: punto_origen_suelo.Y = punto_origen_suelo.Y - informacion.grosor_pared; break;
                case 2: punto_origen_suelo.X = punto_origen_suelo.X + informacion.grosor_pared; break;
                case 3: punto_origen_suelo.Y = punto_origen_suelo.Y + informacion.grosor_pared; break;
                case 4: punto_origen_suelo.X = punto_origen_suelo.X - informacion.grosor_pared; break;
            }
            suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            
            if (informacion.ubicacion_pb)
            {  brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(209, 209, 135));}
            else { brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent); }
            
            informacion.g.FillRectangle(brocha_suelo, suelo); */
        }

        private static void banios(Info_forma informacion, PictureBox pintura)
        {

            Pen pared = new Pen(Color.Black, informacion.grosor_pared);

        }

        private static void divisiones(Info_forma informacion, PictureBox pintura)
        {

            Pen pared = new Pen(Color.Black, informacion.grosor_pared+2);

            int tamaño_limite = Herramienta.retornar_mayor(informacion.ancho_forma, informacion.alto_forma)/2; //Una casa solo será hasta un tamaño de la mitad de la capa 
            int cantidad_limite = Herramienta.retornar_mayor(informacion.ancho_forma, informacion.alto_forma) / 3; //Veces que se puede instancia una habitacion de 3 metros
            int cantidad_maxima = azar.Next(2, cantidad_limite+2); 

            //Faltaria dividir por area 
            
            for (int i = 0; i < cantidad_maxima; i++)
            {  
                Point origen_division = Herramienta.seleccionar_punto_cuadricula(informacion.d.X, informacion.d.Y, 200, informacion.a.X, informacion.a.Y);
                int ancho_esta_div = azar.Next(2, tamaño_limite + 1);
                int alto_esta_div = azar.Next(2, tamaño_limite + 1);
             
                if (origen_division.X <= informacion.punto_medio.X && origen_division.Y <= informacion.punto_medio.Y)  //izq arriba
                {
                    
                }
                else if (origen_division.X <= informacion.punto_medio.X && origen_division.Y >= informacion.punto_medio.Y)   //izq abajo
                {
                    origen_division.Y = origen_division.Y - (informacion.alto_forma / 2) * 100;
                }
                else if (origen_division.X >= informacion.punto_medio.X && origen_division.Y <= informacion.punto_medio.Y)   //der arriba
                {
                    origen_division.X = origen_division.X - (informacion.ancho_forma / 2) * 100;
                }
                else if (origen_division.X >= informacion.punto_medio.X && origen_division.Y >= informacion.punto_medio.Y)   //der abajo
                {
                    origen_division.Y = origen_division.Y - (informacion.alto_forma / 2)*100;
                    origen_division.X = origen_division.X - (informacion.ancho_forma / 2)*100;
                }

                // si los pixeles son grises o blancos, continue.
                Info_forma D = new Info_forma(ancho_esta_div, alto_esta_div, 0, informacion.grosor_pared, origen_division, 3);


                if (Herramienta.pixel_es_de_un_color(D.a, (Bitmap)pintura.Image, 255, 255, 255)
                  ||Herramienta.pixel_es_de_un_color(D.b, (Bitmap)pintura.Image, 255, 255, 255)
                  ||Herramienta.pixel_es_de_un_color(D.c, (Bitmap)pintura.Image, 255, 255, 255)
                  ||Herramienta.pixel_es_de_un_color(D.d, (Bitmap)pintura.Image, 255, 255, 255)
                  || Herramienta.pixel_es_de_un_color(D.a, (Bitmap)pintura.Image, 88, 88, 88)
                  || Herramienta.pixel_es_de_un_color(D.b, (Bitmap)pintura.Image, 88, 88, 88)
                  || Herramienta.pixel_es_de_un_color(D.c, (Bitmap)pintura.Image, 88, 88, 88)
                  || Herramienta.pixel_es_de_un_color(D.d, (Bitmap)pintura.Image, 88, 88, 88))
                {
                    continue;
                }
                else
                {
                    Formas.forma("ui_forma_casa_rectangular", D, pintura);

                    //objetos. 1 puerta 
                    List<string> seleccion_objeto = new List<string> { "ui_objetos_puerta" };
                    seleccionados(seleccion_objeto, D, pintura, 99,false);
                    //objetos. 1 columna
                    //seleccion_objeto = new List<string> { "ui_objetos_columna_cuadrada" };
                    //Objetos.seleccionados(seleccion_objeto, D, pintura, 75);

                }

            }

        }
       
        private static void data(Info_forma informacion, PictureBox pintura)
        {

            SolidBrush ventana = new SolidBrush(Color.Blue);
            string Data = informacion.ancho_forma.ToString() + " x " + informacion.alto_forma.ToString();
            Font drawFont = new Font("Arial", 16);

            informacion.g.DrawString(Data, drawFont, ventana, informacion.nuevo_origen.X + (informacion.ancho_forma * 100), informacion.nuevo_origen.Y + (informacion.alto_forma * 100));

        }

        private static void zotano(Info_forma informacion, PictureBox pintura)
        {
            Point origen_zotano = Herramienta.seleccionar_punto_cuadricula(informacion.d.X, informacion.d.Y, 200, informacion.a.X, informacion.a.Y);
            SolidBrush Transparencia = new SolidBrush(Color.DarkGreen);   
            List<Point> rectangulo = new List<Point>();

            // Se usan 4 listas para cada lado
            List<Point> lado_superior = Herramienta.calcular_lado(origen_zotano, 1, "x");
            List<Point> lado_izquierdo = Herramienta.calcular_lado(origen_zotano, 1, "y");
            List<Point> lado_derecho = Herramienta.calcular_lado(new Point(origen_zotano.X + 1 * 100, origen_zotano.Y), 1, "y");
            List<Point> lado_inferior = Herramienta.calcular_lado(new Point(origen_zotano.X, origen_zotano.Y + 1 * 100), 1, "x");

            rectangulo.AddRange(lado_superior);
            rectangulo.AddRange(lado_derecho);
            lado_inferior.Reverse();
            rectangulo.AddRange(lado_inferior);
            lado_izquierdo.Reverse();
            rectangulo.AddRange(lado_izquierdo);

            //Elimino los puntos repetidos
            rectangulo = rectangulo.Distinct().ToList();

            informacion.g.FillPolygon(Transparencia, rectangulo.ToArray());
            //pintura.Refresh();

        }
        private static void ascensor(Info_forma informacion, PictureBox pintura)
        {
            //Se dibuja la pared
            Pen borde = new Pen(Color.Black, informacion.grosor_pared);
            Point origen = informacion.origen_asc;
            SolidBrush Transparencia = new SolidBrush(Color.DarkGreen);
            List<Point> rectangulo = new List<Point>();

            // Se usan 4 listas para cada lado
            List<Point> lado_superior = Herramienta.calcular_lado(origen, 2, "x");
            List<Point> lado_izquierdo = Herramienta.calcular_lado(origen, 2, "y");
            List<Point> lado_derecho = Herramienta.calcular_lado(new Point(origen.X + 2 * 100, origen.Y), 2, "y");
            List<Point> lado_inferior = Herramienta.calcular_lado(new Point(origen.X, origen.Y + 2 * 100), 2, "x");

            rectangulo.AddRange(lado_superior);
            rectangulo.AddRange(lado_derecho);
            lado_inferior.Reverse();
            rectangulo.AddRange(lado_inferior);
            lado_izquierdo.Reverse();
            rectangulo.AddRange(lado_izquierdo);

            //Elimino los puntos repetidos
            rectangulo = rectangulo.Distinct().ToList();

            informacion.g.FillPolygon(Transparencia, rectangulo.ToArray());

            Point[] a = lado_izquierdo.ToArray();
            Point[] b = lado_derecho.ToArray();
            Point[] c = lado_inferior.ToArray();
            Point[] d = lado_superior.ToArray();

            int lado = informacion.Lado_asc;

            if (lado == 1)
            { //abc
                informacion.g.DrawLines(borde, a);
                informacion.g.DrawLines(borde, b);
                informacion.g.DrawLines(borde, c);
            }
            else if (lado == 2)
            { //bcd
                informacion.g.DrawLines(borde, b);
                informacion.g.DrawLines(borde, c);
                informacion.g.DrawLines(borde, d);
            }
            else if (lado == 3)
            { //cda
                informacion.g.DrawLines(borde, c);
                informacion.g.DrawLines(borde, d);
                informacion.g.DrawLines(borde, a);
            }
            else if (lado == 4)
            { //dac
                informacion.g.DrawLines(borde, d);
                informacion.g.DrawLines(borde, a);
                informacion.g.DrawLines(borde, c);
            }

        }
        private static void escalera(Info_forma informacion, PictureBox pintura)
        {
            //Se dibuja la pared
            Pen borde = new Pen(Color.Black, informacion.grosor_pared);
            Point origen_zotano = informacion.origen_esc;
            SolidBrush Transparencia = new SolidBrush(Color.DarkGreen);
            List<Point> rectangulo = new List<Point>();

            // Se usan 4 listas para cada lado
            List<Point> lado_superior = Herramienta.calcular_lado(origen_zotano, 3, "x");
            List<Point> lado_izquierdo = Herramienta.calcular_lado(origen_zotano, 3, "y");
            List<Point> lado_derecho = Herramienta.calcular_lado(new Point(origen_zotano.X + 3 * 100, origen_zotano.Y), 3, "y");
            List<Point> lado_inferior = Herramienta.calcular_lado(new Point(origen_zotano.X, origen_zotano.Y + 3 * 100), 3, "x");

            rectangulo.AddRange(lado_superior);
            rectangulo.AddRange(lado_derecho);
            lado_inferior.Reverse();
            rectangulo.AddRange(lado_inferior);
            lado_izquierdo.Reverse();
            rectangulo.AddRange(lado_izquierdo);

            //Elimino los puntos repetidos
            rectangulo = rectangulo.Distinct().ToList();

            informacion.g.FillPolygon(Transparencia, rectangulo.ToArray());
    
        }

    }
}
