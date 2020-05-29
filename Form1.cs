using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSA
{
    public partial class Form1 : Form
    {
        Page[] Pages;
        List<Page> LasyPages;
        public Form1()
        {
            InitializeComponent();
            Pages = new Page[10];
            for (int i = 0; i < Pages.Length; i++)
            {
                Pages[i] = new Page();
            }
            LasyPages = new List<Page>();
            Output();
        }
        private void Output()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < Pages.Length; i++)
            {
                dataGridView1.Rows.Add(new object[] { Pages[i].Index, Pages[i].R, Pages[i].M, null, null, Pages[i].time.TimeOfDay, Pages[i].tact });
            }
            dataGridView2.Rows.Clear();
            for (int i = 0; i < LasyPages.Count; i++)
            {
                dataGridView2.Rows.Add(new object[] { LasyPages[i].Index });
            }
        }
        private void dataGridView1Software_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Column3"].Index && e.RowIndex >= 0)
            {
                Pages[e.RowIndex].Mod();
                Output();
            }
            if (e.ColumnIndex == dataGridView1.Columns["ReadC"].Index && e.RowIndex >= 0)
            {
                Pages[e.RowIndex].Read();
                Output();
            }
        }

        private void dataGridView2Software_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["dataGridViewButtonColumn1"].Index && e.RowIndex >= 0)
            {
                int lasyPage = Find();
                if (Pages[lasyPage].M)
                    LasyPages.Add(Pages[lasyPage]);
                Pages[lasyPage] = (LasyPages[e.RowIndex]);
                LasyPages.RemoveAt(e.RowIndex);
                Output();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int lasyPage = Find();
            if (Pages[lasyPage].M)
            {
                Pages[lasyPage].Old();
                Pages[lasyPage].Save();
                LasyPages.Add(Pages[lasyPage]);
            }
            Pages[lasyPage] = new Page();
            Output();
        }

        private int Find()
        {
            if(button1.Text == "NRU")
            {
                for (int i = 0; i < Pages.Length; i++)
                {
                    if (Pages[i].Class == 0)
                        return i;
                }
                for (int i = 0; i < Pages.Length; i++)
                {
                    if (Pages[i].Class == 1)
                        return i;
                }
                for (int i = 0; i < Pages.Length; i++)
                {
                    if (Pages[i].Class == 2)
                        return i;
                }
                return 0;
            }
            else
            {
                for (int i = 0; i < Pages.Length; i++)
                {
                    if (Pages[i].R)
                        Pages[i].Old();
                    else
                        return i;
                }
                return 0;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (button1.Text)
            {
                case "Clock":
                    {
                        button1.Text = "NRU";
                        button3.Visible = true;
                        break;
                    }
                case "NRU":
                    {
                        button1.Text = "Clock";
                        button3.Visible = false;
                        break;
                    }
            }
        }
        private void Tact(object sender, EventArgs e)
        {
            foreach (var page in Pages)
            {
                page.Old();
            }
            Output();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
