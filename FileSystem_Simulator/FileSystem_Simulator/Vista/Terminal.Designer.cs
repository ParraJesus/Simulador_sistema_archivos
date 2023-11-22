namespace FileSystem_Simulator
{
    partial class Terminal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbTerminal = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.panel1.Controls.Add(this.rtbTerminal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1233, 578);
            this.panel1.TabIndex = 0;
            // 
            // rtbTerminal
            // 
            this.rtbTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.rtbTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTerminal.Font = new System.Drawing.Font("Courier New", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTerminal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.rtbTerminal.Location = new System.Drawing.Point(0, 0);
            this.rtbTerminal.Name = "rtbTerminal";
            this.rtbTerminal.Size = new System.Drawing.Size(1233, 578);
            this.rtbTerminal.TabIndex = 0;
            this.rtbTerminal.Text = "";
            this.rtbTerminal.SelectionChanged += new System.EventHandler(this.rtbTerminal_SelectionChanged);
            this.rtbTerminal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbTerminal_KeyDown);
            this.rtbTerminal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtbTerminal_KeyPress);
            this.rtbTerminal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbTerminal_KeyUp);
            // 
            // Terminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 578);
            this.Controls.Add(this.panel1);
            this.Name = "Terminal";
            this.Text = "Terminal";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtbTerminal;
    }
}

