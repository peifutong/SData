using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SData
{
    public partial class Form1 : Form
    {
        int numcount;
        public struct Student
        {
            public int Id;
            public String _Name;
            public String No;
            public int Score;
        };
        //成绩查询
        public void TableScore()
        {
            grid1.Rows.Clear();//清空
            Dao dao = new Dao();

            string sql = $"select * from S_form1 where name = '{txtScore.Text}'";
            IDataReader dc = dao.read(sql);
            while (dc.Read())
            {
                grid1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }
        //姓名查询
        public void TableName()
        {
            grid1.Rows.Clear();//清空
            Dao dao = new Dao();

            string sql = $"select * from S_form1 where name like '%{txtName.Text}%'";
            IDataReader dc = dao.read(sql);
            while (dc.Read())
            {
                grid1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }
        //学号查询
        public void TableNo()
        {
            grid1.Rows.Clear();//清空
            Dao dao = new Dao();

            string sql = $"select * from S_form1 where No='{txtNo.Text}'";
            IDataReader dc = dao.read(sql);
            while (dc.Read())
            {
                grid1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }
        public void Table()
        {
            /*
            grid1.Rows.Clear();//清空
            Dao dao = new Dao();
            string sql = "select * from S_form1";
            IDataReader dc = dao.read(sql);
            while (dc.Read())
            {
                grid1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString());
            }
            dc.Close();
            dao.DaoClose();*/
            grid1.Rows.Clear();//清空
            using (SqlConnection conn = new SqlConnection("server=USP;uid=admin;pwd=admin;database=SData"))
            {
                conn.Open();

                String sql = "select Id,Name,No,Score from S_form1";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Student S = new Student();
                    S.Id = Convert.ToInt32(sdr["Id"]);
                    S._Name = sdr["Name"] + " ";
                    S.No = sdr["No"] + " ";
                    S.Score = Convert.ToInt32(sdr["Score"]);
                    grid1.Rows.Add(S.Id, S._Name, S.No, S.Score);
                }
                
                
                sdr.Close();
            }
        }
        
        //总数量
        public void totalNum()
        {

            using (SqlConnection conn = new SqlConnection("server=USP;uid=admin;pwd=admin;database=SData"))
            {
                conn.Open();

                String sql = "select COUNT(*) from S_form1";

                SqlCommand cmd = new SqlCommand(sql, conn);

                Object result = cmd.ExecuteScalar();

                txtS.Text = result.ToString();
            }
        }

        public Form1()
        {
            InitializeComponent();
            numcount = 1;
            totalNum();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn1 = new SqlConnection("server=USP;uid=admin;pwd=admin;database=SData")) 
            {
                conn1.Open();

                String sql = "INSERT INTO S_form1 ([Id],[Name],[No],[Score]) VALUES(@Id,@Name,@No,@Score)";

                SqlCommand cmd = new SqlCommand(sql, conn1);
                cmd.Parameters.Add(new SqlParameter("@Id", numcount));numcount++;
                cmd.Parameters.Add(new SqlParameter("@Name", txtName.Text));
                cmd.Parameters.Add(new SqlParameter("@No", txtNo.Text));
                cmd.Parameters.Add(new SqlParameter("@Score", txtScore.Text));

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) MessageBox.Show("success");
                
            }

            Table(); 
            totalNum();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rows=0;
            using (SqlConnection conn = new SqlConnection("server=USP;uid=admin;pwd=admin;database=SData"))
            {
                conn.Open();
                //姓名删除
                String sqlName = "delete from S_form1 where Name=@Name";
                SqlCommand cmdName = new SqlCommand(sqlName, conn);
                //学号删除
                String sqlNo = "delete from S_form1 where No=@No";
                SqlCommand cmdNo = new SqlCommand(sqlNo, conn);
                //成绩删除
                String sqlScore = "delete from S_form1 where Score=@Score";
                SqlCommand cmdScore = new SqlCommand(sqlScore, conn);

                if (txtName.Text != "")
                {
                    cmdName.Parameters.Add(new SqlParameter("@Name", txtName.Text));
                    rows = cmdName.ExecuteNonQuery();
                }
                else if (txtNo.Text != "")
                {
                    cmdNo.Parameters.Add(new SqlParameter("@No", txtNo.Text));
                    rows = cmdNo.ExecuteNonQuery();
                }
                else if(txtScore.Text != "")
                {
                    cmdScore.Parameters.Add(new SqlParameter("@Score", txtScore.Text));
                    rows = cmdScore.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("请输入名字、学号或成绩");
                }

                
                MessageBox.Show("删除成功" + rows + "行！");
                totalNum();


            }
            Table();
        }

       

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help form2 = new help();
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = grid1.SelectedRows[0].Cells[1].Value.ToString();
                string No = grid1.SelectedRows[0].Cells[2].Value.ToString();
                string Score = grid1.SelectedRows[0].Cells[3].Value.ToString();


                changeform admin = new changeform(Name, No, Score);
                admin.ShowDialog();

                Table();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }


            Table();
            totalNum();
        }



        private void txtS_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Table();
        }

        private void grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if(txtName.Text != "")
            {
                TableName();
            }
            else if (txtNo.Text != "")
            {
                TableNo();
            }
            else if (txtScore.Text != "")
            {
                TableScore();
            }
            else
            {
                MessageBox.Show("请输入内容");
                Table();
            }
        }
    }
}

