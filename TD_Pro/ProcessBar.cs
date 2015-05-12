using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TD_Pro
{
    public partial class ProcessBar : Form
    {
        private DateTime d1, d2;
        private string sbn, sn, sno;
        private int stop_tp = 0;
        private DataGridView dgview;
        public ProcessBar(DateTime getd1,DateTime getd2,DataGridView dg,string billno,string saleno,string stno)
        {
            InitializeComponent();
            d1 = getd1;
            d2 = getd2;
            sbn = billno;
            sn = saleno;
            sno = stno;
            dgview = dg;
        }
        sql_con sc=new sql_con();

        private void ProcessBar_Load(object sender, EventArgs e)
        {
           Thread t=new Thread(new ThreadStart(run_sp));
            t.Start();
        }

        private void run_sp()
        {
            if (sc.run_pro(d1, d2, sbn, sn, sno))
            {
                stop_tp = 1;
            }
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (stop_tp == 1)
            {
                dgview.DataSource = sc.ds.Tables["sh"];
                timer1.Enabled = false;
                this.Close();
            }
        }



    }
}
