using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TD_Pro
{
    class sql_con
    {
        static string sql_string = @"Password=P@ssw0rd;Persist Security Info=false;User ID=sa;Initial Catalog=UFDATA_001_2012;Data Source=192.168.0.21";
        //static string sql_string = @"Password=Simgui123;Persist Security Info=false;User ID=mes;Initial Catalog=simgui;Data Source=192.168.0.13";
        SqlConnection sc = new SqlConnection(sql_string);
        public DataSet ds=new DataSet();
        
        public bool sqlconstatus()
        {
            
            sc.Open();
            if (sc.State==ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool run_pro(DateTime d1,DateTime d2,string s1,string s2,string s3)
        {
            
            SqlCommand scom=new SqlCommand();
            scom.Connection = sc;
            scom.CommandType = CommandType.StoredProcedure;
            scom.CommandText = "sp_td_info";
            scom.CommandTimeout = 300;
            scom.Parameters.Add(new SqlParameter("@starttime", d1));
            scom.Parameters.Add(new SqlParameter("@endtime", d2));
            scom.Parameters.Add(new SqlParameter("@bill_no", s1));
            scom.Parameters.Add(new SqlParameter("@sale_no", s2));
            scom.Parameters.Add(new SqlParameter("@st_no", s3));
            //sc.Parameters.Add(d2);
        //SqlDataReader dr=    sc.ExecuteReader();
            SqlDataAdapter sda = new SqlDataAdapter(scom);
            if (ds.Tables["sh"]!=null)
            {
                ds.Tables["sh"].Clear();
            }
            sda.Fill(ds,"sh");

            return true;
        }










        public void W_Excel(DataGridView mydgv)
        {


            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.DefaultExt = "xls";
            savedialog.Filter = "microsoft office execl files (*.xls)|*.xls";
            savedialog.FilterIndex = 0;
            savedialog.RestoreDirectory = true;
            savedialog.Title = "导出数据到excel表格";
            savedialog.ShowDialog();
            if (savedialog.FileName.IndexOf(":") < 0) return; //被点了取消  
            //Microsoft.office.interop.excel.application xlapp = new microsoft.office.interop.excel.application();
            //    Microsoft.Office.Interop.Excel.Application xlapp=new Microsoft.Office.Interop.Excel.Application()
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            if (xlapp == null)
            {
                MessageBox.Show("可能您的机子未安装excel，无法创建excel对象！", "系统提示 ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlapp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook =
                workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet =
                (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1]; //取得sheet1  
            //定义表格内数据的行数和列数   
            int rowscount = mydgv.Rows.Count;
            int colscount = mydgv.Columns.Count;
            //行数不可以大于65536   
            if (rowscount > 65536)
            {
                MessageBox.Show("数据行记录超过65536行，不能保存！", "系统提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //列数不可以大于255   
            if (colscount > 256)
            {
                MessageBox.Show("数据列记录超过256列，不能保存！", "系统提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //写入标题
            for (int i = 0; i < mydgv.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = mydgv.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < mydgv.Rows.Count; r++)
            {
                for (int i = 0; i < mydgv.ColumnCount; i++)
                {
                    if (mydgv[i, r].ValueType == typeof(string))
                    {
                        worksheet.Cells[r + 2, i + 1] = "" + mydgv.Rows[r].Cells[i].Value; //将长数值转换成文本
                    }
                    else
                    {
                        worksheet.Cells[r + 2, i + 1] = mydgv.Rows[r].Cells[i].Value;
                    }
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit(); //列宽自适应
            if (savedialog.FileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(savedialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！..." + ex.Message, "系统提示 ", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
            //GC.Collect();//强行销毁  
            MessageBox.Show("数据导出成功！ ", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //关闭excel进程
            if (xlapp != null)
            {
                xlapp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp);
                foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                {
                    //先关闭图形窗口。如果关闭失败...有的时候在状态里看不到图形窗口的excel了，  
                    //但是在进程里仍然有EXCEL.EXE的进程存在，那么就需要杀掉它:p  
                    if (theProc.CloseMainWindow() == false)
                    {
                        theProc.Kill();
                    }
                }
                xlapp = null;
            }
        }
        public void s_ds(string sqlstring,string tb_name)
        {
            if (sc.State!=ConnectionState.Open)
            {
                sc.Open();
            }

            SqlCommand scom = new SqlCommand(sqlstring, sc);
            SqlDataAdapter sda=new SqlDataAdapter(scom);
            try
            {
                if (ds.Tables[tb_name] != null)
                {
                    ds.Tables[tb_name].Clear();
                }
                sda.Fill(ds,tb_name);
                
             //   s_ds(sqlstring, tb_name);
            }
            catch (Exception ex)
            {

                throw;
            }
           
            
        }


    }
}
