using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

//Resize control :https://www.experts-exchange.com/articles/4274/A-simple-trick-to-resize-a-control-at-runtime.html
namespace UmlDesigner.Components
{

    public class MainUserControl:PictureBox 
    {
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

        /// <summary>
        /// Metoda wywoływana w konstruktorze służąca do wprowadzenia ustawień początkowych Kontrolki
        /// </summary>
        protected void ComponentPresets()
        {
            DoubleBuffered = true;
            Size = new Size(100, 50);
        }
        /// <summary>
        /// Metoda ustawiające podstawowe parametry gumek, dodaje eventy MouseDown i MouseMove oraz ustawia zmiane wyglądu cursora po najechaniu na gumkę
        /// </summary>
        protected void RubbersPresets()
        {
            for (int i = 0; i < Rubbers.Length; i++)
            {
                Rubbers[i].BackColor = Color.Silver;
                Controls.Add(Rubbers[i]);
                Rubbers[i].Visible = false;
                Rubbers[i].Size = StartEndDictionary.RubberSize;
                Rubbers[i].Name = i.ToString();
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
        /// Metoda służąca do zapisania miejsca wcisniecia LPM na 1z8 picturebox'ow nazywanych gumkami
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Rubbers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation_Rubbers = e.Location;
            }
        }
        /// <summary>
        /// Metoda służąca do zmiany rozmiaru kontrolki w wyniku przesuwania "przeciągania" jednej z gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Rubbers_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((sender as Label).Name == "0") { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
                else if ((sender as Label).Name == "1") { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
                else if ((sender as Label).Name == "2") { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; Width += e.X; }
                else if ((sender as Label).Name == "3") { Width += e.X; }
                else if ((sender as Label).Name == "4") { Width += e.X; Height += e.Y; }
                else if ((sender as Label).Name == "5") { Height += e.Y; }
                else if ((sender as Label).Name == "6") { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Height += e.Y; }
                else if ((sender as Label).Name == "7") { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; }
                Invalidate();
            }
        }

        /// <summary>
        /// Event reagujący na zmianę rozmiaru kontrolki, wywołuje on metode aktualizującą położenie gumek
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnResize(EventArgs e)
        //{
        //    CutTheShape();
        //    UpdateRubbersLocation();

        //}

        /// <summary>
        /// Metoda wywoływana przez event OnPaint w celu wyliczenia odpowiedniego rozmiaru czcionki dla tekstu aby ten miescił sie w Kontrolce
        /// </summary>
        /// <param name="font"></param>
        /// <param name="stringSize"></param>
        /// <param name="e"></param>
        protected void FindSuitableFontAndFontSizeForText(out Font font, out Size stringSize, PaintEventArgs e,string text)
        {
            int fontSize = (int)Math.Ceiling(Height / 3.0) + 1;
            do
            {
                fontSize--;
                font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                stringSize = e.Graphics.MeasureString(text, font).ToSize();
            } while (stringSize.Width > Width && fontSize > 1);
        }
        /// <summary>
        /// Metoda aktualizująca widoczność gumek
        /// </summary>
        protected void UpdateRubberVisible()
        {
            for (int i = 0; i < Rubbers.Length; i++)
                Rubbers[i].Visible = IsSelected;
        }
        /// <summary>
        /// Metoda służąca do odcięcia obszaru na zewnątrz docelowego kształtu komponentu- Przezroczystość
        /// </summary>
        protected void CutTheShape()
        {
            GraphicsPath GrPath = new GraphicsPath();
            GrPath.AddEllipse(1, 1, ClientSize.Width - 2, ClientSize.Height - 3);
            CutTheCornerRubbers(ref GrPath);
            Region = new System.Drawing.Region(GrPath);
        }
        protected void CutDecisionShape()
        {
            GraphicsPath GrPath = new GraphicsPath();
            GrPath.AddLines(new Point[]{new Point(StartEndDictionary.RubberSize.Width, Height / 2),new Point(Width / 2, 0),new Point(Width, Height / 2),new Point(Width / 2, Height)});

            CutTheCornerRubbers(ref GrPath);
            Region = new System.Drawing.Region(GrPath);

        }
        protected void CutTheCornerRubbers(ref GraphicsPath GrPath)
        {
            for (int i = 0; i < Rubbers.Length; i += 2)
            {
                GrPath.AddRectangle(new Rectangle(Rubbers[i].Location, Rubbers[i].Size));
            }
        }
        protected void CutTheAllRubbers(ref GraphicsPath GrPath)
        {
            for (int i = 0; i < Rubbers.Length; i +=1)
            {
                GrPath.AddRectangle(new Rectangle(Rubbers[i].Location, Rubbers[i].Size));
            }
        }
        /// <summary>
        /// Metoda aktualizująca położenie gumek wywoływana przez event OnResize();
        /// </summary>
        protected void UpdateRubbersLocation()
        {
            Point topLeft = new Point(0, 0);
            Point topCenter = new Point(Width / 2 - StartEndDictionary.RubberSize.Width / 2, 0);
            Point topRight = new Point(Width - StartEndDictionary.RubberSize.Width, 0);
            Point centerLeft = new Point(0, Height / 2 - StartEndDictionary.RubberSize.Height / 2);
            Point centerRight = new Point(Width - StartEndDictionary.RubberSize.Width, Height / 2 - StartEndDictionary.RubberSize.Height / 2);
            Point bottomLeft = new Point(0, Height - StartEndDictionary.RubberSize.Height);
            Point bottomCenter = new Point(Width / 2 - StartEndDictionary.RubberSize.Width / 2, Height - StartEndDictionary.RubberSize.Height);
            Point bottomRight = new Point(Width - StartEndDictionary.RubberSize.Width, Height - StartEndDictionary.RubberSize.Height);

            Rubbers[0].Location = topLeft;
            Rubbers[1].Location = topCenter;
            Rubbers[2].Location = topRight;
            Rubbers[3].Location = centerRight;
            Rubbers[4].Location = bottomRight;
            Rubbers[5].Location = bottomCenter;
            Rubbers[6].Location = bottomLeft;
            Rubbers[7].Location = centerLeft;
            Invalidate();
        }

    }
}

//private Point MouseDownLocation;
///// <summary>
///// Metoda zapisująca miejsce wciśnięcia lewego glawisza myszy na kontrolce z wyłączeniem gumek posiadającymi włąsny event
///// </summary>
///// <param name="e"></param>
//protected override void OnMouseDown(MouseEventArgs e)
//{
//    if (e.Button == MouseButtons.Left)
//    {
//        MouseDownLocation = e.Location;
//    }
//}
///// <summary>
///// Metoda służąca do zmiany położenia Kontrolki 
///// </summary>
///// <param name="e"></param>
//protected override void OnMouseMove(MouseEventArgs e)
//{
//    if (e.Button == MouseButtons.Left)
//    {
//        Left = e.X + Left - MouseDownLocation.X;
//        Top = e.Y + Top - MouseDownLocation.Y;
//    }
//}