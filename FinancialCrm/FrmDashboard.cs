using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialCrm.Models;
namespace FinancialCrm
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities db= new FinancialCrmDbEntities();
        int count = 0;
        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            var totalbalance = db.Banks.Sum(x => x.BankBalance);
            lblTotalBalance.Text = totalbalance.ToString()+ "$";

            var lastBankProcessAmount=db.BankProcesses.OrderByDescending(x=>x.BankProcessId).Take(1).Select(y=>y.Amount).FirstOrDefault();
            lblLastBankProcessAmount.Text=lastBankProcessAmount.ToString()+ "$";

            //char 1 kodları
            var bankdata = db.Banks.Select(x => new
            {
                x.BankTitle,
                x.BankBalance
            }).ToList();
            chart1.Series.Clear();
            var series = chart1.Series.Add("Series1");
            foreach (var item in bankdata) 
            {
                series.Points.AddXY(item.BankTitle, item.BankBalance);
            }

            //char2 Kodları
            var billData =db.Bills.Select(x=>new
            {
                x.BillTitle,
                x.BillAmount
            }).ToList();
            chart2.Series.Clear();
            var series2 = chart2.Series.Add("Faturalar");
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pyramid;
            foreach(var item in billData)
            {
                series2.Points.AddXY(item.BillTitle,item.BillAmount);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if(count % 4 == 1)
            {
                var elektrikFaturasi=db.Bills.Where(x=>x.BillTitle=="Elektrik Faturası").Select(y=>y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Elektrik faturası";
                lblBillAmount.Text= elektrikFaturasi.ToString()+"$";
            }
            if (count % 4 == 2)
            {
                var elektrikFaturasi = db.Bills.Where(x => x.BillTitle == "Doğalgaz Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Doğalgaz faturası";
                lblBillAmount.Text = elektrikFaturasi.ToString() + "$";
            }
            if (count % 4 == 3)
            {
                var elektrikFaturasi = db.Bills.Where(x => x.BillTitle == "Su Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Su faturası";
                lblBillAmount.Text = elektrikFaturasi.ToString() + "$";
            }
            if (count % 4 == 0)
            {
                var elektrikFaturasi = db.Bills.Where(x => x.BillTitle == "İnternet Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "İnternet faturası";
                lblBillAmount.Text = elektrikFaturasi.ToString() + "$";
            }
        }
    }
}
