using System;
using System.Drawing;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;
using image = RB3_X360_Keyboard.Properties.Resources;

namespace Program
{
	public partial class MainWindow : Form
	{
		/// <summary>
		/// Array for the MIDI lights.
		/// </summary>
		/// <remarks>
		/// Indexing:
		/// 0 = Output enabled, 1 = Foot Controller, 2 = Channel Volume, 3 = Drum Mode
		/// </remarks>
		bool[] midiLight = new bool[4];
		/// <summary>
		/// Indicates if the MIDI lights are animating.
		/// </summary>
		bool animating = false;
		/// <summary>
		/// Indicates the animation type.
		/// </summary>
		/// <remarks>
		/// <para>0 = No animation, 1 = End animation</para>
		/// <para>1x = Increasing, 2x = Decreasing, 3x = Reset</para>
		/// <para>x0 = part 1, x1 = part 2, x2 = part 3, x3 = part 4</para>
		/// </remarks>
		int animationType = 0;


		/// <summary>
		/// Updates the values of the window controls.
		/// </summary>
		void UpdateValues()
		{
			// Toggle drum mode.
			if(checkbox_Setting_DrumMode.Enabled)
			{
				if(stateCurrent.dpadU != statePrevious.dpadU)
				{
					if(stateCurrent.dpadU)
					{
						drumMode = !drumMode;

						checkbox_Setting_DrumMode.CheckedChanged -= checkbox_SettingDrumMode_Changed;
						checkbox_Setting_DrumMode.Checked = drumMode;
						checkbox_Setting_DrumMode.CheckedChanged += checkbox_SettingDrumMode_Changed;
					}
				}
			}

			// Set the analog pedal mode.
			if(radio_Pedal_Expression.Enabled)
			{
				if(stateCurrent.dpadL != statePrevious.dpadL)
				{
					if(stateCurrent.dpadL)
					{
						pedalMode = 1;
						radio_Pedal_Expression.Checked = true;
					}
				}
			}
			if(radio_Pedal_ChannelVolume.Enabled)
			{
				if(stateCurrent.dpadD != statePrevious.dpadD)
				{
					if(stateCurrent.dpadD)
					{
						pedalMode = 2;
						radio_Pedal_ChannelVolume.Checked = true;
					}
				}
			}
			if(radio_Pedal_FootController.Enabled)
			{
				if(stateCurrent.dpadR != statePrevious.dpadR)
				{
					if(stateCurrent.dpadR)
					{
						pedalMode = 3;
						radio_Pedal_FootController.Checked = true;
					}
				}
			}

			// Switch the program number.
			if(numUpDown_Setting_Program.Enabled)
			{
				if((stateCurrent.btnA != statePrevious.btnA) || (stateCurrent.btnY != statePrevious.btnY))
				{
					if(stateCurrent.btnA && stateCurrent.btnY)
					{
						program = 0;
						PlayAnimation(3);
					}
					else
					{
						if(stateCurrent.btnA)	
						{
							program -= 1;
							PlayAnimation(2);
						}
						if(stateCurrent.btnY)	
						{
							program += 1;
							PlayAnimation(1);
						}
					}
					program = Math.Clamp((byte)program, (byte)0, (byte)127);
					numUpDown_Setting_Program.Value = program + 1;
				}
			}

			// Switch the octave number.
			if(numUpDown_Setting_Octave.Enabled)
			{
				if((stateCurrent.btnB != statePrevious.btnB) || (stateCurrent.btnX != statePrevious.btnX))
				{
					if(stateCurrent.btnB && stateCurrent.btnX)
					{
						octave = 4;
						PlayAnimation(3);
					}
					else
					{
						if(stateCurrent.btnX)
						{
							octave -= 1;
							PlayAnimation(2);
						}
						if(stateCurrent.btnB)
						{
							octave += 1;
							PlayAnimation(1);
						}
					}
					octave = Math.Clamp(octave, 0, 8);
					numUpDown_Setting_Octave.Value = octave;
				}
			}

			if(!animating)
			{
				midiLight[0] = outputStarted;
				midiLight[1] = pedalMode == 3;
				midiLight[2] = pedalMode == 2;
				midiLight[3] = drumMode;
			}

			if(midiLight[0]) image_MidiLight1.BackColor = Color.Red;
			else image_MidiLight1.BackColor = Color.Black;

			if(midiLight[1]) image_MidiLight2.BackColor = Color.Red;
			else image_MidiLight2.BackColor = Color.Black;

			if(midiLight[2]) image_MidiLight3.BackColor = Color.Red;
			else image_MidiLight3.BackColor = Color.Black;

			if(midiLight[3]) image_MidiLight4.BackColor = Color.Red;
			else image_MidiLight4.BackColor = Color.Black;
		}

		/// <summary>
		/// Plays one of the MIDI light animations.
		/// </summary>
		private void PlayAnimation(int animation)
		{
			switch(animation)
			{
				case 1:
					animationTimer.Enabled = true;
					animating = true;
					animationType = 10;
					break;

				case 2:
					animationTimer.Enabled = true;
					animating = true;
					animationType = 20;
					break;

				case 3:
					animationTimer.Enabled = true;
					animating = true;
					animationType = 30;
					break;
			}
		}

		/// <summary>
		/// Progresses through one of the MIDI light animations.
		/// </summary>
		private void animationTimer_Elapsed(Object source, ElapsedEventArgs e)
		{
			switch(animationType)
			{
				default:
					break;

				// Animation end
				// 0000
				case 1:
					midiLight[0] = false;
					midiLight[1] = false;
					midiLight[2] = false;
					midiLight[3] = false;
					animationType = 2;
					break;

				case 2:
					midiLight[0] = false;
					midiLight[1] = false;
					midiLight[2] = false;
					midiLight[3] = false;
					animationType = 0;
					animationTimer.Enabled = false;
					animating = false;
					break;

				// Increase
				// 1000
				case 10:
					midiLight[0] = true;
					midiLight[1] = false;
					midiLight[2] = false;
					midiLight[3] = false;
					animationType = 11;
					break;

				// 1100
				case 11:
					midiLight[0] = true;
					midiLight[1] = true;
					midiLight[2] = false;
					midiLight[3] = false;
					animationType = 12;
					break;

				// 1110
				case 12:
					midiLight[0] = true;
					midiLight[1] = true;
					midiLight[2] = true;
					midiLight[3] = false;
					animationType = 13;
					break;

				// 1111
				case 13:
					midiLight[0] = true;
					midiLight[1] = true;
					midiLight[2] = true;
					midiLight[3] = true;
					animationType = 14;
					break;

				// 1111
				case 14:
					midiLight[0] = true;
					midiLight[1] = true;
					midiLight[2] = true;
					midiLight[3] = true;
					animationType = 1;
					break;

				// Decrease
				// 1111
				case 20:
					midiLight[0] = true;
					midiLight[1] = true;
					midiLight[2] = true;
					midiLight[3] = true;
					animationType = 21;
					break;

				// 1110
				case 21:
					midiLight[0] = true;
					midiLight[1] = true;
					midiLight[2] = true;
					midiLight[3] = false;
					animationType = 22;
					break;

				// 1100
				case 22:
					midiLight[0] = true;
					midiLight[1] = true;
					midiLight[2] = false;
					midiLight[3] = false;
					animationType = 23;
					break;

				// 1000
				case 23:
					midiLight[0] = true;
					midiLight[1] = false;
					midiLight[2] = false;
					midiLight[3] = false;
					animationType = 24;
					break;

				// 1000
				case 24:
					midiLight[0] = true;
					midiLight[1] = false;
					midiLight[2] = false;
					midiLight[3] = false;
					animationType = 1;
					break;

				// Reset
				// 0110
				case 30:
					midiLight[0] = false;
					midiLight[1] = true;
					midiLight[2] = true;
					midiLight[3] = false;
					animationType = 31;
					break;

				// 0110
				case 31:
					midiLight[0] = false;
					midiLight[1] = true;
					midiLight[2] = true;
					midiLight[3] = false;
					animationType = 32;
					break;

				// 1001
				case 32:
					midiLight[0] = true;
					midiLight[1] = false;
					midiLight[2] = false;
					midiLight[3] = true;
					animationType = 33;
					break;

				// 1001
				case 33:
					midiLight[0] = true;
					midiLight[1] = false;
					midiLight[2] = false;
					midiLight[3] = true;
					animationType = 1;
					break;
			}
		}

		/// <summary>
		/// Updates the state of the images on the window.
		/// </summary>
		void UpdateImages()
		{
			// Update the state of the button/key images based on the current state of each button.
			// Buttons
			if(stateCurrent.btnA) image_AButton.Image = (Image)image.aPressed_cropped;
			else image_AButton.Image = (Image)image.a_cropped;

			if(stateCurrent.btnB) image_BButton.Image = (Image)image.bPressed_cropped;
			else image_BButton.Image = (Image)image.b_cropped;

			if(stateCurrent.btnX) image_XButton.Image = (Image)image.xPressed_cropped;
			else image_XButton.Image = (Image)image.x_cropped;

			if(stateCurrent.btnY) image_YButton.Image = (Image)image.yPressed_cropped;
			else image_YButton.Image = (Image)image.y_cropped;

			if(stateCurrent.btnSt) image_StartButton.Image = (Image)image.startPressed_cropped;
			else image_StartButton.Image = (Image)image.start_cropped;

			if(stateCurrent.btnBk) image_BackButton.Image = (Image)image.backPressed_cropped;
			else image_BackButton.Image = (Image)image.back_cropped;

			if(stateCurrent.overdrive) image_OverdriveButton.Image = (Image)image.overdrivePressed_cropped;
			else image_OverdriveButton.Image = (Image)image.overdrive_cropped;

			if(stateCurrent.pedalDigital) image_Pedal.Image = (Image)image.pedalPressed_small;
			else image_Pedal.Image = (Image)image.pedal_small;

			// D-pad
			switch(stateCurrent.dpadU, stateCurrent.dpadD, stateCurrent.dpadL, stateCurrent.dpadR)
			{
				// Not pressed
				default:
				case (false, false, false, false):
					image_Dpad.Image = (Image)image.dpad_cropped;
					break;

				// Up
				case (true, false, false, false):
					image_Dpad.Image = (Image)image.dpadUp_cropped;
					break;
				// Down
				case (false, true, false, false):
					image_Dpad.Image = (Image)image.dpadDown_cropped;
					break;
				// Left
				case (false, false, true, false):
					image_Dpad.Image = (Image)image.dpadLeft_cropped;
					break;
				// Right
				case (false, false, false, true):
					image_Dpad.Image = (Image)image.dpadRight_cropped;
					break;
				
				// Up + Down (impossible under normal circumstances)
				case (true, true, false, false):
					image_Dpad.Image = (Image)image.dpadUpDown_cropped;
					break;
				// Up + Left
				case (true, false, true, false):
					image_Dpad.Image = (Image)image.dpadUpLeft_cropped;
					break;
				// Up + Right
				case (true, false, false, true):
					image_Dpad.Image = (Image)image.dpadUpRight_cropped;
					break;
				// Down + Left
				case (false, true, true, false):
					image_Dpad.Image = (Image)image.dpadDownLeft_cropped;
					break;
				// Down + Right
				case (false, true, false, true):
					image_Dpad.Image = (Image)image.dpadDownRight_cropped;
					break;
				// Left + Right (impossible under normal circumstances)
				case (false, false, true, true):
					image_Dpad.Image = (Image)image.dpadDownRight_cropped;
					break;

				// Up + Down + Left (impossible under normal circumstances)
				case (true, true, true, false):
					image_Dpad.Image = (Image)image.dpadDownLeftUp_cropped;
					break;
				// Up + Down + Right (impossible under normal circumstances)
				case (true, true, false, true):
					image_Dpad.Image = (Image)image.dpadUpRightDown_cropped;
					break;
				// Up + Left + Right (impossible under normal circumstances)
				case (true, false, true, true):
					image_Dpad.Image = (Image)image.dpadUpLeftRight_cropped;
					break;
				// Down + Left + Right (impossible under normal circumstances)
				case (false, true, true, true):
					image_Dpad.Image = (Image)image.dpadDownLeftRight_cropped;
					break;

				// Up + Down + Left + Right (impossible under normal circumstances)
				case (true, true, true, true):
					image_Dpad.Image = (Image)image.dpadUpDownLeftRight_cropped;
					break;
			}

			// Guide button
			label_guideConnectionStatus.Visible = !controllerConnected;
			switch (playerIndex)
			{
				default:
				case -1:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedLightsOff_cropped;
					else image_GuideButton.Image = (Image)image.guideLightsOff_cropped;
					break;
				case 0:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer1_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer1_cropped;
					break;
				case 1:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer2_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer2_cropped;
					break;
				case 2:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer3_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer3_cropped;
					break;
				case 3:
					if(stateCurrent.btnGuide) image_GuideButton.Image = (Image)image.guidePressedPlayer4_cropped;
					else image_GuideButton.Image = (Image)image.guidePlayer4_cropped;
					break;
			}

			// Keys
			if(stateCurrent.key[0]) image_KeyC1.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyC1.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[1]) image_KeyDb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyDb1.Image = (Image)image.keyBlack_cropped.Clone();
			
			if(stateCurrent.key[2]) image_KeyD1.Image = (Image)image.keyCenterPressed_cropped.Clone();
			else image_KeyD1.Image = (Image)image.keyCenter_cropped.Clone();

			if(stateCurrent.key[3]) image_KeyEb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyEb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[4]) image_KeyE1.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyE1.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[5]) image_KeyF1.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyF1.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[6]) image_KeyGb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyGb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[7]) image_KeyG1.Image = (Image)image.keyMiddleLeftPressed_cropped.Clone();
			else image_KeyG1.Image = (Image)image.keyMiddleLeft_cropped.Clone();

			if(stateCurrent.key[8]) image_KeyAb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyAb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[9]) image_KeyA1.Image = (Image)image.keyMiddleRightPressed_cropped.Clone();
			else image_KeyA1.Image = (Image)image.keyMiddleRight_cropped.Clone();

			if(stateCurrent.key[10]) image_KeyBb1.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyBb1.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[11]) image_KeyB1.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyB1.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[12]) image_KeyC2.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyC2.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[13]) image_KeyDb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyDb2.Image = (Image)image.keyBlack_cropped.Clone();
			
			if(stateCurrent.key[14]) image_KeyD2.Image = (Image)image.keyCenterPressed_cropped.Clone();
			else image_KeyD2.Image = (Image)image.keyCenter_cropped.Clone();

			if(stateCurrent.key[15]) image_KeyEb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyEb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[16]) image_KeyE2.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyE2.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[17]) image_KeyF2.Image = (Image)image.keyLeftPressed_cropped.Clone();
			else image_KeyF2.Image = (Image)image.keyLeft_cropped.Clone();

			if(stateCurrent.key[18]) image_KeyGb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyGb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[19]) image_KeyG2.Image = (Image)image.keyMiddleLeftPressed_cropped.Clone();
			else image_KeyG2.Image = (Image)image.keyMiddleLeft_cropped.Clone();

			if(stateCurrent.key[20]) image_KeyAb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyAb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[21]) image_KeyA2.Image = (Image)image.keyMiddleRightPressed_cropped.Clone();
			else image_KeyA2.Image = (Image)image.keyMiddleRight_cropped.Clone();

			if(stateCurrent.key[22]) image_KeyBb2.Image = (Image)image.keyBlackPressed_cropped.Clone();
			else image_KeyBb2.Image = (Image)image.keyBlack_cropped.Clone();

			if(stateCurrent.key[23]) image_KeyB2.Image = (Image)image.keyRightPressed_cropped.Clone();
			else image_KeyB2.Image = (Image)image.keyRight_cropped.Clone();

			if(stateCurrent.key[24]) image_KeyC3.Image = (Image)image.keyEndPressed_cropped;
			else image_KeyC3.Image = (Image)image.keyEnd_cropped;
		}
	}
}