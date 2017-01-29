using System;
using System.Collections.Generic;
using System.Drawing;

namespace UmlDesigner.Components
{
    class Dictionary
    {
        public static string TextStart = "START";
        public static string TextDecision = "DecisionBlock";
        public static string TextExecution = "Execution";
        public static string TextInput = "Input";
        public static string TextEnd= "END";
        public static SolidBrush ShapeColor_Start = new SolidBrush(Color.FromArgb(200, 50, 50));
        public static SolidBrush ShapeColor_End = new SolidBrush(Color.FromArgb(200, 50, 50));
        public static SolidBrush ShapeColor_Decision = new SolidBrush(Color.Orange);
        public static SolidBrush ShapeColor_Execution = new SolidBrush(Color.Blue);
        public static SolidBrush ShapeColor_Input = new SolidBrush(Color.Green);
        public static Color RubberColor = Color.Silver;
        public static Size RubberSize = new Size(10, 10);

    }
}
