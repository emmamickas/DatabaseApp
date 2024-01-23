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
using Npgsql;
using System.Threading;

namespace Milestone1_Dellwo
{
    /// <summary>
    /// Interaction logic for TipWindow.xaml
    /// </summary>
    public partial class TipWindow : Window
    {
        string userId = string.Empty;

        string businessId = string.Empty;

        public TipWindow(string userId, string businessId)
        {
            this.userId = userId;
            Console.WriteLine(userId);
            this.businessId = businessId;

            InitializeComponent();

            addColumns2TipGrid();
            addColumns2FriendsTipGrid();

            loadTips();
            loadFriendsTips();
        }

        public class Tip
        {
            public string tip_date { get; set; }

            public string tip_user_id { get; set; }

            public string tip_user_name { get; set; }

            public string tip_likes { get; set; }

            public string tip_text { get; set; }
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2test; password = Password";
        }

        private void addColumns2TipGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("tip_date");
            col1.Header = "Date";
            col1.Width = 140;
            Business_Tips.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("tip_user_name");
            col2.Header = "User Name";
            col2.Width = 70;
            Business_Tips.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("tip_likes");
            col3.Header = "Likes";
            col3.Width = 35;
            Business_Tips.Columns.Add(col3);


            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("tip_text");
            col4.Header = "Text";
            col4.Width = 400;
            Business_Tips.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("tip_user_id");
            col5.Header = "User ID";
            col5.Width = 0;
            Business_Tips.Columns.Add(col5);
        }

        private void addColumns2FriendsTipGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("tip_user_name");
            col1.Header = "User Name";
            col1.Width = 70;
            Friends_Tips.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("tip_date");
            col2.Header = "Date";
            col2.Width = 140;
            Friends_Tips.Columns.Add(col2);


            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("tip_text");
            col3.Header = "Text";
            col3.Width = 400;
            Friends_Tips.Columns.Add(col3);
        }

        private void loadTips()
        {
            string y_business_id = businessId;

            Business_Tips.Items.Clear();

            if (y_business_id != null)
            {
                string sqlStr = "SELECT tip.y_date, users.y_user_name, tip.y_like_count, tip.y_tip_text, tip.y_user_id FROM users, tip WHERE y_business_id = '"
                    + y_business_id.ToString() + "' AND tip.y_user_id = users.y_user_id"
                    + " ORDER BY y_date DESC";

                execute_query(sqlStr, addTipRow);
            }
        }

        private void loadFriendsTips()
        {
            string y_business_id = businessId;

            string y_user_id = this.userId;

            Friends_Tips.Items.Clear();

            if (y_business_id != null)
            {
                string sqlStr = "SELECT users.y_user_name, tip.y_date, tip.y_tip_text, tip.y_user_id FROM users, tip"
                    + " WHERE y_business_id = '" + y_business_id.ToString()
                    + "' AND tip.y_user_id = users.y_user_id"
                    + " AND tip.y_user_id in"
                    + " (SELECT friends_with.y_user_id_friended FROM friends_with"
                    + " WHERE friends_with.y_user_id_friender = '" + y_user_id.ToString() + "')"
                    + " ORDER BY y_date";

                execute_query(sqlStr, addFriendTipRow);
            }
        }

        private void addTipRow(NpgsqlDataReader R)
        {
            Tip tip = new Tip() { tip_date = R.GetDateTime(0).ToString(), tip_user_name = R.GetString(1).ToString(), tip_likes = R.GetInt32(2).ToString(), tip_text = R.GetString(3).ToString(), tip_user_id = R.GetString(4).ToString() };

            Business_Tips.Items.Add(tip);
        }

        private void addFriendTipRow(NpgsqlDataReader R)
        {
            Tip tip = new Tip() { tip_user_name = R.GetString(0).ToString(), tip_date = R.GetDateTime(1).ToString(), tip_text = R.GetString(2).ToString(), tip_user_id = R.GetString(3) };

            Console.WriteLine("Adding tip: ", tip.tip_text);

            Friends_Tips.Items.Add(tip);
        }

        private void likeTip(NpgsqlDataReader R)
        {
            loadTips();
            loadFriendsTips();
        }

        private void execute_query(string sqlstr, Action<NpgsqlDataReader> myf)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;

                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            myf(reader);
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("Sql error: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void Like_Click(object sender, RoutedEventArgs e)
        {
            string y_business_id = businessId;

            if (Business_Tips.SelectedItem != null)
            {
                Console.WriteLine("Updating tip...");

                Tip selectedTip = (Tip)Business_Tips.SelectedItem;

                string y_date = selectedTip.tip_date;
                string y_user_name = selectedTip.tip_user_name;
                string y_user_id = selectedTip.tip_user_id;

                string sqlStr = "UPDATE tip"
                    + " SET y_like_count = y_like_count + 1"
                    + " WHERE y_user_id = '" + y_user_id.ToString() + "' AND"
                    + " y_business_id = '" + y_business_id.ToString() + "' AND"
                    + " y_date = '" + y_date.ToString() + "'";

                execute_query(sqlStr, likeTip);

                loadTips();
            }
            else if (Friends_Tips.SelectedItem != null)
            {
                Tip selectedTip = (Tip)Friends_Tips.SelectedItem;

                string y_date = selectedTip.tip_date;
                string y_user_name = selectedTip.tip_user_name;
                string y_user_id = selectedTip.tip_user_id;

                string sqlStr = "UPDATE tip"
                    + " SET y_like_count = y_like_count + 1"
                    + " WHERE y_user_id = '" + y_user_id.ToString() + "' AND"
                    + " y_business_id = '" + y_business_id.ToString() + "' AND"
                    + " y_date = '" + y_date.ToString() + "'";

                execute_query(sqlStr, likeTip);

                loadFriendsTips();
                loadTips();
            }
            else
            {
                return;
            }
        }

        private void Add_Tip_Click(object sender, RoutedEventArgs e)
        {
            string y_business_id = businessId;

            if (y_business_id != null)
            {
                string tip_text = New_Tip_Text.Text;
                string time = DateTime.Now.ToString("MM-dd-yyyy h:mm:ss");
                //string sqlStr = "INSERT INTO Tip (y_date, y_tip_text, y_user_id, y_business_id, y_like_count) VALUES ( '" + time + "','" + tip_text + "', '4XChL029mKr5hydo79Ljxg','" + y_business_id + "', 0)";
                string sqlStr = "INSERT INTO Tip (y_date, y_tip_text, y_user_id, y_business_id, y_like_count) VALUES ( '" + time + "','" + tip_text + "', '" + this.userId + "', '" + y_business_id + "', 0)";

                execute_query(sqlStr, addTipRow);

                New_Tip_Text.Text = "Added tip: " + tip_text;

                loadTips();
            }
            else
            {
                New_Tip_Text.Text = "Could not get business id to add tip.";
            }
        }
    }
}
