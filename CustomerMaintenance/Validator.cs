//AUTHOR:		Kush Shah, David Daniel, Jennifer Ellefson
//COURSE:		ISYS 315: 501 (e.g., ISYS 
//			250.501)
//FORM:		Validator.cs (e.g., frmHowManyYears.cs)
//PURPOSE:		The purpose of this program is to manage the customers 
//			that fit within the database of Wildcat Pizza. Through
//			the use of the forms, there are options to change, and add customers.
//			
//			
//			
//INITIALIZE:	There msut be a connection between MySQL and Visual Studio
//			before initializing the program and in order to manipulate
//			customer data. 
//			
//INPUT:		The input includes things like textboxes, databases
//			from mySQL, and user inputs about customer information.  
//			
//			
//			
//PROCESS:		To modify the customer, the information has to be changed
//			to add a customer, the information must be inputted. 
//			
//OUTPUT:		The output of the program will be displaying the customer
//			information and having the changes reflect in MySQL.
//			
//			
//TERMINATE:	This program does not require any cleaning up after 
//			its execution of its purpose. 
//			
//			
//HONOR CODE:	“On my honor, as an Aggie, I have neither given 
//			nor received unauthorized aid on this academic 
//			work.”
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerMaintenance
{
    /// <summary>
    /// Provides static methods for validating data.
    /// </summary>
    public static class Validator
    {
        private static string strTitle = "Entry Error";

        /// <summary>
        /// The title that will appear in dialog boxes.
        /// </summary>
        public static string strTitle1
        {
            get
            {
                return strTitle;
            }
            set
            {
                strTitle = value;
            }
        }

        /// <summary>
        /// Checks whether the user entered data into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <returns>True if the user has entered data.</returns>
        public static bool blnIsPresent(Control control)
        {
            if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
            {
                TextBox txtTextBox = (TextBox)control;
                if (txtTextBox.Text == "")
                {
                    MessageBox.Show(txtTextBox.Tag + " is a required field.", strTitle1);
                    txtTextBox.Focus();
                    return false;
                }
            }
            else if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
            {
                ComboBox cboComboBox = (ComboBox)control;
                if (cboComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show(cboComboBox.Tag + " is a required field.", "Entry Error");
                    cboComboBox.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks whether the user entered a decimal value into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <returns>True if the user has entered a decimal value.</returns>
        public static bool blnIsDecimal(TextBox textBox)
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be a decimal number.", strTitle1);
                textBox.Focus();
                return false;
            }
        }

        /// <summary>
        /// Checks whether the user entered an int value into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <returns>True if the user has entered an int value.</returns>
        public static bool blnIsInt32(TextBox textBox)
        {
            try
            {
                Convert.ToInt32(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be an integer.",strTitle);
                textBox.Focus();
                return false;
            }
        }

        /// <summary>
        /// Checks whether the user entered a value within a specified range into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <param name="min">The minimum value for the range.</param>
        /// <param name="max">The maximum value for the range.</param>
        /// <returns>True if the user has entered a value within the specified range.</returns>
        public static bool blnIsWithinRange(TextBox textBox, decimal min, decimal max)
        {
            decimal decNumber = Convert.ToDecimal(textBox.Text);
            if (decNumber < min || decNumber > max)
            {
                MessageBox.Show(textBox.Tag + " must be between " + min.ToString()
                    + " and " + max.ToString() + ".", strTitle1);
                textBox.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks whether the user entered a value within a specified range into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <param name="min">The minimum value for the range.</param>
        /// <param name="max">The maximum value for the range.</param>
        /// <returns>True if the user has entered a value within the specified range.</returns>
        public static bool IsAllLetters(TextBox textbox)
        {
            string strLetter = Convert.ToString(textbox.Text);

            if (!Regex.IsMatch(strLetter, "^[a-zA-Z -]*$"))
            {
                MessageBox.Show(textbox.Tag + " must be an alpha (A-Z) term. ", strTitle);
                textbox.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsAllNumbers(TextBox textbox)
        {
            string strNumber = Convert.ToString(textbox.Text);

            if (!Regex.IsMatch(strNumber, "^[0-9]*$"))
            {
                MessageBox.Show(textbox.Tag + " must be an alphanumberic (0-9) term. ", strTitle);
                textbox.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }




    }
}