//AUTHOR:		Kush Shah, David Daniel, Jennifer Ellefson
//COURSE:		ISYS 315: 501 (e.g., ISYS 
//			250.501)
//FORM:		frmCustomerMaintenance.cs (e.g., frmHowManyYears.cs)
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
using System.Data.SqlClient;

namespace CustomerMaintenance
{
    public partial class frmCustomerMaintenance : Form
    {
        public frmCustomerMaintenance()
        {
            InitializeComponent();
        }

        private Customer customer;

        /// <summary>
        /// This is what takes place when you click Get Customer. It pulls the information from the MySQL DB.
        /// </summary>
        /// <param>customer</param>
        /// <returns>customer information from MySql</returns>
        private void btnGetCustomer_Click(object sender, EventArgs e)
        {
            if (Validator.blnIsPresent(txtCustomerID) &&
                Validator.blnIsInt32(txtCustomerID))
            {
                int intCustomerID = Convert.ToInt32(txtCustomerID.Text);
                this.GetCustomer(intCustomerID);
                if (customer == null)
                {
                    MessageBox.Show("No customer found with this ID. " +
                         "Please try again.", "Customer Not Found");
                    this.ClearControls();
                }
                else
                    this.DisplayCustomer();
            }
        }

        /// <summary>
        /// This method is the try catch to make sure the button pulls information.
        /// </summary>
        /// <param></param>
        /// <returns>validation</returns>
        private void GetCustomer(int intCustomerID)
        {
            try
            {
                customer = CustomerDB.GetCustomer(intCustomerID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        //This enables the textboxes to be filled with information.
        private void ClearControls()
        {
            txtCustomerID.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtStreetName.Text = "";
            txtStreetNumber.Text = "";
            txtCity.Text = "";
            txtInputReferral.Text = "";
            txtState.Text = "";
            txtPhone.Text = "";
            btnModify.Enabled = false;
            txtCustomerID.Focus();
        }
        //This stipulates what information from the database fills which textboxes.
        private void DisplayCustomer()
        {
            txtFName.Text = customer.CUSTOMER_FIRST_NAME;
            txtLName.Text = customer.CUSTOMER_LAST_NAME;
            txtStreetName.Text = customer.CUSTOMER_STREET_NAME;
            txtStreetNumber.Text = customer.CUSTOMER_STREET_NUMBER;
            txtCity.Text = customer.CUSTOMER_CITY;
            txtState.Text = customer.CUSTOMER_STATE;
            txtPhone.Text = customer.CUSTOMER_PHONE;
            txtInputReferral.Text = customer.CUSTOMER_REFERRED_BY;
            btnModify.Enabled = true;

        }
        //This code adds the new customer information to the DB.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddModifyCustomer addCustomerForm = new frmAddModifyCustomer();
            addCustomerForm.blnAddCustomer = true;
            DialogResult result = addCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                customer = addCustomerForm.customer;
                txtCustomerID.Text = customer.CUSTOMER_ID.ToString();
                this.DisplayCustomer();
            }
        }
        //This code modifies the customer information in the DB.
        private void btnModify_Click(object sender, EventArgs e)
        {
            frmAddModifyCustomer modifyCustomerForm = new frmAddModifyCustomer();
            modifyCustomerForm.blnAddCustomer = false;
            modifyCustomerForm.customer = customer;
            DialogResult result = modifyCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                customer = modifyCustomerForm.customer;
                this.DisplayCustomer();
            }
            else if (result == DialogResult.Retry)
            {
                this.GetCustomer(customer.CUSTOMER_ID);
                if (customer != null)
                    this.DisplayCustomer();
                else
                    this.ClearControls();           
            }
        }
        //This code exits us from the program.
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //This button clears the information from our textboxes.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFName.Clear();
            txtLName.Clear();
            txtStreetName.Clear();
            txtStreetNumber.Clear();
            txtCity.Clear();
            txtCustomerID.Clear();
            txtPhone.Clear();
            txtState.Clear();
            txtInputReferral.Clear();
        }
    }
}
