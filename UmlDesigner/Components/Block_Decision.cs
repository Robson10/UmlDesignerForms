using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner.Components
{
    class Block_Decision: Block_Template
    {
        public Block_Decision()
        {
            ComponentPresets();
            ShapeDecision();
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Dictionary.ShapeColor_Decision, ShapeDecision());
        }

        private Rectangle ShapeDecision()
        {
            GraphicsPath GrPath = new GraphicsPath();
            Point[] points = new Point[]
            {
                new Point(Dictionary.RubberSize.Width, Height / 2),
                new Point(Width / 2, Dictionary.RubberSize.Height),
                new Point(Width-Dictionary.RubberSize.Width, Height / 2),
                new Point(Width / 2, Height-Dictionary.RubberSize.Height)
            };
            GrPath.AddLines(points);
            if (IsSelected) CutTheRubbers(ref GrPath);
            Region = new System.Drawing.Region(GrPath);
            return new Rectangle(Dictionary.RubberSize.Width, Dictionary.RubberSize.Height, ClientRectangle.Width - Dictionary.RubberSize.Width * 2, ClientRectangle.Height - Dictionary.RubberSize.Height * 2);

        }

        protected override void OnResize(EventArgs e)
        {
            UpdateRubbersLocation();
        }
    }
}
