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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
