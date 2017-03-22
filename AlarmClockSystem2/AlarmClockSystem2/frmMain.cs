using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace AlarmClockSystem2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //判断设定时间是否大于现在的时间

            //获取现在的时间
            DateTime now = DateTime.Now;
            //获取设定的时间
            DateTime utime = Convert.ToDateTime(cmbHour.Text + ":" + cmbMinute.Text + ":" + cmbSecond.Text);
            //计算时间差
            TimeSpan ts = utime - now;
            if (Convert.ToInt32(ts.TotalHours) < 0 || Convert.ToInt32(ts.TotalMinutes) < 0 || Convert.ToInt32(ts.TotalSeconds) < 0)
            {
                MessageBox.Show("设定时间小于当前时间，请重新设定！", "温馨提示");
            }
            else
            {
                ShowMsg show = new ShowMsg();

                lblTip.Visible = true;
                cmbHour.Enabled = false; cmbMinute.Enabled = false; cmbSecond.Enabled = false;

                label4.Visible = true; label5.Visible = true; label6.Visible = true;
                label7.Visible = true; label8.Visible = true; label9.Visible = true; label10.Visible = true;
                timer1.Enabled = true;
                TimeRemain.Enabled = true;

                show.ShowMessageBoxTimeout("时间设定成功！", "温馨提示", MessageBoxButtons.OK, 1000);
            }
            btnReSet.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e) //负责计算
        {
            DateTime dt = DateTime.Now;
            string hour = dt.Hour.ToString();
            string minute = dt.Minute.ToString();
            string second = dt.Second.ToString();

            if (cmbHour.Text.Trim() == hour.Trim() && Convert.ToInt32(cmbMinute.Text.Trim()) <= Convert.ToInt32(minute.Trim()) && Convert.ToInt32(cmbSecond.Text.Trim()) <= Convert.ToInt32(second.Trim()))
            {
                timer1.Enabled = false;
                TimeRemain.Enabled = false;
                SoundPlayer player = new SoundPlayer("../../Resource/8236.wav");
                player.Play();

                MethodInvoker mshow = new MethodInvoker(msgshow);
                this.BeginInvoke(mshow);
            }
        }
        private void msgshow()
        {
            MessageBox.Show("时间到！", "温馨提示");
        }

        private void TimeRemain_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string hour = dt.Hour.ToString();
            string minute = dt.Minute.ToString();
            string second = dt.Second.ToString();

            label8.Text = (Convert.ToInt32(cmbHour.Text) - Convert.ToInt32(hour)).ToString();

            if (Convert.ToInt32(cmbSecond.Text) < Convert.ToInt32(second))
            {
                label9.Text = (Convert.ToInt32(cmbMinute.Text) - Convert.ToInt32(minute) - 1).ToString();
                label10.Text = (Convert.ToInt32(cmbSecond.Text) + 60 - Convert.ToInt32(second)).ToString();
            }
            else
            {
                label9.Text = (Convert.ToInt32(cmbMinute.Text) - Convert.ToInt32(minute)).ToString();
                label10.Text = (Convert.ToInt32(cmbSecond.Text) - Convert.ToInt32(second)).ToString();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            cmbHour.Text = dt.Hour.ToString();
            cmbMinute.Text = dt.Minute.ToString();
            cmbSecond.Text = dt.Second.ToString();
        }

        private void btnReSet_Click(object sender, EventArgs e)
        {
            cmbHour.Enabled = true;
            cmbMinute.Enabled = true;
            cmbSecond.Enabled = true;
            lblTip.Visible = false;
        }
    }
}
