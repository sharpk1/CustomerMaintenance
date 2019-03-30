//AUTHOR:		Kush Shah, David Daniel, Jennifer Ellefson
//COURSE:		ISYS 315: 501 (e.g., ISYS 
//			250.501)
//FORM:		frmAddModifyCustomer.cs (e.g., frmHowManyYears.cs)
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerMaintenance
{
    public partial class frmAddModifyCustomer : Form
    {
        public frmAddModifyCustomer()
        {
            InitializeComponent();
        }

        public bool blnAddCustomer;
        public Customer customer;

        /// <summary>
        /// Loads the Add/Modify Window.
        /// </summary>
        /// <returns>Loaded Combo Box</returns>
        private void frmAddModifyCustomer_Load(object sender, EventArgs e)
        {
            if (blnAddCustomer)
            {
                this.Text = "Add Customer";
                cboStates.SelectedIndex = -1;
            }
            else
            {
                this.Text = "Modify Customer";
                this.DisplayCustomer();
            }
        }


        /// <summary>
        /// Loads textboxes on form.
        /// </summary>
        /// <returns>Window</returns>
        private void DisplayCustomer()
        {
            txtFName.Text = customer.CUSTOMER_FIRST_NAME;
            txtLName.Text = customer.CUSTOMER_LAST_NAME;
            txtStreetNumber.Text = customer.CUSTOMER_STREET_NUMBER;
            txtStreetName.Text = customer.CUSTOMER_STREET_NAME;
            txtCity.Text = customer.CUSTOMER_CITY;
            cboStates.SelectedIndex = getStateIndex(customer.CUSTOMER_STATE);
            txtPhone.Text = customer.CUSTOMER_PHONE;
            txtCustomerReferral.Text = customer.CUSTOMER_REFERRED_BY;
        }

        /// <summary>
        /// Validates the inputted information and sends it to MySql.
        /// </summary>
        /// <param></param>
        /// <returns>Conslidated customer data</returns>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            //Validates the data with the IsValidData method.
            if (blnIsValidData())
            {
                if (blnAddCustomer)
                {
                    customer = new Customer();
                    this.PutCustomerData(customer);
                    try
                    {
                        customer.CUSTOMER_ID = CustomerDB.AddCustomer(customer);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    //Creates an instance of a new customer.
                    Customer newCustomer = new Customer();
                    newCustomer.CUSTOMER_ID = customer.CUSTOMER_ID;
                    this.PutCustomerData(newCustomer);
                    try
                    {
                        if (! CustomerDB.blnUpdateCustomer(customer, newCustomer))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that customer.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            customer = newCustomer;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }

        /// <summary>
        /// This validates the data.
        /// </summary>
        /// <param>All textboxes in this form</param>
        /// <returns>Recognition of valid data</returns>
        private bool blnIsValidData()
        {
            return
                Validator.blnIsPresent(txtFName) &&
                Validator.blnIsPresent(txtStreetNumber) &&
                Validator.blnIsPresent(txtStreetName) &&
                Validator.blnIsPresent(txtCity) &&
                Validator.blnIsPresent(cboStates) &&
                Validator.blnIsPresent(txtLName) &&

                Validator.blnIsPresent(txtPhone) &&

                Validator.blnIsInt32(txtPhone) &&
                //Validator.blnIsInt32(txtCustomerReferral) &&
                Validator.blnIsInt32(txtStreetNumber) &&

                Validator.IsAllLetters(txtFName) &&
                Validator.IsAllLetters(txtLName) &&
                Validator.IsAllLetters(txtStreetName) &&
                Validator.blnIsWithinRange(txtPhone, 1000000000, 9999999999) &&
                Validator.IsAllLetters(txtCity);

        }

        /// <summary>
        /// This puts customer data in the textboxes.
        /// </summary>
        /// <param></param>
        /// <returns>Filled in textboxes</returns>
        private void PutCustomerData(Customer customer)
        {
            customer.CUSTOMER_FIRST_NAME = txtFName.Text;
            customer.CUSTOMER_LAST_NAME = txtLName.Text;
            customer.CUSTOMER_STREET_NAME = txtStreetName.Text;
            customer.CUSTOMER_STREET_NUMBER = txtStreetNumber.Text;
            customer.CUSTOMER_CITY = txtCity.Text;
            customer.CUSTOMER_STATE = cboStates.SelectedItem.ToString();
            customer.CUSTOMER_PHONE = txtPhone.Text;
            customer.CUSTOMER_REFERRED_BY = txtCustomerReferral.Text;
        }

        //This is our generated list of states and their associated string value.
        public int getStateIndex(string State)
        {
            if (State == "AL") return 0;
            if (State == "AK") return 1;
            if (State == "AZ") return 2;
            if (State == "AR") return 3;
            if (State == "CA") return 4;
            if (State == "CO") return 5;
            if (State == "CT") return 6;
            if (State == "DE") return 7;
            if (State == "FL") return 8;
            if (State == "GA") return 9;
            if (State == "HI") return 10;
            if (State == "ID") return 11;
            if (State == "IL") return 12;
            if (State == "IN") return 13;
            if (State == "IA") return 14;
            if (State == "KS") return 15;
            if (State == "KY") return 16;
            if (State == "LA") return 17;
            if (State == "ME") return 18;
            if (State == "MD") return 19;
            if (State == "MA") return 20;
            if (State == "MI") return 21;
            if (State == "MN") return 22;
            if (State == "MS") return 23;
            if (State == "MO") return 24;
            if (State == "MT") return 25;
            if (State == "NE") return 26;
            if (State == "NV") return 27;
            if (State == "NH") return 28;
            if (State == "NJ") return 29;
            if (State == "NM") return 30;
            if (State == "NY") return 31;
            if (State == "NC") return 32;
            if (State == "ND") return 33;
            if (State == "OH") return 34;
            if (State == "OK") return 35;
            if (State == "OR") return 36;
            if (State == "PA") return 37;
            if (State == "RI") return 38;
            if (State == "SC") return 39;
            if (State == "SD") return 40;
            if (State == "TN") return 41;
            if (State == "TX") return 42;
            if (State == "UT") return 43;
            if (State == "VT") return 44;
            if (State == "VA") return 45;
            if (State == "WA") return 46;
            if (State == "WV") return 47;
            if (State == "WI") return 48;
            if (State == "WY") return 49;

            return 0;
        }

    }
}
