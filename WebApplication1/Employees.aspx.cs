using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Employees : System.Web.UI.Page
    {
        SqlConnection objCon;
        SqlCommand objCmd;
        DataTable objDt;
        string strConn = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Show();
        }

        public void Show() 
        {
            objCon = new SqlConnection(strConn);
            string strSQL = "Select * from Employee";
            objCmd = new SqlCommand(strSQL, objCon);
            objDt = new DataTable();
            SqlDataAdapter objSDA = new SqlDataAdapter();
            objSDA.SelectCommand = objCmd;
            objSDA.Fill(objDt);
            GridView1.DataSource = objDt;
            GridView1.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objCon = new SqlConnection(strConn);
            string Gender = "";
            if (rbMale.Checked == true) { Gender = "Male"; }
            else if (rbFemale.Checked == true) { Gender = "Female"; }

            string strSQL = "insert into Employee Values('" + txtEmpCode.Text + "', '" + txtName.Text + "', '" + ddlDepartment.SelectedValue + "', '" + Gender + "', '" + Convert.ToDateTime(txtBOD.Text) + "', '" + Convert.ToDateTime(txtJoining.Text) + "', '" + Convert.ToInt32(txtPreExp.Text) + "', '" + Convert.ToInt32(txtSalary.Text) + "', '" + txtAddress.Text + "')";
            objCmd = new SqlCommand(strSQL, objCon);
            objCon.Open();
            objCmd.ExecuteNonQuery();
            objCon.Close();
            Response.Write("Insert Record Successfully");

            Show();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Show();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Show();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int Empid = (Convert.ToInt32(((Label)GridView1.Rows[e.RowIndex].FindControl("Label1")).Text));

            string EmpCode = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox2")).Text;
            string EmpName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox1")).Text;
            string Department = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox3")).Text;
            string Gender = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox4")).Text;
            DateTime EmpBOD = (Convert.ToDateTime(((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox5")).Text));
            DateTime EmpJoiningDate = (Convert.ToDateTime(((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox6")).Text));
            int PreExp = (Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox7")).Text));
            int salary = (Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox8")).Text));
            string EmpAddress = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox9")).Text;

            objCon = new SqlConnection(strConn);
            string strSQL = "Update Employee set EmpCode = '" + EmpCode + "', EmpName = '" + EmpName + "', Department = '" + Department + "', Gender = '" + Gender + "', EmpBOD = '" + EmpBOD + "', EmpJoiningDate = '" + EmpJoiningDate + "', PrevExperience = '" + PreExp + "', Salary = '" + salary + "', EmpAddress = '" + EmpAddress + "' where EmployeeID = '" + Empid + "' ";
            objCmd = new SqlCommand(strSQL, objCon);
            objCon.Open();
            objCmd.ExecuteNonQuery();
            objCon.Close();
            GridView1.EditIndex = -1;
            Show();
        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }
    }
}