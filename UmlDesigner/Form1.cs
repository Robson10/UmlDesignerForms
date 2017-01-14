using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner.Components;
namespace UmlDesigner
{
    public partial class Form1 : Form
    {
        List<MainUserControl> UserControls = new List<MainUserControl>();

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            MouseClick += OnMouseDown;
            KeyUp += Form1_KeyUp;
            KeyDown += Form1_KeyDown;
        }
        /// <summary>
        /// Eventy służące do odczytywania wciśniętego oraz puszczonego klawisza. służą do wykrywania 
        /// wielokrotnego zaznaczania
        /// </summary>
        Keys IsMultiSelect = Keys.None;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            IsMultiSelect = e.KeyCode;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            IsMultiSelect = Keys.None;
        }

        private Point MouseDownLocation;
        /// <summary>
        /// Metoda odznaczająca zaznaczone kontrolki
        /// </summary>
        private void DeselectControls()
        {
            for (int i = 0; i < UserControls.Count; i++)
                UserControls[i].IsSelected = false;
        }
        /// <summary>
        /// Metoda zapisująca miejsce wciśnięcia lewego glawisza myszy na kontrolce z wyłączeniem gumek posiadającymi włąsny event
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (IsMultiSelect != Keys.ControlKey)
                DeselectControls();

            if (e.Button == MouseButtons.Left && sender is MainUserControl)
            {
                (sender as MainUserControl).IsSelected = true;
                MouseDownLocation = e.Location;
            }
        }
        /// <summary>
        /// Metoda służąca do zmiany położenia Kontrolki 
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseMove(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < UserControls.Count; i++)
                {
                    if (UserControls[i].IsSelected && !UserControls[i].IsProtected)
                    {
                        UserControls[i].Invalidate();
                        UserControls[i].Left = e.X + UserControls[i].Left - MouseDownLocation.X;
                        UserControls[i].Top = e.Y + UserControls[i].Top - MouseDownLocation.Y;
                    }
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Start start = new Start();
            start.MouseDown += OnMouseDown;
            start.MouseMove += OnMouseMove;
            UserControls.Add(start);

            Decision decision = new Decision();
            decision.Location = new Point(0, 200);
            decision.MouseDown += OnMouseDown;
            decision.MouseMove += OnMouseMove;
            UserControls.Add(decision);
            //Controls.Add(decision);
            End end = new End();
            end.Location = new Point(0, 100);
            end.MouseDown += OnMouseDown;
            end.MouseMove += OnMouseMove;
            UserControls.Add(end);
            for (int i = 0; i < UserControls.Count; i++)
            {
                Controls.Add(UserControls[i]);
            }
        }

    }
}
