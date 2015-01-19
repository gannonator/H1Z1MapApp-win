using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace H1Z1MapApp
{
    public partial class Controlls : Form
    {
        Map map;
        public Controlls(Map map)
        {
            InitializeComponent();
            comboBoxMapSize.SelectedIndex = 1;
            numericUpDown1.Maximum = map.NumberOfPins;
            this.map = map;
            map.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x, z;
            z = Convert.ToInt32(textBoxZ.Text);
            x = Convert.ToInt32(textBoxX.Text);
            if (x > -4000 && z > -4000 && x < 4000 && z < 4000)
            {
                map.SetPoint((int)numericUpDown1.Value - 1, Convert.ToInt32(textBoxZ.Text), Convert.ToInt32(textBoxX.Text));
                map.PinNames[(int)numericUpDown1.Value - 1] = textBoxPinName.Text;
                map.DrawPoint();
            }
            else
            {
                MessageBox.Show(this, "Invalid Coordinates X and Y must be not less then -3999 and no more then 3999");
            }
        }

        private void Controlls_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            map.Resize(comboBoxMapSize.SelectedIndex);
            map.DrawPoint();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            textBoxPinName.Text = map.PinNames[(int)numericUpDown1.Value - 1];
        }
    }
}
