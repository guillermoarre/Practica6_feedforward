using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica6_Feedforward
{
    public partial class Form1 : Form
    {

        //double[,] w1 = new double[2,2];
        double[,] w1;
        double[,] w2;
        double[,] w3;
        double[] u1;
        double[] u2;
        double[] u3;
        int[] percXcapaO;
        int entradas, estados, salidas, capas;
        public Form1()
        {
            
            /*
            double[,] w1 = { { 5.191129, 2.758669 }, { 5.473012, 2.769596 } };
            */
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_feed_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();
                listBox1.Items.Clear();
                //MessageBox.Show("Op 1");
                realizaXOR();
            }
            else if (radioButton2.Checked == true)
            {
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();
                listBox1.Items.Clear();
                //MessageBox.Show("Op 2");
                realizaEjercicio();
            }

        }

        public void realizaXOR()
        {
            w1 = new[,] { { 5.191129, 2.758669 }, { 5.473012, 2.769596 } };
            w2 = new[,] { { 5.839709, -6.186834 }};
            u1 = new[] { -1.90289, -4.127002 };
            u2 = new[] { -2.570539 };
            percXcapaO = new[] { 2 };
            entradas = 2;
            estados = 4;
            salidas = 1;
            capas = 3;
            //MessageBox.Show(u1.Last().ToString());
            realizaTabla(entradas, estados, salidas);
            miRedNeuronal ob1 = new miRedNeuronal(entradas, salidas,capas,ref percXcapaO,ref w1,  ref w2, ref u1, ref u2);
            recorreEntradas(estados,entradas,capas,salidas,ob1,dataGridView1,listBox1);
        }
        public void realizaEjercicio()
        {
            w1 = new[,] { {1.0,1.0}, {1.0,1.0} };
            w2 = new[,] { {1.0,1.0}, {1.0,1.0} };
            w3 = new[,] { { 1.0, 1.0 }, { 1.0, 1.0 }};
            u1 = new[] {0.5,0.5};
            u2 = new[] {0.5,0.5 };
            u3 = new[] {0.5,0.5};
            percXcapaO = new[] { 2, 2 };
            entradas = 2;
            estados = 4;
            salidas = 2;
            capas = 4; 
            realizaTabla(entradas, estados, salidas);
            miRedNeuronal ob2 = new miRedNeuronal(entradas, salidas, capas, ref percXcapaO, ref w1, ref w2,ref w3, ref u1, ref u2,ref u3);
            recorreEntradas(estados,entradas,capas,salidas,ob2,dataGridView1,listBox1);
        }
        public void realizaTabla(int e, int es, int sa)
        {
            dataGridView1.RowCount = es;
            dataGridView1.ColumnCount = e + sa;
            for (int i = 0; i < e; i++)
            {
                dataGridView1.Columns[i].HeaderText = "x"+(i+1).ToString();
            }
            for (int i = e; i < sa+e; i++)
            {
                dataGridView1.Columns[i].HeaderText = "y" + (i-e+1).ToString();
            }
            llenaTabla(es, e);
        }
        public void llenaTabla(int m, int n)
        {
            int i, j, c = 0, x2 = 0;
            int x1 = m / 2;
            for (j = 0; j < n; j++)
            {
                for (i = 0; i < m; i++)
                {
                    if (x2 == 0)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = 0;
                        c++;
                    }
                    else if (x2 == 1)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = 1;
                        c--;
                    }
                    if (c == x1)
                    {
                        //llegaste a la mitad de la tabla
                        x2 = 1;
                    }
                    if (c == 0)
                    {
                        //terminaste con la columa Xn
                        x2 = 0;
                    }
                }
                x1 = x1 / 2;
            }


        }

        static void recorreEntradas(int m, int n,int capas,int sal, miRedNeuronal ob1,DataGridView dg, ListBox lb)
        {
            int contador = 0;
            double[] aux;
            double[] entradas = new double[n];
            aux = new double[sal];
            int i, j;
             
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    entradas[j] = double.Parse(dg.Rows[i].Cells[j].Value.ToString());
                }
                contador++;
                if (sal == 1)
                    aux[0] = ob1.recibeEntradas(entradas);
                else if (sal == 2)
                    aux = ob1.recibeEntradas2(entradas); //recibe vector

                imprimeDatos(capas-1,n,contador,sal,lb,ob1);
                if (sal == 1)
                    dg.Rows[i].Cells[2].Value = aux[0];
                else if (sal == 2)
                { 
                    dg.Rows[i].Cells[2].Value = aux[0];
                    dg.Rows[i].Cells[3].Value = aux[1];
                }
            }
        }
        static void imprimeDatos(int capas,int entradas,int contador,int sal, ListBox lb,miRedNeuronal ob1)
        {
            int i,j;
            lb.Items.Add("");
            lb.Items.Add("PATRÓN No."+ contador.ToString());
            lb.Items.Add("");
            for (i = 0; i < capas; i++)
            {
                lb.Items.Add("Capa no." + (i+2).ToString());
                for (j = 0; j < entradas; j++)
                {
                    lb.Items.Add("C("+(i+2).ToString()+").ac("+j.ToString()+")="+
                    ob1.valores[i, j].ToString());
                }

            }
            lb.Items.Add("Salidas del patrón: " + contador.ToString());
            for (i = 0; i < sal; i++)
            {
                lb.Items.Add("y(" + (contador).ToString() + ")=" + ob1.valores[capas-1,i].ToString());
            }
        }
    }
}
