﻿using System;
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
        public int grosor_pared;
        public Point punto_origen;
        public Point nuevo_origen;
        public int columna_cuadrada_valor;
        public int columna_redonda_valor;

        public Info_forma(int Ancho_lienzo, int Alto_Lienzo, int Ancho_forma, int Alto_forma, int Grosor_pared, Point Punto_origen, Point Nuevo_origen, int Columna_cuadrada_valor, int Columna_redonda_valor) 
        {
            ancho_lienzo = Ancho_lienzo;
            alto_lienzo = Alto_Lienzo;
            ancho_forma = Ancho_forma;
            alto_forma = Alto_forma;
            grosor_pared = Grosor_pared;
            punto_origen = Punto_origen;
            nuevo_origen = Nuevo_origen;
            columna_cuadrada_valor = Columna_cuadrada_valor;
            columna_redonda_valor = Columna_redonda_valor;      
        }
    }
}
