﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Linq;
using System.Xml;
using System.Collections;
using ketoansoft.app.Class.Global;

namespace ketoansoft.app
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        #region Data
        private bool Load_Server()
        {
            try
            {
                string myServer = Environment.MachineName;
                DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                if (servers.Rows.Count > 0)
                {
                    for (int i = 0; i < servers.Rows.Count; i++)
                    {
                        if (myServer == servers.Rows[i]["ServerName"].ToString())
                        {
                            cboNameServer.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);

                        }
                        else
                        {
                            cboNameServer.Items.Add(servers.Rows[i]["ServerName"]);

                        }
                    }
                    return true;
                }
                else { lblMsg.Text = "Không tìm thấy bất kỳ server nào!"; return false; }
            }
            catch { lblMsg.Text = "Lỗi kết nối! Hãy tắt và mở lại chương trình"; return false; }
        }
        private bool Load_Database()
        {
            try
            {
                if (cboNameServer.Text != "")
                {
                    cboNameDatabase.Text = "";
                    cboNameDatabase.Items.Clear();
                    using (var con = new SqlConnection("Data Source=" + cboNameServer.Text + "; Integrated Security=True;"))
                    {
                        con.Open();
                        DataTable databases = con.GetSchema("Databases");
                        if (databases != null)
                        {
                            foreach (DataRow database in databases.Rows)
                            {
                                String databaseName = database.Field<String>("database_name");
                                short dbID = database.Field<short>("dbid");
                                if (databaseName != "master" && databaseName != "tempdb" && databaseName != "model" && databaseName != "msdb")
                                    cboNameDatabase.Items.Add(databaseName);
                            }
                        }
                        con.Close();
                    }
                }
                return true;
            }
            catch { return false; }
        }
        private bool Logon(string ServerName, string DatabaseName, string UserName, string Password)
        {
            try
            {
                string connectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";Persist Security Info=True;User ID=" + UserName + ";Password=" + Password + "";
                var con = new SqlConnection(connectionString);
                con.Open();
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["ketoansoft.app.Properties.Settings.dbConnectionString"].ConnectionString = connectionString;
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("connectionStrings");
                return true;
            }
            catch { return false; }
        }
        private void ReadXML()
        {
            try
            {
                ArrayList Arr = new ArrayList();
                Arr = Unit.ReadDatabaseXML();
                string ServerName = Arr[0].ToString();
                string DatabaseName = Arr[1].ToString();
                string UserName = Arr[2].ToString();
                string Password = Arr[3].ToString();
                if (Logon(ServerName, DatabaseName, UserName, Password))
                {
                    this.Hide();
                    fTerm _form = new fTerm();
                    _form.ShowDialog();
                    this.Close();
                }
            }
            catch { MessageBox.Show("Method ReadXML() failed"); }
        }
        #endregion

        #region Event
        private void fLogin_Load(object sender, EventArgs e)
        {
            if(!Const.ISCHANGEDATABASE)
                ReadXML();
            Load_Server();
        }
        private void cboNameServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Database();
        }
        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (Logon(cboNameServer.Text, cboNameDatabase.Text, txtUser.Text, txtPassword.Text))
            {
                this.Hide();
                Unit.WriteDatabaseXML(cboNameServer.Text, cboNameDatabase.Text, txtUser.Text, txtPassword.Text);
                fTerm _form = new fTerm();
                _form.ShowDialog();
                this.Close();
            }
            else {
                MessageBox.Show("Tài khoản và mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
