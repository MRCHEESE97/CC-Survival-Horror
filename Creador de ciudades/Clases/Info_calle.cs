using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creador_de_ciudades.Clases
{
    class Info_calle
    {
       
        public int ancho_forma;
        public int alto_forma;
        public Point po;
        public Point punto_medio;
        public List<Point> area_puntos;
        public Point a, b, c, d;

        public Info_calle(int Ancho_forma, int Alto_forma,Point Punto_origen)
        {
           
            ancho_forma = Ancho_forma;
            alto_forma = Alto_forma;        
            po = Punto_origen;
            a = A();
            b = B();
            c = C();
            d = D();
            area_puntos = area();
            punto_medio = centro();

        }
        // Toda figura geometrica tendrá un limite para que no haya una interseccion con otras, la forma de este limite será un rectangulo
       
        private List<Point> area()
        {
            List<Point> recolector = new List<Point>();
            recolector.AddRange(Herramienta.obtener_puntos_diagonal(a.X, a.Y, b.X, b.Y));
            recolector.AddRange(Herramienta.obtener_puntos_diagonal(b.X, b.Y, d.X, d.Y));
            recolector.AddRange(Herramienta.obtener_puntos_diagonal(c.X, c.Y, d.X, d.Y));
            recolector.AddRange(Herramienta.obtener_puntos_diagonal(a.X, a.Y, c.X, c.Y));
            //Hasta aqui he encontrado los puntos del cuadrado

            recolector.AddRange(Herramienta.obtener_puntos_diagonal(d.X, d.Y, a.X, a.Y));
            recolector.AddRange(Herramienta.obtener_puntos_diagonal(b.X, b.Y, c.X, c.Y));
            //Encontradas las 2 diagonales

            //recolector.AddRange(Herramienta.obtener_puntos_internos(po, ancho_forma, alto_forma));

            return recolector;
        }
        private Point A()
        {
            Point a = po;
            return a;
        }
        private Point B()
        {
            Point b = new Point(po.X + ancho_forma * 100, po.Y);          
            return b;
        }
        private Point C()
        {
            Point c = new Point(po.X, po.Y + alto_forma * 100);
            return c;
        }
        private Point D()
        {
            Point d = new Point(po.X + ancho_forma * 100, po.Y + alto_forma * 100);
            return d;
        }
        private Point centro()
        {
            return new Point((po.X + (po.X + ancho_forma * 100)) / 2,
                             (po.Y + (po.Y + alto_forma * 100)) / 2);
        }
    }
}
