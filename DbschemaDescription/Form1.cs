using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using System.Data.SqlClient;

namespace DbschemaDescription
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 加入nlog物件
        /// </summary>
        public static Logger oLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 未處理異常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                Exception e = (Exception)args.ExceptionObject;
                oLogger.Fatal(string.Format("發生未處理的錯誤！\n\n{0}\n{1}\n", e.Message, e.StackTrace));
            }
            finally
            {
                Environment.Exit(999);
            }
        }
        public Form1()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomainUnhandledException);
            InitializeComponent();
            txtDbConnect.Text = " (localdb)\\ProjectsV13";
            txtDbAccount.Text = "ray";
            txtDbPassword.Text = "cory0209";
            txtDbTableName.Text = "Northwind";

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string dbConnectStr = txtDbConnect.Text.Trim();
            string dbAccountStr = txtDbAccount.Text.Trim();
            string dbPasswordStr = txtDbPassword.Text.Trim();
            string dbTableNamedStr = txtDbTableName.Text.Trim();
         
            #region 檢查輸入資料
            if (string.IsNullOrEmpty(dbConnectStr))
            {
                MessageBox.Show("連線位置不可為空");
                return;
            }
            if (string.IsNullOrEmpty(dbAccountStr))
            {
                MessageBox.Show("連線帳號不可為空");
                return;
            }
            if (string.IsNullOrEmpty(dbPasswordStr))
            {
                MessageBox.Show("連線密碼不可為空");
                return;
            }
            if (string.IsNullOrEmpty(dbTableNamedStr))
            {
                MessageBox.Show("連線資料庫不可為空");
                return;
            }

            #endregion
            //登入

            SqlConnection sqlconnection = new SqlConnection();
            sqlconnection.ConnectionString = "Data Source=" + dbConnectStr + ";Initial Catalog=" + dbTableNamedStr + ";User ID=" + dbAccountStr + ";Password=" + dbPasswordStr + ";";
            sqlconnection.Open();
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.Connection = sqlconnection;
            sqlcommand.CommandText = "SELECT TABLE_NAME, TABLE_TYPE  FROM INFORMATION_SCHEMA.TABLES   ";
            sqlcommand.CommandText += " ORDER BY TABLE_NAME ";
            SqlDataReader reader = sqlcommand.ExecuteReader();
            DataTable readertable = new DataTable();
            readertable.Load(reader);
            reader.Close();

        }
    }
}
