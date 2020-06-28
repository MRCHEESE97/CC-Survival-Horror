using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Creador_de_ciudades
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //Subsistema TabControl
        //Objetivo: Esta parte del código instancia las paginas para el tabControl

        decimal prevValue_ui_cantidad_pisos = 0;

        private void ui_cantidad_pisos_ValueChanged(object sender, EventArgs e)
        {   
           
            if (ui_cantidad_pisos.Value > prevValue_ui_cantidad_pisos)
            { 
                string title = "Planta " + ((TabControl.TabCount - 1) + 1).ToString();
                TabPage myTabPage = new TabPage(title);
                TabControl.TabPages.Add(myTabPage);
            }
            else if (ui_cantidad_pisos.Value < prevValue_ui_cantidad_pisos)
            {
                TabControl.TabPages.RemoveAt(TabControl.TabCount - 1);
            }

            prevValue_ui_cantidad_pisos = ui_cantidad_pisos.Value;

        }

       
        //Subsistema tamaño lienzos
        //Objetivo: Definir tamaños de los lienzos

        private int ancho_lienzo()
        {
            int multiplicador = 4; // el valor que agranda el lienzo con respecto al tamaño de las casas
            int ancho = ((Convert.ToInt32(ui_max_ancho_casa.Value) * 100) * Convert.ToInt32(ui_cantidad_casas.Value)) * multiplicador;
            return ancho;
        }

        private int alto_lienzo()
        {
            int multiplicador = 4; // el valor que agranda el lienzo con respecto al tamaño de las casas
            int alto = ((Convert.ToInt32(ui_max_alto_casa.Value) * 100) * Convert.ToInt32(ui_cantidad_casas.Value)) * multiplicador;
            return alto;
        }

        List<PictureBox> Lienzos = new List<PictureBox>();

        //Subsistema de datos para los Lienzos

        public struct datos
        {
            Point punto_origen;
            int ancho;
            int alto;
        }

        public List<datos> datos_forma = new List<datos>();

        //Subsistema de dibujo
        //Objetivo: Dibujar los planos en todos los lienzos.

        private void dibujar()
        {
            
            for (int generar_casas = 0; generar_casas < ui_cantidad_casas.Value; generar_casas++)
            {


            }   
      
            for(int recorrer_lienzos=0;recorrer_lienzos<ui_cantidad_pisos.Value;recorrer_lienzos++)
            {
                




            }

        }

        

        private void ui_construir_Click(object sender, EventArgs e)
        {       
            dibujar();
        }
    }
}
