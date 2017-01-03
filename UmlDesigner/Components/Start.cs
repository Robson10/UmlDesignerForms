using System.Drawing;
using System.Windows.Forms;
namespace UmlDesigner.Components
{
    public partial class Start : MainUserControl
    { 
        public Start()
        {
            InitializeComponent();
            ComponentPresets();
            RubbersPresets();
            Invalidate();
        }

        /// <summary>
        /// Metoda rysująca kształty na kontrolce w tym wypadku elipsę oraz gumki do zmiany rozmiaru kontrolki jesli jest zaznaczona;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(StartEndDictionary.BackgroundColor), 0, 0, Width - 1, Height - 1);
            Size stringSize;
            Font font;
            FindSuitableFontAndFontSizeForText(out font, out stringSize,e,StartEndDictionary.TextStart);
            e.Graphics.DrawString(StartEndDictionary.TextStart, font, new SolidBrush(Color.Black), (Width - stringSize.Width) / 2, Height / 2 - stringSize.Height / 2);
            e.Graphics.FillEllipse(new SolidBrush(Color.YellowGreen), Width / 2 - 4, Height - 8, 8, 8);
        }
    }
}

#region Backup
/*
//private PictureBox[] Rubbers = new PictureBox[8] { new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox() };

//private bool _isSelected = false;
//public bool IsSelected
//{
//    get { return _isSelected; }
//    set
//    {
//        _isSelected = value;
//        Invalidate();
//    }
//}


public Start()
{
    InitializeComponent();
    ComponentPresets();
    RubbersPresets();
    Invalidate();
}
///// <summary>
///// Metoda wywoływana w konstruktorze służąca do wprowadzenia ustawień początkowych Kontrolki
///// </summary>
//private void ComponentPresets()
//{
//    DoubleBuffered = true;
//    Size = new Size(100, 50);
//}
///// <summary>
///// Metoda ustawiające podstawowe parametry gumek, dodaje eventy MouseDown i MouseMove oraz ustawia zmiane wyglądu cursora po najechaniu na gumkę
///// </summary>
//private void RubbersPresets()
//{
//    for (int i = 0; i < Rubbers.Length; i++)
//    {
//        Rubbers[i].BackColor = Color.Silver;
//        Controls.Add(Rubbers[i]);
//        Rubbers[i].Visible = false;
//        Rubbers[i].Size = StartEndDictionary.RubberSize;
//        Rubbers[i].Name = i.ToString();
//        Rubbers[i].MouseDown += Rubbers_MouseDown;
//        Rubbers[i].MouseMove += Rubbers_MouseMove;
//    }

//    Rubbers[0].Cursor = Cursors.SizeNWSE;
//    Rubbers[1].Cursor = Cursors.SizeNS;
//    Rubbers[2].Cursor = Cursors.SizeNESW;
//    Rubbers[3].Cursor = Cursors.SizeWE;
//    Rubbers[4].Cursor = Cursors.SizeNWSE;
//    Rubbers[5].Cursor = Cursors.SizeNS;
//    Rubbers[6].Cursor = Cursors.SizeNESW;
//    Rubbers[7].Cursor = Cursors.SizeWE;
//}
//private Point MouseDownLocation_Rubbers;
///// <summary>
///// Metoda służąca do zapisania miejsca wcisniecia LPM na 1z8 picturebox'ow nazywanych gumkami
///// </summary>
///// <param name="sender"></param>
///// <param name="e"></param>
//private void Rubbers_MouseDown(object sender, MouseEventArgs e)
//{
//    if (e.Button == MouseButtons.Left)
//    {
//        MouseDownLocation_Rubbers = e.Location;
//    }
//}
///// <summary>
///// Metoda służąca do zmiany rozmiaru kontrolki w wyniku przesuwania "przeciągania" jednej z gumek
///// </summary>
///// <param name="sender"></param>
///// <param name="e"></param>
//private void Rubbers_MouseMove(object sender, MouseEventArgs e)
//{
//    if (e.Button == MouseButtons.Left)
//    {
//        if ((sender as PictureBox).Name == "0") { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
//        else if ((sender as PictureBox).Name == "1") { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
//        else if ((sender as PictureBox).Name == "2") { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; Width += e.X; }
//        else if ((sender as PictureBox).Name == "3") { Width += e.X; }
//        else if ((sender as PictureBox).Name == "4") { Width += e.X; Height += e.Y; }
//        else if ((sender as PictureBox).Name == "5") { Height += e.Y; }
//        else if ((sender as PictureBox).Name == "6") { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Height += e.Y; }
//        else if ((sender as PictureBox).Name == "7") { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; }
//        Invalidate();
//    }
//}
/// <summary>
/// Metoda rysująca kształty na kontrolce w tym wypadku elipsę oraz gumki do zmiany rozmiaru kontrolki jesli jest zaznaczona;
/// </summary>
/// <param name="e"></param>
protected override void OnPaint(PaintEventArgs e)
{

    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    e.Graphics.FillEllipse(new SolidBrush(StartEndDictionary.BackgroundColor), 0, 0, Width - 1, Height - 1);
    Size stringSize;
    Font font;
    int fontSize = (int)Math.Ceiling(Height / 3.0) + 1;
    do
    {
        fontSize--;
        font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
        stringSize = e.Graphics.MeasureString(StartEndDictionary.TextStart, font).ToSize();
    } while (stringSize.Width > Width && fontSize > 1);

    e.Graphics.DrawString(StartEndDictionary.TextStart, font, new SolidBrush(Color.Black), (Width - stringSize.Width) / 2, Height / 2 - stringSize.Height / 2);

    e.Graphics.FillEllipse(new SolidBrush(Color.YellowGreen), Width / 2 - 4, Height - 8, 8, 8);
    for (int i = 0; i < Rubbers.Length; i += 2)
        Rubbers[i].Visible = (IsSelected) ? true : false;

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
///// <summary>
///// Event reagujący na zmianę rozmiaru kontrolki, wywołuje on metode aktualizującą położenie gumek
///// </summary>
///// <param name="e"></param>
//protected override void OnResize(EventArgs e)
//{
//    UpdateRubbersLocation();
//}
///// <summary>
///// Metoda aktualizująca położenie gumek wywoływana przez event OnResize();
///// </summary>
//private void UpdateRubbersLocation()
//{
//    Point topLeft = new Point(0, 0);
//    Point topCenter = new Point(Width / 2 - StartEndDictionary.RubberSize.Width / 2, 0);
//    Point topRight = new Point(Width - StartEndDictionary.RubberSize.Width, 0);
//    Point centerLeft = new Point(0, Height / 2 - StartEndDictionary.RubberSize.Height / 2);
//    Point centerRight = new Point(Width - StartEndDictionary.RubberSize.Width, Height / 2 - StartEndDictionary.RubberSize.Height / 2);
//    Point bottomLeft = new Point(0, Height - StartEndDictionary.RubberSize.Height);
//    Point bottomCenter = new Point(Width / 2 - StartEndDictionary.RubberSize.Width / 2, Height - StartEndDictionary.RubberSize.Height);
//    Point bottomRight = new Point(Width - StartEndDictionary.RubberSize.Width, Height - StartEndDictionary.RubberSize.Height);

//    Rubbers[0].Location = topLeft;
//    Rubbers[1].Location = topCenter;
//    Rubbers[2].Location = topRight;
//    Rubbers[3].Location = centerRight;
//    Rubbers[4].Location = bottomRight;
//    Rubbers[5].Location = bottomCenter;
//    Rubbers[6].Location = bottomLeft;
//    Rubbers[7].Location = centerLeft;
//    Invalidate();
//}
///// <summary>
///// Zdarzenie włączające zaznaczenie kontrolki oraz wywołujące jej ponowne rysowanie;
///// </summary>
///// <param name="e"></param>
//protected override void OnMouseClick(MouseEventArgs e)
//{
//    IsSelected = !IsSelected;
//    Invalidate();
//}
*/
#endregion