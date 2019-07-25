using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RandomNames
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        int days, years, mons, hours, mins, secs;
        DateTime cru, nows;
        TimeSpan daysa;
        string path = System.IO.Directory.GetCurrentDirectory();

        private void CloseBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseBox_MouseEnter(object sender, EventArgs e)
        {
            CloseBox.BackgroundImage = Image.FromFile(path + "\\pictures\\close1.png");
        }

        private void CloseBox_MouseLeave(object sender, EventArgs e)
        {
            CloseBox.BackgroundImage = Image.FromFile(path + "\\pictures\\close.png");
        }
        string timedata;
        private void TimerUp_Tick(object sender, EventArgs e)
        {
            nows = DateTime.Now;
            daysa = cru - nows;
            daysbox.Text = daysa.Days.ToString("D3");daysbox.Refresh();
            hoursbox.Text = daysa.Hours.ToString("D2"); hoursbox.Refresh();
            minsbox.Text = daysa.Minutes.ToString("D2"); minsbox.Refresh();
            secsbox.Text = daysa.Seconds.ToString("D2"); secsbox.Refresh();

        }
        int times;
        private void Form2_Load(object sender, EventArgs e)
        {
            if (File.Exists(path + "\\userdata\\time.txt"))
            {
                timedata = File.ReadAllText(path + "\\userdata\\time.txt");
                times = Int32.Parse(timedata);
                if (times / 20000000 == 0)
                {
                    MessageBox.Show("毕业时间信息格式不正确，请前往基本设置进行检查。\n请按格式设置完毕后重启软件，目前暂不可用。");
                    this.Close();
                }
                years = times / 10000;
                mons = (times % 10000) / 100;
                days = times % 100;
                nows = DateTime.Now;
                cru = new DateTime(years, mons, days, 9, 0, 0);
                if (nows >= cru)
                {
                    MessageBox.Show("毕业了！");
                    this.Close();
                }
                daysa = cru - nows;
            }
            else
            {
                File.Create(path + "\\userdata\\time.txt").Close();
                File.WriteAllText(path + "\\userdata\\time.txt", "00000000");
                MessageBox.Show("毕业时间信息缺失，请前往基本设置进行设置。\n请按格式设置完毕后重启软件，目前暂不可用。");
                this.Close();
            }
        }
    }
}
