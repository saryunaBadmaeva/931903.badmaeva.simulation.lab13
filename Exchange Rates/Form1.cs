using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exchange_Rates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double mu = 0.2, sigma = 0.35;

        Random rnd = new Random();
        double normalRV1, normalRV2;
        double W1 = 0, W2 = 0;
        double l1, l2;
        const double k = 0.1;

        double eur, usd;

        int day = 0;

        bool firstIn = true;

        private void btStSp_Click(object sender, EventArgs e)
        {
            if(firstIn==true)
            {
                usd = (double)edUsd.Value;
                eur = (double)edEur.Value;

                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddXY(0, usd);
                chart1.Series[1].Points.Clear();
                chart1.Series[1].Points.AddXY(0, eur);

                firstIn = false;
            }
            timer1.Enabled = !timer1.Enabled;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            day += 1;

            l1 = rnd.NextDouble();
            l2 = rnd.NextDouble();

            normalRV1 = Math.Sqrt((-2) * Math.Log(l1)) * Math.Sin(2 * Math.PI * l2);
            W1 = /*W1 +*/ (double)(Math.Sqrt(k) * normalRV1 * 0.25);
            usd = usd * Math.Exp((mu - (double)((sigma * sigma) / 2)) * k * 0.25 + (double)(sigma * W1));

            normalRV2 = Math.Sqrt((-2) * Math.Log(l1)) * Math.Cos(2 * Math.PI * l2);
            W2 = /*W2 +*/ (double)(Math.Sqrt(k) * normalRV2 * 0.25);
            eur = eur * Math.Exp((mu - (double)((sigma * sigma) / 2)) * k * 0.25 + (double)(sigma * W2));

            //label3.Text = Math.Round(W1, 2).ToString() + " " + Math.Round(W2, 2).ToString();
            chart1.Series[0].Points.AddXY(day, usd);
            chart1.Series[1].Points.AddXY(day, eur);
        }
    }
}
