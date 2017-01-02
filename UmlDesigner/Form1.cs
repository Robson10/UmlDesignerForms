using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UmlDesigner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            start1.MouseDown += MyControl_MouseDown;
            start1.MouseMove += MyControl_Drag;
            end1.MouseDown += MyControl_MouseDown;
            end1.MouseMove += MyControl_Drag;
        }
        /// <summary>
        /// dwie metody odpowiedzialne za uchwycenie pozycji startowej w ktorej 
        /// zaczynamy przesówać kontrolkę a nastepnie MouseMove odpowiedzialne
        /// juz za samą zmianę pozycji wraz z przemieszczaniem sie kursora
        /// </summary>
        private Point MouseDownLocation;
        private void MyControl_Drag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                (sender as UserControl).Left = e.X + (sender as UserControl).Left - MouseDownLocation.X;
                (sender as UserControl).Top = e.Y + (sender as UserControl).Top - MouseDownLocation.Y;
            }
        }

        private void MyControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        

       


    }
}
