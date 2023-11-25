using System.Drawing;
using System.Windows.Forms;

namespace FileSystem_Simulator.Controlador
{
    public class TerminalController
    {
        #region Attributes
        private Terminal terminal;
        private RichTextBox richTextBox;

        Color textColor = Color.White;
        Color bgColor;
        #endregion

        #region Constructor
        public TerminalController(Terminal terminal)
        {
            this.terminal = terminal;

            richTextBox = terminal.GetRichTextBox();
        }
        #endregion

        #region Functions
        public void clearRtc()
        {
            richTextBox.Clear();
        }

        private Color getColorFromCode(char colorCode)
        {
            switch (colorCode)
            {
                case '0': return Color.Black;
                case '1': return Color.Blue;
                case '2': return Color.Green;
                case '3': return Color.Cyan;
                case '4': return Color.Red;
                case '5': return Color.Purple;
                case '6': return Color.Yellow;
                case '7': return Color.Peru;
                case '8': return Color.Gray;
                case '9': return Color.LightBlue;
                case 'a': return Color.LightGreen;
                case 'b': return Color.LightCyan;
                case 'c': return Color.LightCoral;
                case 'd': return Color.LightPink;
                case 'e': return Color.LightYellow;
                case 'f': return Color.White;
                default: return Color.Empty;
            }
        }

        public void changeColor(char textColorCode, char bgColorCode)
        {
            textColor = getColorFromCode(textColorCode);
            bgColor = getColorFromCode(bgColorCode);

            richTextBox.SelectionColor = textColor;

            if (textColor != Color.Empty) richTextBox.SelectionColor = textColor;
            if (bgColor != Color.Empty) richTextBox.BackColor = bgColor;
        }
        #endregion

        #region GetterSetters
        public Terminal Terminal { get => terminal; set => terminal = value; } 

        public Color TextColor { get => textColor; set => textColor = value; }
        public Color BgColor { get => bgColor; set => bgColor = value; }
        #endregion
    }
}
