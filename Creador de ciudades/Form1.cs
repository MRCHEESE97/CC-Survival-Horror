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

        //Subsistema análogo
        //Objetivo: Dibujar los planos en todos los lienzos.

        List<PictureBox> Lienzos = new List<PictureBox>();




    }
}
