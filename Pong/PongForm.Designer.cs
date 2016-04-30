namespace Pong
{
    partial class PongForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.speedPicker = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.gameRenderer = new Pong.GameRenderer();
            ((System.ComponentModel.ISupportInitialize)(this.speedPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 30;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(29, 27);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // speedPicker
            // 
            this.speedPicker.Location = new System.Drawing.Point(351, 30);
            this.speedPicker.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.speedPicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speedPicker.Name = "speedPicker";
            this.speedPicker.Size = new System.Drawing.Size(43, 20);
            this.speedPicker.TabIndex = 2;
            this.speedPicker.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.speedPicker.ValueChanged += new System.EventHandler(this.speedPicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Speed: ";
            // 
            // gameRenderer
            // 
            this.gameRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameRenderer.Cursor = System.Windows.Forms.Cursors.Default;
            this.gameRenderer.Game = null;
            this.gameRenderer.Location = new System.Drawing.Point(12, 56);
            this.gameRenderer.Name = "gameRenderer";
            this.gameRenderer.Size = new System.Drawing.Size(878, 418);
            this.gameRenderer.TabIndex = 0;
            // 
            // PongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 486);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.speedPicker);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.gameRenderer);
            this.KeyPreview = true;
            this.Name = "PongForm";
            this.Text = "Pong";
            this.Load += new System.EventHandler(this.PongForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PongForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.speedPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GameRenderer gameRenderer;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NumericUpDown speedPicker;
        private System.Windows.Forms.Label label1;
    }
}

