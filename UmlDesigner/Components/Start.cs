using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UmlDesigner.Components;
//Resize control :https://www.experts-exchange.com/articles/4274/A-simple-trick-to-resize-a-control-at-runtime.html
namespace UmlDesigner.Components
{
    public partial class Start : UserControl
    {
        RectangleF[] Rubbers = new RectangleF[8] ;

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set {
                _isSelected = value;
                Invalidate();
            }
        }
        public Color BackgroundColor = Color.FromArgb(200, 50, 50);
        public Color RubberColor = Color.Silver;
        public Size RubberSize = new Size(10,10);

        public Start()
        {
            InitializeComponent();
            ComponentPresets();
            Invalidate();
        }
        /// <summary>
        /// Metoda wywoływana w konstruktorze służąca do wprowadzenia ustawień początkowych Kontrolki
        /// </summary>
        private void ComponentPresets()
        {
            DoubleBuffered = true;
            Size = new Size(100, 50);
        }
        /// <summary>
        /// Metoda rysująca kształty na kontrolce w tym wypadku elipsę oraz gumki do zmiany rozmiaru kontrolki jesli jest zaznaczona;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            BackColor = Color.Transparent;
            e.Graphics.FillEllipse(new SolidBrush(BackgroundColor), 0, 0, Width - 1, Height - 1);
            
            Font font = new Font(FontFamily.GenericSansSerif,(int) Math.Ceiling( Height / 3.0), FontStyle.Bold);

            float stringWidth = e.Graphics.MeasureString(StartEndDictionary.TextStart, font).Width;
            e.Graphics.DrawString(StartEndDictionary.TextStart, font, new SolidBrush(Color.Black), (Width - stringWidth) / 2, Height / 4);
            e.Graphics.FillEllipse(new SolidBrush(Color.YellowGreen), Width / 2 - 4, Height - 8, 8, 8);

            if (IsSelected)
            {
                for (int i = 0; i < Rubbers.Length; i++)
                    e.Graphics.FillRectangle(new SolidBrush(RubberColor), Rubbers[i]);
            }
        }

        private Point MouseDownLocation;
        private bool IsMoveing = true;
        private int SelectedRubber;
        /// <summary>
        /// Metoda zapisująca miejsce wciśnięcia lewego glawisza myszy oraz sprawdzająca czy nie wciśnięto którejś gumki. Jeżeli wciśnięto gumkę zmienia się flaga IsMoveing na "false"
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            IsMoveing = true;
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;

                if (IsSelected)
                {
                    for (int i = 0; i < Rubbers.Length; i++)
                    {
                        if (Rubbers[i].Contains(e.Location))
                        {
                            SelectedRubber = i;
                            IsMoveing = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Metoda służąca do zmiany położenia Kontrolki jeżeli flaga IsMoveing==True, w przeciwnym razie oznacza to zmianę rozmiaru kontrolki- ściśle współpracuje z zmienną SelectedRubber 
        /// by zmieniać rozmiar kontrolki wg wybranej Gumki
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMoveing)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Left = e.X + Left - MouseDownLocation.X;
                    Top = e.Y + Top - MouseDownLocation.Y;
                }
            }
            //Resize
            else
            {
                if (SelectedRubber == 0){Left = e.X + Left - MouseDownLocation.X;Width -= e.X - MouseDownLocation.X;Top = e.Y + Top - MouseDownLocation.Y;Height -= e.Y - MouseDownLocation.Y;}
                else if (SelectedRubber == 1){Top = e.Y + Top - MouseDownLocation.Y;Height -= e.Y - MouseDownLocation.Y;}
                else if(SelectedRubber == 2){Top = e.Y + Top - MouseDownLocation.Y;Height -= e.Y - MouseDownLocation.Y;Width = e.X;}
                else if(SelectedRubber == 3) { Width = e.X; }
                else if(SelectedRubber == 4){Width = e.X;Height = e.Y;}
                else if(SelectedRubber == 5){ Height = e.Y; }
                else if(SelectedRubber == 6){Left = e.X + Left - MouseDownLocation.X;Width -= e.X - MouseDownLocation.X;Height = e.Y;}
                else if(SelectedRubber == 7) { Left = e.X + Left - MouseDownLocation.X;Width -=  e.X - MouseDownLocation.X;}
                Invalidate();
            }
        }



        /// <summary>
        /// Event reagujący na zmianę rozmiaru obiektu który służy do zaktualizowania położenia "gumek" w obiekcie
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            PointF topLeft = new PointF(0, 0);
            PointF topCenter = new PointF(Width / 2 - RubberSize.Width / 2, 0);
            PointF topRight = new PointF(Width - RubberSize.Width, 0);
            PointF centerLeft = new PointF(0, Height / 2 - RubberSize.Height / 2);
            PointF centerRight = new PointF(Width - RubberSize.Width, Height / 2 - RubberSize.Height / 2);
            PointF bottomLeft = new PointF(0, Height - RubberSize.Height);
            PointF bottomCenter = new PointF(Width / 2 - RubberSize.Width / 2, Height - RubberSize.Height);
            PointF bottomRight = new PointF(Width - RubberSize.Width, Height - RubberSize.Height);

            Rubbers[0] = (new RectangleF(topLeft, RubberSize));
            Rubbers[1] = (new RectangleF(topCenter, RubberSize));
            Rubbers[2] = (new RectangleF(topRight, RubberSize));
            Rubbers[3] = (new RectangleF(centerRight, RubberSize));
            Rubbers[4] = (new RectangleF(bottomRight, RubberSize));
            Rubbers[5] = (new RectangleF(bottomCenter, RubberSize));
            Rubbers[6] = (new RectangleF(bottomLeft, RubberSize));
            Rubbers[7] = (new RectangleF(centerLeft, RubberSize));
        }
        /// <summary>
        /// Zdarzenie włączające zaznaczenie kontrolki oraz wywołujące jej ponowne rysowanie;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            IsSelected = !IsSelected;
            IsMoveing = true;
            Invalidate();
        }
        /// <summary>
        /// Zdarzenie wyłączające zaznaczenie kontrolki oraz wywołujące jej ponowne rysowanie;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            IsSelected = false;
            IsMoveing = true;
            Invalidate();
        }
        /// <summary>
        /// Metoda mająca na celu utworzenie oraz rozmieszczenie "gumek" na kontrolce które będą służyć do zmiany rozmiaru kontrolki.
        /// metoda jest wywoływana jest w evencie OnResize()
        /// </summary>



        //problems
        /// <summary>
        /// Zdarzenie wyłączające zaznaczenie kontrolki oraz wywołujące jej ponowne rysowanie;
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnValidating(CancelEventArgs e)
        //{
        //    IsSelected = false;
        //    Invalidate();
        //}

    }
}
