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
            FillUserControls();
            KeyPreview = true;
            this.MouseClick += UserControls_MouseClick;
            KeyDown += Form1_KeyDown;
        }

        private void FillUserControls()
        {
            Start s = new Start();
            s.Location = new Point(0, 0);
            s.MouseClick += UserControls_MouseClick;
            UserControls.Add(s);
            End e = new End();
            e.Location = new Point(0, 100);
            e.MouseClick += UserControls_MouseClick;
            UserControls.Add(e);
            for (int i = 0; i < UserControls.Count; i++)
            {
                Controls.Add(UserControls[i]);
            }
        }

        Keys IsMultiSelect = Keys.Q;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            IsMultiSelect = e.KeyCode;
        }

        private void UserControls_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (IsMultiSelect != Keys.ControlKey )
            {
               
                for (int i = 0; i < UserControls.Count; i++)
                    UserControls[i].IsSelected = false;
            }
            if (sender is MainUserControl)
            (sender as MainUserControl).IsSelected = true;
        }


    }
}
