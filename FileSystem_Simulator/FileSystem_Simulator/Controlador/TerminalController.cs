using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystem_Simulator.Controlador
{
    public class TerminalController
    {
        private Terminal terminal;
        private RichTextBox richTextBox;
        public TerminalController(Terminal terminal) 
        {
            this.terminal = terminal;

            richTextBox = terminal.GetRichTextBox();
        }

        public void clearRtc() 
        {
            richTextBox.Clear();
        }

        #region GetterSetters
        public Terminal Terminal { get => terminal; set => terminal = value; } 
        #endregion
    }
}
