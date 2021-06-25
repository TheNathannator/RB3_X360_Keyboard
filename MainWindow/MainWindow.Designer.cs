using System.Windows.Forms;

namespace Program
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label_OctaveSwitcher;
            System.Windows.Forms.Label label_ProgramSelector;
            this.radio_pedal_expression = new System.Windows.Forms.RadioButton();
            this.numUpDown_OctaveSwitcher = new System.Windows.Forms.NumericUpDown();
            this.numUpDown_ProgramSelector = new System.Windows.Forms.NumericUpDown();
            this.radio_pedal_channelVolume = new System.Windows.Forms.RadioButton();
            this.radio_pedal_footController = new System.Windows.Forms.RadioButton();
            this.drumModeCheckbox = new System.Windows.Forms.CheckBox();
            this.radio_playerIndex1 = new System.Windows.Forms.RadioButton();
            this.radio_playerIndex2 = new System.Windows.Forms.RadioButton();
            this.radio_playerIndex3 = new System.Windows.Forms.RadioButton();
            this.radio_playerIndex4 = new System.Windows.Forms.RadioButton();
            this.button_Start = new System.Windows.Forms.Button();
            label_OctaveSwitcher = new System.Windows.Forms.Label();
            label_ProgramSelector = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_OctaveSwitcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_ProgramSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // label_OctaveSwitcher
            // 
            label_OctaveSwitcher.AutoSize = true;
            label_OctaveSwitcher.Enabled = false;
            label_OctaveSwitcher.Location = new System.Drawing.Point(141, 14);
            label_OctaveSwitcher.Name = "label_OctaveSwitcher";
            label_OctaveSwitcher.Size = new System.Drawing.Size(44, 15);
            label_OctaveSwitcher.TabIndex = 10;
            label_OctaveSwitcher.Text = "Octave";
            // 
            // label_ProgramSelector
            // 
            label_ProgramSelector.AutoSize = true;
            label_ProgramSelector.Location = new System.Drawing.Point(141, 43);
            label_ProgramSelector.Name = "label_ProgramSelector";
            label_ProgramSelector.Size = new System.Drawing.Size(53, 15);
            label_ProgramSelector.TabIndex = 11;
            label_ProgramSelector.Text = "Program";
            // 
            // radio_pedal_expression
            // 
            this.radio_pedal_expression.AutoSize = true;
            this.radio_pedal_expression.Location = new System.Drawing.Point(223, 12);
            this.radio_pedal_expression.Name = "radio_pedal_expression";
            this.radio_pedal_expression.Size = new System.Drawing.Size(81, 19);
            this.radio_pedal_expression.TabIndex = 0;
            this.radio_pedal_expression.TabStop = true;
            this.radio_pedal_expression.Text = "Expression";
            this.radio_pedal_expression.UseVisualStyleBackColor = true;
            // 
            // numUpDown_OctaveSwitcher
            // 
            this.numUpDown_OctaveSwitcher.Location = new System.Drawing.Point(100, 12);
            this.numUpDown_OctaveSwitcher.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numUpDown_OctaveSwitcher.Name = "numUpDown_OctaveSwitcher";
            this.numUpDown_OctaveSwitcher.Size = new System.Drawing.Size(35, 23);
            this.numUpDown_OctaveSwitcher.TabIndex = 1;
            this.numUpDown_OctaveSwitcher.Tag = "";
            this.numUpDown_OctaveSwitcher.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numUpDown_ProgramSelector
            // 
            this.numUpDown_ProgramSelector.Location = new System.Drawing.Point(100, 41);
            this.numUpDown_ProgramSelector.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numUpDown_ProgramSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown_ProgramSelector.Name = "numUpDown_ProgramSelector";
            this.numUpDown_ProgramSelector.Size = new System.Drawing.Size(35, 23);
            this.numUpDown_ProgramSelector.TabIndex = 2;
            this.numUpDown_ProgramSelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDown_ProgramSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radio_pedal_channelVolume
            // 
            this.radio_pedal_channelVolume.AutoSize = true;
            this.radio_pedal_channelVolume.Location = new System.Drawing.Point(223, 36);
            this.radio_pedal_channelVolume.Name = "radio_pedal_channelVolume";
            this.radio_pedal_channelVolume.Size = new System.Drawing.Size(112, 19);
            this.radio_pedal_channelVolume.TabIndex = 3;
            this.radio_pedal_channelVolume.TabStop = true;
            this.radio_pedal_channelVolume.Text = "Channel Volume";
            this.radio_pedal_channelVolume.UseVisualStyleBackColor = true;
            // 
            // radio_pedal_footController
            // 
            this.radio_pedal_footController.AutoSize = true;
            this.radio_pedal_footController.Location = new System.Drawing.Point(223, 61);
            this.radio_pedal_footController.Name = "radio_pedal_footController";
            this.radio_pedal_footController.Size = new System.Drawing.Size(105, 19);
            this.radio_pedal_footController.TabIndex = 4;
            this.radio_pedal_footController.TabStop = true;
            this.radio_pedal_footController.Text = "Foot Controller";
            this.radio_pedal_footController.UseVisualStyleBackColor = true;
            // 
            // drumModeCheckbox
            // 
            this.drumModeCheckbox.AutoSize = true;
            this.drumModeCheckbox.Location = new System.Drawing.Point(369, 37);
            this.drumModeCheckbox.Name = "drumModeCheckbox";
            this.drumModeCheckbox.Size = new System.Drawing.Size(90, 19);
            this.drumModeCheckbox.TabIndex = 5;
            this.drumModeCheckbox.Text = "Drum Mode";
            this.drumModeCheckbox.UseVisualStyleBackColor = true;
            this.drumModeCheckbox.CheckedChanged += new System.EventHandler(this.drumModeCheckbox_CheckedChanged);
            // 
            // radio_playerIndex1
            // 
            this.radio_playerIndex1.AutoSize = true;
            this.radio_playerIndex1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radio_playerIndex1.Location = new System.Drawing.Point(12, 12);
            this.radio_playerIndex1.Name = "radio_playerIndex1";
            this.radio_playerIndex1.Size = new System.Drawing.Size(31, 19);
            this.radio_playerIndex1.TabIndex = 6;
            this.radio_playerIndex1.TabStop = true;
            this.radio_playerIndex1.Text = "1";
            this.radio_playerIndex1.UseVisualStyleBackColor = true;
            this.radio_playerIndex1.CheckedChanged += new System.EventHandler(this.radio_playerIndex1_CheckedChanged);
            // 
            // radio_playerIndex2
            // 
            this.radio_playerIndex2.AutoSize = true;
            this.radio_playerIndex2.Location = new System.Drawing.Point(49, 12);
            this.radio_playerIndex2.Name = "radio_playerIndex2";
            this.radio_playerIndex2.Size = new System.Drawing.Size(31, 19);
            this.radio_playerIndex2.TabIndex = 7;
            this.radio_playerIndex2.TabStop = true;
            this.radio_playerIndex2.Text = "2";
            this.radio_playerIndex2.UseVisualStyleBackColor = true;
            // 
            // radio_playerIndex3
            // 
            this.radio_playerIndex3.AutoSize = true;
            this.radio_playerIndex3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radio_playerIndex3.Location = new System.Drawing.Point(12, 32);
            this.radio_playerIndex3.Name = "radio_playerIndex3";
            this.radio_playerIndex3.Size = new System.Drawing.Size(31, 19);
            this.radio_playerIndex3.TabIndex = 8;
            this.radio_playerIndex3.TabStop = true;
            this.radio_playerIndex3.Text = "3";
            this.radio_playerIndex3.UseVisualStyleBackColor = true;
            // 
            // radio_playerIndex4
            // 
            this.radio_playerIndex4.AutoSize = true;
            this.radio_playerIndex4.Location = new System.Drawing.Point(49, 32);
            this.radio_playerIndex4.Name = "radio_playerIndex4";
            this.radio_playerIndex4.Size = new System.Drawing.Size(31, 19);
            this.radio_playerIndex4.TabIndex = 9;
            this.radio_playerIndex4.TabStop = true;
            this.radio_playerIndex4.Text = "4";
            this.radio_playerIndex4.UseVisualStyleBackColor = true;
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(488, 17);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(102, 44);
            this.button_Start.TabIndex = 12;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 361);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(label_ProgramSelector);
            this.Controls.Add(label_OctaveSwitcher);
            this.Controls.Add(this.radio_playerIndex4);
            this.Controls.Add(this.radio_playerIndex3);
            this.Controls.Add(this.radio_playerIndex2);
            this.Controls.Add(this.radio_playerIndex1);
            this.Controls.Add(this.drumModeCheckbox);
            this.Controls.Add(this.radio_pedal_footController);
            this.Controls.Add(this.radio_pedal_channelVolume);
            this.Controls.Add(this.numUpDown_ProgramSelector);
            this.Controls.Add(this.numUpDown_OctaveSwitcher);
            this.Controls.Add(this.radio_pedal_expression);
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_OctaveSwitcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_ProgramSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton radio_pedal_expression;
        private NumericUpDown numUpDown_OctaveSwitcher;
        private NumericUpDown numUpDown_ProgramSelector;
        private RadioButton radio_pedal_channelVolume;
        private RadioButton radio_pedal_footController;
        private CheckBox drumModeCheckbox;
        private RadioButton radio_playerIndex1;
        private RadioButton radio_playerIndex2;
        private RadioButton radio_playerIndex3;
        private RadioButton radio_playerIndex4;
        private Label label_OctaveSwitcher;
        private Label label_ProgramSelector;
        private Button button_Start;
    }
}

