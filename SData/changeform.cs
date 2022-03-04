using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SData
{
    public partial class changeform : Form
    {
        string ID = "";
        public changeform(String name1,String no,String score)
        {
            InitializeComponent();
            ID = textBox1.Text = name1;
            textBox2.Text = no;
            textBox3.Text = score.ToString();
            

        }

        public changeform()
        {
            InitializeComponent();
        }

        private void changeform_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"update S_form1 set Name='{textBox1.Text}',No" +
                $"='{textBox2.Text}',Score='{textBox3.Text}' where  Name= '{ID}'";
            Dao dao = new Dao();
            if (dao.Execute(sql) > 0)
            {
                MessageBox.Show("修改成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失败");

            }
        }
    }
}


