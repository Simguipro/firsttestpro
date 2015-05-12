using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TD_Pro
{
    public partial class Sa_detail : Form
    {
        private string sn = "";
        private int idx = 0;
        sql_con sc=new sql_con();
        public Sa_detail(string sa_no,int select_index)
        {
            InitializeComponent();
            sn = sa_no;
            idx = select_index;
        }

        private void Sa_detail_Load(object sender, EventArgs e)
        {
            string sql = "";
            if (idx==1)
            {
                label1.Visible = true;
                label2.Visible = true;
                sql= @"SELECT t.billmaincode 销售订单,
dbo.fn_dep_time(t.billmaincode,(select sas.subitemname from   Sys_ApproveSubitem sas 
where sas.subitemid=l.subitemid),l.approve_date) 部门时间_小时,
case  (SELECT     Sa_OrdersMain.FlowStatus FROM         Sa_OrdersMain
WHERE     (Sa_OrdersMain.BillNo = t.billmaincode)) when '3' then '已审核' when '2' then '审核中' when '1' then '已提交' when null then '未提交' end  订单状态 ,
       l.approveattitude 审批意见,
       case l.flowstatus
         when '1' then
          '提交单据'
         when '-3' then
          '退回'
         when '2' then
          '通过'
         when '3' then
          '终审通过'
       end 状态,
       l.approve_date 提交时间,
 datename(week,l.approve_date) 当前周数,
     su.cUserName 员工姓名,(SELECT     cDepName
FROM         Sys_Dept sd where sd.idepid=suv.idepid) 部门,
(select sas.subitemname from   Sys_ApproveSubitem sas 
where sas.subitemid=l.subitemid) 审批流程
  FROM Sys_ApproveWaiteApproveList t,
       Sys_ApproveBillApproveLog   l,
       Sys_User                    su,
		Sys_UserVSDept	suv
 WHERE l.BillApproveID = t.BillApproveID
and su.iuserid=suv.iuserid
   and su.iUserID = l.approve_uid
   and t.billmaincode = '" + sn+"' order by 6 desc";
            }
            else
            {
                label1.Visible = false;
                label2.Visible = false;
                sql = @"SELECT  ep.productcode 零件编码,
case l.flowstatus when 1 then '提交审核' when 2 then '审核中' when 3 then '已审核' when -3 then '退单' end  状态 , 
t.billmaincode 零件号,

 (SELECT cUserName FROM Sys_User WHERE (iUserID = l.approve_uid)) 审批人,
l.approve_date 提交时间,
datename(ww,l.approve_date) 周数
FROM    dbo.Sys_ApproveWaiteApproveList   t ,
       dbo.Sys_ApproveBillApproveLog   l ,
       EB_ProductList ep
where  t.BillApproveID = l.BillApproveID
and ep.billno=t.billmaincode
and ep.productcode= '" + sn + "' order by 5 desc";
            }



            sc.s_ds(sql,"SAD");
            dataGridView1.DataSource = sc.ds.Tables["SAD"];
            count_longt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sc.W_Excel(dataGridView1);
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }
        private void count_longt()
        {
            if (dataGridView1[1, 0].Value.ToString() == "已审核"||dataGridView1[3, 0].Value.ToString() =="终审通过")
            {
                int c = dataGridView1.RowCount;
                int week1 = 0;
                int week2 = Convert.ToInt16(dataGridView1[5, 0].Value);
                DateTime d1 = DateTime.Now;
                DateTime d2;
                d2 = Convert.ToDateTime(dataGridView1[4, 0].Value);
                for (int i = 0; i < c; i++)
                {

                    if (dataGridView1[1, i].Value.ToString() == "提交单据")
                    {
                        d1 = Convert.ToDateTime(dataGridView1[4, i].Value);
                        week1 = Convert.ToInt16(dataGridView1[5, i].Value);
                        break;
                    }


                }
                var t = d2 - d1;
                int d = t.Days;
                int h = t.Hours;
                int m = t.Minutes;
                if (week2 - week1 > 0)
                {
                    d = d - 2;
                }
                label2.Text = (d*24 + h).ToString() + "小时" + m.ToString() + "分钟";


            }
            else
            {
                label2.Text = "未审核完！";
            }
        }
    }
}
