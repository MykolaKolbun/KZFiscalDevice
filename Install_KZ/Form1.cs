using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Install_KZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblDone.Visible = false;
            cbxInstallType.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            lblDone.Visible = false;
            try
            {

                if (cbxInstallType.SelectedIndex == 0)
                {
                    if (AddTextToDB())
                    {
                        checkBoxAddTextToDB.Checked = true;
                    }
                    else
                    {
                        checkBoxAddTextToDB.BackColor = Color.Red;
                    }

                    if (AddFiscalToDB())
                    {
                        checkBoxAddDevicesToDB.Checked = true;
                    }
                    else
                    {
                        checkBoxAddDevicesToDB.BackColor = Color.Red;
                    }

                    if (checkBoxAddDevicesToDB.Checked & checkBoxAddTextToDB.Checked)
                    {
                        lblDone.Visible = true;
                    }
                }
                if (cbxInstallType.SelectedIndex != 0)
                {
                    if (CopyDll())
                    {
                        checkBoxCopyFiles.Checked = true;
                    }
                    else
                    {
                        checkBoxCopyFiles.BackColor = Color.Red;
                    }
                    if (checkBoxCopyFiles.Checked)
                    {
                        lblDone.Visible = true;
                    }
                }
            }
            catch(UnauthorizedAccessException)
            {
                MessageBox.Show("Close and start the application as administrator");
            }
            catch(SqlException ex)
            {
                MessageBox.Show($"SQLError: {ex.Message}");
            }
        }

        private bool CopyDll()
        {   
            string fileName = @"KZ_ShtrikhM_FiscalDevice.dll";
            string skidataFolder = @"Skidata\Parking\OEM\FDI";
            string logFolder = @"C:\Log";
            string fiscalFolder = @"C:\FiscalFolder";
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string sourcePath = @"sources";
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFolder = System.IO.Path.Combine(targetPath, skidataFolder);
            string destFile = System.IO.Path.Combine(destFolder, fileName);
            System.IO.Directory.CreateDirectory(logFolder);
            System.IO.Directory.CreateDirectory(fiscalFolder);
            System.IO.Directory.CreateDirectory(targetPath);
            System.IO.File.Copy(sourceFile, destFile, true);
            return true;
        }

        private bool AddFiscalToDB()
        {
            string connectionString = $"Data Source = {txbServerIP.Text}; User ID = {txbSQLUser.Text}; Password = {txbSQLPass.Text}; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "INSERT INTO SDSYSFISKALDRUCKERTYP (Fiskaldruckertyp, BezTxtCode, Landescode, Typ, DLLName, ClassName) VALUES (110, 'FISKBEZ110', 997, 2, 'KZ_ShtrikhM_FiscalDevice.dll', 'KZ_ShtrikhM_FiscalDevice.APM_FiscalDevice')";
                command.ExecuteNonQuery();
                //command.CommandText = "INSERT INTO SDSYSFISKALDRUCKERTYP (Fiskaldruckertyp, BezTxtCode, Landescode, Typ, DLLName, ClassName) VALUES (111, 'FISKBEZ111', 997, 2, 'KZ_ShtrikhM_FiscalDevice.dll', 'KZ_ShtrikhM_FiscalDevice.Cash_FiscalDevice')";
                //command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        private bool CheckConnection()
        {
            string connectionString = $"Data Source = {txbServerIP.Text}; User ID = {txbSQLUser.Text}; Password = {txbSQLPass.Text}; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (SqlException e)
            {
                return false;
            }
        }
        
        private bool AddTextToDB()
        {
            string connectionString = $"Data Source = {txbServerIP.Text}; User ID = {txbSQLUser.Text}; Password = {txbSQLPass.Text}; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXTDEF] ([TxtCode], [TxtLaenge], [AnzZeilen], [IstUmschaltbar]) VALUES ('FISKBEZ110' ,25 ,1 ,0)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode] ,[Txt]) VALUES (1033, 'FISKBEZ110', 'KZ_ShtrikhM.FiscalDevice.APM')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache] ,[TxtCode], [Txt]) VALUES (1049, 'FISKBEZ110', 'KZ_ShtrikhM.FiscalDevice.APM')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (1031, 'FISKBEZ110', 'KZ_ShtrikhM.FiscalDevice.APM')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO [dbo].[SDSYSTXT] ([Sprache], [TxtCode], [Txt]) VALUES (2057, 'FISKBEZ110', 'KZ_ShtrikhM.FiscalDevice.APM')";
                connection.Close();
            }
            return true;
        }

        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            if (CheckConnection())
                lblTestConnection.Text = "Successfull";
            else
                lblTestConnection.Text = "No connecion";
        }
    }
}