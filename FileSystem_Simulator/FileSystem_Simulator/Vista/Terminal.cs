using FileSystem_Simulator.Controllador;
using FileSystem_Simulator.Modelo;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FileSystem_Simulator
{
    public partial class Terminal : Form
    {
        #region Attributes
        private User user;
        private CommandController command;
        private string userPrompt;

        private Color currentSelectionColor = Color.White;
        #endregion

        #region Constructor
        public Terminal(UserController userController)
        {
            InitializeComponent();

            user = userController.Users.First();
            command = new CommandController(user, userController, this);

            userPrompt = $"{user.Name}@linux:-$ ";

            appendTextWithColor(userPrompt);
            //rtbTerminal.AppendText(userPrompt);
        }
        #endregion

        #region Methods
        private void rtbTerminal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rtbTerminal.GetLineFromCharIndex(rtbTerminal.SelectionStart) < rtbTerminal.Text.LastIndexOf(Environment.NewLine))
            {
                e.Handled = true;
            }
        }

        private void rtbTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            int promptLength = userPrompt.Length;

            if (e.KeyCode == Keys.Enter)
            {
                execute(promptLength);

                promptLength = rtbTerminal.Text.Length;

                appendTextWithColor(Environment.NewLine + userPrompt);
                //rtbTerminal.AppendText(Environment.NewLine + userPrompt);
                e.SuppressKeyPress = true;

                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                int currentPosition = rtbTerminal.SelectionStart;

                int currentLineIndex = rtbTerminal.GetLineFromCharIndex(currentPosition);
                int currentLineStart = rtbTerminal.GetFirstCharIndexFromLine(currentLineIndex);

                if (currentPosition > currentLineStart && currentPosition <= (currentLineStart + userPrompt.Length))
                {
                    e.SuppressKeyPress = true;
                    return;
                }
            }
            if (rtbTerminal.SelectionStart < rtbTerminal.Text.Length)
            {
                rtbTerminal.SelectionStart = rtbTerminal.Text.Length;
                rtbTerminal.SelectionLength = 0;
            }
        }

        private void rtbTerminal_SelectionChanged(object sender, EventArgs e)
        {
            rtbTerminal.SelectionStart = rtbTerminal.Text.Length;
            rtbTerminal.SelectionLength = 0;
        }

        private void execute(int promptLength)
        {
            string commandText = rtbTerminal.Lines.Last().Substring(promptLength);

            string result = command.executeCommand(commandText);

            appendTextWithColor("\n" + result);
            //rtbTerminal.AppendText("\n" + result);

        }

        private void appendTextWithColor(string text)
        {
            rtbTerminal.SelectionColor = currentSelectionColor;
            rtbTerminal.AppendText(text);
        }
        #endregion

        #region GettersSetters
        public string UserPrompt { get => userPrompt; set => userPrompt = value; }

        public RichTextBox GetRichTextBox()
        {
            return rtbTerminal;
        }

        public Color CurrentSelectionColor { get => currentSelectionColor; set => currentSelectionColor = value; }
        #endregion
    }
}
