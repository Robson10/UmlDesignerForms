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
        List<Block_Template> UserControls = new List<Block_Template>();

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
        private Point MouseDownLocation;
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (IsMultiSelect != Keys.ControlKey)
                DeselectControls();

            if (e.Button == MouseButtons.Left && sender is Block_Template)
            {
                (sender as Block_Template).IsSelected = true;
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
                    UserControls[i].Refresh();//bez tego kontrolki zostawiałyby na sobie ślady
                    if (UserControls[i].IsSelected && !UserControls[i].IsProtected)
                    {
                        UserControls[i].Invalidate();//dzieki temu podczas ruchu nie ma przycinek
                        UserControls[i].Left = e.X + UserControls[i].Left - MouseDownLocation.X;
                        UserControls[i].Top = e.Y + UserControls[i].Top - MouseDownLocation.Y;
                    }
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Block_Start start = new Block_Start();
            start.MouseDown += OnMouseDown;
            start.MouseMove += OnMouseMove;
            UserControls.Add(start);

            Block_End end = new Block_End();
            end.Location = new Point(0, 100);
            end.MouseDown += OnMouseDown;
            end.MouseMove += OnMouseMove;
            UserControls.Add(end);

            Block_Decision decision = new Block_Decision();
            decision.Location = new Point(0, 200);
            decision.MouseDown += OnMouseDown;
            decision.MouseMove += OnMouseMove;
            UserControls.Add(decision);
            //Controls.Add(decision);

            Block_Input Input = new Block_Input();
            Input.Location = new Point(0, 300);
            Input.MouseDown += OnMouseDown;
            Input.MouseMove += OnMouseMove;
            UserControls.Add(Input);

            Block_Execution Execution = new Block_Execution();
            Execution.Location = new Point(0, 400);
            Execution.MouseDown += OnMouseDown;
            Execution.MouseMove += OnMouseMove;
            UserControls.Add(Execution);
            for (int i = 0; i < UserControls.Count; i++)
            {
                Controls.Add(UserControls[i]);
            }
        }

    }
}
