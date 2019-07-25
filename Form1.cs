using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Resources;
using System.IO;
using System.Threading.Tasks;

namespace RandomNames
{
    public partial class Main : Form
    {
        int mode;
        string modestring;
        bool working = false;
        bool sexok = false;
        string[] names;
        string[] sexs;
        bool[] sexi;
        int number;
        int u1, u2, u3, u4, u5, u6;
        bool[] avertool;
        string[] avertools;
        int usernumber;
        int lastman;
        int lastwom;
        int last;
        bool ok;
        int malenumber, femalenumber;
        Random root;
        string path = Directory.GetCurrentDirectory();
        public Main()
        {
            InitializeComponent();
        }
        private void Caculatetime()
        {
            string timedata;
            if (File.Exists(path+"\\userdata\\time.txt"))
            {
                timedata = File.ReadAllText(path+"\\userdata\\time.txt");
                int times = Int32.Parse(timedata);
                if (times / 20000000 == 0)
                {
                    TimeLast.Text = ""; TimeLast.Refresh();
                    MessageBox.Show("毕业时间信息格式不正确，请前往基本设置进行检查。\n请按格式设置完毕后重启软件，目前暂不可用。");
                    return;
                }
                int years = times / 10000;
                int months = (times % 10000) / 100;
                int days = times % 100;
                DateTime nows = DateTime.Now;
                DateTime cru = new DateTime(years, months, days, 23, 59, 59);
                if (nows >= cru)
                {
                    TimeLast.Text = ""; TimeLast.Refresh();
                    MessageBox.Show("毕业了！");
                    return;
                }
                TimeSpan daysa = cru - nows;
                TimeLast.Text = daysa.Days.ToString("D3"); TimeLast.Refresh();
            }
            else
            {
                TimeLast.Text = ""; TimeLast.Refresh();
                File.Create(path+"\\userdata\\time.txt").Close();
                File.WriteAllText(path+"\\userdata\\time.txt", "00000000");
                MessageBox.Show("毕业时间信息缺失，请前往基本设置进行设置。\n请按格式设置完毕后重启软件，目前暂不可用。");
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
            {
                switch (m.Result.ToInt32())
                {
                    case 1:
                        m.Result = new IntPtr(2);
                        break;
                }
            }
        }//用来移动窗口
        private void ReadData()
        {
            if (File.Exists(path+"\\userdata\\usernumber.txt"))
            {
                ok = int.TryParse(File.ReadAllText(path+"\\userdata\\usernumber.txt"), out usernumber);
                if (!ok)
                {
                    usernumber = 6;
                    File.WriteAllText(path+"\\userdata\\usernumber.txt", usernumber.ToString());
                }

            }
            else
            {
                File.Create(path+"\\userdata\\usernumber.txt").Close();
                usernumber = 6;
                File.WriteAllText(path+"\\userdata\\usernumber.txt", usernumber.ToString());
            }
            if (File.Exists(path+"\\userdata\\number.txt"))
            {
                ok = int.TryParse(File.ReadAllText(path+"\\userdata\\number.txt"), out number);
                if (!ok)
                {
                    number = 60;
                    File.WriteAllText(path+"\\userdata\\number.txt", number.ToString());
                }
            }
            else
            {
                MessageBox.Show("没有发现相关设置文件,已经按照60人进行默认设置！\n请前往设置");
                File.Create(path+"\\userdata\\number.txt").Close();
                File.WriteAllText(path+"\\userdata\\number.txt", "60");
                number = 60;
            }
            if (File.Exists(path+"\\userdata\\names.txt"))
            {
                names = File.ReadAllLines(path+"\\userdata\\names.txt");
                if (number != names.Length)
                {
                    number = names.Length;
                    MessageBox.Show("名单中的人数与设置不相符合！已经按照名单数量刷新总人数。\n当前人数：" + names.Length.ToString());
                    File.WriteAllText(path+"\\userdata\\number.txt", number.ToString());
                }
                //File.WriteAllText(path+"\\userdata\\names.txt","卢文杰\n郭安澜\n刘骁宇\n朱星雨\n赵佳祺\n杨   柳\n唐子蛟\n胡婷婷\n易宏祖\n胡   昆\n陈诗清\n张哲菲\n付新航\n任昊天\n明诗雅\n李   璐\n耿倩倩\n卢若凡\n袁小凡\n邱爱琳\n李子琤\n聂儒雅\n栗   林\n刘子莹\n吴非函\n雷一鸣\n孙佳丽\n薛欣瑞\n张   越\n李晶羽\n耿晓彤\n余方家\n王昱棋\n袁雨溪\n李   琪\n夏远瑾\n陈百川\n韩诗雨\n杨姝欣\n陈   宇\n徐灏媛\n王   瑞\n李凯悦\n袁婉若\n张筱昀\n高怡然\n吴柔萱\n万红乔\n黄慧阳\n丁大亮\n王玺华\n陈鑫普\n金智慧\n卢   畅\n江雨函\n朱   艺\n严培文\n尹鹏程\n王丝雨\n张舒益\n翟晨翔\n刘   玮\n黄梦宇");
            }
            if (File.Exists(path+"\\userdata\\sex.txt"))
            {
                sexs = File.ReadAllLines(path+"\\userdata\\sex.txt");
                if (sexs.Length != number)
                {
                    MessageBox.Show("性别信息设置不正确/未设置！暂时无法使用性别抽取模式");
                    sexok = false;
                }
                else
                {
                    sexi = new bool[number]; sexok = true;
                    malenumber = femalenumber = 0;
                    for (int i = 0; i < number; i++)
                    {
                        if (sexs[i] == "0") { sexi[i] = false; malenumber++; }
                        else if (sexs[i] == "1") { sexi[i] = true; femalenumber++; }
                        else
                        {
                            sexok = false;
                            MessageBox.Show("性别设置有误！请前往设置按照指引修复，修复后重启软件。\n目前性别抽取模式暂不可用");
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("未发现性别设置信息！请前往设置按照指引修复这一错误.");
                sexok = false;
                File.Create(path+"\\userdata\\sex.txt").Close();
                sexs = new string[number];
                File.WriteAllLines(path+"\\userdata\\sex.txt", sexs);
            }
            if (File.Exists(path+"\\userdata\\avertool.txt"))
            {
                avertools = File.ReadAllLines(path+"\\userdata\\avertool.txt");
                avertool = new bool[number];
                if (avertools.Length == number)
                {
                    for (int i = 0; i < number; i++)
                    {
                        if (avertools[i] == "1") avertool[i] = true;
                        else avertool[i] = false;
                    }
                }
                else
                {
                    avertool = new bool[number];
                    for (int i = 0; i < number; i++) avertool[i] = false;
                    WriteAvertool();
                }
            }
            else
            {
                avertool = new bool[number];
                for (int i = 0; i < number; i++) avertool[i] = false;
                WriteAvertool();
            }
            ShowNumber.Text = "全班共" + number.ToString() + "人";
            ShowNumber.Refresh();
            NameEditor.Text = File.ReadAllText(path+"\\userdata\\names.txt", Encoding.UTF8);
            SexEditor.Text = File.ReadAllText(path+"\\userdata\\sex.txt");
            TimeEditor.Text = File.ReadAllText(path+"\\userdata\\time.txt");
            NumberEditor.Text = File.ReadAllText(path+"\\userdata\\number.txt");
        }
        private void LoadPannelBox()
        {
            PannelBox.BackgroundImage = Image.FromFile(path+"\\pictures\\pannelbox" + usernumber.ToString() + ".png");
        }
        private void WriteAvertool()
        {
            avertools = new string[number];
            for (int i = 0; i < number; i++)
            {
                if (avertool[i]) avertools[i] = "1";
                else avertools[i] = "0";
            }
            File.WriteAllLines(path+"\\userdata\\avertool.txt", avertools);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            SettingPannel.Visible = false;
            InfoPannel.Visible = false;
            RandomPannel.Visible = true;
            u5lab.Parent = PannelBox;
            u5lab.Location = new Point(8, 22);
            u3lab.Parent = PannelBox;
            u3lab.Location = new Point(8 + 156, 22);
            u1lab.Parent = PannelBox;
            u1lab.Location = new Point(8 + 156 * 2, 22);
            u2lab.Parent = PannelBox;
            u2lab.Location = new Point(8 + 156 * 3, 22);
            u4lab.Parent = PannelBox;
            u4lab.Location = new Point(8 + 156 * 4, 22);
            u6lab.Parent = PannelBox;
            u6lab.Location = new Point(8 + 156 * 5, 22);
            NameBox5.Parent = PannelBox;
            NameBox5.Location = new Point(22, 162);
            NameBox3.Parent = PannelBox;
            NameBox3.Location = new Point(22 + 156, 162);
            NameBox1.Parent = PannelBox;
            NameBox1.Location = new Point(22 + 156 * 2, 162);
            NameBox2.Parent = PannelBox;
            NameBox2.Location = new Point(22 + 156 * 3, 162);
            NameBox4.Parent = PannelBox;
            NameBox4.Location = new Point(22 + 156 * 4, 162);
            NameBox6.Parent = PannelBox;
            NameBox6.Location = new Point(22 + 156 * 5, 162);
            NameBox1.Text = " "; NameBox1.Refresh();
            NameBox2.Text = " "; NameBox2.Refresh();
            NameBox3.Text = " "; NameBox3.Refresh();
            NameBox4.Text = " "; NameBox4.Refresh();
            NameBox5.Text = " "; NameBox5.Refresh();
            NameBox6.Text = " "; NameBox6.Refresh();
            u1lab.Text = " "; u1lab.Refresh();
            u2lab.Text = " "; u2lab.Refresh();
            u3lab.Text = " "; u3lab.Refresh();
            u4lab.Text = " "; u4lab.Refresh();
            u5lab.Text = " "; u5lab.Refresh();
            u6lab.Text = " "; u6lab.Refresh();
            working = false;
            mode = 1;
            ReadData();
            modestring = "默认模式";
            //defaultmod.BorderStyle = BorderStyle.FixedSingle;
            usernumberlab.Text = usernumber.ToString();
            usernumberlab.Refresh();
            
            defaultmod.BackgroundImage = Image.FromFile(path+"\\pictures\\default1.png");
            
            globaltips.SetToolTip(this.defaultmod, "默认模式,当前为" + modestring);
            globaltips.SetToolTip(this.malemod, "男生模式,当前为" + modestring);
            globaltips.SetToolTip(this.femalemod, "女生模式,当前为" + modestring);
            globaltips.SetToolTip(this.averangemod, "平均模式,当前为" + modestring);
            globaltips.SetToolTip(this.editmod, "基本设置,当前为" + modestring);
            globaltips.SetToolTip(this.infomod, "软件信息,当前为" + modestring);
            globaltips.SetToolTip(this.closebox, "安全处理点名数据并退出");
            globaltips.SetToolTip(this.minbox, "最小化");
            globaltips.SetToolTip(this.clickbox, "单击查看详细的高考倒计时");
            globaltips.SetToolTip(this.TimeLast, "单击查看详细的高考倒计时");
            Caculatetime();
            LoadPannelBox();
        }//加载窗口时要做的事情
        private void Defaultmod_Click(object sender, EventArgs e)
        {
            RandomPannel.Visible = true;
            SettingPannel.Visible = false;
            InfoPannel.Visible = false;
            mode = 1;
            modestring = "默认模式"; Goback();
            //defaultmod.BorderStyle = BorderStyle.FixedSingle;
            defaultmod.BackgroundImage = Image.FromFile(path+"\\pictures\\default1.png");
        }//点击按钮的切换效果
        private void Malemod_Click(object sender, EventArgs e)
        {
            if (sexok == false)
            {
                MessageBox.Show("性别点名方式不可用！请设置正确性别名单后再试");
                return;
            }
            RandomPannel.Visible = true;
            SettingPannel.Visible = false;
            InfoPannel.Visible = false;
            mode = 2;
            modestring = "男生模式"; Goback();
            //malemod.BorderStyle = BorderStyle.FixedSingle;
            malemod.BackgroundImage = Image.FromFile(path+"\\pictures\\male1.png");
        }//同上
        private void Femalemod_Click(object sender, EventArgs e)
        {
            if (sexok == false)
            {
                MessageBox.Show("性别点名方式不可用！请设置正确性别名单后再试");
                return;
            }
            RandomPannel.Visible = true;
            SettingPannel.Visible = false;
            InfoPannel.Visible = false;
            mode = 3;
            modestring = "女生模式"; Goback();
            //femalemod.BorderStyle = BorderStyle.FixedSingle;
            femalemod.BackgroundImage = Image.FromFile(path+"\\pictures\\female1.png");
        }
        private void Averangemod_Click(object sender, EventArgs e)
        {
            if (sexok == false)
            {
                MessageBox.Show("性别点名方式不可用！请设置正确性别名单后再试");
                return;
            }
            if (usernumber == 1)
            {
                MessageBox.Show("平均模式要求人数在两人以上。");
                return;
            }
            RandomPannel.Visible = true;
            SettingPannel.Visible = false;
            InfoPannel.Visible = false;
            mode = 4;
            modestring = "平均模式"; Goback();
            //averangemod.BorderStyle = BorderStyle.FixedSingle;
            averangemod.BackgroundImage = Image.FromFile(path+"\\pictures\\averange1.png");
        }
        private void Editmod_Click(object sender, EventArgs e)
        {
            mode = 5;
            RandomPannel.Visible = false;
            SettingPannel.Visible = true;
            InfoPannel.Visible = false;
            modestring = "基本设置"; Goback();
            //editmod.BorderStyle = BorderStyle.FixedSingle;
            editmod.BackgroundImage = Image.FromFile(path+"\\pictures\\edit1.png");
        }
        private void Infomod_Click(object sender, EventArgs e)
        {
            RandomPannel.Visible = false;
            SettingPannel.Visible = false;
            InfoPannel.Visible = true;
            mode = 6;
            modestring = "软件信息"; Goback();
            //infomod.BorderStyle = BorderStyle.FixedSingle;
            infomod.BackgroundImage = Image.FromFile(path+"\\pictures\\info1.png");
        }
        private void Goback()
        {
            //defaultmod.BorderStyle = BorderStyle.None;
            defaultmod.BackgroundImage = Image.FromFile(path+"\\pictures\\default.png");
            //defaultmod.BackColor = Color.Transparent;
            //malemod.BorderStyle = BorderStyle.None;
            malemod.BackgroundImage = Image.FromFile(path+"\\pictures\\male.png");
            //malemod.BackColor = Color.Transparent;
            //femalemod.BorderStyle = BorderStyle.None;
            femalemod.BackgroundImage = Image.FromFile(path+"\\pictures\\female.png");
            //femalemod.BackColor = Color.Transparent;
            //averangemod.BorderStyle = BorderStyle.None;
            averangemod.BackgroundImage = Image.FromFile(path+"\\pictures\\averange.png");
            //averangemod.BackColor = Color.Transparent;
            //editmod.BorderStyle = BorderStyle.None;
            editmod.BackgroundImage = Image.FromFile(path+"\\pictures\\edit.png");
            //editmod.BackColor = Color.Transparent;
            //infomod.BorderStyle = BorderStyle.None;
            infomod.BackgroundImage = Image.FromFile(path+"\\pictures\\info.png");
            //infomod.BackColor = Color.Transparent;
            globaltips.SetToolTip(this.defaultmod, "默认模式,当前为" + modestring);
            globaltips.SetToolTip(this.malemod, "男生模式,当前为" + modestring);
            globaltips.SetToolTip(this.femalemod, "女生模式,当前为" + modestring);
            globaltips.SetToolTip(this.averangemod, "平均模式,当前为" + modestring);
            globaltips.SetToolTip(this.editmod, "基本设置,当前为" + modestring);
            globaltips.SetToolTip(this.infomod, "软件信息,当前为" + modestring);
        }//用来回到最初状态，避免模式交叉
        private void Closebox_Click(object sender, EventArgs e)
        {
            WriteAvertool();
            File.WriteAllText(path+"\\userdata\\usernumber.txt", usernumber.ToString());
            this.Close();
        }//关窗口
        private void Closebox_MouseEnter(object sender, EventArgs e)
        {
            closebox.BackgroundImage = Image.FromFile(path+"\\pictures\\close1.png");
        }
        private void Closebox_MouseLeave(object sender, EventArgs e)
        {
            closebox.BackgroundImage = Image.FromFile(path+"\\pictures\\close.png");
        }
        private void Minbox_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }//最小化
        private void Minbox_MouseEnter(object sender, EventArgs e)
        {
            minbox.BackgroundImage = Image.FromFile(path+"\\pictures\\minbox1.png");
        }
        private void Minbox_MouseLeave(object sender, EventArgs e)
        {
            minbox.BackgroundImage = Image.FromFile(path+"\\pictures\\minbox.png");
        }
        private void IN1(object sender, EventArgs e)
        {
            defaultmod.BackgroundImage = Image.FromFile(path+"\\pictures\\default1.png");
        }//鼠标移入的动画
        private void IN2(object sender, EventArgs e)
        {
            malemod.BackgroundImage = Image.FromFile(path+"\\pictures\\male1.png");
        }
        private void IN3(object sender, EventArgs e)
        {
            femalemod.BackgroundImage = Image.FromFile(path+"\\pictures\\female1.png");
        }
        private void IN4(object sender, EventArgs e)
        {
            averangemod.BackgroundImage = Image.FromFile(path+"\\pictures\\averange1.png");
        }
        private void IN5(object sender, EventArgs e)
        {
            editmod.BackgroundImage = Image.FromFile(path+"\\pictures\\edit1.png");
        }
        private void IN6(object sender, EventArgs e)
        {
            infomod.BackgroundImage = Image.FromFile(path+"\\pictures\\info1.png");
        }
        private void OUT1(object sender, EventArgs e)
        {
            if (mode != 1)
                defaultmod.BackgroundImage = Image.FromFile(path+"\\pictures\\default.png");
        }//鼠标移出的动画
        private void OUT2(object sender, EventArgs e)
        {
            if (mode != 2)
                malemod.BackgroundImage = Image.FromFile(path+"\\pictures\\male.png");
        }
        private void OUT3(object sender, EventArgs e)
        {
            if (mode != 3)
                femalemod.BackgroundImage = Image.FromFile(path+"\\pictures\\female.png");
        }
        private void OUT4(object sender, EventArgs e)
        {
            if (mode != 4)
                averangemod.BackgroundImage = Image.FromFile(path+"\\pictures\\averange.png");
        }
        private void OUT5(object sender, EventArgs e)
        {
            if (mode != 5)
                editmod.BackgroundImage = Image.FromFile(path+"\\pictures\\edit.png");
        }
        private void OUT6(object sender, EventArgs e)
        {
            if (mode != 6)
                infomod.BackgroundImage = Image.FromFile(path+"\\pictures\\info.png");
        }//好吧这算不上动画        
        private void MainMode_MouseEnter(object sender, EventArgs e)
        {
            MainMode.BackgroundImage = Image.FromFile(path+"\\pictures\\windowmod1.png");
        }
        private void MainMode_MouseLeave(object sender, EventArgs e)
        {
            MainMode.BackgroundImage = Image.FromFile(path+"\\pictures\\windowmod.png");
        }
        private void MainMode_Click(object sender, EventArgs e)
        {
            MessageBox.Show("智能模式开发中，即将推出。");
        }
        private void StartButton_MouseEnter(object sender, EventArgs e)
        {
            StartButton.BackgroundImage = Image.FromFile(path+"\\pictures\\start1.png");
        }
        private void StartButton_MouseLeave(object sender, EventArgs e)
        {
            if (working == false)
                StartButton.BackgroundImage = Image.FromFile(path+"\\pictures\\start.png");
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            RandomNameFuc();
        }
        private void Playing()
        {
            root = new Random();
            for (int i = 0; i < 20; i++)
            {
                u1 = root.Next(0, number);
                if (usernumber > 1) u2 = root.Next(0, number);
                if (usernumber >= 4) u3 = root.Next(0, number);
                if (usernumber >= 4) u4 = root.Next(0, number);
                if (usernumber >= 6) u5 = root.Next(0, number);
                if (usernumber >= 6) u6 = root.Next(0, number);
                u1lab.Text = (u1 + 1).ToString("D2"); u1lab.Refresh();
                if (usernumber > 1) { u2lab.Text = (u2 + 1).ToString("D2"); u2lab.Refresh(); }
                if (usernumber >= 4) { u3lab.Text = (u3 + 1).ToString("D2"); u3lab.Refresh(); }
                if (usernumber >= 4) { u4lab.Text = (u4 + 1).ToString("D2"); u4lab.Refresh(); }
                if (usernumber >= 6) { u5lab.Text = (u5 + 1).ToString("D2"); u5lab.Refresh(); }
                if (usernumber >= 6) { u6lab.Text = (u6 + 1).ToString("D2"); u6lab.Refresh(); }
                Thread.Sleep(20);
            }
        }

        private void Leftbox_MouseEnter(object sender, EventArgs e)
        {
            leftbox.BackgroundImage = Image.FromFile(path+"\\pictures\\left1.png");
        }

        private void Leftbox_MouseLeave(object sender, EventArgs e)
        {
            leftbox.BackgroundImage = Image.FromFile(path+"\\pictures\\left.png");
        }

        private void Rightbox_MouseEnter(object sender, EventArgs e)
        {
            rightbox.BackgroundImage = Image.FromFile(path+"\\pictures\\right1.png");
        }

        private void Rightbox_MouseLeave(object sender, EventArgs e)
        {
            rightbox.BackgroundImage = Image.FromFile(path+"\\pictures\\right.png");
        }

        private void Leftbox_Click(object sender, EventArgs e)
        {
            if (usernumber == 2 && mode == 4)
            {
                MessageBox.Show("平均模式要求人数在两人以上。");
                return;
            }
            if (usernumber == 6) usernumber = 4;
            else if (usernumber == 4) usernumber = 2;
            else if (usernumber == 2) usernumber = 1;
            usernumberlab.Text = usernumber.ToString();
            usernumberlab.Refresh();
            PannelBox.BackgroundImage = Image.FromFile(path+"\\pictures\\pannelbox" + usernumber.ToString() + ".png");
            PannelBox.Refresh();
        }

        private void Rightbox_Click(object sender, EventArgs e)
        {
            if (usernumber == 4) usernumber = 6;
            else if (usernumber == 2) usernumber = 4;
            else if (usernumber == 1) usernumber = 2;
            usernumberlab.Text = usernumber.ToString();
            usernumberlab.Refresh();
            PannelBox.BackgroundImage = Image.FromFile(path+"\\pictures\\pannelbox" + usernumber.ToString() + ".png");
            PannelBox.Refresh();
        }

        private void SexEditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText(path+"\\userdata\\names.txt", NameEditor.Text);
            File.WriteAllText(path+"\\userdata\\sex.txt", SexEditor.Text);
            File.WriteAllText(path+"\\userdata\\time.txt", TimeEditor.Text);
            File.WriteAllText(path+"\\userdata\\number.txt", NumberEditor.Text);
            ReadData();
            Caculatetime();
        }

        private void Clickbox_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void TimeLast_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void RandomNameFuc()
        {
            NameBox1.Text = " "; NameBox1.Refresh();
            NameBox2.Text = " "; NameBox2.Refresh();
            NameBox3.Text = " "; NameBox3.Refresh();
            NameBox4.Text = " "; NameBox4.Refresh();
            NameBox5.Text = " "; NameBox5.Refresh();
            NameBox6.Text = " "; NameBox6.Refresh();
            u1lab.Text = " "; u1lab.Refresh();
            u2lab.Text = " "; u2lab.Refresh();
            u3lab.Text = " "; u3lab.Refresh();
            u4lab.Text = " "; u4lab.Refresh();
            u5lab.Text = " "; u5lab.Refresh();
            u6lab.Text = " "; u6lab.Refresh();

            working = true;
            StartButton.BackgroundImage = Image.FromFile(path+"\\pictures\\start2.png");
            StartButton.Refresh();
            Playing();
            u1 = u2 = u3 = u4 = u5 = u6 = number + 1;
            switch (mode)
            {
                case 1:
                    {
                        if (usernumber > number)
                        {
                            MessageBox.Show("点名人数多于总人数。");
                            break;
                        }
                        LastPer();
                        if (last < usernumber)
                        {
                            ClearTools();
                        }
                        root = new Random(DateTime.Now.Millisecond);
                        u1 = root.Next(0, number);
                        if (usernumber >= 2) u2 = root.Next(0, number);
                        if (usernumber >= 4) { u3 = root.Next(0, number); u4 = root.Next(0, number); }
                        if (usernumber >= 6) { u5 = root.Next(0, number); u6 = root.Next(0, number); }
                        while (avertool[u1]) u1 = root.Next(0, number);
                        while (usernumber >= 2 && (u2 == u1 || avertool[u2])) u2 = root.Next(0, number);
                        while (usernumber >= 4 && (u3 == u1 || u3 == u2 || avertool[u3])) u3 = root.Next(0, number);
                        while (usernumber >= 4 && (u4 == u1 || u4 == u2 || u4 == u3 || avertool[u4])) u4 = root.Next(0, number);
                        while (usernumber >= 6 && (u5 == u1 || u5 == u2 || u5 == u3 || u5 == u4 || avertool[u5])) u5 = root.Next(0, number);
                        while (usernumber >= 6 && (u6 == u1 || u6 == u2 || u6 == u3 || u6 == u4 || u6 == u5 || avertool[u6])) u6 = root.Next(0, number);
                        avertool[u1] = true;
                        if (usernumber > 1) avertool[u2] = true;
                        if (usernumber >= 4) avertool[u3] = true;
                        if (usernumber >= 4) avertool[u4] = true;
                        if (usernumber >= 6) avertool[u5] = true;
                        if (usernumber >= 6) avertool[u6] = true;
                        u1lab.Text = (u1 + 1).ToString("D2"); u1lab.Refresh(); NameBox1.Text = names[u1]; NameBox1.Refresh();
                        if (usernumber > 1) { u2lab.Text = (u2 + 1).ToString("D2"); u2lab.Refresh(); NameBox2.Text = names[u2]; NameBox2.Refresh(); }
                        if (usernumber >= 4) { u3lab.Text = (u3 + 1).ToString("D2"); u3lab.Refresh(); NameBox3.Text = names[u3]; NameBox3.Refresh(); }
                        if (usernumber >= 4) { u4lab.Text = (u4 + 1).ToString("D2"); u4lab.Refresh(); NameBox4.Text = names[u4]; NameBox4.Refresh(); }
                        if (usernumber >= 6) { u5lab.Text = (u5 + 1).ToString("D2"); u5lab.Refresh(); NameBox5.Text = names[u5]; NameBox5.Refresh(); }
                        if (usernumber >= 6) { u6lab.Text = (u6 + 1).ToString("D2"); u6lab.Refresh(); NameBox6.Text = names[u6]; NameBox6.Refresh(); }
                        break;
                    }
                case 2:
                    {
                        if (usernumber > malenumber)
                        {
                            MessageBox.Show("点名人数多于总人数。");
                            break;
                        }
                        LastMan();
                        if (lastman < usernumber)
                        {
                            ClearTools();
                        }
                        root = new Random(DateTime.Now.Millisecond);
                        u1 = root.Next(0, number);
                        if (usernumber >= 2) u2 = root.Next(0, number);
                        if (usernumber >= 4) { u3 = root.Next(0, number); u4 = root.Next(0, number); }
                        if (usernumber >= 6) { u5 = root.Next(0, number); u6 = root.Next(0, number); }
                        while (avertool[u1] || sexi[u1]) u1 = root.Next(0, number);
                        while (usernumber >= 2 && (u2 == u1 || avertool[u2] || sexi[u2])) u2 = root.Next(0, number);
                        while (usernumber >= 4 && (u3 == u1 || u3 == u2 || avertool[u3] || sexi[u3])) u3 = root.Next(0, number);
                        while (usernumber >= 4 && (u4 == u1 || u4 == u2 || u4 == u3 || avertool[u4] || sexi[u4])) u4 = root.Next(0, number);
                        while (usernumber >= 6 && (u5 == u1 || u5 == u2 || u5 == u3 || u5 == u4 || avertool[u5] || sexi[u5])) u5 = root.Next(0, number);
                        while (usernumber >= 6 && (u6 == u1 || u6 == u2 || u6 == u3 || u6 == u4 || u6 == u5 || avertool[u6] || sexi[u6])) u6 = root.Next(0, number);
                        avertool[u1] = true;
                        if (usernumber > 1) avertool[u2] = true;
                        if (usernumber >= 4) avertool[u3] = true;
                        if (usernumber >= 4) avertool[u4] = true;
                        if (usernumber >= 6) avertool[u5] = true;
                        if (usernumber >= 6) avertool[u6] = true;
                        u1lab.Text = (u1 + 1).ToString("D2"); u1lab.Refresh(); NameBox1.Text = names[u1]; NameBox1.Refresh();
                        if (usernumber > 1) { u2lab.Text = (u2 + 1).ToString("D2"); u2lab.Refresh(); NameBox2.Text = names[u2]; NameBox2.Refresh(); }
                        if (usernumber >= 4) { u3lab.Text = (u3 + 1).ToString("D2"); u3lab.Refresh(); NameBox3.Text = names[u3]; NameBox3.Refresh(); }
                        if (usernumber >= 4) { u4lab.Text = (u4 + 1).ToString("D2"); u4lab.Refresh(); NameBox4.Text = names[u4]; NameBox4.Refresh(); }
                        if (usernumber >= 6) { u5lab.Text = (u5 + 1).ToString("D2"); u5lab.Refresh(); NameBox5.Text = names[u5]; NameBox5.Refresh(); }
                        if (usernumber >= 6) { u6lab.Text = (u6 + 1).ToString("D2"); u6lab.Refresh(); NameBox6.Text = names[u6]; NameBox6.Refresh(); }
                        break;
                    }
                case 3:
                    {
                        if (usernumber > femalenumber)
                        {
                            MessageBox.Show("点名人数多于总人数。");
                            break;
                        }
                        LastWom();
                        if (lastwom < usernumber)
                        {
                            ClearTools();
                        }
                        root = new Random(DateTime.Now.Millisecond);
                        u1 = root.Next(0, number);
                        if (usernumber >= 2) u2 = root.Next(0, number);
                        if (usernumber >= 4) { u3 = root.Next(0, number); u4 = root.Next(0, number); }
                        if (usernumber >= 6) { u5 = root.Next(0, number); u6 = root.Next(0, number); }
                        while (avertool[u1] || !sexi[u1]) u1 = root.Next(0, number);
                        while (usernumber >= 2 && (u2 == u1 || avertool[u2] || !sexi[u2])) u2 = root.Next(0, number);
                        while (usernumber >= 4 && (u3 == u1 || u3 == u2 || avertool[u3] || !sexi[u3])) u3 = root.Next(0, number);
                        while (usernumber >= 4 && (u4 == u1 || u4 == u2 || u4 == u3 || avertool[u4] || !sexi[u4])) u4 = root.Next(0, number);
                        while (usernumber >= 6 && (u5 == u1 || u5 == u2 || u5 == u3 || u5 == u4 || avertool[u5] || !sexi[u5])) u5 = root.Next(0, number);
                        while (usernumber >= 6 && (u6 == u1 || u6 == u2 || u6 == u3 || u6 == u4 || u6 == u5 || avertool[u6] || !sexi[u6])) u6 = root.Next(0, number);
                        avertool[u1] = true;
                        if (usernumber > 1) avertool[u2] = true;
                        if (usernumber >= 4) avertool[u3] = true;
                        if (usernumber >= 4) avertool[u4] = true;
                        if (usernumber >= 6) avertool[u5] = true;
                        if (usernumber >= 6) avertool[u6] = true;
                        u1lab.Text = (u1 + 1).ToString("D2"); u1lab.Refresh(); NameBox1.Text = names[u1]; NameBox1.Refresh();
                        if (usernumber > 1) { u2lab.Text = (u2 + 1).ToString("D2"); u2lab.Refresh(); NameBox2.Text = names[u2]; NameBox2.Refresh(); }
                        if (usernumber >= 4) { u3lab.Text = (u3 + 1).ToString("D2"); u3lab.Refresh(); NameBox3.Text = names[u3]; NameBox3.Refresh(); }
                        if (usernumber >= 4) { u4lab.Text = (u4 + 1).ToString("D2"); u4lab.Refresh(); NameBox4.Text = names[u4]; NameBox4.Refresh(); }
                        if (usernumber >= 6) { u5lab.Text = (u5 + 1).ToString("D2"); u5lab.Refresh(); NameBox5.Text = names[u5]; NameBox5.Refresh(); }
                        if (usernumber >= 6) { u6lab.Text = (u6 + 1).ToString("D2"); u6lab.Refresh(); NameBox6.Text = names[u6]; NameBox6.Refresh(); }
                        break;
                    }
                case 4:
                    {
                        if (usernumber / 2 > malenumber || usernumber / 2 > femalenumber)
                        {
                            MessageBox.Show("点名人数多于总人数。");
                            break;
                        }
                        LastMan();LastWom();
                        if (lastman < usernumber/2||lastwom<usernumber/2)
                        {
                            ClearTools();
                        }
                        root = new Random(DateTime.Now.Millisecond);
                        u1 = root.Next(0, number);
                        if (usernumber >= 2) u2 = root.Next(0, number);
                        if (usernumber >= 4) { u3 = root.Next(0, number); u4 = root.Next(0, number); }
                        if (usernumber >= 6) { u5 = root.Next(0, number); u6 = root.Next(0, number); }
                        while (avertool[u1] || !sexi[u1]) u1 = root.Next(0, number);
                        while (usernumber >= 2 && (u2 == u1 || avertool[u2] || sexi[u2])) u2 = root.Next(0, number);
                        while (usernumber >= 4 && (u3 == u1 || u3 == u2 || avertool[u3] || !sexi[u3])) u3 = root.Next(0, number);
                        while (usernumber >= 4 && (u4 == u1 || u4 == u2 || u4 == u3 || avertool[u4] || sexi[u4])) u4 = root.Next(0, number);
                        while (usernumber >= 6 && (u5 == u1 || u5 == u2 || u5 == u3 || u5 == u4 || avertool[u5] || !sexi[u5])) u5 = root.Next(0, number);
                        while (usernumber >= 6 && (u6 == u1 || u6 == u2 || u6 == u3 || u6 == u4 || u6 == u5 || avertool[u6] || sexi[u6])) u6 = root.Next(0, number);
                        avertool[u1] = true;
                        if (usernumber > 1) avertool[u2] = true;
                        if (usernumber >= 4) avertool[u3] = true;
                        if (usernumber >= 4) avertool[u4] = true;
                        if (usernumber >= 6) avertool[u5] = true;
                        if (usernumber >= 6) avertool[u6] = true;
                        u1lab.Text = (u1 + 1).ToString("D2"); u1lab.Refresh(); NameBox1.Text = names[u1]; NameBox1.Refresh();
                        if (usernumber > 1) { u2lab.Text = (u2 + 1).ToString("D2"); u2lab.Refresh(); NameBox2.Text = names[u2]; NameBox2.Refresh(); }
                        if (usernumber >= 4) { u3lab.Text = (u3 + 1).ToString("D2"); u3lab.Refresh(); NameBox3.Text = names[u3]; NameBox3.Refresh(); }
                        if (usernumber >= 4) { u4lab.Text = (u4 + 1).ToString("D2"); u4lab.Refresh(); NameBox4.Text = names[u4]; NameBox4.Refresh(); }
                        if (usernumber >= 6) { u5lab.Text = (u5 + 1).ToString("D2"); u5lab.Refresh(); NameBox5.Text = names[u5]; NameBox5.Refresh(); }
                        if (usernumber >= 6) { u6lab.Text = (u6 + 1).ToString("D2"); u6lab.Refresh(); NameBox6.Text = names[u6]; NameBox6.Refresh(); }
                        break;
                    }
            }
            working = false;
            StartButton.BackgroundImage = Image.FromFile(path+"\\pictures\\start1.png");
            StartButton.Refresh();
        }
        private void LastMan()
        {
            lastman = 0;
            for (int i = 0; i < number; i++)
            {
                if (avertool[i] == false && !sexi[i]) lastman++;
            }
        }

        

        private void LastWom()
        {
            lastwom = 0;
            for (int i = 0; i < number; i++)
            {
                if (avertool[i] == false && sexi[i]) lastwom++;
            }
        }
        private void ClearTools()
        {
            for (int i = 0; i < number; i++) avertool[i] = false;
            WriteAvertool();
        }
        private void LastPer()
        {
            last = 0;
            for (int i = 0; i < number; i++)
            {
                if (!avertool[i]) last++;
            }
        }
        private void AutoSave_Tick(object sender, EventArgs e)
        {
            WriteAvertool();
            File.WriteAllText(path+"\\userdata\\usernumber.txt", usernumber.ToString());
        }
        /*private void showLineNo()
        {
            //获得当前坐标信息
            Point p = this.editor1.Location;
            int crntFirstIndex = this.editor1.GetCharIndexFromPosition(p);

            int crntFirstLine = this.editor1.GetLineFromCharIndex(crntFirstIndex);

            Point crntFirstPos = this.editor1.GetPositionFromCharIndex(crntFirstIndex);

            p.Y += this.editor1.Height;

            int crntLastIndex = this.editor1.GetCharIndexFromPosition(p);

            int crntLastLine = this.editor1.GetLineFromCharIndex(crntLastIndex);
            Point crntLastPos = this.editor1.GetPositionFromCharIndex(crntLastIndex);

            //准备画图
            Graphics g = this.SettingPannel.CreateGraphics();

            Font font = new Font(this.editor1.Font, this.editor1.Font.Style);

            SolidBrush brush = new SolidBrush(Color.Green);

            //画图开始

            //刷新画布

            Rectangle rect = this.SettingPannel.ClientRectangle;
            brush.Color = this.SettingPannel.BackColor;

            g.FillRectangle(brush, 0, 0, this.SettingPannel.ClientRectangle.Width, this.SettingPannel.ClientRectangle.Height);

            brush.Color = Color.White;//重置画笔颜色

            //绘制行号

            int lineSpace = 0;

            if (crntFirstLine != crntLastLine)
            {
                lineSpace = (crntLastPos.Y - crntFirstPos.Y) / (crntLastLine - crntFirstLine);

            }

            else
            {
                lineSpace = Convert.ToInt32(this.editor1.Font.Size);

            }

            int brushX = this.SettingPannel.ClientRectangle.Width - Convert.ToInt32(font.Size * 3);

            int brushY = crntLastPos.Y + Convert.ToInt32(font.Size * 0.21f);//惊人的算法啊！！
            for (int i = crntLastLine; i >= crntFirstLine; i--)
            {

                g.DrawString((i + 1).ToString(), font, brush, brushX, brushY);

                brushY -= lineSpace;
            }

            g.Dispose();

            font.Dispose();

            brush.Dispose();
        }
         * private int GetLineNoVscroll(RichTextBox rtb)
        {
            //获得当前坐标信息
            Point p = rtb.Location;
            int crntFirstIndex = rtb.GetCharIndexFromPosition(p);
            int crntFirstLine = rtb.GetLineFromCharIndex(crntFirstIndex);
            return crntFirstLine;
        }
        private void TrunRowsId(int iCodeRowsID, RichTextBox rtb)
        {
            try
            {
                rtb.SelectionStart = rtb.GetFirstCharIndexFromLine(iCodeRowsID);
                rtb.SelectionLength = 0;
                rtb.ScrollToCaret();
            }
            catch
            {

            }
        }*/

    }
}
