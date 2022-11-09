using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Practica6_Feedforward
{
    internal class miRedNeuronal
    {
        double[,] w1_o;
        double[,] act_cap;
        double[,] w2_o;
        double[,] w3_o;
        double[] u1_o;
        double[] u2_o;
        double[] u3_o;
        int[] no_perceXcapaOc;
        private int entradas;
        private int capas;
        private int salidas;
        double[] a0;
        double[] a1;
        double[] a2;
        double[] y;

        public miRedNeuronal(int entradas, int salidas, int capas,ref int[] nopXcap, ref double[,] w1, ref double[,] w2, ref double[] u1, ref double[] u2)
        {
            this.entradas = entradas;
            this.capas = capas - 2;
            this.salidas = salidas;

            w1_o = new double[2, 2];
            w2_o = new double[1,2];
            u1_o = new double[2];
            u2_o = new double[1];
            no_perceXcapaOc = new int[capas];

            a0 = new double[entradas];
            a1 = new double[entradas];
            y = new double[salidas];

            act_cap = new double[capas - 1, entradas];

            this.w1_o = w1;
            this.w2_o = w2;
            this.u1_o = u1;
            this.u2_o = u2;
            this.no_perceXcapaOc = nopXcap;

        }
        public miRedNeuronal(int entradas, int salidas, int capas, ref int[] nopXcap, ref double[,] w1, ref double[,] w2, ref double[,] w3, ref double[] u1, ref double[] u2, ref double[] u3)
        {
            //capas = 4
            this.entradas = entradas;
            this.capas = capas - 2;
            this.salidas = salidas;

            w1_o = new double[2, 2];
            w2_o = new double[2, 2];
            w3_o = new double[2, 2];
            u1_o = new double[2];
            u2_o = new double[2];
            u3_o = new double[2];
            no_perceXcapaOc = new int[capas];

            a0 = new double[entradas];
            a1 = new double[nopXcap[0]];
            a2 = new double[nopXcap[1]];
            y = new double[salidas];

            act_cap = new double[capas - 1, entradas];

            this.w1_o = w1;
            this.w2_o = w2;
            this.w3_o = w3;
            this.u1_o = u1;
            this.u2_o = u2;
            this.u3_o = u3;
            this.no_perceXcapaOc = nopXcap;

        }
        public string mostrarMatriz()
        {
            string aux = "";
            foreach (var i in w1_o)
            {
                aux += i.ToString();
            }
            return aux;
        }
        public double recibeEntradas(double[] entradas1)
        {
            int contador = 0, k=0;
            int percepCapa1 = entradas;
            int i, j;
            double aux=0;
            //capa de entrada
            for (i = 0; i < entradas1.Length; i++)
            {
                a0[i] = entradas1[i];
            }

            do
            {
                //capas ocultas
                for (i = 0; i < entradas; i++)
                {
                    for (j = 0; j < no_perceXcapaOc[k]; j++)
                    {
                        aux += w1_o[j, i] * a0[j];
                    }
                    aux += u1_o[i];
                    aux = realizaFuncion(aux);
                    a1[i] = aux;
                    act_cap[contador, i] = aux;
                    aux = 0;
                }
                k++;
            } while (k < this.capas);

            contador++;
            //capa de salidas
            aux = 0;
            for (i = 0; i < salidas; i++)
            {
                for (j = 0; j < no_perceXcapaOc.Last(); j++)
                {
                    aux += w2_o[0,j] * a1[j];
                }
                aux += u2_o[i];
                aux = realizaFuncion(aux);
                y[i] = aux;
                act_cap[contador,i] = aux;
            } 
            return aux;
        }

        public double[] recibeEntradas2(double[] entradas1)
        {
            int contador = 0, k = 0;
            int percepCapa1 = entradas;
            int i, j, ti=0;
            double aux = 0;
            //capa de entrada
            for (i = 0; i < entradas1.Length; i++)
            {
                a0[i] = entradas1[i];
            }

            do
            {
                //capas ocultas
                for (i = 0; i < entradas; i++)
                {
                    for (j = 0; j < no_perceXcapaOc[k]; j++)
                    {
                        if (k == 0)
                        {
                            aux += w1_o[j, i] * a0[j];
                        }
                        else if (k == 1)
                        {
                            aux += w2_o[j, i] * a1[j];
                        }
                    }
                    if (k == 0)
                    {
                        aux += u1_o[i];
                        aux = realizaFuncion(aux);
                        a1[i] = aux;
                        act_cap[contador, i] = aux;
                    }
                    else if (k == 1)
                    {
                        aux += u2_o[i];
                        aux = realizaFuncion(aux);
                        a2[i] = aux;
                        act_cap[contador, i] = aux;
                    }
                    aux = 0;
                }

                k++;
                contador++;
            } while (k < this.capas);

            aux = 0;
            //capa de salidas
            for (i = 0; i < salidas; i++)
            {
                for (j = 0; j < no_perceXcapaOc.Last(); j++)
                {
                    aux += w3_o[j, i] * a2[j];
                }
                aux += u3_o[i];
                aux = realizaFuncion(aux);
                y[i] = aux;
                act_cap[2, i] = aux;
                aux = 0;
            }
                    return y;
        }


        private double realizaFuncion(double arg)
        {
            return (1 / (1 + Math.Exp(-arg)));
        }
        public double dameValorCapa(int m, int n)
        {
            return act_cap[m,n];
        }


        
        public double[,] valores
        {
            get { return act_cap; }
            set { act_cap = value; }
        }
        
       

    }
}
