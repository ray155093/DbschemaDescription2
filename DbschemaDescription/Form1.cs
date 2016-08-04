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
using NPOI.HSSF.UserModel;
using System.IO;

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
        #region Button
        /// <summary>
        /// 登入功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            sqlcommand.CommandText += "  WHERE TABLE_TYPE = 'BASE TABLE' ";
            sqlcommand.CommandText += " ORDER BY TABLE_NAME ";
            SqlDataReader reader = sqlcommand.ExecuteReader();
            DataTable readerTable = new DataTable();
            readerTable.Load(reader);
            reader.Close();

            //顯示資料庫中資料表清單
            ddlTable.Visible = true;
            List<ComboItem> tableLists = new List<ComboItem>();
            ComboItem itemSelectAll = new ComboItem();
            itemSelectAll.Text = "全選";
            itemSelectAll.Value = "all";
            tableLists.Add(itemSelectAll);
            foreach (DataRow dr in readerTable.Rows)
            {
                ComboItem otable = new ComboItem();
                otable.Value = dr["TABLE_NAME"].ToString();
                otable.Text = dr["TABLE_NAME"].ToString() + "(" + dr["TABLE_TYPE"].ToString() + ")";
                tableLists.Add(otable);
            }
            ddlTable.DataSource = tableLists;
            ddlTable.DisplayMember = "Text";
            ddlTable.ValueMember = "Value";

            btnExport.Visible = true;
            txtDbAccount.Enabled = false;
            txtDbConnect.Enabled = false;
            txtDbPassword.Enabled = false;
            txtDbTableName.Enabled = false;
        }


        /// <summary>
        /// 匯出資料庫結構
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string selectTableName = ddlTable.SelectedValue.ToString();
            string dbConnectStr = txtDbConnect.Text.Trim();
            string dbAccountStr = txtDbAccount.Text.Trim();
            string dbPasswordStr = txtDbPassword.Text.Trim();
            string dbTableNamedStr = txtDbTableName.Text.Trim();
            SqlConnection sqlconnection = new SqlConnection();
            sqlconnection.ConnectionString = "Data Source=" + dbConnectStr + ";Initial Catalog=" + dbTableNamedStr + ";User ID=" + dbAccountStr + ";Password=" + dbPasswordStr + ";";
            sqlconnection.Open();
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.Connection = sqlconnection;
            sqlcommand.CommandText = " SELECT ";
            sqlcommand.CommandText += " a.Table_name '資料表名稱'   , ";
            sqlcommand.CommandText += " b.COLUMN_NAME '欄位名稱'  ,";
            sqlcommand.CommandText += " UPPER(b.DATA_TYPE) '欄位型態',";
            sqlcommand.CommandText += "CASE(b.CHARACTER_MAXIMUM_LENGTH) WHEN 0 THEN '' ELSE b.CHARACTER_MAXIMUM_LENGTH END '欄位長度', ";
            sqlcommand.CommandText += "isnull(b.COLUMN_DEFAULT, '') as '資料預設值'    ,";
            sqlcommand.CommandText += " b.IS_NULLABLE as '是否允許空值'    ,";
            sqlcommand.CommandText += "(SELECT value FROM fn_listextendedproperty(NULL, 'schema', a.Table_schema, 'table', a.TABLE_NAME, 'column', default)       WHERE name = 'MS_Description' and objtype = 'COLUMN'        and objname Collate Chinese_Taiwan_Stroke_CI_AS = b.COLUMN_NAME) as '欄位描述' ";
            sqlcommand.CommandText += "FROM INFORMATION_SCHEMA.TABLES a ";
            sqlcommand.CommandText += "LEFT JOIN INFORMATION_SCHEMA.COLUMNS b ON a.TABLE_NAME = b.TABLE_NAME    ";
            sqlcommand.CommandText += "WHERE TABLE_TYPE='BASE TABLE'  ";
            if (selectTableName != "all")
                sqlcommand.CommandText += "AND a.TABLE_NAME='" + selectTableName + "' ";
            sqlcommand.CommandText += " ORDER BY a.TABLE_NAME , b.ORDINAL_POSITION ";
            SqlDataReader reader = sqlcommand.ExecuteReader();
            DataTable readertable = new DataTable();
            readertable.Load(reader);
            reader.Close();

            var workbook = new HSSFWorkbook();
            var sheetReportResult = workbook.CreateSheet(txtDbTableName.Text);

            //產生第一個要用CreateRow 
            sheetReportResult.CreateRow(0).CreateCell(0).SetCellValue("資料表名稱");

            //之後的用GetRow 取得在CreateCell
            sheetReportResult.GetRow(0).CreateCell(1).SetCellValue("欄位名稱");
            sheetReportResult.GetRow(0).CreateCell(2).SetCellValue("欄位型態");
            sheetReportResult.GetRow(0).CreateCell(3).SetCellValue("欄位長度");
            sheetReportResult.GetRow(0).CreateCell(4).SetCellValue("資料預設值");
            sheetReportResult.GetRow(0).CreateCell(5).SetCellValue("是否允許空值");
            sheetReportResult.GetRow(0).CreateCell(6).SetCellValue("欄位描述");

            //第一Row 已經被Title用掉了 所以從1開始
            if (readertable.Rows.Count == 0)
            {
                MessageBox.Show("沒有資料!");
                return;
            }
            for (var i = 1; i <= readertable.Rows.Count; i++)
            {
                sheetReportResult.CreateRow(i).CreateCell(0).SetCellValue(readertable.Rows[i-1]["資料表名稱"].ToString());
                sheetReportResult.GetRow(i).CreateCell(1).SetCellValue(readertable.Rows[i-1]["欄位名稱"].ToString());
                sheetReportResult.GetRow(i).CreateCell(2).SetCellValue(readertable.Rows[i-1]["欄位型態"].ToString());
                sheetReportResult.GetRow(i).CreateCell(3).SetCellValue(readertable.Rows[i-1]["欄位長度"].ToString());
                sheetReportResult.GetRow(i).CreateCell(4).SetCellValue(readertable.Rows[i-1]["資料預設值"].ToString());
                sheetReportResult.GetRow(i).CreateCell(5).SetCellValue(readertable.Rows[i-1]["是否允許空值"].ToString());
                sheetReportResult.GetRow(i).CreateCell(6).SetCellValue(readertable.Rows[i-1]["欄位描述"].ToString());

            }
            sheetReportResult.SetColumnWidth(0, 20 * 256);
            sheetReportResult.SetColumnWidth(1, 40 * 256);
            sheetReportResult.SetColumnWidth(2, 40 * 256);
            sheetReportResult.SetColumnWidth(3, 20 * 256);
            sheetReportResult.SetColumnWidth(4, 40 * 256);
            sheetReportResult.SetColumnWidth(5, 40 * 256);
            sheetReportResult.SetColumnWidth(6, 60 * 256);

            var file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + txtDbTableName.Text + "資料庫結構碼.xls", FileMode.Create);
            workbook.Write(file);
            file.Close();

        }
        #endregion

        /// <summary>
        /// ddlTable
        /// </summary>
        private class ComboItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }

    }
}
