using System.Windows.Forms;

namespace FileSystem_Simulator.Controlador
{
    public class TerminalController
    {
        #region Attributes
        private Terminal terminal;
        private RichTextBox richTextBox; 
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
        #endregion

        #region GetterSetters
        public Terminal Terminal { get => terminal; set => terminal = value; } 
        #endregion
    }
}
