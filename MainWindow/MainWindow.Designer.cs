using System.Windows.Forms;
using System.Drawing;
using image = RB3_X360_Keyboard.Properties.Resources;

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
            if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox group_PedalMode;
            System.Windows.Forms.GroupBox group_Settings;
            System.Windows.Forms.GroupBox group_OutputType;
            System.Windows.Forms.GroupBox group_KeyboardInputs;
            System.Windows.Forms.Label image_OrangeRange;
            System.Windows.Forms.Label image_GreenRange;
            System.Windows.Forms.Label image_BlueRange;
            System.Windows.Forms.Label image_YellowRange;
            System.Windows.Forms.Label image_RedRange;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.GroupBox group_ButtonInputs;
            System.Windows.Forms.Label label_Dpad_FootController;
            System.Windows.Forms.Label label_Dpad_ChannelVolume;
            System.Windows.Forms.Label label_Dpad_Expression;
            System.Windows.Forms.Label label_Dpad_DrumMode;
            System.Windows.Forms.Label label_ProgramIncrement;
            System.Windows.Forms.Label label_OctaveIncrement;
            System.Windows.Forms.Label label_OctaveDecrement;
            System.Windows.Forms.Label label_ProgramDecrement;
            System.Windows.Forms.GroupBox group_Start;
            this.radio_Pedal_Expression = new System.Windows.Forms.RadioButton();
            this.radio_Pedal_ChannelVolume = new System.Windows.Forms.RadioButton();
            this.radio_Pedal_FootController = new System.Windows.Forms.RadioButton();
            this.numUpDown_Setting_Octave = new System.Windows.Forms.NumericUpDown();
            this.numUpDown_Setting_Program = new System.Windows.Forms.NumericUpDown();
            this.label_Setting_Octave = new System.Windows.Forms.Label();
            this.label_Setting_Program = new System.Windows.Forms.Label();
            this.checkbox_Setting_DrumMode = new System.Windows.Forms.CheckBox();
            this.dropdown_Output_MidiDevice = new System.Windows.Forms.ComboBox();
            this.radio_Output_Xbox360 = new System.Windows.Forms.RadioButton();
            this.radio_Output_Keyboard = new System.Windows.Forms.RadioButton();
            this.radio_Output_Midi = new System.Windows.Forms.RadioButton();
            this.image_KeyC3 = new System.Windows.Forms.Label();
            this.image_KeyBb2 = new System.Windows.Forms.Label();
            this.image_KeyAb2 = new System.Windows.Forms.Label();
            this.image_KeyGb2 = new System.Windows.Forms.Label();
            this.image_KeyEb2 = new System.Windows.Forms.Label();
            this.image_KeyDb2 = new System.Windows.Forms.Label();
            this.image_KeyB2 = new System.Windows.Forms.Label();
            this.image_KeyA2 = new System.Windows.Forms.Label();
            this.image_KeyG2 = new System.Windows.Forms.Label();
            this.image_KeyF2 = new System.Windows.Forms.Label();
            this.image_KeyE2 = new System.Windows.Forms.Label();
            this.image_KeyD2 = new System.Windows.Forms.Label();
            this.image_KeyC2 = new System.Windows.Forms.Label();
            this.image_KeyBb1 = new System.Windows.Forms.Label();
            this.image_KeyAb1 = new System.Windows.Forms.Label();
            this.image_KeyGb1 = new System.Windows.Forms.Label();
            this.image_KeyEb1 = new System.Windows.Forms.Label();
            this.image_KeyDb1 = new System.Windows.Forms.Label();
            this.image_KeyB1 = new System.Windows.Forms.Label();
            this.image_KeyA1 = new System.Windows.Forms.Label();
            this.image_KeyG1 = new System.Windows.Forms.Label();
            this.image_KeyF1 = new System.Windows.Forms.Label();
            this.image_KeyE1 = new System.Windows.Forms.Label();
            this.image_KeyD1 = new System.Windows.Forms.Label();
            this.image_KeyC1 = new System.Windows.Forms.Label();
            this.image_Pedal = new System.Windows.Forms.Label();
            this.label_guideConnectionStatus = new System.Windows.Forms.Label();
            this.image_MidiLight4 = new System.Windows.Forms.Label();
            this.image_MidiLight3 = new System.Windows.Forms.Label();
            this.image_MidiLight2 = new System.Windows.Forms.Label();
            this.image_MidiLight1 = new System.Windows.Forms.Label();
            this.image_OverdriveButton = new System.Windows.Forms.Label();
            this.image_Touchstrip = new System.Windows.Forms.Label();
            this.image_Dpad = new System.Windows.Forms.Label();
            this.image_BackButton = new System.Windows.Forms.Label();
            this.image_StartButton = new System.Windows.Forms.Label();
            this.image_GuideButton = new System.Windows.Forms.Label();
            this.image_BButton = new System.Windows.Forms.Label();
            this.image_YButton = new System.Windows.Forms.Label();
            this.image_XButton = new System.Windows.Forms.Label();
            this.image_AButton = new System.Windows.Forms.Label();
            this.button_Start = new System.Windows.Forms.Button();
            this.group_Debug = new System.Windows.Forms.GroupBox();
            this.button_Debug = new System.Windows.Forms.Button();
            this.timer_IOLoop = new System.Windows.Forms.Timer(this.components);
            group_PedalMode = new System.Windows.Forms.GroupBox();
            group_Settings = new System.Windows.Forms.GroupBox();
            group_OutputType = new System.Windows.Forms.GroupBox();
            group_KeyboardInputs = new System.Windows.Forms.GroupBox();
            image_OrangeRange = new System.Windows.Forms.Label();
            image_GreenRange = new System.Windows.Forms.Label();
            image_BlueRange = new System.Windows.Forms.Label();
            image_YellowRange = new System.Windows.Forms.Label();
            image_RedRange = new System.Windows.Forms.Label();
            group_ButtonInputs = new System.Windows.Forms.GroupBox();
            label_Dpad_FootController = new System.Windows.Forms.Label();
            label_Dpad_ChannelVolume = new System.Windows.Forms.Label();
            label_Dpad_Expression = new System.Windows.Forms.Label();
            label_Dpad_DrumMode = new System.Windows.Forms.Label();
            label_ProgramIncrement = new System.Windows.Forms.Label();
            label_OctaveIncrement = new System.Windows.Forms.Label();
            label_OctaveDecrement = new System.Windows.Forms.Label();
            label_ProgramDecrement = new System.Windows.Forms.Label();
            group_Start = new System.Windows.Forms.GroupBox();
            group_PedalMode.SuspendLayout();
            group_Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Setting_Octave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Setting_Program)).BeginInit();
            group_OutputType.SuspendLayout();
            group_KeyboardInputs.SuspendLayout();
            group_ButtonInputs.SuspendLayout();
            group_Start.SuspendLayout();
            this.group_Debug.SuspendLayout();
            this.SuspendLayout();
            // 
            // group_PedalMode
            // 
            group_PedalMode.Controls.Add(this.radio_Pedal_Expression);
            group_PedalMode.Controls.Add(this.radio_Pedal_ChannelVolume);
            group_PedalMode.Controls.Add(this.radio_Pedal_FootController);
            group_PedalMode.Location = new System.Drawing.Point(12, 12);
            group_PedalMode.Name = "group_PedalMode";
            group_PedalMode.Size = new System.Drawing.Size(120, 93);
            group_PedalMode.TabIndex = 0;
            group_PedalMode.TabStop = false;
            group_PedalMode.Text = "Pedal Mode";
            // 
            // radio_Pedal_Expression
            // 
            this.radio_Pedal_Expression.AutoSize = true;
            this.radio_Pedal_Expression.Checked = true;
            this.radio_Pedal_Expression.Enabled = false;
            this.radio_Pedal_Expression.Location = new System.Drawing.Point(6, 22);
            this.radio_Pedal_Expression.Name = "radio_Pedal_Expression";
            this.radio_Pedal_Expression.Size = new System.Drawing.Size(81, 19);
            this.radio_Pedal_Expression.TabIndex = 0;
            this.radio_Pedal_Expression.TabStop = true;
            this.radio_Pedal_Expression.Text = "Expression";
            this.radio_Pedal_Expression.UseVisualStyleBackColor = true;
            this.radio_Pedal_Expression.CheckedChanged += new System.EventHandler(this.radio_PedalMode_Change);
            // 
            // radio_Pedal_ChannelVolume
            // 
            this.radio_Pedal_ChannelVolume.AutoSize = true;
            this.radio_Pedal_ChannelVolume.Enabled = false;
            this.radio_Pedal_ChannelVolume.Location = new System.Drawing.Point(6, 43);
            this.radio_Pedal_ChannelVolume.Name = "radio_Pedal_ChannelVolume";
            this.radio_Pedal_ChannelVolume.Size = new System.Drawing.Size(112, 19);
            this.radio_Pedal_ChannelVolume.TabIndex = 1;
            this.radio_Pedal_ChannelVolume.Text = "Channel Volume";
            this.radio_Pedal_ChannelVolume.UseVisualStyleBackColor = true;
            this.radio_Pedal_ChannelVolume.CheckedChanged += new System.EventHandler(this.radio_PedalMode_Change);
            // 
            // radio_Pedal_FootController
            // 
            this.radio_Pedal_FootController.AutoSize = true;
            this.radio_Pedal_FootController.Enabled = false;
            this.radio_Pedal_FootController.Location = new System.Drawing.Point(6, 64);
            this.radio_Pedal_FootController.Name = "radio_Pedal_FootController";
            this.radio_Pedal_FootController.Size = new System.Drawing.Size(105, 19);
            this.radio_Pedal_FootController.TabIndex = 2;
            this.radio_Pedal_FootController.Text = "Foot Controller";
            this.radio_Pedal_FootController.UseVisualStyleBackColor = true;
            this.radio_Pedal_FootController.CheckedChanged += new System.EventHandler(this.radio_PedalMode_Change);
            // 
            // group_Settings
            // 
            group_Settings.Controls.Add(this.numUpDown_Setting_Octave);
            group_Settings.Controls.Add(this.numUpDown_Setting_Program);
            group_Settings.Controls.Add(this.label_Setting_Octave);
            group_Settings.Controls.Add(this.label_Setting_Program);
            group_Settings.Controls.Add(this.checkbox_Setting_DrumMode);
            group_Settings.Location = new System.Drawing.Point(138, 12);
            group_Settings.Name = "group_Settings";
            group_Settings.Size = new System.Drawing.Size(120, 93);
            group_Settings.TabIndex = 1;
            group_Settings.TabStop = false;
            group_Settings.Text = "Settings";
            // 
            // numUpDown_Setting_Octave
            // 
            this.numUpDown_Setting_Octave.Enabled = false;
            this.numUpDown_Setting_Octave.Location = new System.Drawing.Point(10, 37);
            this.numUpDown_Setting_Octave.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numUpDown_Setting_Octave.Name = "numUpDown_Setting_Octave";
            this.numUpDown_Setting_Octave.Size = new System.Drawing.Size(36, 23);
            this.numUpDown_Setting_Octave.TabIndex = 1;
            this.numUpDown_Setting_Octave.Tag = "";
            this.numUpDown_Setting_Octave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDown_Setting_Octave.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numUpDown_Setting_Octave.ValueChanged += new System.EventHandler(this.numUpDown_SettingOctave_Change);
            // 
            // numUpDown_Setting_Program
            // 
            this.numUpDown_Setting_Program.Enabled = false;
            this.numUpDown_Setting_Program.Location = new System.Drawing.Point(60, 37);
            this.numUpDown_Setting_Program.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numUpDown_Setting_Program.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown_Setting_Program.Name = "numUpDown_Setting_Program";
            this.numUpDown_Setting_Program.Size = new System.Drawing.Size(45, 23);
            this.numUpDown_Setting_Program.TabIndex = 3;
            this.numUpDown_Setting_Program.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDown_Setting_Program.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown_Setting_Program.ValueChanged += new System.EventHandler(this.numUpDown_SettingProgram_Change);
            // 
            // label_Setting_Octave
            // 
            this.label_Setting_Octave.AutoSize = true;
            this.label_Setting_Octave.Enabled = false;
            this.label_Setting_Octave.Location = new System.Drawing.Point(6, 19);
            this.label_Setting_Octave.Name = "label_Setting_Octave";
            this.label_Setting_Octave.Size = new System.Drawing.Size(44, 15);
            this.label_Setting_Octave.TabIndex = 0;
            this.label_Setting_Octave.Text = "Octave";
            // 
            // label_Setting_Program
            // 
            this.label_Setting_Program.AutoSize = true;
            this.label_Setting_Program.Enabled = false;
            this.label_Setting_Program.Location = new System.Drawing.Point(56, 19);
            this.label_Setting_Program.Name = "label_Setting_Program";
            this.label_Setting_Program.Size = new System.Drawing.Size(53, 15);
            this.label_Setting_Program.TabIndex = 2;
            this.label_Setting_Program.Text = "Program";
            // 
            // checkbox_Setting_DrumMode
            // 
            this.checkbox_Setting_DrumMode.AutoSize = true;
            this.checkbox_Setting_DrumMode.Enabled = false;
            this.checkbox_Setting_DrumMode.Location = new System.Drawing.Point(10, 66);
            this.checkbox_Setting_DrumMode.Name = "checkbox_Setting_DrumMode";
            this.checkbox_Setting_DrumMode.Size = new System.Drawing.Size(90, 19);
            this.checkbox_Setting_DrumMode.TabIndex = 4;
            this.checkbox_Setting_DrumMode.Text = "Drum Mode";
            this.checkbox_Setting_DrumMode.UseVisualStyleBackColor = true;
            this.checkbox_Setting_DrumMode.CheckedChanged += new System.EventHandler(this.checkbox_SettingDrumMode_Changed);
            // 
            // group_OutputType
            // 
            group_OutputType.Controls.Add(this.dropdown_Output_MidiDevice);
            group_OutputType.Controls.Add(this.radio_Output_Xbox360);
            group_OutputType.Controls.Add(this.radio_Output_Keyboard);
            group_OutputType.Controls.Add(this.radio_Output_Midi);
            group_OutputType.Location = new System.Drawing.Point(264, 12);
            group_OutputType.Name = "group_OutputType";
            group_OutputType.Size = new System.Drawing.Size(216, 93);
            group_OutputType.TabIndex = 2;
            group_OutputType.TabStop = false;
            group_OutputType.Text = "Output Type";
            // 
            // dropdown_Output_MidiDevice
            // 
            this.dropdown_Output_MidiDevice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.dropdown_Output_MidiDevice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.dropdown_Output_MidiDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdown_Output_MidiDevice.FormattingEnabled = true;
            this.dropdown_Output_MidiDevice.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dropdown_Output_MidiDevice.IntegralHeight = false;
            this.dropdown_Output_MidiDevice.Location = new System.Drawing.Point(56, 62);
            this.dropdown_Output_MidiDevice.MaxDropDownItems = 100;
            this.dropdown_Output_MidiDevice.Name = "dropdown_Output_MidiDevice";
            this.dropdown_Output_MidiDevice.Size = new System.Drawing.Size(154, 23);
            this.dropdown_Output_MidiDevice.TabIndex = 3;
            this.dropdown_Output_MidiDevice.DropDown += new System.EventHandler(this.dropdown_OutputMidi_Device_Open);
            // 
            // radio_Output_Xbox360
            // 
            this.radio_Output_Xbox360.AutoSize = true;
            this.radio_Output_Xbox360.Enabled = false;
            this.radio_Output_Xbox360.Location = new System.Drawing.Point(6, 22);
            this.radio_Output_Xbox360.Name = "radio_Output_Xbox360";
            this.radio_Output_Xbox360.Size = new System.Drawing.Size(129, 19);
            this.radio_Output_Xbox360.TabIndex = 0;
            this.radio_Output_Xbox360.Text = "Xbox 360 Controller";
            this.radio_Output_Xbox360.UseVisualStyleBackColor = true;
            this.radio_Output_Xbox360.CheckedChanged += new System.EventHandler(this.radio_OutputType_Change);
            // 
            // radio_Output_Keyboard
            // 
            this.radio_Output_Keyboard.AutoSize = true;
            this.radio_Output_Keyboard.Checked = true;
            this.radio_Output_Keyboard.Location = new System.Drawing.Point(6, 43);
            this.radio_Output_Keyboard.Name = "radio_Output_Keyboard";
            this.radio_Output_Keyboard.Size = new System.Drawing.Size(75, 19);
            this.radio_Output_Keyboard.TabIndex = 1;
            this.radio_Output_Keyboard.TabStop = true;
            this.radio_Output_Keyboard.Text = "Keyboard";
            this.radio_Output_Keyboard.UseVisualStyleBackColor = true;
            this.radio_Output_Keyboard.CheckedChanged += new System.EventHandler(this.radio_OutputType_Change);
            // 
            // radio_Output_Midi
            // 
            this.radio_Output_Midi.AutoSize = true;
            this.radio_Output_Midi.Location = new System.Drawing.Point(6, 64);
            this.radio_Output_Midi.Name = "radio_Output_Midi";
            this.radio_Output_Midi.Size = new System.Drawing.Size(50, 19);
            this.radio_Output_Midi.TabIndex = 2;
            this.radio_Output_Midi.Text = "MIDI";
            this.radio_Output_Midi.UseVisualStyleBackColor = true;
            this.radio_Output_Midi.CheckedChanged += new System.EventHandler(this.radio_OutputType_Change);
            // 
            // group_KeyboardInputs
            // 
            group_KeyboardInputs.Controls.Add(image_OrangeRange);
            group_KeyboardInputs.Controls.Add(image_GreenRange);
            group_KeyboardInputs.Controls.Add(image_BlueRange);
            group_KeyboardInputs.Controls.Add(image_YellowRange);
            group_KeyboardInputs.Controls.Add(image_RedRange);
            group_KeyboardInputs.Controls.Add(this.image_KeyC3);
            group_KeyboardInputs.Controls.Add(this.image_KeyBb2);
            group_KeyboardInputs.Controls.Add(this.image_KeyAb2);
            group_KeyboardInputs.Controls.Add(this.image_KeyGb2);
            group_KeyboardInputs.Controls.Add(this.image_KeyEb2);
            group_KeyboardInputs.Controls.Add(this.image_KeyDb2);
            group_KeyboardInputs.Controls.Add(this.image_KeyB2);
            group_KeyboardInputs.Controls.Add(this.image_KeyA2);
            group_KeyboardInputs.Controls.Add(this.image_KeyG2);
            group_KeyboardInputs.Controls.Add(this.image_KeyF2);
            group_KeyboardInputs.Controls.Add(this.image_KeyE2);
            group_KeyboardInputs.Controls.Add(this.image_KeyD2);
            group_KeyboardInputs.Controls.Add(this.image_KeyC2);
            group_KeyboardInputs.Controls.Add(this.image_KeyBb1);
            group_KeyboardInputs.Controls.Add(this.image_KeyAb1);
            group_KeyboardInputs.Controls.Add(this.image_KeyGb1);
            group_KeyboardInputs.Controls.Add(this.image_KeyEb1);
            group_KeyboardInputs.Controls.Add(this.image_KeyDb1);
            group_KeyboardInputs.Controls.Add(this.image_KeyB1);
            group_KeyboardInputs.Controls.Add(this.image_KeyA1);
            group_KeyboardInputs.Controls.Add(this.image_KeyG1);
            group_KeyboardInputs.Controls.Add(this.image_KeyF1);
            group_KeyboardInputs.Controls.Add(this.image_KeyE1);
            group_KeyboardInputs.Controls.Add(this.image_KeyD1);
            group_KeyboardInputs.Controls.Add(this.image_KeyC1);
            group_KeyboardInputs.Location = new System.Drawing.Point(12, 255);
            group_KeyboardInputs.Name = "group_KeyboardInputs";
            group_KeyboardInputs.Size = new System.Drawing.Size(804, 345);
            group_KeyboardInputs.TabIndex = 5;
            group_KeyboardInputs.TabStop = false;
            group_KeyboardInputs.Text = "Keyboard Inputs";
            // 
            // image_OrangeRange
            // 
            image_OrangeRange.AutoSize = true;
            image_OrangeRange.Image = global::RB3_X360_Keyboard.Properties.Resources.orangeLabel_cropped;
            image_OrangeRange.Location = new System.Drawing.Point(748, 19);
            image_OrangeRange.MaximumSize = new System.Drawing.Size(50, 9);
            image_OrangeRange.MinimumSize = new System.Drawing.Size(50, 9);
            image_OrangeRange.Name = "image_OrangeRange";
            image_OrangeRange.Size = new System.Drawing.Size(50, 9);
            image_OrangeRange.TabIndex = 4;
            // 
            // image_GreenRange
            // 
            image_GreenRange.AutoSize = true;
            image_GreenRange.Image = global::RB3_X360_Keyboard.Properties.Resources.greenLabel_cropped;
            image_GreenRange.Location = new System.Drawing.Point(536, 19);
            image_GreenRange.MaximumSize = new System.Drawing.Size(209, 9);
            image_GreenRange.MinimumSize = new System.Drawing.Size(209, 9);
            image_GreenRange.Name = "image_GreenRange";
            image_GreenRange.Size = new System.Drawing.Size(209, 9);
            image_GreenRange.TabIndex = 3;
            // 
            // image_BlueRange
            // 
            image_BlueRange.AutoSize = true;
            image_BlueRange.Image = global::RB3_X360_Keyboard.Properties.Resources.blueLabel_cropped;
            image_BlueRange.Location = new System.Drawing.Point(377, 19);
            image_BlueRange.MaximumSize = new System.Drawing.Size(156, 9);
            image_BlueRange.MinimumSize = new System.Drawing.Size(156, 9);
            image_BlueRange.Name = "image_BlueRange";
            image_BlueRange.Size = new System.Drawing.Size(156, 9);
            image_BlueRange.TabIndex = 2;
            // 
            // image_YellowRange
            // 
            image_YellowRange.AutoSize = true;
            image_YellowRange.Image = global::RB3_X360_Keyboard.Properties.Resources.yellowLabel_cropped;
            image_YellowRange.Location = new System.Drawing.Point(165, 19);
            image_YellowRange.MaximumSize = new System.Drawing.Size(209, 9);
            image_YellowRange.MinimumSize = new System.Drawing.Size(209, 9);
            image_YellowRange.Name = "image_YellowRange";
            image_YellowRange.Size = new System.Drawing.Size(209, 9);
            image_YellowRange.TabIndex = 1;
            // 
            // image_RedRange
            // 
            image_RedRange.AutoSize = true;
            image_RedRange.Image = global::RB3_X360_Keyboard.Properties.Resources.redLabel_cropped;
            image_RedRange.Location = new System.Drawing.Point(6, 19);
            image_RedRange.MaximumSize = new System.Drawing.Size(156, 9);
            image_RedRange.MinimumSize = new System.Drawing.Size(156, 9);
            image_RedRange.Name = "image_RedRange";
            image_RedRange.Size = new System.Drawing.Size(156, 9);
            image_RedRange.TabIndex = 0;
            // 
            // image_KeyC3
            // 
            this.image_KeyC3.AutoSize = true;
            this.image_KeyC3.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyC3.Image = global::RB3_X360_Keyboard.Properties.Resources.keyEnd_cropped;
            this.image_KeyC3.Location = new System.Drawing.Point(748, 31);
            this.image_KeyC3.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyC3.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyC3.Name = "image_KeyC3";
            this.image_KeyC3.Size = new System.Drawing.Size(50, 308);
            this.image_KeyC3.TabIndex = 29;
            this.image_KeyC3.Text = " C3\r\n";
            this.image_KeyC3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyBb2
            // 
            this.image_KeyBb2.AutoSize = true;
            this.image_KeyBb2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyBb2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyBb2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyBb2.Image")));
            this.image_KeyBb2.Location = new System.Drawing.Point(684, 31);
            this.image_KeyBb2.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyBb2.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyBb2.Name = "image_KeyBb2";
            this.image_KeyBb2.Size = new System.Drawing.Size(30, 196);
            this.image_KeyBb2.TabIndex = 27;
            this.image_KeyBb2.Text = "A#\r\nBb2\r\n\r\n";
            this.image_KeyBb2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyAb2
            // 
            this.image_KeyAb2.AutoSize = true;
            this.image_KeyAb2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyAb2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyAb2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyAb2.Image")));
            this.image_KeyAb2.Location = new System.Drawing.Point(625, 31);
            this.image_KeyAb2.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyAb2.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyAb2.Name = "image_KeyAb2";
            this.image_KeyAb2.Size = new System.Drawing.Size(30, 196);
            this.image_KeyAb2.TabIndex = 25;
            this.image_KeyAb2.Text = "G#\r\nAb2\r\n\r\n";
            this.image_KeyAb2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyGb2
            // 
            this.image_KeyGb2.AutoSize = true;
            this.image_KeyGb2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyGb2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyGb2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyGb2.Image")));
            this.image_KeyGb2.Location = new System.Drawing.Point(567, 31);
            this.image_KeyGb2.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyGb2.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyGb2.Name = "image_KeyGb2";
            this.image_KeyGb2.Size = new System.Drawing.Size(30, 196);
            this.image_KeyGb2.TabIndex = 23;
            this.image_KeyGb2.Text = "F#\r\nGb2\r\n\r\n";
            this.image_KeyGb2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyEb2
            // 
            this.image_KeyEb2.AutoSize = true;
            this.image_KeyEb2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyEb2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyEb2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyEb2.Image")));
            this.image_KeyEb2.Location = new System.Drawing.Point(472, 31);
            this.image_KeyEb2.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyEb2.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyEb2.Name = "image_KeyEb2";
            this.image_KeyEb2.Size = new System.Drawing.Size(30, 196);
            this.image_KeyEb2.TabIndex = 20;
            this.image_KeyEb2.Text = "D#\r\nEb2\r\n\r\n";
            this.image_KeyEb2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyDb2
            // 
            this.image_KeyDb2.AutoSize = true;
            this.image_KeyDb2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyDb2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyDb2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyDb2.Image")));
            this.image_KeyDb2.Location = new System.Drawing.Point(408, 31);
            this.image_KeyDb2.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyDb2.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyDb2.Name = "image_KeyDb2";
            this.image_KeyDb2.Size = new System.Drawing.Size(30, 196);
            this.image_KeyDb2.TabIndex = 18;
            this.image_KeyDb2.Text = "C#\r\nDb2\r\n\r\n";
            this.image_KeyDb2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyB2
            // 
            this.image_KeyB2.AutoSize = true;
            this.image_KeyB2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyB2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyB2.Image")));
            this.image_KeyB2.Location = new System.Drawing.Point(695, 31);
            this.image_KeyB2.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyB2.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyB2.Name = "image_KeyB2";
            this.image_KeyB2.Size = new System.Drawing.Size(50, 308);
            this.image_KeyB2.TabIndex = 28;
            this.image_KeyB2.Text = " B2\r\n";
            this.image_KeyB2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyA2
            // 
            this.image_KeyA2.AutoSize = true;
            this.image_KeyA2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyA2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyA2.Image")));
            this.image_KeyA2.Location = new System.Drawing.Point(642, 31);
            this.image_KeyA2.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyA2.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyA2.Name = "image_KeyA2";
            this.image_KeyA2.Size = new System.Drawing.Size(50, 308);
            this.image_KeyA2.TabIndex = 26;
            this.image_KeyA2.Text = " A2\r\n";
            this.image_KeyA2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyG2
            // 
            this.image_KeyG2.AutoSize = true;
            this.image_KeyG2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyG2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyG2.Image")));
            this.image_KeyG2.Location = new System.Drawing.Point(589, 31);
            this.image_KeyG2.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyG2.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyG2.Name = "image_KeyG2";
            this.image_KeyG2.Size = new System.Drawing.Size(50, 308);
            this.image_KeyG2.TabIndex = 24;
            this.image_KeyG2.Text = " G2\r\n";
            this.image_KeyG2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyF2
            // 
            this.image_KeyF2.AutoSize = true;
            this.image_KeyF2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyF2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyF2.Image")));
            this.image_KeyF2.Location = new System.Drawing.Point(536, 31);
            this.image_KeyF2.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyF2.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyF2.Name = "image_KeyF2";
            this.image_KeyF2.Size = new System.Drawing.Size(50, 308);
            this.image_KeyF2.TabIndex = 22;
            this.image_KeyF2.Text = " F2\r\n";
            this.image_KeyF2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyE2
            // 
            this.image_KeyE2.AutoSize = true;
            this.image_KeyE2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyE2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyE2.Image")));
            this.image_KeyE2.Location = new System.Drawing.Point(483, 31);
            this.image_KeyE2.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyE2.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyE2.Name = "image_KeyE2";
            this.image_KeyE2.Size = new System.Drawing.Size(50, 308);
            this.image_KeyE2.TabIndex = 21;
            this.image_KeyE2.Text = " E2\r\n";
            this.image_KeyE2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyD2
            // 
            this.image_KeyD2.AutoSize = true;
            this.image_KeyD2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyD2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyD2.Image")));
            this.image_KeyD2.Location = new System.Drawing.Point(430, 31);
            this.image_KeyD2.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyD2.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyD2.Name = "image_KeyD2";
            this.image_KeyD2.Size = new System.Drawing.Size(50, 308);
            this.image_KeyD2.TabIndex = 19;
            this.image_KeyD2.Text = " D2\r\n";
            this.image_KeyD2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyC2
            // 
            this.image_KeyC2.AutoSize = true;
            this.image_KeyC2.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyC2.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyC2.Image")));
            this.image_KeyC2.Location = new System.Drawing.Point(377, 31);
            this.image_KeyC2.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyC2.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyC2.Name = "image_KeyC2";
            this.image_KeyC2.Size = new System.Drawing.Size(50, 308);
            this.image_KeyC2.TabIndex = 17;
            this.image_KeyC2.Text = " C2\r\n";
            this.image_KeyC2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyBb1
            // 
            this.image_KeyBb1.AutoSize = true;
            this.image_KeyBb1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyBb1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyBb1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyBb1.Image")));
            this.image_KeyBb1.Location = new System.Drawing.Point(313, 31);
            this.image_KeyBb1.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyBb1.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyBb1.Name = "image_KeyBb1";
            this.image_KeyBb1.Size = new System.Drawing.Size(30, 196);
            this.image_KeyBb1.TabIndex = 15;
            this.image_KeyBb1.Text = "A#\r\nBb1\r\n\r\n";
            this.image_KeyBb1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyAb1
            // 
            this.image_KeyAb1.AutoSize = true;
            this.image_KeyAb1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyAb1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyAb1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyAb1.Image")));
            this.image_KeyAb1.Location = new System.Drawing.Point(254, 31);
            this.image_KeyAb1.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyAb1.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyAb1.Name = "image_KeyAb1";
            this.image_KeyAb1.Size = new System.Drawing.Size(30, 196);
            this.image_KeyAb1.TabIndex = 13;
            this.image_KeyAb1.Text = "G#\r\nAb1\r\n\r\n";
            this.image_KeyAb1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyGb1
            // 
            this.image_KeyGb1.AutoSize = true;
            this.image_KeyGb1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyGb1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyGb1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyGb1.Image")));
            this.image_KeyGb1.Location = new System.Drawing.Point(196, 31);
            this.image_KeyGb1.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyGb1.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyGb1.Name = "image_KeyGb1";
            this.image_KeyGb1.Size = new System.Drawing.Size(30, 196);
            this.image_KeyGb1.TabIndex = 11;
            this.image_KeyGb1.Text = "F#\r\nGb1\r\n\r\n";
            this.image_KeyGb1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyEb1
            // 
            this.image_KeyEb1.AutoSize = true;
            this.image_KeyEb1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyEb1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyEb1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyEb1.Image")));
            this.image_KeyEb1.Location = new System.Drawing.Point(101, 31);
            this.image_KeyEb1.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyEb1.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyEb1.Name = "image_KeyEb1";
            this.image_KeyEb1.Size = new System.Drawing.Size(30, 196);
            this.image_KeyEb1.TabIndex = 8;
            this.image_KeyEb1.Text = "D#\r\nEb1\r\n\r\n";
            this.image_KeyEb1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyDb1
            // 
            this.image_KeyDb1.AutoSize = true;
            this.image_KeyDb1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyDb1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.image_KeyDb1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyDb1.Image")));
            this.image_KeyDb1.Location = new System.Drawing.Point(37, 31);
            this.image_KeyDb1.MaximumSize = new System.Drawing.Size(30, 196);
            this.image_KeyDb1.MinimumSize = new System.Drawing.Size(30, 196);
            this.image_KeyDb1.Name = "image_KeyDb1";
            this.image_KeyDb1.Size = new System.Drawing.Size(30, 196);
            this.image_KeyDb1.TabIndex = 6;
            this.image_KeyDb1.Text = "C#\r\nDb1\r\n\r\n";
            this.image_KeyDb1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyB1
            // 
            this.image_KeyB1.AutoSize = true;
            this.image_KeyB1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyB1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyB1.Image")));
            this.image_KeyB1.Location = new System.Drawing.Point(324, 31);
            this.image_KeyB1.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyB1.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyB1.Name = "image_KeyB1";
            this.image_KeyB1.Size = new System.Drawing.Size(50, 308);
            this.image_KeyB1.TabIndex = 16;
            this.image_KeyB1.Text = " B1\r\n";
            this.image_KeyB1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyA1
            // 
            this.image_KeyA1.AutoSize = true;
            this.image_KeyA1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyA1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyA1.Image")));
            this.image_KeyA1.Location = new System.Drawing.Point(271, 31);
            this.image_KeyA1.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyA1.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyA1.Name = "image_KeyA1";
            this.image_KeyA1.Size = new System.Drawing.Size(50, 308);
            this.image_KeyA1.TabIndex = 14;
            this.image_KeyA1.Text = " A1\r\n";
            this.image_KeyA1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyG1
            // 
            this.image_KeyG1.AutoSize = true;
            this.image_KeyG1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyG1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyG1.Image")));
            this.image_KeyG1.Location = new System.Drawing.Point(218, 31);
            this.image_KeyG1.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyG1.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyG1.Name = "image_KeyG1";
            this.image_KeyG1.Size = new System.Drawing.Size(50, 308);
            this.image_KeyG1.TabIndex = 12;
            this.image_KeyG1.Text = " G1\r\n";
            this.image_KeyG1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyF1
            // 
            this.image_KeyF1.AutoSize = true;
            this.image_KeyF1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyF1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyF1.Image")));
            this.image_KeyF1.Location = new System.Drawing.Point(165, 31);
            this.image_KeyF1.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyF1.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyF1.Name = "image_KeyF1";
            this.image_KeyF1.Size = new System.Drawing.Size(50, 308);
            this.image_KeyF1.TabIndex = 10;
            this.image_KeyF1.Text = " F1\r\n";
            this.image_KeyF1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyE1
            // 
            this.image_KeyE1.AutoSize = true;
            this.image_KeyE1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyE1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyE1.Image")));
            this.image_KeyE1.Location = new System.Drawing.Point(112, 31);
            this.image_KeyE1.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyE1.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyE1.Name = "image_KeyE1";
            this.image_KeyE1.Size = new System.Drawing.Size(50, 308);
            this.image_KeyE1.TabIndex = 9;
            this.image_KeyE1.Text = " E1\r\n";
            this.image_KeyE1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyD1
            // 
            this.image_KeyD1.AutoSize = true;
            this.image_KeyD1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyD1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyD1.Image")));
            this.image_KeyD1.Location = new System.Drawing.Point(59, 31);
            this.image_KeyD1.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyD1.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyD1.Name = "image_KeyD1";
            this.image_KeyD1.Size = new System.Drawing.Size(50, 308);
            this.image_KeyD1.TabIndex = 7;
            this.image_KeyD1.Text = " D1\r\n";
            this.image_KeyD1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // image_KeyC1
            // 
            this.image_KeyC1.AutoSize = true;
            this.image_KeyC1.Font = new System.Drawing.Font("Lato", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.image_KeyC1.Image = ((System.Drawing.Image)(resources.GetObject("image_KeyC1.Image")));
            this.image_KeyC1.Location = new System.Drawing.Point(6, 31);
            this.image_KeyC1.MaximumSize = new System.Drawing.Size(50, 308);
            this.image_KeyC1.MinimumSize = new System.Drawing.Size(50, 308);
            this.image_KeyC1.Name = "image_KeyC1";
            this.image_KeyC1.Size = new System.Drawing.Size(50, 308);
            this.image_KeyC1.TabIndex = 5;
            this.image_KeyC1.Text = " C1\r\n";
            this.image_KeyC1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // group_ButtonInputs
            // 
            group_ButtonInputs.Controls.Add(this.image_Pedal);
            group_ButtonInputs.Controls.Add(this.label_guideConnectionStatus);
            group_ButtonInputs.Controls.Add(this.image_MidiLight4);
            group_ButtonInputs.Controls.Add(this.image_MidiLight3);
            group_ButtonInputs.Controls.Add(this.image_MidiLight2);
            group_ButtonInputs.Controls.Add(this.image_MidiLight1);
            group_ButtonInputs.Controls.Add(label_Dpad_FootController);
            group_ButtonInputs.Controls.Add(label_Dpad_ChannelVolume);
            group_ButtonInputs.Controls.Add(label_Dpad_Expression);
            group_ButtonInputs.Controls.Add(label_Dpad_DrumMode);
            group_ButtonInputs.Controls.Add(label_ProgramIncrement);
            group_ButtonInputs.Controls.Add(label_OctaveIncrement);
            group_ButtonInputs.Controls.Add(label_OctaveDecrement);
            group_ButtonInputs.Controls.Add(label_ProgramDecrement);
            group_ButtonInputs.Controls.Add(this.image_OverdriveButton);
            group_ButtonInputs.Controls.Add(this.image_Touchstrip);
            group_ButtonInputs.Controls.Add(this.image_Dpad);
            group_ButtonInputs.Controls.Add(this.image_BackButton);
            group_ButtonInputs.Controls.Add(this.image_StartButton);
            group_ButtonInputs.Controls.Add(this.image_GuideButton);
            group_ButtonInputs.Controls.Add(this.image_BButton);
            group_ButtonInputs.Controls.Add(this.image_YButton);
            group_ButtonInputs.Controls.Add(this.image_XButton);
            group_ButtonInputs.Controls.Add(this.image_AButton);
            group_ButtonInputs.Location = new System.Drawing.Point(12, 111);
            group_ButtonInputs.Name = "group_ButtonInputs";
            group_ButtonInputs.Size = new System.Drawing.Size(804, 138);
            group_ButtonInputs.TabIndex = 4;
            group_ButtonInputs.TabStop = false;
            group_ButtonInputs.Text = "Button Inputs";
            // 
            // image_Pedal
            // 
            this.image_Pedal.AutoSize = true;
            this.image_Pedal.Image = global::RB3_X360_Keyboard.Properties.Resources.pedal_small;
            this.image_Pedal.Location = new System.Drawing.Point(33, 22);
            this.image_Pedal.MaximumSize = new System.Drawing.Size(130, 51);
            this.image_Pedal.MinimumSize = new System.Drawing.Size(130, 51);
            this.image_Pedal.Name = "image_Pedal";
            this.image_Pedal.Size = new System.Drawing.Size(130, 51);
            this.image_Pedal.TabIndex = 23;
            // 
            // label_guideConnectionStatus
            // 
            this.label_guideConnectionStatus.AutoSize = true;
            this.label_guideConnectionStatus.Location = new System.Drawing.Point(440, 16);
            this.label_guideConnectionStatus.Name = "label_guideConnectionStatus";
            this.label_guideConnectionStatus.Size = new System.Drawing.Size(82, 30);
            this.label_guideConnectionStatus.TabIndex = 22;
            this.label_guideConnectionStatus.Text = "No controllers\r\nconnected";
            this.label_guideConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // image_MidiLight4
            // 
            this.image_MidiLight4.AutoSize = true;
            this.image_MidiLight4.BackColor = System.Drawing.Color.Black;
            this.image_MidiLight4.Location = new System.Drawing.Point(493, 103);
            this.image_MidiLight4.MaximumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight4.MinimumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight4.Name = "image_MidiLight4";
            this.image_MidiLight4.Size = new System.Drawing.Size(7, 7);
            this.image_MidiLight4.TabIndex = 21;
            // 
            // image_MidiLight3
            // 
            this.image_MidiLight3.AutoSize = true;
            this.image_MidiLight3.BackColor = System.Drawing.Color.Black;
            this.image_MidiLight3.Location = new System.Drawing.Point(482, 103);
            this.image_MidiLight3.MaximumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight3.MinimumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight3.Name = "image_MidiLight3";
            this.image_MidiLight3.Size = new System.Drawing.Size(7, 7);
            this.image_MidiLight3.TabIndex = 20;
            // 
            // image_MidiLight2
            // 
            this.image_MidiLight2.AutoSize = true;
            this.image_MidiLight2.BackColor = System.Drawing.Color.Black;
            this.image_MidiLight2.Location = new System.Drawing.Point(471, 103);
            this.image_MidiLight2.MaximumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight2.MinimumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight2.Name = "image_MidiLight2";
            this.image_MidiLight2.Size = new System.Drawing.Size(7, 7);
            this.image_MidiLight2.TabIndex = 19;
            // 
            // image_MidiLight1
            // 
            this.image_MidiLight1.AutoSize = true;
            this.image_MidiLight1.BackColor = System.Drawing.Color.Black;
            this.image_MidiLight1.Location = new System.Drawing.Point(460, 103);
            this.image_MidiLight1.MaximumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight1.MinimumSize = new System.Drawing.Size(7, 7);
            this.image_MidiLight1.Name = "image_MidiLight1";
            this.image_MidiLight1.Size = new System.Drawing.Size(7, 7);
            this.image_MidiLight1.TabIndex = 18;
            // 
            // label_Dpad_FootController
            // 
            label_Dpad_FootController.AutoSize = true;
            label_Dpad_FootController.Location = new System.Drawing.Point(349, 56);
            label_Dpad_FootController.Name = "label_Dpad_FootController";
            label_Dpad_FootController.Size = new System.Drawing.Size(60, 30);
            label_Dpad_FootController.TabIndex = 6;
            label_Dpad_FootController.Text = "Foot\r\nController";
            label_Dpad_FootController.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Dpad_ChannelVolume
            // 
            label_Dpad_ChannelVolume.AutoSize = true;
            label_Dpad_ChannelVolume.Location = new System.Drawing.Point(262, 111);
            label_Dpad_ChannelVolume.Name = "label_Dpad_ChannelVolume";
            label_Dpad_ChannelVolume.Size = new System.Drawing.Size(94, 15);
            label_Dpad_ChannelVolume.TabIndex = 5;
            label_Dpad_ChannelVolume.Text = "Channel Volume";
            label_Dpad_ChannelVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Dpad_Expression
            // 
            label_Dpad_Expression.AutoSize = true;
            label_Dpad_Expression.Location = new System.Drawing.Point(208, 64);
            label_Dpad_Expression.Name = "label_Dpad_Expression";
            label_Dpad_Expression.Size = new System.Drawing.Size(63, 15);
            label_Dpad_Expression.TabIndex = 4;
            label_Dpad_Expression.Text = "Expression";
            label_Dpad_Expression.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Dpad_DrumMode
            // 
            label_Dpad_DrumMode.AutoSize = true;
            label_Dpad_DrumMode.BackColor = System.Drawing.Color.Transparent;
            label_Dpad_DrumMode.Location = new System.Drawing.Point(274, 17);
            label_Dpad_DrumMode.Name = "label_Dpad_DrumMode";
            label_Dpad_DrumMode.Size = new System.Drawing.Size(71, 15);
            label_Dpad_DrumMode.TabIndex = 3;
            label_Dpad_DrumMode.Text = "Drum Mode";
            label_Dpad_DrumMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_ProgramIncrement
            // 
            label_ProgramIncrement.AutoSize = true;
            label_ProgramIncrement.BackColor = System.Drawing.Color.Transparent;
            label_ProgramIncrement.Location = new System.Drawing.Point(625, 12);
            label_ProgramIncrement.Name = "label_ProgramIncrement";
            label_ProgramIncrement.Size = new System.Drawing.Size(110, 15);
            label_ProgramIncrement.TabIndex = 17;
            label_ProgramIncrement.Text = "Program Increment";
            label_ProgramIncrement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_OctaveIncrement
            // 
            label_OctaveIncrement.AutoSize = true;
            label_OctaveIncrement.Location = new System.Drawing.Point(727, 56);
            label_OctaveIncrement.Name = "label_OctaveIncrement";
            label_OctaveIncrement.Size = new System.Drawing.Size(61, 30);
            label_OctaveIncrement.TabIndex = 13;
            label_OctaveIncrement.Text = "Octave\r\nIncrement";
            label_OctaveIncrement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_OctaveDecrement
            // 
            label_OctaveDecrement.AutoSize = true;
            label_OctaveDecrement.Location = new System.Drawing.Point(565, 56);
            label_OctaveDecrement.Name = "label_OctaveDecrement";
            label_OctaveDecrement.Size = new System.Drawing.Size(65, 30);
            label_OctaveDecrement.TabIndex = 15;
            label_OctaveDecrement.Text = "Octave\r\nDecrement";
            label_OctaveDecrement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_ProgramDecrement
            // 
            label_ProgramDecrement.AutoSize = true;
            label_ProgramDecrement.BackColor = System.Drawing.Color.Transparent;
            label_ProgramDecrement.Location = new System.Drawing.Point(623, 117);
            label_ProgramDecrement.Name = "label_ProgramDecrement";
            label_ProgramDecrement.Size = new System.Drawing.Size(114, 15);
            label_ProgramDecrement.TabIndex = 11;
            label_ProgramDecrement.Text = "Program Decrement";
            label_ProgramDecrement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // image_OverdriveButton
            // 
            this.image_OverdriveButton.AutoSize = true;
            this.image_OverdriveButton.Image = global::RB3_X360_Keyboard.Properties.Resources.overdrive_cropped;
            this.image_OverdriveButton.Location = new System.Drawing.Point(139, 80);
            this.image_OverdriveButton.MaximumSize = new System.Drawing.Size(44, 40);
            this.image_OverdriveButton.MinimumSize = new System.Drawing.Size(44, 40);
            this.image_OverdriveButton.Name = "image_OverdriveButton";
            this.image_OverdriveButton.Size = new System.Drawing.Size(44, 40);
            this.image_OverdriveButton.TabIndex = 1;
            // 
            // image_Touchstrip
            // 
            this.image_Touchstrip.AutoSize = true;
            this.image_Touchstrip.Image = global::RB3_X360_Keyboard.Properties.Resources.touchstrip_cropped;
            this.image_Touchstrip.Location = new System.Drawing.Point(14, 80);
            this.image_Touchstrip.MaximumSize = new System.Drawing.Size(117, 43);
            this.image_Touchstrip.MinimumSize = new System.Drawing.Size(117, 43);
            this.image_Touchstrip.Name = "image_Touchstrip";
            this.image_Touchstrip.Size = new System.Drawing.Size(117, 43);
            this.image_Touchstrip.TabIndex = 0;
            // 
            // image_Dpad
            // 
            this.image_Dpad.AutoSize = true;
            this.image_Dpad.Image = global::RB3_X360_Keyboard.Properties.Resources.dpad_cropped;
            this.image_Dpad.Location = new System.Drawing.Point(277, 39);
            this.image_Dpad.MaximumSize = new System.Drawing.Size(66, 66);
            this.image_Dpad.MinimumSize = new System.Drawing.Size(66, 66);
            this.image_Dpad.Name = "image_Dpad";
            this.image_Dpad.Size = new System.Drawing.Size(66, 66);
            this.image_Dpad.TabIndex = 2;
            // 
            // image_BackButton
            // 
            this.image_BackButton.AutoSize = true;
            this.image_BackButton.Image = global::RB3_X360_Keyboard.Properties.Resources.back_cropped;
            this.image_BackButton.Location = new System.Drawing.Point(430, 62);
            this.image_BackButton.MaximumSize = new System.Drawing.Size(20, 20);
            this.image_BackButton.MinimumSize = new System.Drawing.Size(20, 20);
            this.image_BackButton.Name = "image_BackButton";
            this.image_BackButton.Size = new System.Drawing.Size(20, 20);
            this.image_BackButton.TabIndex = 7;
            // 
            // image_StartButton
            // 
            this.image_StartButton.AutoSize = true;
            this.image_StartButton.Image = global::RB3_X360_Keyboard.Properties.Resources.start_cropped;
            this.image_StartButton.Location = new System.Drawing.Point(510, 62);
            this.image_StartButton.MaximumSize = new System.Drawing.Size(20, 20);
            this.image_StartButton.MinimumSize = new System.Drawing.Size(20, 20);
            this.image_StartButton.Name = "image_StartButton";
            this.image_StartButton.Size = new System.Drawing.Size(20, 20);
            this.image_StartButton.TabIndex = 9;
            // 
            // image_GuideButton
            // 
            this.image_GuideButton.AutoSize = true;
            this.image_GuideButton.Image = global::RB3_X360_Keyboard.Properties.Resources.guideLightsOff_cropped;
            this.image_GuideButton.Location = new System.Drawing.Point(460, 52);
            this.image_GuideButton.MaximumSize = new System.Drawing.Size(40, 40);
            this.image_GuideButton.MinimumSize = new System.Drawing.Size(40, 40);
            this.image_GuideButton.Name = "image_GuideButton";
            this.image_GuideButton.Size = new System.Drawing.Size(40, 40);
            this.image_GuideButton.TabIndex = 8;
            // 
            // image_BButton
            // 
            this.image_BButton.AutoSize = true;
            this.image_BButton.Image = global::RB3_X360_Keyboard.Properties.Resources.b_cropped;
            this.image_BButton.Location = new System.Drawing.Point(693, 58);
            this.image_BButton.MaximumSize = new System.Drawing.Size(28, 28);
            this.image_BButton.MinimumSize = new System.Drawing.Size(28, 28);
            this.image_BButton.Name = "image_BButton";
            this.image_BButton.Size = new System.Drawing.Size(28, 28);
            this.image_BButton.TabIndex = 12;
            // 
            // image_YButton
            // 
            this.image_YButton.AutoSize = true;
            this.image_YButton.Image = global::RB3_X360_Keyboard.Properties.Resources.y_cropped;
            this.image_YButton.Location = new System.Drawing.Point(665, 30);
            this.image_YButton.MaximumSize = new System.Drawing.Size(28, 28);
            this.image_YButton.MinimumSize = new System.Drawing.Size(28, 28);
            this.image_YButton.Name = "image_YButton";
            this.image_YButton.Size = new System.Drawing.Size(28, 28);
            this.image_YButton.TabIndex = 16;
            // 
            // image_XButton
            // 
            this.image_XButton.AutoSize = true;
            this.image_XButton.Image = global::RB3_X360_Keyboard.Properties.Resources.x_cropped;
            this.image_XButton.Location = new System.Drawing.Point(637, 58);
            this.image_XButton.MaximumSize = new System.Drawing.Size(28, 28);
            this.image_XButton.MinimumSize = new System.Drawing.Size(28, 28);
            this.image_XButton.Name = "image_XButton";
            this.image_XButton.Size = new System.Drawing.Size(28, 28);
            this.image_XButton.TabIndex = 14;
            // 
            // image_AButton
            // 
            this.image_AButton.AutoSize = true;
            this.image_AButton.Image = global::RB3_X360_Keyboard.Properties.Resources.a_cropped;
            this.image_AButton.Location = new System.Drawing.Point(665, 86);
            this.image_AButton.MaximumSize = new System.Drawing.Size(28, 28);
            this.image_AButton.MinimumSize = new System.Drawing.Size(28, 28);
            this.image_AButton.Name = "image_AButton";
            this.image_AButton.Size = new System.Drawing.Size(28, 28);
            this.image_AButton.TabIndex = 10;
            // 
            // group_Start
            // 
            group_Start.Controls.Add(this.button_Start);
            group_Start.Location = new System.Drawing.Point(671, 12);
            group_Start.Name = "group_Start";
            group_Start.Size = new System.Drawing.Size(145, 93);
            group_Start.TabIndex = 3;
            group_Start.TabStop = false;
            group_Start.Text = "Start!";
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(21, 30);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(102, 44);
            this.button_Start.TabIndex = 0;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // group_Debug
            // 
            this.group_Debug.Controls.Add(this.button_Debug);
            this.group_Debug.Location = new System.Drawing.Point(486, 12);
            this.group_Debug.Name = "group_Debug";
            this.group_Debug.Size = new System.Drawing.Size(179, 93);
            this.group_Debug.TabIndex = 4;
            this.group_Debug.TabStop = false;
            this.group_Debug.Text = "Key/Velocity Debug";
            this.group_Debug.Visible = false;
            // 
            // button_Debug
            // 
            this.button_Debug.Location = new System.Drawing.Point(21, 30);
            this.button_Debug.Name = "button_Debug";
            this.button_Debug.Size = new System.Drawing.Size(135, 44);
            this.button_Debug.TabIndex = 0;
            this.button_Debug.Text = "Debug";
            this.button_Debug.UseVisualStyleBackColor = true;
            this.button_Debug.Visible = false;
            this.button_Debug.Click += new System.EventHandler(this.button_Debug_Click);
            // 
            // timer_IOLoop
            // 
            this.timer_IOLoop.Enabled = true;
            this.timer_IOLoop.Interval = 1;
            this.timer_IOLoop.Tick += new System.EventHandler(this.timer_IOLoop_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 611);
            this.Controls.Add(this.group_Debug);
            this.Controls.Add(group_Start);
            this.Controls.Add(group_ButtonInputs);
            this.Controls.Add(group_KeyboardInputs);
            this.Controls.Add(group_OutputType);
            this.Controls.Add(group_Settings);
            this.Controls.Add(group_PedalMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(844, 620);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "RB3 X360 Keyboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.appClosing);
            group_PedalMode.ResumeLayout(false);
            group_PedalMode.PerformLayout();
            group_Settings.ResumeLayout(false);
            group_Settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Setting_Octave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Setting_Program)).EndInit();
            group_OutputType.ResumeLayout(false);
            group_OutputType.PerformLayout();
            group_KeyboardInputs.ResumeLayout(false);
            group_KeyboardInputs.PerformLayout();
            group_ButtonInputs.ResumeLayout(false);
            group_ButtonInputs.PerformLayout();
            group_Start.ResumeLayout(false);
            this.group_Debug.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private RadioButton radio_Pedal_Expression;
        private NumericUpDown numUpDown_Setting_Octave;
        private NumericUpDown numUpDown_Setting_Program;
        private RadioButton radio_Pedal_FootController;
        private CheckBox checkbox_Setting_DrumMode;
        private Button button_Start;
        private RadioButton radio_Pedal_ChannelVolume;
        private RadioButton radio_Output_Xbox360;
        private RadioButton radio_Output_Keyboard;
        private RadioButton radio_Output_Midi;
        private Label image_KeyC1;
        private Label image_KeyD1;
        private Label image_KeyE1;
        private Label image_KeyDb1;
        private Label image_KeyB1;
        private Label image_KeyA1;
        private Label image_KeyG1;
        private Label image_KeyF1;
        private Label image_KeyEb1;
        private Label image_KeyAb1;
        private Label image_KeyGb1;
        private Label image_KeyBb1;
        private Label image_KeyC3;
        private Label image_KeyBb2;
        private Label image_KeyAb2;
        private Label image_KeyGb2;
        private Label image_KeyEb2;
        private Label image_KeyDb2;
        private Label image_KeyB2;
        private Label image_KeyA2;
        private Label image_KeyG2;
        private Label image_KeyF2;
        private Label image_KeyE2;
        private Label image_KeyD2;
        private Label image_KeyC2;
        private ComboBox dropdown_Output_MidiDevice;
        private Label image_AButton;
        private Label image_XButton;
        private Label image_YButton;
        private Label image_BButton;
        private Label image_GuideButton;
        private Label image_StartButton;
        private Label image_BackButton;
        private Label image_Dpad;
        private Label image_Touchstrip;
        private Label image_OverdriveButton;
        private Label image_MidiLight4;
        private Label image_MidiLight3;
        private Label image_MidiLight2;
        private Label image_MidiLight1;
        private Label label_guideConnectionStatus;
        private Timer timer_IOLoop;
        private Label label_Setting_Octave;
        private Label label_Setting_Program;
        private Label image_Pedal;
        private Button button_Debug;
        private GroupBox group_Debug;
    }
}

