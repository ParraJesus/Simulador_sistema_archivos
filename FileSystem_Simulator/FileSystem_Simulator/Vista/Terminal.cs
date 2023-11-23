using FileSystem_Simulator.Controllador;
using FileSystem_Simulator.Modelo;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace FileSystem_Simulator
{
    public partial class Terminal : Form
    {

        private UserController userController;
        private User user;

        CommandController command;

        private string userPrompt;
        

        public Terminal(UserController userController)
        {
            InitializeComponent();

            user = userController.Users.First();
            command = new CommandController(user, userController, this);

            userPrompt = $"{user.Name}@linux:-$ ";
            rtbTerminal.AppendText(userPrompt);

            this.userController = userController;
        }

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

                rtbTerminal.AppendText(Environment.NewLine + userPrompt);
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

        private void rtbTerminal_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void rtbTerminal_SelectionChanged(object sender, EventArgs e)
        {
            rtbTerminal.SelectionStart = rtbTerminal.Text.Length;
            rtbTerminal.SelectionLength = 0;
        }

        private void execute(int promptLength) 
        {
            string commandText = rtbTerminal.Lines.Last().Substring(promptLength);
            Debug.WriteLine("terminal: " + rtbTerminal.Lines.Last().Substring(promptLength));
            
            string result = command.executeCommand(commandText);

            rtbTerminal.AppendText("\n" + result);

        }



        #region GettersSetters
        public string UserPrompt { get => userPrompt; set => userPrompt = value; }

        public RichTextBox GetRichTextBox()
        {
            return rtbTerminal;
        }
        #endregion
    }
}
