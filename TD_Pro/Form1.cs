using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TD_Pro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        sql_con scon=new sql_con();
        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
          
            tab_show();

        }
     
         private void Form1_Load(object sender, EventArgs e)
        {
            if (!scon.sqlconstatus())
            {
                MessageBox.Show("无法连接数据库!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
//            string find_sql = @"SELECT row_number() over(order by M_OrderMain.billdate,M_OrderMain.billno) id, M_OrderMain.billno               单据编号,
//       M_OrderMain.billdate             单据日期,
//       M_OrderMain.planbegindate        开始日期,
//       M_OrderDetail.sourcebillno       销售订单,
//       EB_ProductList.ProductCode       存货编码,
//       EB_ProductList.ProductName       存货名称,
//       EB_ProductList.ProductModel      规格,
//       M_OrderDetail.subvender          衬底厂家,
//       M_OrderDetail.suspecno           衬底规范号,
//       M_OrderDetail.epiworkjt          加工机台,
//(select 
//     datediff(dd, max(case mm.stat
//             when '提交单据' then
//              mm.subtime
//           end), max(case mm.stat
//             when '终审通过' then
//              mm.subtime
//           end)) -
// (convert (int, datename(week,max(case mm.stat
//             when '终审通过' then
//              mm.subtime
//           end)))-
//convert (int,datename(week,max(case mm.stat
//             when '提交单据' then
//              mm.subtime
//           end))) )*2
//  from (SELECT case l.flowstatus
//                 when '1' then
//                  '提交单据'
//                 when '-3' then
//                  '退回'
//                 when '2' then
//                  '通过'
//                 when '3' then
//                  '终审通过'
//               end stat,
//               l.approve_date subtime
//          FROM Sys_ApproveWaiteApproveList t, Sys_ApproveBillApproveLog l
//         WHERE l.BillApproveID = t.BillApproveID
//           and t.billmaincode = M_OrderDetail.sourcebillno ) mm ) 订单审核时间
//  FROM M_OrderDetail, M_OrderMain, EB_ProductList
// WHERE M_OrderDetail.billid = M_OrderMain.billid
//   and EB_ProductList.productid = M_OrderDetail.productid
//   and M_OrderDetail.SourceBillNo in 
//       (SELECT DISTINCT BillNo
//           FROM Sa_OrdersMain
//          WHERE (SalesDetails = '样品')
//            AND (Arrears <> '三厂')) 
//and (M_OrderDetail.SourceBillNo='" + textBox2.Text + "' or M_OrderMain.billno ='" + textBox1.Text + "' or   EB_ProductList.ProductCode ='"+textBox3.Text+"')";
//            scon.s_ds(find_sql, "FS");
//            dataGridView1.DataSource = scon.ds.Tables["FS"];

        }
       
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabControl1.SelectedIndex==0)
            {
                if (e.ColumnIndex == 8 || e.ColumnIndex == 10 )
                {
                    int l = 0;
                    string val = "";
                    if (e.ColumnIndex==8)
                    {
                        l = 2;
                        val = dataGridView1[3, e.RowIndex].Value.ToString();
                    }
                    else
                    {
                        l = 1;
                        val = dataGridView1[2, e.RowIndex].Value.ToString();
                    }
                    Sa_detail sd = new Sa_detail(val, l);
                sd.ShowDialog();
              //  MessageBox.Show(e.RowIndex.ToString() + e.ColumnIndex.ToString());
            }
                else if (e.ColumnIndex == 11)
                {
                    Sa_detail sd = new Sa_detail(dataGridView1[5, e.RowIndex].Value.ToString(), 2);
                    sd.ShowDialog();
                }
            }
            else if (tabControl1.SelectedIndex == 1 && e.ColumnIndex == 0)
            {
                //Sa_detail sd = new Sa_detail(dataGridView4[e.ColumnIndex, e.RowIndex].Value.ToString(), 1);
                //sd.ShowDialog();

            }



            else if (tabControl1.SelectedIndex == 2 && e.ColumnIndex == 0)
            {
                Sa_detail sd = new Sa_detail(dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString(), 1);
                sd.ShowDialog();
            }
            else if (tabControl1.SelectedIndex == 3 && e.ColumnIndex == 0)
            {
                Sa_detail sd = new Sa_detail(dataGridView3[e.ColumnIndex, e.RowIndex].Value.ToString(), 1);
                sd.ShowDialog();
            }

            //  
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 || e.ColumnIndex == 10 )
            {
                if (e.CellStyle != null)
                {
                    e.CellStyle.Font = new System.Drawing.Font("Arial", 8, FontStyle.Underline);
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
          //  dataGridView1.Columns[0].Width = 50;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex==0)
                {
                    scon.W_Excel(dataGridView1); 
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    //scon.W_Excel(dataGridView4);
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    scon.W_Excel(dataGridView2);
                }
                else if (tabControl1.SelectedIndex ==3)
                {
                    scon.W_Excel(dataGridView3);
                }
               
            }
            catch (Exception)
            {
                throw;
            }
       
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0 )
            {
                textBox1.Enabled = false;
                    textBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                //if (tabControl1.SelectedIndex == 1)
                //{
                //    textBox1.Enabled = false;
                //    textBox2.Enabled = false;
                //}
               
            }
           // tab_show();
        }


        public void tab_show()
        {

            int idx = tabControl1.SelectedIndex;
            string basestr = "";
            string a1, b1, c1;
            if (tabControl1.SelectedIndex == 0)
            {
                a1 = textBox1.Text;
                b1 = textBox2.Text;
                if (a1 == "")
                {
                    a1 = "ALL";
                }
                if (b1 == "")
                {
                    b1 = "ALL";
                }


                basestr = @"
                select IDdate 单据日期,
                       cSOCode  销售订单,
                       cCusName 客户名称, 
                       cInvName 存货名称,
                       man.cInvCode 存货编码,
                       cast(workday as decimal(18,2)) '订单审核时间(d)',
                       in_date 入库时间,
                       out_date 销售出库时间,
          cast( dbo.fn_work_day( out_date,so_list.max_time)/24 as decimal(18,2))  '生产时间(d)',
           cast(dbo.fn_work_day((case
                              when t5 IS null then
                               case when t4 IS null then
									case when t3 is null then 
										t2
									else t3 end
							   else t4 end
                              else
                               t5
                            end),
                            t1) / 24 as decimal(18, 2)) '零件总时间(d)',
                       p1,
                       t1,
                       p2,
                       t2,
                    cast( dbo.fn_work_day(t2,t1)/24 as decimal(18,2)) '零件审核1(d)',
                       p3,
                       t3,
                 cast(   case when t5 IS null then dbo.fn_work_day(t3,t1) else dbo.fn_work_day(t3,t2) end/24 as decimal(18,2)) '零件审核2(d)',
                       p4,
                       t4,
                  cast( case when t5 IS null then dbo.fn_work_day(t4,t3) else dbo.fn_work_day(t4,t2) end/24 as decimal(18,2)) '零件审核3(d)',
                       p5,
                       t5,
                  cast(case when t5 IS not null then dbo.fn_work_day(t5,t4) end/24 as decimal(18,2))  '零件审核4(d)',
                         so_list.aper,so_list.atime,
so_list.bper,so_list.btime,
cast(dbo.fn_work_day(so_list.btime,so_list.atime)/24 as decimal(18,2))  job_t1,
so_list.cper,so_list.ctime,
cast(dbo.fn_work_day(so_list.ctime,so_list.btime)/24 as decimal(18,2)) job_t2,
so_list.dper,so_list.dtime,
cast(dbo.fn_work_day(so_list.dtime,so_list.ctime)/24 as decimal(18,2)) job_t3,
so_list.eper,so_list.etime,
cast(dbo.fn_work_day(so_list.etime,so_list.ctime)/24 as decimal(18,2)) job_t4,
so_list.fper,so_list.ftime,
cast(dbo.fn_work_day(so_list.ftime,so_list.ctime)/24 as decimal(18,2)) job_t5,
so_list.gper,so_list.gtime,
cast(dbo.fn_work_day(so_list.gtime,so_list.ftime)/24 as decimal(18,2)) job_t6,
so_list.hper,so_list.htime,
cast(dbo.fn_work_day(so_list.htime,so_list.gtime)/24 as decimal(18,2)) job_t7,
so_list.iper,so_list.itime,
so_list.jper,so_list.jtime,
so_list.kper,so_list.ktime,
so_list.lper,so_list.ltime,
so_list.mper,so_list.mtime,
so_list.nper,so_list.ntime,
so_list.oper,so_list.otime,
so_list.pper,so_list.ptime,
so_list.qper,so_list.qtime,
so_list.rper,so_list.rtime,
so_list.sper,so_list.stime,
so_list.tper,so_list.ttime,
so_list.uper,so_list.utime,
so_list.vper,so_list.vtime,
so_list.wper,so_list.wtime,
so_list.xper,so_list.xtime,
so_list.yper,so_list.ytime,
so_list.a0per,so_list.a0time,
so_list.b0per,so_list.b0time,
so_list.c0per,so_list.c0time,
so_list.d0per,so_list.d0time
                         
                from (
                select distinct  CONVERT(varchar(30),SO_SOMain.dDate,23 ) IDdate ,
                 SO_SOMain.cSOCode ,
                 SO_SOMain.cCusName ,
                 SO_SODetails.cInvName,
                 SO_SODetails.cInvCode,
                dbo.fn_work_day(SO_SOMain.dverifysystime,SO_SOMain.dcreatesystime)/24 workday,
                (
                select  max(rdrecord10.dDate) from rdrecords10,rdrecord10          
                where rdrecords10.ID=rdrecord10.ID
                and rdrecords10.cInvCode= SO_SODetails.cInvCode
                and rdrecords10.csocode= SO_SODetails.cSOCode
                )  in_date,
                (select max(rdrecord32.dDate) from rdrecord32 ,rdrecords32
                where rdrecord32.ID=rdrecords32.id 
                and rdrecords32.cInvCode=SO_SODetails.cInvCode
                AND rdrecords32.iordercode=SO_SOMain.cSOCode) out_date
                from SO_SOMain     ,SO_SODetails      
                where   cDefine1 like '样品%'
                and SO_SOMain.cDefine2 <>'三厂'
                and SO_SOMain.cSOCode=SO_SODetails.cSOCode
                and (SO_SODetails.cInvCode='" + a1+"' or 'ALL'='"+a1+"')"+
                "and (SO_SOMain.cSOCode='"+b1+"' or 'ALL'='"+b1+"')"+
                @"and SO_SOMain.dDate>='" + dateTimePicker1.Value.ToShortDateString() + "' AND SO_SOMain.dDate<'" + dateTimePicker2.Value.ToShortDateString() + "' and  SO_SOMain.iverifystate=2 ) as man left join ( select t1.cInvCode," +
@"max(case when t.lvl=1 then t.OperatorName else null end) p1,
                MAX( case when t.lvl=1 then t.OperationDate  else null end ) t1,
                max(case when t.lvl=2 then t.OperatorName else null end )p2,
                max(case when t.lvl=2 then t.OperationDate else null end ) t2,
                max(case when t.lvl=3 then t.OperatorName else null end ) p3,
                max(case when t.lvl=3 then t.OperationDate else null end ) t3,
                max(case when  t.lvl=4 then t.OperatorName else null end) p4, 
                max(case when  t.lvl=4 then t.OperationDate else null end ) t4,
                max(case when  t.lvl=5 then t.OperatorName else null end) p5, 
                max(case when  t.lvl=5 then t.OperationDate else null end ) t5
                from (
                
                SELECT t.VoucherCode, t.OperatorName,t.OperationDate,
                ROW_NUMBER()over (partition by t.vouchercode order by t.operationdate asc ) as lvl FROM WFAudit T
                where T.VoucherCode like 'PRO%'
                and (t.OperationDate>=(select max(tmp.OperationDate) from WFAudit tmp where tmp.VoucherId=t.VoucherId and tmp.Action=8) or 
                (t.OperationDate>=(select max(tmp.OperationDate) from WFAudit tmp where tmp.VoucherId=t.VoucherId and tmp.Action=0) and (select max(tmp.OperationDate) from WFAudit tmp where tmp.VoucherId=t.VoucherId and tmp.Action=8)is null)
                )
                )  as t,
                (
                select max( AA_NewInvenApp.cNewInvenAppCode) cNewInvenAppCode, AA_NewInventory.cInvCode from AA_NewInvenApp,AA_NewInventory
                where AA_NewInventory.U870_0003_E001_PK=AA_NewInvenApp.U870_0003_E001_PK group by AA_NewInventory.cInvCode ) as t1
                where t1.cNewInvenAppCode=t.VoucherCode
                group by  t1.cInvCode
                ) as pro
                
                
                 on pro.cinvcode = man.cInvCode
                 left join 
                (
                select so_l.VoucherCode,

                MAX(case when so_l.lvl=1 then so_l.OperatorName end) aper,MAX(case when so_l.lvl=1 then so_l.OperationDate end) aTime,
                MAX(case when so_l.lvl=2 then so_l.OperatorName end) bper,MAX(case when so_l.lvl=2 then so_l.OperationDate end) bTime,
                MAX(case when so_l.lvl=3 then so_l.OperatorName end) cper,MAX(case when so_l.lvl=3 then so_l.OperationDate end) cTime,
                MAX(case when so_l.lvl=4 then so_l.OperatorName end) dper,MAX(case when so_l.lvl=4 then so_l.OperationDate end) dTime,
                MAX(case when so_l.lvl=5 then so_l.OperatorName end) eper,MAX(case when so_l.lvl=5 then so_l.OperationDate end) eTime,
                MAX(case when so_l.lvl=6 then so_l.OperatorName end) fper,MAX(case when so_l.lvl=6 then so_l.OperationDate end) fTime,
                MAX(case when so_l.lvl=7 then so_l.OperatorName end) gper,MAX(case when so_l.lvl=7 then so_l.OperationDate end) gTime,
                MAX(case when so_l.lvl=8 then so_l.OperatorName end) hper,MAX(case when so_l.lvl=8 then so_l.OperationDate end) hTime,
                MAX(case when so_l.lvl=9 then so_l.OperatorName end) iper,MAX(case when so_l.lvl=9 then so_l.OperationDate end) iTime,
                MAX(case when so_l.lvl=10 then so_l.OperatorName end) jper,MAX(case when so_l.lvl=10 then so_l.OperationDate end) jTime,
                MAX(case when so_l.lvl=11 then so_l.OperatorName end) kper,MAX(case when so_l.lvl=11 then so_l.OperationDate end) kTime,
                MAX(case when so_l.lvl=12 then so_l.OperatorName end) lper,MAX(case when so_l.lvl=12 then so_l.OperationDate end) lTime,
                MAX(case when so_l.lvl=13 then so_l.OperatorName end) mper,MAX(case when so_l.lvl=13 then so_l.OperationDate end) mTime,
                MAX(case when so_l.lvl=14 then so_l.OperatorName end) nper,MAX(case when so_l.lvl=14 then so_l.OperationDate end) nTime,
                MAX(case when so_l.lvl=15 then so_l.OperatorName end) oper,MAX(case when so_l.lvl=15 then so_l.OperationDate end) oTime,
                MAX(case when so_l.lvl=16 then so_l.OperatorName end) pper,MAX(case when so_l.lvl=16 then so_l.OperationDate end) pTime,
                MAX(case when so_l.lvl=17 then so_l.OperatorName end) qper,MAX(case when so_l.lvl=17 then so_l.OperationDate end) qTime,
                MAX(case when so_l.lvl=18 then so_l.OperatorName end) rper,MAX(case when so_l.lvl=18 then so_l.OperationDate end) rTime,
                MAX(case when so_l.lvl=19 then so_l.OperatorName end) sper,MAX(case when so_l.lvl=19 then so_l.OperationDate end) sTime,
                MAX(case when so_l.lvl=20 then so_l.OperatorName end) tper,MAX(case when so_l.lvl=20 then so_l.OperationDate end) tTime,
                MAX(case when so_l.lvl=21 then so_l.OperatorName end) uper,MAX(case when so_l.lvl=21 then so_l.OperationDate end) uTime,
                MAX(case when so_l.lvl=22 then so_l.OperatorName end) vper,MAX(case when so_l.lvl=22 then so_l.OperationDate end) vTime,
                MAX(case when so_l.lvl=23 then so_l.OperatorName end) wper,MAX(case when so_l.lvl=23 then so_l.OperationDate end) wTime,
                MAX(case when so_l.lvl=24 then so_l.OperatorName end) xper,MAX(case when so_l.lvl=24 then so_l.OperationDate end) xTime,
                MAX(case when so_l.lvl=25 then so_l.OperatorName end) yper,MAX(case when so_l.lvl=25 then so_l.OperationDate end) yTime,
                MAX(case when so_l.lvl=26 then so_l.OperatorName end) a0per,MAX(case when so_l.lvl=26 then so_l.OperationDate end) a0Time,
                MAX(case when so_l.lvl=27 then so_l.OperatorName end) b0per,MAX(case when so_l.lvl=27 then so_l.OperationDate end) b0Time,
                MAX(case when so_l.lvl=28 then so_l.OperatorName end) c0per,MAX(case when so_l.lvl=28 then so_l.OperationDate end) c0Time,
                MAX(case when so_l.lvl=29 then so_l.OperatorName end) d0per,MAX(case when so_l.lvl=29 then so_l.OperationDate end) d0Time,
                MAX(case when so_l.lvl=30 then so_l.OperatorName end) e0per,MAX(case when so_l.lvl=30 then so_l.OperationDate end) e0Time,
max(so_l.operationdate) max_time
                
                 from (
                select t.VoucherCode,t.OperationDate,t.OperatorName, ROW_NUMBER()over (partition by t.vouchercode order by t.operationdate asc )lvl from WFAudit t
                where t.VoucherCode like 'SO%'
AND T.OperationDate>=(select MAX(at.OperationDate) from WFAudit at where at.VoucherCode=t.VoucherCode and at.Action=0)
                )so_l
                group by so_l.VoucherCode)  as so_list
                 on so_list.VoucherCode=man.cSOCode
                 order by 1";




                //ProcessBar pb = new ProcessBar(Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")),
                //                               Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd")),
                //                               dataGridView1, a1, b1, c1);
                ////scon.run_pro();
                //pb.ShowDialog();

            }



            else   if (tabControl1.SelectedIndex == 1)
            {
                basestr = @" select SO_SOMain.cSOCode 销售订单, cast(dbo.fn_work_day(CURRENT_TIMESTAMP,(select MAX(t.OperationDate) from WFAudit t
where t.VoucherCode=SO_SOMain.cSOCode
and t.Action=0))/24 as decimal(18,2)) 间隔时间,
(select MAX(t.OperationDate) from WFAudit t
where t.VoucherCode=SO_SOMain.cSOCode
and t.Action=0) 提交时间
,SO_SODetails.cInvCode 存货代码,SO_SODetails.cInvName 存货名称 from SO_SOMain     ,SO_SODetails      
                where   cDefine1 like'样品%'
                and SO_SOMain.cDefine2 <>'三厂'
                and SO_SOMain.iverifystate=1
                and SO_SOMain.cSOCode=SO_SODetails.cSOCode
                and SO_SOMain.cSOCode like 'SO%'";
//@"select Sa_OrdersMain.billno 销售订单,
//Convert(decimal(18,1),(dbo.fn_get_diffdate_time(getdate(),(SELECT max(l.approve_date) FROM Sys_ApproveWaiteApproveList t, Sys_ApproveBillApproveLog l
// WHERE l.BillApproveID = t.BillApproveID
//and l.flowstatus=1
//   and t.billmaincode =Sa_OrdersMain.billno)))/24.0) 间隔时间,
//(SELECT max(l.approve_date) FROM Sys_ApproveWaiteApproveList t, Sys_ApproveBillApproveLog l
// WHERE l.BillApproveID = t.BillApproveID
//and (l.flowstatus=1 or l.flowstatus=2)
//   and t.billmaincode =Sa_OrdersMain.billno) 提交时间,
//       Sa_OrdersMain.billdate 单据日期,
//           EB_ProductList.productcode 存货代码,
//       EB_ProductList.productname 存货名称
//  from Sa_OrdersMain, EB_ProductList,  Sa_OrdersDetail
// where SalesDetails = '样品'
//AND Sa_OrdersMain.BILLID=Sa_OrdersDetail.BILLID
//   and EB_ProductList.PRODUCTid = Sa_OrdersDetail.PRODUCTid
//   and (Sa_OrdersMain.flowstatus = 2 or Sa_OrdersMain.flowstatus = 1 ) ";
                    ;
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    basestr = @"select AA_NewInvenApp.cNewInvenAppCode 零件编号,
           (select MAX(t.OperationDate) from WFAudit t
where t.VoucherCode=AA_NewInvenApp.cNewInvenAppCode
and t.Action=0 ) 提交时间,
CAST(
dbo.fn_work_day(CURRENT_TIMESTAMP,   (select MAX(t.OperationDate) from WFAudit t
where t.VoucherCode=AA_NewInvenApp.cNewInvenAppCode
and t.Action=0 ))/24 as decimal(18,2)) 间隔时间,
            AA_NewInventory.cinvcode 零件编码,AA_NewInventory.cInvName 零件名称,
            AA_NewInventory.cIDefine6 机台,
            AA_NewInventory.cInvDefine4,
            AA_NewInventory.cInvDefine5
                from AA_NewInvenApp,
            AA_NewInventory
                where AA_NewInventory.U870_0003_E001_PK=AA_NewInvenApp.U870_0003_E001_PK 
                and AA_NewInvenApp.iverifystate<>2
                and AA_NewInventory.cInvCode like 'F%'";
//@"select pl.billno 零件编号 ,pl.productcode 零件编码,
//Convert(decimal(18,1),(dbo.fn_get_diffdate_time(getdate(),(SELECT max(l.approve_date) 
//FROM Sys_ApproveWaiteApproveList t, Sys_ApproveBillApproveLog l
// WHERE l.BillApproveID = t.BillApproveID
//and l.flowstatus=1
//   and t.billmainkeyid =pl.billid)))/24.0) 间隔时间,
//(SELECT max(l.approve_date) FROM Sys_ApproveWaiteApproveList t, Sys_ApproveBillApproveLog l
// WHERE l.BillApproveID = t.BillApproveID
//and l.flowstatus=1
//   and t.billmainkeyid =pl.billid) 提交时间,
//       pl.productname 零件名称,
//       pl.elthicknesssum 厚度,
//       pl.elresistivitysum 电阻率,
//       pl.addition 掺杂,
//       pl.suspecno 衬底规范号
//  from EB_ProductList pl
// where (pl.flowstatus = 2 or pl.flowstatus=1)
//   and pl.producttypeid = 1";
                    ////////////////////////pl.flowstatus 0 未提交,1 已提交,-3 退回, 3已审核, 2 审核中
                    ///  
                }
//                else if (tabControl1.SelectedIndex == 1)
//                {
//                    basestr = @"select pl.billno 零件编号,
//convert(numeric(8,1),dbo.fn_get_diffdate_time(
//
//(select max(slog.approve_date) from Sys_ApproveBillApproveLog slog,Sys_ApproveWaiteApproveList slist
//where slog.BillApproveID = slist.BillApproveID
//and slist.billmainkeyid=pl.billid
//and slog.flowstatus=3),(select max(slog.approve_date) from Sys_ApproveBillApproveLog slog,Sys_ApproveWaiteApproveList slist
//where slog.BillApproveID = slist.BillApproveID
//and slist.billmainkeyid=pl.billid
//and slog.flowstatus=1))/24.0) 间隔时间,
//
//Convert(varchar(10),PL.billdate,120) 单据日期, 
//
//       pl.productcode 零件编码,
//       pl.productname 零件名称,
//       pl.productdm 存货代码,
//       pl.productmodel 规格型号,
//       case
//         when pl.producttypeid = 1 then
//          '外延'
//       end 存货类别,
//       pl.elthicknesssum   厚度,
//       pl.elresistivitysum 电阻率,
//       pl.addition         掺杂,
//       pl.suspecno         衬底规范号
//  from EB_ProductList pl
// where pl.flowstatus = 3 and PL.billdate>=Convert(VARCHAR(30),'" + dateTimePicker1.Value +
//                              "' ,111 ) and PL.billdate<=Convert(VARCHAR(30),'" + dateTimePicker2.Value +
//                              "' ,111 ) and pl.producttypeid = 1 order by 3 desc";

//                }


                scon.s_ds(basestr, "SHOW" + idx);

                if (tabControl1.SelectedIndex == 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = scon.ds.Tables["SHOW" + idx];
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = scon.ds.Tables["SHOW" + idx];
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    dataGridView3.DataSource = null;
                    dataGridView3.DataSource = scon.ds.Tables["SHOW" + idx];
                }
                
            
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    if (e.CellStyle != null)
            //    {
            //        e.CellStyle.Font = new System.Drawing.Font("Arial", 8, FontStyle.Underline);
            //        e.CellStyle.ForeColor = Color.Red;
            //    }
            //}
        }



    }
}
