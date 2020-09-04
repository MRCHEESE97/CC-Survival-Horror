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
    static class Objetos
    {
        public static Random azar = new Random();
        public static void objeto(List<String> seleccion_objeto, Info_forma datos, PictureBox lienzo)
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
                else if (nombre_objeto.Equals("ui_objetos_elevador") && datos.ubicacion_pb == false)
                {
                    elevador(datos, lienzo);
                }
                else if (nombre_objeto.Equals("ui_objetos_puerta"))
                {
                    puerta(datos, lienzo); 
                }
            }          
        }
        private static void puerta(Info_forma informacion, PictureBox pintura)
        {   
            //Optimizar

            Bitmap bmp = (Bitmap)pintura.Image;
            Graphics g;
            g = Graphics.FromImage(bmp);
            Brush brocha_puerta = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            Rectangle puerta;
            bool colisiona = true;

            do {
                int seleccion_origen = azar.Next(0, 8);
                Point origen_puerta;
                //Puntos esquineros
                Point punto_superior_izquierdo_izquierdo = new Point(informacion.punto_origen.X, informacion.punto_origen.Y + informacion.columna_cuadrada_valor);
                Point punto_superior_izquierdo_arriba = new Point(informacion.punto_origen.X + informacion.columna_cuadrada_valor, informacion.punto_origen.Y);
                Point punto_superior_derecho_arriba = new Point(informacion.punto_origen.X + informacion.ancho_forma * 100 - informacion.grosor_pared, informacion.punto_origen.Y);
                Point punto_superior_derecho_derecho = new Point(informacion.punto_origen.X + informacion.ancho_forma * 100, informacion.punto_origen.Y + informacion.columna_cuadrada_valor);
                Point punto_inferior_derecho_derecho = new Point(informacion.punto_origen.X + informacion.ancho_forma * 100, informacion.punto_origen.Y + informacion.alto_forma * 100 - informacion.columna_cuadrada_valor);
                Point punto_inferior_derecho_abajo = new Point(informacion.punto_origen.X + informacion.ancho_forma * 100 - informacion.grosor_pared, informacion.punto_origen.Y + informacion.alto_forma * 100 - informacion.grosor_pared);
                Point punto_inferior_izquierdo_abajo = new Point(informacion.punto_origen.X + informacion.columna_cuadrada_valor, informacion.punto_origen.Y + informacion.alto_forma * 100 - informacion.grosor_pared);
                Point punto_inferior_izquierdo_izquierdo = new Point(informacion.punto_origen.X, informacion.punto_origen.Y + informacion.alto_forma * 100 - informacion.columna_cuadrada_valor);
                //Puntos medios
                Point pm_lado_izquierdo = new Point(punto_inferior_izquierdo_izquierdo.X, (punto_inferior_izquierdo_izquierdo.Y + punto_superior_izquierdo_izquierdo.Y)/2);
                Point pm_lado_superior = new Point((punto_superior_derecho_arriba.X + punto_superior_izquierdo_arriba.X)/2, punto_superior_izquierdo_arriba.Y);
                Point pm_lado_derecho = new Point(punto_superior_derecho_arriba.X, (punto_inferior_derecho_derecho.Y + punto_superior_derecho_derecho.Y)/2);
                Point pm_lado_inferior = new Point((punto_inferior_derecho_abajo.X + punto_inferior_izquierdo_abajo.X)/2, punto_inferior_izquierdo_abajo.Y);

                if (seleccion_origen == 0)
                {                    
                    origen_puerta = new Point(pm_lado_izquierdo.X, azar.Next(punto_superior_izquierdo_izquierdo.Y, pm_lado_izquierdo.Y));
                    puerta = new Rectangle(origen_puerta, new Size(informacion.grosor_pared, 100));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)){ colisiona = false; g.FillRectangle(brocha_puerta, puerta); }                  
                }
                else if (seleccion_origen == 1)
                {
                    origen_puerta = new Point(azar.Next(punto_superior_izquierdo_arriba.X, pm_lado_superior.X), pm_lado_superior.Y);
                    puerta = new Rectangle(origen_puerta, new Size(100, informacion.grosor_pared));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)) { colisiona = false; g.FillRectangle(brocha_puerta, puerta); }
                }
                else if (seleccion_origen == 2)
                {
                    origen_puerta = new Point(azar.Next(pm_lado_superior.X, punto_superior_derecho_arriba.X) - 100, pm_lado_superior.Y);
                    puerta = new Rectangle(origen_puerta, new Size(100, informacion.grosor_pared));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)) { colisiona = false; g.FillRectangle(brocha_puerta, puerta); }
                }
                else if (seleccion_origen == 3)
                {
                    origen_puerta = new Point(pm_lado_derecho.X, azar.Next(punto_superior_derecho_derecho.Y, pm_lado_derecho.Y));
                    puerta = new Rectangle(origen_puerta, new Size(informacion.grosor_pared, 100));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)) { colisiona = false; g.FillRectangle(brocha_puerta, puerta); }
                }
                else if (seleccion_origen == 4)
                {
                    origen_puerta = new Point(pm_lado_derecho.X, azar.Next(pm_lado_derecho.Y, punto_inferior_derecho_abajo.Y) - 100);
                    puerta = new Rectangle(origen_puerta, new Size(informacion.grosor_pared, 100));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)) { colisiona = false; g.FillRectangle(brocha_puerta, puerta); }
                }
                else if (seleccion_origen == 5)
                {
                    origen_puerta = new Point(azar.Next(pm_lado_inferior.X, punto_inferior_derecho_abajo.X) - 100, pm_lado_inferior.Y);
                    puerta = new Rectangle(origen_puerta, new Size(100, informacion.grosor_pared));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)) { colisiona = false; g.FillRectangle(brocha_puerta, puerta); }
                }
                else if (seleccion_origen == 6)
                {
                    origen_puerta = new Point(azar.Next(punto_inferior_izquierdo_abajo.X, pm_lado_inferior.X), pm_lado_inferior.Y);
                    puerta = new Rectangle(origen_puerta, new Size(100, informacion.grosor_pared));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)) { colisiona = false; g.FillRectangle(brocha_puerta, puerta); }
                }
                else if (seleccion_origen == 7)
                {
                    origen_puerta = new Point(pm_lado_izquierdo.X, azar.Next(pm_lado_izquierdo.Y, punto_inferior_izquierdo_izquierdo.Y) - 100);
                    puerta = new Rectangle(origen_puerta, new Size(informacion.grosor_pared, 100));
                    if (!Distribuidor.validar_interseccion(puerta, informacion.espacio_elevador)) { colisiona = false; g.FillRectangle(brocha_puerta, puerta); }
                }
            } while(colisiona);  
         
        }
        private static void columna_cuadrada(Info_forma informacion, PictureBox pintura)
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
        private static void columna_circular(Info_forma informacion, PictureBox pintura)
        {

        }

        private static void elevador(Info_forma informacion, PictureBox pintura)
        {
            //Dibuja el elevador

            Bitmap bmp = (Bitmap)pintura.Image;
            Graphics g;
            g = Graphics.FromImage(bmp);

            //Aqui se dibuja la pared
            Brush brocha_pared = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Rectangle pared = new Rectangle(informacion.origen_elevador, new Size(2 * 100, 2 * 100));
            g.FillRectangle(brocha_pared, pared);

            //Aqui se dibuja el suelo
            Point punto_origen_suelo = new Point(informacion.origen_elevador.X + informacion.grosor_pared, informacion.origen_elevador.Y + informacion.grosor_pared);
            int ancho_suelo = 2 * 100 - informacion.grosor_pared * 2;
            int alto_suelo = 2 * 100 - informacion.grosor_pared * 2;
            Rectangle suelo = new Rectangle(punto_origen_suelo, new Size(ancho_suelo, alto_suelo));
            Brush brocha_suelo = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent);

            //Cambiando el modo de composicion paso a modo forzado de pintado. Predeterminado: SourceOver
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            g.FillRectangle(brocha_suelo, suelo);            
        }

    }
}
