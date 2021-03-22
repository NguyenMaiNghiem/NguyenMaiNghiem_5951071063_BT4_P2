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

namespace NguyenMaiNghiem_5951071063_B2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetStudensRecord();
        }       
        private void GetStudensRecord()
        {
            //Ket noi DB
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EPK62FG;Initial Catalog=DemoCRUD;Integrated Security=True");

            //Truy van DB
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTB", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }
        private bool IsValidData()
        {
            if(TxtHName.Text == string.Empty
                || TxtNName.Text == string.Empty
                || TxtAdress.Text == string.Empty
                || string.IsNullOrEmpty(TxtPhone.Text)
                || string.IsNullOrEmpty(TxtRoll.Text))
            {
                MessageBox.Show("Co cho chua nhap du lieu !!!", "Loi du lieu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES" + "(@Name, @FatherName, @RollNumber,@Address,@Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudensRecord();
            }
        }
        public int StudentID;
        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
            TxtHName.Text = StudentRecordData.SelectedRows[0].Cells[1].Value.ToString();
            TxtNName.Text = StudentRecordData.SelectedRows[0].Cells[2].Value.ToString();
            TxtRoll.Text = StudentRecordData.SelectedRows[0].Cells[3].Value.ToString();
            TxtAdress.Text = StudentRecordData.SelectedRows[0].Cells[4].Value.ToString();
            TxtPhone.Text = StudentRecordData.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET " +
                    "Name = @Name, FatherName = @FatherName," + 
                    "RollNumber = @RollNumber, Address = @Address," + 
                    "Mobile = @Mobile Where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudensRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cap nhat bi loi!!!", "Loi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
