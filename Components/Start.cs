using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
    public partial class Start : UserControl
    {
        Label text = new Label();
        public Start()
        {
            InitializeComponent();
            text.Text = Dictionary.UmlBlock_TextForStart;
        }

        protected override void OnResize(EventArgs e)
        {

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Pen borderPen = new Pen(new SolidBrush(Color.Red));
            e.Graphics.DrawEllipse(borderPen, 0, 0, Width - 1, Height - 1);
        }
    }
}