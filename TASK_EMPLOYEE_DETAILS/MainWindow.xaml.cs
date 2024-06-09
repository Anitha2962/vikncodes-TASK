using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace TASK_EMPLOYEE_DETAILS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-C3E8P2C;Initial Catalog=employee_details;Integrated Security=True;");
        public void Clear()
        {
            name_txt.Clear();
            job_txt.Clear();
            department_txt.Clear() ;
            search_txt.Clear();

        }
        public bool isValid()
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            if (job_txt.Text == string.Empty)
            {
                MessageBox.Show("Jobtitle is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            if (department_txt.Text == string.Empty)
            {
                MessageBox.Show("Department is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            return true;
        }
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from employee_curd", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;


        }
        private void clearbtn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void createbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO employee_curd VALUES(@Name,@JobTitle,@Department)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                    cmd.Parameters.AddWithValue("@JobTitle", job_txt.Text);
                    cmd.Parameters.AddWithValue("@Department", department_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Successfully registerd", "saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from employee_curd where EmployeeID = " +search_txt.Text+ "",con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted.","Deleted",MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                Clear();
                LoadGrid();
                con.Close() ;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Not deleted" +ex.Message);
            }
            finally
            {
                con.Close();

            }
                
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("Update employee_curd set Name = '" + name_txt.Text + "' ,JobTitle ='" + job_txt.Text + "',Department = '" + department_txt.Text + "' where EmployeeID = '" + search_txt.Text + "' ",con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has beren updated successfully","updated",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                Clear();
                LoadGrid();
            }
        }
    }
}
