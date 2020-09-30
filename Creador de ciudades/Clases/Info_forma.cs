using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creador_de_ciudades.Clases
{
    //Esta clase almacena la información casa forma se esta de cualquier tipo
    public class Info_forma
    {
        public int ancho_lienzo;
        public int alto_lienzo;
        public int ancho_forma;
        public int alto_forma;
        public int resp_ancho_forma;
        public int resp_alto_forma;
        public int grosor_pared;
        public Point punto_origen;
        public Point b,c,d;
        public Point nuevo_origen;
        public int columna_cuadrada_valor;
        public int columna_redonda_valor;
        public Rectangle espacio_forma;
        public Rectangle espacio_elevador;
        public bool ubicacion_pb = true;
        public Point origen_elevador;
        public int mover_ascensor;
        //Solo escala constante variable hace uso de esta variable
        public int pisos_reales;
        public int grados;
        public float distancia_entre_columnas;
        public Graphics g;
        public bool rotar_activo;
        public List<Point> area_puntos;
       
       
        public Info_forma(int Ancho_lienzo, int Alto_Lienzo, int Ancho_forma, int Alto_forma, int Grosor_pared, Point Punto_origen, Point Nuevo_origen, int Columna_cuadrada_valor, int Columna_redonda_valor, int Pisos_reales, int Grados, float Distancia_entre_columnas, int Mover_ascensor, bool Rotar) 
        {
            ancho_lienzo = Ancho_lienzo;
            alto_lienzo = Alto_Lienzo;
            ancho_forma = Ancho_forma;
            alto_forma = Alto_forma;
            grosor_pared = Grosor_pared;
            punto_origen = Punto_origen;
            b = B();
            c = C();
            d = D();
            nuevo_origen = Nuevo_origen;
            columna_cuadrada_valor = Columna_cuadrada_valor;
            columna_redonda_valor = Columna_redonda_valor;
            espacio_forma = rectangulo();
            pisos_reales = Pisos_reales;
            grados = Grados;
            distancia_entre_columnas = Distancia_entre_columnas;
            mover_ascensor = Mover_ascensor;
            rotar_activo = Rotar;
            area_puntos = area();
        }
        // Toda figura geometrica tendrá un limite para que no haya una interseccion con otras, la forma de este limite será un rectangulo
        private Rectangle rectangulo()
        {
          Rectangle limite = new Rectangle(punto_origen, new Size(ancho_forma * 100, alto_forma * 100));
          return limite;
        }

        private List<Point> area()
        {
            List<Point> recolector = new List<Point>();
            recolector.AddRange(Distribuidor.obtener_puntos_diagonal(punto_origen.X,punto_origen.Y,b.X,b.Y));
            recolector.AddRange(Distribuidor.obtener_puntos_diagonal(b.X, b.Y, d.X, d.Y));
            recolector.AddRange(Distribuidor.obtener_puntos_diagonal(c.X, c.Y, d.X, d.Y));
            recolector.AddRange(Distribuidor.obtener_puntos_diagonal(punto_origen.X, punto_origen.Y, c.X, c.Y));
            //Hasta aqui he encontrado los puntos del cuadrado
            recolector.AddRange(Distribuidor.obtener_puntos_diagonal(d.X, d.Y, punto_origen.X, punto_origen.Y));
            recolector.AddRange(Distribuidor.obtener_puntos_diagonal(b.X, b.Y, c.X, c.Y));
            return recolector;    
        }

        private Point B()
        {
            return new Point(punto_origen.X + ancho_forma * 100, punto_origen.Y);
        }
        private Point C()
        {
            return new Point(punto_origen.X, punto_origen.Y + alto_forma * 100);
        }
        private Point D()
        {
            return new Point(punto_origen.X + ancho_forma * 100, punto_origen.Y + alto_forma * 100);
        }

    }
}
