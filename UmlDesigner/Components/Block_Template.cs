using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

//Resize control :https://www.experts-exchange.com/articles/4274/A-simple-trick-to-resize-a-control-at-runtime.html
namespace UmlDesigner.Components
{

    public class Block_Template:UserControl 
    {
        #region properties and variables
        protected Label [] Rubbers = new Label[8] { new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label() };
        protected bool _IsProtected = false;
        public bool IsProtected
        {
            get { return _IsProtected; }
            set
            {
                _IsProtected = value;
                Invalidate();
            }
        }
        protected bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                UpdateRubberVisible();
                Invalidate();
            }
        }
        #endregion

        #region othersMethods
        /// <summary>
        /// Metoda wywoływana w konstruktorze służąca do 
        /// wprowadzenia ustawień początkowych Kontrolki
        /// </summary>
        protected void ComponentPresets()
        {
            DoubleBuffered = true;
            Size = new Size(100, 50);
            RubbersPresets();
        }

        #endregion

        #region RubbersEvents and methods
        /// <summary>
        /// Metoda ustawiające podstawowe parametry gumek, 
        /// dodaje zdarzenia Myszy oraz ustawia m.in. zmiane wyglądu cursora po najechaniu na gumkę
        /// </summary>
        private void RubbersPresets()
        {
            for (int i = 0; i < Rubbers.Length; i++)
            {
                Rubbers[i].BackColor = Color.Silver;
                Controls.Add(Rubbers[i]);
                Rubbers[i].Visible = false;
                Rubbers[i].Size = Dictionary.RubberSize;
                Rubbers[i].TabIndex = i;
                Rubbers[i].MouseDown += Rubbers_MouseDown;
                Rubbers[i].MouseMove += Rubbers_MouseMove;
            }
            Rubbers[0].Cursor = Cursors.SizeNWSE;
            Rubbers[1].Cursor = Cursors.SizeNS;
            Rubbers[2].Cursor = Cursors.SizeNESW;
            Rubbers[3].Cursor = Cursors.SizeWE;
            Rubbers[4].Cursor = Cursors.SizeNWSE;
            Rubbers[5].Cursor = Cursors.SizeNS;
            Rubbers[6].Cursor = Cursors.SizeNESW;
            Rubbers[7].Cursor = Cursors.SizeWE;
            UpdateRubbersLocation();
        }

        private Point MouseDownLocation_Rubbers;
        /// <summary>
        /// Metoda służąca do zapisania miejsca wcisniecia LPM na 1z8 gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubbers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownLocation_Rubbers = e.Location;
        }
        /// <summary>
        /// Metoda służąca do zmiany rozmiaru kontrolki w wyniku ciągnięcia jednej z gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubbers_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((sender as Label).TabIndex == 0) { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
                else if ((sender as Label).TabIndex == 1) { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
                else if ((sender as Label).TabIndex == 2) { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; Width += e.X; }
                else if ((sender as Label).TabIndex == 3) { Width += e.X; }
                else if ((sender as Label).TabIndex == 4) { Width += e.X; Height += e.Y; }
                else if ((sender as Label).TabIndex == 5) { Height += e.Y; }
                else if ((sender as Label).TabIndex == 6) { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Height += e.Y; }
                else if ((sender as Label).TabIndex == 7) { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; }
                Invalidate();
            }
        }
        /// <summary>
        /// Metoda aktualizująca położenie gumek wywoływana przez event OnResize();
        /// </summary>
        protected void UpdateRubbersLocation()
        {
            Point topLeft = new Point(0, 0);
            Point topCenter = new Point(Width / 2 - Dictionary.RubberSize.Width / 2, 0);
            Point topRight = new Point(Width - Dictionary.RubberSize.Width, 0);
            Point centerLeft = new Point(0, Height / 2 - Dictionary.RubberSize.Height / 2);
            Point centerRight = new Point(Width - Dictionary.RubberSize.Width, Height / 2 - Dictionary.RubberSize.Height / 2);
            Point bottomLeft = new Point(0, Height - Dictionary.RubberSize.Height);
            Point bottomCenter = new Point(Width / 2 - Dictionary.RubberSize.Width / 2, Height - Dictionary.RubberSize.Height);
            Point bottomRight = new Point(Width - Dictionary.RubberSize.Width, Height - Dictionary.RubberSize.Height);
            Rubbers[0].Location = topLeft;
            Rubbers[1].Location = topCenter;
            Rubbers[2].Location = topRight;
            Rubbers[3].Location = centerRight;
            Rubbers[4].Location = bottomRight;
            Rubbers[5].Location = bottomCenter;
            Rubbers[6].Location = bottomLeft;
            Rubbers[7].Location = centerLeft;
        }        
        /// <summary>
        /// Metoda aktualizująca widoczność gumek wywoływana w geterze IsSelected
        /// </summary>
        protected void UpdateRubberVisible()
        {
            for (int i = 0; i < Rubbers.Length; i++)
                Rubbers[i].Visible = IsSelected;
        }
        #endregion

        /// <summary>
        /// metoda pozostawiająca nie wycięte miejsca w których są gumki (wszystkie)
        /// </summary>
        /// <param name="GrPath"></param>
        protected void CutTheRubbers(ref GraphicsPath GrPath)
        {
            for (int i = 0; i < Rubbers.Length; i += 1)
                GrPath.AddRectangle(new Rectangle(Rubbers[i].Location, Rubbers[i].Size));

        }

    }
}
