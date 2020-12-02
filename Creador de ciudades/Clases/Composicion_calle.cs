using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creador_de_ciudades.Clases
{
    class Composicion_calle
    {

        public int ancho_total, ancho_calle;
        public Point inicio;
        public Point fin;
        public Point ext1;
        public Pen calle_base, calle;

        //Linea
        public Composicion_calle(Pen Calle_base, Pen Calle, Point Inicio, Point Fin)
        {
            inicio = Inicio;
            fin = Fin;
            calle_base =Calle_base;
            calle = Calle;
        }
        public Composicion_calle(int Ancho_total, int Ancho_calle,Point Inicio, Point Fin)
        {
            ancho_total = Ancho_total;
            ancho_calle = Ancho_calle;
            inicio = Inicio;
            fin = Fin;
        }
        public Composicion_calle(Point Inicio, Point Fin, Point Avanzado)
        {
            inicio = Inicio;
            fin = Fin;
            ext1 = Avanzado;
        }

    }
}
