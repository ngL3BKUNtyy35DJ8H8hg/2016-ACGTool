using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace HTLibrary
{
    public static class TextBoxHelper
    {
        #region KeyDown

        // Boolean flag used to determine when a character other than a number is entered. 
        public static bool nonNumberEntered = false;

        private static int _initPosition = 0; //Vị trí đầu tiên khi enter vào textbox
        private static int _selectionStart = 0; //Vị trí hiện tại khi nhập giá trị vào textbox

        private static int GetCurrPostion(object sender)
        {
            int currPos = 0;
            if (sender is TextBox)
            {
                TextBox txtNumber = ((TextBox)sender);
                currPos = txtNumber.Text.Length - txtNumber.SelectionStart;
            }
            else if (sender is RichTextBox)
            {
                RichTextBox txtNumber = ((RichTextBox)sender);
                currPos = txtNumber.Text.Length - txtNumber.SelectionStart;
            }
            return currPos;
        }
        private static void TextBoxNumber_Enter(object sender, EventArgs e)
        {
            _initPosition = GetCurrPostion(sender);
        }

        private static void TextBoxNumber_Leave(object sender, EventArgs e)
        {
            _initPosition = 0;
        }

        // Handle the KeyDown event to determine the type of character entered into the control. 
        private static void TextBoxNumber_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back)
                    {
                        _initPosition = GetCurrPostion(sender);
                    }
                    else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                        _initPosition = GetCurrPostion(sender);
                    }
                }
            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }


        // This event occurs after the KeyDown event and can be used to prevent 
        // characters from entering the control. 
        private static void TextBoxNumber_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event. 
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        // This event occurs after the KeyDown event and can be used to prevent 
        // characters from entering the control. 
        private static void TextBoxNumber_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (nonNumberEntered == false)
            {
                decimal value;
                if (sender is TextBox)
                {
                    TextBox txtNumber = ((TextBox)sender);
                    _selectionStart = txtNumber.Text.Length - _initPosition;
                    //= currPos > 0 ? txtNumber.Text.Length - 1 : currPos;
                    if (decimal.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                    {
                        //txtNumber.Text = value.ToString("###,##0", CultureInfo.InvariantCulture).TrimStart();
                        txtNumber.Text = CollectionHelper.FormatQuantityStyle(value);
                        txtNumber.Select(_selectionStart, 0);
                    }
                }
                else if (sender is RichTextBox)
                {
                    RichTextBox txtNumber = ((RichTextBox)sender);
                    _selectionStart = txtNumber.Text.Length - _initPosition;
                    //= currPos > 0 ? txtNumber.Text.Length - 1 : currPos;
                    if (decimal.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                    {
                        //txtNumber.Text = value.ToString("###,##0", CultureInfo.InvariantCulture).TrimStart();
                        txtNumber.Text = CollectionHelper.FormatQuantityStyle(value);
                        txtNumber.Select(_selectionStart, 0);
                    }
                }

            }
        }

        //Sự kiện viết hoa
        private static void TextBoxUpper_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.KeyChar = Char.Parse(e.KeyChar.ToString().ToUpper());
        }

        /// <summary>
        /// Control khi nhập giá trị chỉ cho nhập số và tự động format khi nhập
        /// </summary>
        /// <param name="controlCollect"></param>
        /// <param name="nameList"></param>
        public static void TextBox_InitNumberEvent(Control.ControlCollection controlCollect, List<string> nameList)
        {
            foreach (Control c in controlCollect)
            {
                if (c.Controls.Count > 0)
                    TextBox_InitNumberEvent(c.Controls, nameList);

                //Nếu chứa tên trùng
                if (nameList.Contains(c.Name))
                {
                    c.Enter += new EventHandler(TextBoxNumber_Enter);
                    c.KeyPress += new KeyPressEventHandler(TextBoxNumber_KeyPress);
                    c.KeyDown += new KeyEventHandler(TextBoxNumber_KeyDown);
                    c.KeyUp += new KeyEventHandler(TextBoxNumber_KeyUp);
                    c.Leave += new EventHandler(TextBoxNumber_Leave);
                }
            }
        }

        /// <summary>
        /// Các control khi nhập text vào đều ở dạng in hoa
        /// </summary>
        /// <param name="controlCollect"></param>
        public static void TextBox_InitUpperStringEvent(Control.ControlCollection controlCollect)
        {
            foreach (Control c in controlCollect)
            {
                if (c.Controls.Count > 0)
                    TextBox_InitUpperStringEvent(c.Controls);
                c.KeyPress += new KeyPressEventHandler(TextBoxUpper_KeyPress);
            }
        }
        
        

        #endregion
    }
}
