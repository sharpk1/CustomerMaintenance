﻿//AUTHOR:		Kush Shah, David Daniel, Jennifer Ellefson
//COURSE:		ISYS 315: 501 (e.g., ISYS 
//			250.501)
//FORM:		Program.cs (e.g., frmHowManyYears.cs)
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerMaintenance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmCustomerMaintenance());
        }
    }
}
