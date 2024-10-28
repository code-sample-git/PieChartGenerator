using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PieChartGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //read the input from the textbox
            string input = textBox1.Text;

            List<float> values;

            //parse the input into a list of numbers
            try
            {
                values = input.Split(',').Select(float.Parse).ToList();

            }catch (Exception ex)
            {
                MessageBox.Show("Invalid input. Please enter a list of numbers separated by commas.");
                return;
            }

            //Generate a pie chart based on the input
            PieChartControl pieChart = new PieChartControl();
            pieChart.SetData(values, Enumerable.Range(1, values.Count).Select(i => $"Slice {i}").ToList());
            pieChart.Show();
        }

    }
}
