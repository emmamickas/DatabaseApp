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

using System.Data;
using System.Diagnostics;


namespace Milestone1_Dellwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Business
        {

            public string business_id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zipcode { get; set; }
            public string distance { get; set; }
            public string stars { get; set; }
            public string num_tips { get; set; }
            public string total_checkins { get; set; }


        }

        public class Tip
        {
            //public string date { get; set; }

            public string tip_text { get; set; }

            public string user_id { get; set; }


        }

        public class Friend
        {
            public string user_name { get; set; }

            public string tip_like_count { get; set; }

            public string avg_stars { get; set; }

            public string date_joined { get; set; }

        }

        public class LatestTip
        {

            public string user_name { get; set; }
            public string business { get; set; }
            public string city { get; set; }
            public string text { get; set; }
            public string date { get; set; }

        }

        public MainWindow()
        {

            InitializeComponent();

            addState();
            addColumns2BusinessGrid();
            addColumns2TipGrid();
            addColumns2LatestTipGrid();
            addColumns2Friends();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2test; password = Password";
        }
       
        private void addState()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct y_state FROM business ORDER BY y_state";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            state_list.Items.Add(reader.GetString(0));
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            /*state_list.Items.Add("WA");
            state_list.Items.Add("CA");
            state_list.Items.Add("ID");
            state_list.Items.Add("NV");*/
        }

        // CHANGED 4/22/2022 BY REAGAN KELLEY
        // Changed from addColumns2Grid to new title
        // Added more columns for use case B1
        private void addColumns2BusinessGrid()
        {

            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "Business Name";
            col1.Width = 150;
            business_grid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("address");
            col2.Header = "Address";
            col2.Width = 100;
            business_grid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 75;
            business_grid.Columns.Add(col3);


            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("state");
            col4.Header = "State";
            col4.Width = 50;
            business_grid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("distance");
            col5.Header = "Distance (miles)";
            col5.Width = 50;
            business_grid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("stars");
            col6.Header = "Stars";
            col6.Width = 50;
            business_grid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("num_tips");
            col7.Header = "# of Tips";
            col7.Width = 50;
            business_grid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Binding = new Binding("total_checkins");
            col8.Header = "Total Checkins";
            col8.Width = 50;
            business_grid.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Binding = new Binding("business_id");
            col9.Header = "business_id";
            col9.Width = 0;
            col9.Visibility = Visibility.Collapsed; // don't show business_id in business grid
            business_grid.Columns.Add(col9);

        }

        private void addColumns2TipGrid()
        {


            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("user_id");
            col1.Header = "user_id";
            col1.Width = 125;
            tip_grid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("tip_text");
            col2.Header = "tip";
            col2.Width = 500;
            tip_grid.Columns.Add(col2);


            /*business_grid.Items.Add(new Business() { name = "Business 1", state = "WA", city = "Pullman" });
            business_grid.Items.Add(new Business() { name = "Business 2", state = "CA", city = "Arcata" });
            business_grid.Items.Add(new Business() { name = "Business 3", state = "NV", city = "Reno" });*/

        }

        private void addColumns2LatestTipGrid()
        {


            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("user_name");
            col1.Header = "User Name";
            col1.Width = 125;
            latest_tip_grid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("business");
            col2.Header = "Business";
            col2.Width = 125;
            latest_tip_grid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 125;
            latest_tip_grid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("text");
            col4.Header = "Text";
            col4.Width = 200;
            latest_tip_grid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("date");
            col5.Header = "Date";
            col5.Width = 125;
            latest_tip_grid.Columns.Add(col5);


            /*business_grid.Items.Add(new Business() { name = "Business 1", state = "WA", city = "Pullman" });
            business_grid.Items.Add(new Business() { name = "Business 2", state = "CA", city = "Arcata" });
            business_grid.Items.Add(new Business() { name = "Business 3", state = "NV", city = "Reno" });*/

        }

        private void addColumns2Friends()
        {


            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("user_name");
            col1.Header = "Name";
            col1.Width = 125;
            friends_grid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("tip_like_count");
            col2.Header = "Total Likes";
            col2.Width = 85;
            friends_grid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("avg_stars");
            col3.Header = "Average Stars";
            col3.Width = 95;
            friends_grid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("date_joined");
            col4.Header = "Yelping Since";
            col4.Width = 100;
            friends_grid.Columns.Add(col4);


        }

        private void addItems2SortList()
        {
            sort_list.Items.Clear();

            sort_list.Items.Add("Name");
            sort_list.Items.Add("Rating");
            sort_list.Items.Add("Highest Tip Count");
            sort_list.Items.Add("Highest Check In Count");
            sort_list.Items.Add("Nearest");
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


       /************************************************************************************
        * Name:           user_text_TextChanged()
        * Behavior:       Updates the user list box when username text box is changed
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void user_text_TextChanged(object sender, TextChangedEventArgs args)
        {
            cur_user_list_box.Items.Clear(); // Resets user list

            string sqlStr = "SELECT distinct y_user_id FROM Users WHERE y_user_name = '" + cur_user_text_box.Text +"' ORDER BY y_user_id;";
            execute_query(sqlStr, addUser); // fills list with user_ids with that name

        }

        /************************************************************************************
        * Name:           addUser()
        * Behavior:       Adds rows to the user item list
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addUser(NpgsqlDataReader R)
        {
            cur_user_list_box.Items.Add(R.GetString(0));
        }

        /************************************************************************************
        * Name:           user_list_box_SelectionChanged()
        * Behavior:       Updates the all user relationship info when selecting a user_id
        * Last Changed:   4/21/2022
        * Changed By:     Reagan Kelley
        * Change Details: Added Location enable edit reset
        ************************************************************************************/
        void user_list_box_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Clear All output boxes
            u_name_tbox.Clear();
            u_stars_tbox.Clear();
            u_fans_tbox.Clear();
            u_yelp_since_tbox.Clear();
            u_votes_tbox.Clear();
            u_funny_tbox.Clear();
            u_cool_tbox.Clear();
            u_useful_tbox.Clear();
            u_tip_count_tbox.Clear();
            u_total_likes_tbox.Clear();
            friends_grid.Items.Clear();
            latest_tip_grid.Items.Clear();

            // Reset edit ability of location coordinates
            u_lat_tbox.IsEnabled = false;
            u_long_tbox.IsEnabled = false;

            // update all user information by updating all output boxes
            if (cur_user_list_box.SelectedIndex > -1)
            {
                string selected_y_user_id = cur_user_list_box.SelectedItem.ToString().Trim();

                string sqlStr = "SELECT distinct y_user_name FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addUsername);

                sqlStr = "SELECT y_cool_count FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addCoolCount);

                sqlStr = "SELECT y_funny_count FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addFunnyCount);

                sqlStr = "SELECT y_useful_count FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addUsefulCount);

                sqlStr = "SELECT distinct y_avg_stars FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addAvgStars);

                sqlStr = "SELECT distinct y_date_joined FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addYelpingSince);

                sqlStr = "SELECT distinct y_num_fans FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addNumFans);

                sqlStr = "SELECT distinct y_tip_count FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addTipCount);

                sqlStr = "SELECT distinct y_vote_count FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addVoteCount);

                sqlStr = "SELECT distinct y_tip_like_count FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addTipsLikeCount);

                sqlStr = "SELECT distinct y_longitude, y_latitude FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addLocation);

                sqlStr = "SELECT y_user_name, y_tip_like_count, y_avg_stars, y_date_joined FROM Users, (SELECT y_user_id_friended FROM Friends_With WHERE y_user_id_friender = '" + 
                    selected_y_user_id + "') AS f WHERE Users.y_user_id = f.y_user_id_friended; ";
                execute_query(sqlStr, addFriends);


                // gross
                sqlStr = "CREATE TABLE friend_tips(y_user_name VARCHAR NOT NULL, y_user_id CHAR(33), y_business_id CHAR(33), y_tip_text VARCHAR, y_date TIMESTAMP);" +
                    "INSERT INTO friend_tips(y_user_name, y_user_id, y_business_id, y_tip_text, y_date) SELECT * FROM (SELECT friends.y_user_name, friends.y_user_id, " +
                    "Tip.y_business_id, y_tip_text, y_date FROM Tip,(SELECT Users.y_user_id, y_user_name FROM Users, (SELECT y_user_id_friended FROM Friends_With WHERE " +
                    "y_user_id_friender = '" + selected_y_user_id + "') AS f WHERE Users.y_user_id = f.y_user_id_friended) as friends WHERE friends.y_user_id = Tip.y_user_id) " +
                    "As friend_tips; SELECT y_user_name, Business.y_business_name, Business.y_city, y_tip_text, y_date FROM friend_tips, Business, (SELECT y_user_id, Max(y_date) As NewestDate " +
                    "FROM friend_tips GROUP BY y_user_id) as newestTips WHERE newestTips.y_user_id = friend_tips.y_user_id AND NewestDate = y_date " +
                    "AND Business.y_business_id = friend_tips.y_business_id; DROP TABLE friend_tips;";
                execute_query(sqlStr, addFriendTips); 

            }
        }


        /************************************************************************************
        * Name:           addUsername()
        * Behavior:       Updates text in the username textbox
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addUsername(NpgsqlDataReader R)
        {
            u_name_tbox.Text = R.GetString(0);
        }

        /************************************************************************************
        * Name:           addAvgStars()
        * Behavior:       Updates text in the avg stars textbox
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addAvgStars(NpgsqlDataReader R)
        {
            u_stars_tbox.Text = (R.GetFloat(0)).ToString();
        }

        /************************************************************************************
        * Name:           addYelpingSince()
        * Behavior:       Updates text in the yelping since textbox
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addYelpingSince(NpgsqlDataReader R)
        {
            u_yelp_since_tbox.Text = (R.GetDateTime(0)).ToString();
        }

        /************************************************************************************
        * Name:           addNumFans()
        * Behavior:       Updates text in the Fans textbox
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addNumFans(NpgsqlDataReader R)
        {
            u_fans_tbox.Text = (R.GetInt32(0)).ToString();
        }

        /************************************************************************************
        * Name:           addTipCount()
        * Behavior:       Updates text in the Tip Count textbox
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addTipCount(NpgsqlDataReader R)
        {
             u_tip_count_tbox.Text = (R.GetInt32(0)).ToString();
        }

        /************************************************************************************
        * Name:           addVoteCount()
        * Behavior:       Updates text in the Votes textbox
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addVoteCount(NpgsqlDataReader R)
        {
            u_votes_tbox.Text = (R.GetInt32(0)).ToString();
        }

        /************************************************************************************
        * Name:           addLocation()
        * Behavior:       Updates text in longitude and latitude boxes
        * Last Changed:   4/21/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addLocation(NpgsqlDataReader R)
        {
            u_long_tbox.Text = (R.GetFloat(0)).ToString();
            u_lat_tbox.Text = (R.GetFloat(1)).ToString();

        }

        /************************************************************************************
        * Name:           addTipsLikeCount()
        * Behavior:       Updates text in the Total Tip Likes textbox
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addTipsLikeCount(NpgsqlDataReader R)
        {
            u_total_likes_tbox.Text = (R.GetInt32(0)).ToString();
        }

        private void addCoolCount(NpgsqlDataReader R)
        {
            u_cool_tbox.Text = (R.GetInt32(0)).ToString();
        }

        private void addFunnyCount(NpgsqlDataReader R)
        {
            u_funny_tbox.Text = (R.GetInt32(0)).ToString();
        }

        private void addUsefulCount(NpgsqlDataReader R)
        {
            u_useful_tbox.Text = (R.GetInt32(0)).ToString();
        }

        /************************************************************************************
        * Name:           addFriends()
        * Behavior:       Updates rows in the friends datagrid
        * Last Changed:   4/19/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addFriends(NpgsqlDataReader R)
        {
            Friend item = new Friend() { user_name = R.GetString(0), tip_like_count = (R.GetInt32(1)).ToString(), avg_stars = (R.GetFloat(2)).ToString(), date_joined = (R.GetDateTime(3)).ToString() };

            friends_grid.Items.Add(item);

        }

        /************************************************************************************
        * Name:           addFriendTips()
        * Behavior:       Updates rows in the Latest Tips datagrid
        * Last Changed:   4/21/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void addFriendTips(NpgsqlDataReader R)
        {
            LatestTip item = new LatestTip() { user_name = R.GetString(0), business = R.GetString(1), city = R.GetString(2), text = R.GetString(3), date = (R.GetDateTime(4)).ToString() };

            latest_tip_grid.Items.Add(item);

        }

        private void state_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cat_list_box.Items.Clear();
            City_list_box.Items.Clear();
            Zipcode_list_box.Items.Clear();
            business_grid.Items.Clear();
            if (state_list.SelectedIndex > -1)
            {
                string sqlStr = "SELECT distinct y_city FROM business WHERE y_state = '" + state_list.SelectedItem.ToString() + "' ORDER BY y_city";
                execute_query(sqlStr, addCity);
            }
        }

        

        /************************************************************************************
        * Name:           addBusiness()
        * Behavior:       Updates rows in business grid
        * Last Changed:   4/22/2022
        * Changed By:     Reagan Kelley
        * Change Details: Used to be named addGridRow to more specific name, fixed issues 
        *                 for use-case B1
        ************************************************************************************/
        private void addBusiness(NpgsqlDataReader R)
        {

            Business item = new Business() { name = R.GetString(0), address = R.GetString(1), city = R.GetString(2), state = R.GetString(3), 
                stars = R.GetFloat(4).ToString(), distance = R.GetDouble(8).ToString(), num_tips = R.GetInt32(5).ToString(), total_checkins = R.GetInt32(6).ToString(), business_id = R.GetString(7) };
            business_grid.Items.Add(item);
            
        }

        private void add_att_cat(NpgsqlDataReader R)
        {
            cat_att_list_box.Items.Add(R.GetString(0));
        }

        private void addTipRow(NpgsqlDataReader R)
        {
            Tip item = new Tip() { tip_text = R.GetString(0), user_id = R.GetString(1) };

            tip_grid.Items.Add(item);
        }


        private void addCategory(NpgsqlDataReader R)
        {
            Cat_list_box.Items.Add(R.GetString(0));
        }

        private void addCity(NpgsqlDataReader R)
        {
            City_list_box.Items.Add(R.GetString(0));
        }

        private void addZipcode(NpgsqlDataReader R)
        {
            Zipcode_list_box.Items.Add(R.GetString(0));
        }

        private void addOpenTimes(NpgsqlDataReader R)
        {
            DayOfWeek day = DateTime.Today.DayOfWeek;
            TimeSpan open_time = R.GetTimeSpan(0);
            TimeSpan close_time = R.GetTimeSpan(1);
            business_hours_tbox.Text = "Today (" + day.ToString() + "),  Open: " + open_time.ToString() + ",   Close: " + close_time.ToString();
        }



        /*  private void city_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
              business_grid.Items.Clear();
              if (city_list.SelectedIndex > -1)
              {
                  string sqlStr = "SELECT name, state, city FROM business WHERE state = '" + state_list.SelectedItem.ToString() + "' AND city = '" + city_list.SelectedItem.ToString() + "' ORDER BY name";
                  execute_query(sqlStr, addGridRow);
              }
          }*/

        private void City_list_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cat_list_box.Items.Clear();
            Zipcode_list_box.Items.Clear();
            if (City_list_box.SelectedIndex > -1)
            {
                string sqlStr = "SELECT DISTINCT y_zipcode FROM business WHERE y_state = '" + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString() + "' ORDER BY y_zipcode";
                execute_query(sqlStr, addZipcode);
            }



        }

        /************************************************************************************
       * Name:           get_business_query()
       * Behavior:       Returns sql query string for business grid and calculated distance
       * Last Changed:   4/26/2022
       * Changed By:     Reagan Kelley
       * Change Details: Made it so business grid table is saved for outside function use
       ************************************************************************************/
        private string get_business_query(string selected_businesses)
        {
            // selected business is the query to get get a table of businesses meeting the criteria

            // How this query works
            // 1. define haversine function
            // 2. determine selected user
            // 3. Join selected businesses and selected user via the haversine function
            // 4. Create query that returns business grid info and calculated location
            string haversine = "CREATE OR REPLACE FUNCTION haversine(y_lat1 FLOAT, y_long1 FLOAT, y_lat2 FLOAT, y_long2 FLOAT) RETURNS FLOAT AS " +
                    "'BEGIN RETURN(2 * 6378.137 * asin(sqrt(power(sin((y_lat2 - y_lat1) / 2), 2) + (cos(y_lat1) * cos(y_lat2) * " +
                    "power(sin((y_long2 - y_long1) / 2), 2))))) * 0.621371; END' LANGUAGE plpgsql;";
            string to_return = "";

            if (cur_user_list_box.SelectedIndex > -1)
            {
                string selected_y_user_id = cur_user_list_box.SelectedItem.ToString();
                string selected_user = "(SELECT distinct y_longitude, y_latitude FROM Users WHERE y_user_id = '" + selected_y_user_id + "') as selected_user";


                if (sort_list.SelectedIndex == 4)
                {
                    to_return += "SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, " +
                        "haversine(selected_businesses.y_latitude, selected_businesses.y_longitude, selected_user.y_latitude, selected_user.y_longitude) " +
                        "FROM (" + selected_businesses + ") as selected_businesses," + selected_user
                        + " ORDER BY haversine(selected_businesses.y_latitude, selected_businesses.y_longitude, selected_user.y_latitude, selected_user.y_longitude);";
                }
                else
                {
                    to_return += "SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, " +
                          "haversine(selected_businesses.y_latitude, selected_businesses.y_longitude, selected_user.y_latitude, selected_user.y_longitude) " +
                          "FROM (" + selected_businesses + ") as selected_businesses," + selected_user + ";";
                }
            }
            else
            {
                // if no user is selected don't calculate haversine distance
                to_return += "SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, 0 " +
                    "FROM (" + selected_businesses + ") as selected_businesses;";
            }

            // for further qeuries we need to keep the business query saved
            string save_table = haversine + "DROP TABLE IF EXISTS business_grid; CREATE TABLE business_grid(  y_business_name VARCHAR NOT NULL," +
                "y_address VARCHAR, y_city VARCHAR, y_state VARCHAR, y_rating INT, y_tip_count INTEGER DEFAULT 0, y_checkin_count INT DEFAULT 0, " +
                "y_business_id CHAR(33), haversine FLOAT); INSERT INTO business_grid(y_business_name, y_address, y_city, y_state, " +
                "y_rating, y_tip_count, y_checkin_count, y_business_id, haversine) " + to_return + "SELECT * FROM business_grid;";
            Debug.WriteLine(save_table);

            return save_table;
            
        }

        /************************************************************************************
        * Name:           update_business_grid()
        * Behavior:       updates business grid table with new filters
        * Last Changed:   4/26/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void update_business_grid(string updated_query)
        {

            // applied new where conditions to the existing query and updates its saved table for future filters
            string sqlStr = "CREATE TABLE temp_business_grid (y_business_name VARCHAR NOT NULL, y_address VARCHAR, y_city VARCHAR, y_state VARCHAR, " +
                "y_rating INT, y_tip_count INTEGER DEFAULT 0, y_checkin_count INT DEFAULT 0, y_business_id CHAR(33), haversine FLOAT);" +
                "INSERT INTO temp_business_grid(y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, haversine" +
                ") " + updated_query + "; DELETE FROM business_grid; INSERT INTO business_grid(y_business_name, y_address, y_city, y_state, y_rating, " +
                "y_tip_count, y_checkin_count, y_business_id, haversine) SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, " +
                "y_checkin_count, y_business_id, haversine FROM temp_business_grid; DROP TABLE temp_business_grid; SELECT * FROM business_grid";
            Debug.WriteLine(sqlStr);

            business_grid.Items.Clear();
            execute_query(sqlStr, addBusiness);
        }

        /************************************************************************************
        * Name:           Zipcode_list_box_SelectionChanged()
        * Behavior:       When zipcode is selected, populates business grid given other search criteria
        * Last Changed:   4/22/2022
        * Changed By:     Reagan Kelley
        * Change Details: Fixed business grid issues that were not fulfilling use-cases 
        *                 B1.1 and B1.2
        ************************************************************************************/
        private void Zipcode_list_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cat_list_box.Items.Clear();
            business_grid.Items.Clear();
            if (Zipcode_list_box.SelectedIndex > -1)
            {

                string sqlStr = "Select Distinct y_category_name from  business, categories  WHERE y_state = '"
                                + state_list.SelectedItem.ToString() + "' AND y_city = '"
                                + City_list_box.SelectedItem.ToString()
                                + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString()
                                + "' AND categories.y_business_id = business.y_business_id "
                                + " ORDER BY y_category_name";  // + "' AND y_city = '" + City_list_box.SelectedItem.ToString() +
                                                                //    "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "' AND business.y_business_id = categories.y_business_id'"*/ // +    "' ORDER BY y_category_name";
                execute_query(sqlStr, addCategory);


                // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
                sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE y_state = '"
                    + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString() + "' AND y_zipcode = '" +
                    Zipcode_list_box.SelectedItem.ToString() + "' ORDER BY y_business_name");
                execute_query(sqlStr, addBusiness);

                addItems2SortList();
            }
             



        }

        /************************************************************************************
        * Name:           Cat_list_box_SelectionChanged()
        * Behavior:       When category is selected, populates business grid given other search criteria
        * Last Changed:   4/24/2022
        * Changed By:     Connor Dellwo
        * Change Details: Updated to (actually) work with get_business query function. 
        *                 
        ************************************************************************************/
        private void Cat_list_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sqlStr;
            business_grid.Items.Clear();
            if (Cat_list_box.SelectedItems.Count == 0)
            {
                // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
                sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE y_state = '" + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString() + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "' ORDER BY y_business_name");
                execute_query(sqlStr, addBusiness);
                return;
            }

            // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
            //sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE " +
              //  "y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString());
            sqlStr = "SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE " +
                "y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString();
            int count = 1;
            foreach (var item in Cat_list_box.SelectedItems)
            {

                /// item.ToString();
                /* string sqlStr = "SELECT business.y_business_id, y_business_name, y_state, y_city, y_zipcode FROM business, categories WHERE y_state = '"
                     + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                     + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "'AND y_category_name = '"
                     + item.ToString() + "' AND categories.y_business_id = business.y_business_id " + " ORDER BY y_business_name";*/
                if (count == 1)
                {
                    sqlStr += "' AND y_business_id in (SELECT y_business_id FROM categories where y_category_name = '" + item.ToString() + "' )";
                }
                else
                {
                    sqlStr += " AND y_business_id in (SELECT y_business_id FROM categories where y_category_name = '" + item.ToString() + "' )";
                }
                count += 1;

            }
            if (Cat_list_box.SelectedItem == null)
            {

                return;
            }
            sqlStr += " ORDER BY y_business_name";
            sqlStr = get_business_query(sqlStr);
            execute_query(sqlStr, addBusiness);

        }

        /************************************************************************************
        * Name:           business_grid_SelectionChanged()
        * Behavior:       Updates business info output when business is selected from business grid
        * Last Changed:   4/24/2022
        * Changed By:     Connor Dellwo
        * Change Details: Added display attributes capability
        ************************************************************************************/
        private void business_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tip_grid.Items.Clear();
            cat_att_list_box.Items.Clear();

            if (business_grid.SelectedIndex > -1)
            {
                DayOfWeek day = DateTime.Today.DayOfWeek;
                string d = day.ToString();
                d = char.ToUpper(d[0]) + d.Substring(1); // capitalize the day. This might be my only comment - connor. 
                Business cur_biz = (Business)business_grid.SelectedItem;
                business_name_tbox.Text = "Business Name: " + cur_biz.name;
                business_address_tbox.Text = "Address: " + cur_biz.address;
                
                string sqlStr = "SELECT y_tip_text, y_user_id FROM business, tip WHERE y_state = '"
                    + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                    + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "' AND business.y_business_id = '" + cur_biz.business_id
                    + "' AND tip.y_business_id = business.y_business_id" + " ORDER BY y_date";
                execute_query(sqlStr, addTipRow);

                sqlStr = "SELECT y_opening_time, y_closing_time FROM business_hours WHERE y_business_id = '" + cur_biz.business_id + "' and y_day = '" + d + "'";
                execute_query(sqlStr, addOpenTimes);

                sqlStr = "SELECT y_attribute_name FROM attribute WHERE y_business_id = '" + cur_biz.business_id + "' and y_attribute_value <> 'False' and y_attribute_value <> 'no' ORDER BY y_attribute_name";
                cat_att_list_box.Items.Add("Attributes: ");
                execute_query(sqlStr, add_att_cat);

                cat_att_list_box.Items.Add("Categories: ");
                sqlStr = "SELECT y_category_name from categories WHERE y_business_id = '" + cur_biz.business_id + "' ORDER BY y_category_name";
                execute_query(sqlStr, add_att_cat);
            }
        }

        private void comment_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (business_grid.SelectedItem != null)
                {
                    Business cur_biz = (Business)business_grid.SelectedItem;
                    string tip_text = comment_text_box.Text;
                    string bid = cur_biz.business_id;
                    string time = DateTime.Now.ToString("MM-dd-yyyy h:mm:ss");
                    string sqlStr = "INSERT INTO Tip (y_date, y_tip_text, y_user_id, y_business_id, y_like_count) VALUES ( '" + time + "','" + tip_text + "', '4XChL029mKr5hydo79Ljxg','" + bid + "', 0)";

                    execute_query(sqlStr, addTipRow);

                    comment_text_box.Text = "Added tip: " + tip_text;
                    comment_text_box.Text = "Added tip for: " + cur_biz.name;

                    business_grid_SelectionChanged(null, null); //refresh comments 

                }
                else
                {
                    comment_text_box.Text = "No business selected. Tip not added. ";
                }


            }
        }

        /************************************************************************************
        * Name:           sort_list_SelectionChanged()
        * Behavior:       When sort option is selected, populates business grid given other search criteria
        * Last Changed:   4/22/2022
        * Changed By:     Reagan Kelley
        * Change Details: Fixed business grid issues that were not fulfilling use-cases 
        *                 B1.1 and B1.2
        ************************************************************************************/
        private void sort_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sqlStr;
            business_grid.Items.Clear();

            if (Cat_list_box.SelectedItems.Count != 0)
            {
                // Updates business results (QUERY STRING CHANGED - 4/25/2022 BY EMMA AND CONNOR)
                //sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE " +
                //  "y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString());
                sqlStr = "SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE " +
                    "y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString();
                int count = 1;

                foreach (var item in Cat_list_box.SelectedItems)
                {

                    /// item.ToString();
                    /* string sqlStr = "SELECT business.y_business_id, y_business_name, y_state, y_city, y_zipcode FROM business, categories WHERE y_state = '"
                         + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                         + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString() + "'AND y_category_name = '"
                         + item.ToString() + "' AND categories.y_business_id = business.y_business_id " + " ORDER BY y_business_name";*/
                    if (count == 1)
                    {
                        sqlStr += "' AND y_business_id in (SELECT y_business_id FROM categories where y_category_name = '" + item.ToString() + "' )";
                    }
                    else
                    {
                        sqlStr += " AND y_business_id in (SELECT y_business_id FROM categories where y_category_name = '" + item.ToString() + "' )";
                    }
                    count += 1;

                }

                if (state_list.SelectedItem == null || City_list_box.SelectedItem == null || Zipcode_list_box.SelectedItem == null)
                {
                    return;
                }

                switch (sort_list.SelectedIndex)
                {
                    case 0:

                        // Updates business results (QUERY STRING CHANGED - 4/25/2022 BY REAGAN)
                        sqlStr += " ORDER BY y_business_name";
                        sqlStr = get_business_query(sqlStr);
                        execute_query(sqlStr, addBusiness);

                        break;

                    case 1:

                        // Updates business results (QUERY STRING CHANGED - 4/25/2022 BY EMMA AND CONNOR)
                        sqlStr += " ORDER BY y_rating DESC";
                        sqlStr = get_business_query(sqlStr);
                        execute_query(sqlStr, addBusiness);

                        break;

                    case 2:
                        // Updates business results (QUERY STRING CHANGED - 4/25/2022 BY EMMA AND CONNOR)
                        sqlStr += " ORDER BY y_tip_count DESC";
                        sqlStr = get_business_query(sqlStr);
                        execute_query(sqlStr, addBusiness);

                        break;

                    case 3:
                        // Updates business results (QUERY STRING CHANGED - 4/25/2022 BY EMMA AND CONNOR)
                        sqlStr += " ORDER BY y_checkin_count DESC";
                        sqlStr = get_business_query(sqlStr);
                        execute_query(sqlStr, addBusiness);

                        break;

                    case 4:
                        // Updates business results (QUERY STRING CHANGED - 4/25/2022 BY EMMA AND CONNOR)
                        sqlStr = get_business_query(sqlStr);
                        execute_query(sqlStr, addBusiness);

                        break;
                }

                return;
            }

            business_grid.Items.Clear();

            sqlStr = string.Empty;

            if (state_list.SelectedItem == null || City_list_box.SelectedItem == null || Zipcode_list_box.SelectedItem == null)
            {
                return;
            }

            switch (sort_list.SelectedIndex)
            {
                case 0:

                    // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
                    sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE y_state = '"
                        + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                        + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString()
                        + "' ORDER BY y_business_name");
                    execute_query(sqlStr, addBusiness);

                    break;

                case 1:

                    // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
                    sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE y_state = '"
                        + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                        + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString()
                        + "' ORDER BY y_rating DESC");
                    execute_query(sqlStr, addBusiness);

                    break;

                case 2:
                    // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
                    sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE y_state = '"
                        + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                        + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString()
                        + "' ORDER BY y_tip_count DESC");
                    execute_query(sqlStr, addBusiness);

                    break;

                case 3:
                    // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
                    sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE y_state = '"
                        + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                        + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString()
                        + "' ORDER BY y_checkin_count DESC");
                    execute_query(sqlStr, addBusiness);

                    break;

                case 4:
                    // Updates business results (QUERY STRING CHANGED - 4/22/2022 BY REAGAN)
                    sqlStr = get_business_query("SELECT y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, y_business_id, y_latitude, y_longitude FROM business WHERE y_state = '"
                        + state_list.SelectedItem.ToString() + "' AND y_city = '" + City_list_box.SelectedItem.ToString()
                        + "' AND y_zipcode = '" + Zipcode_list_box.SelectedItem.ToString()
                        + "' ORDER BY y_business_name");
                    execute_query(sqlStr, addBusiness);

                    break;
            }
        }

        private void show_tips_btn_Click(object sender, RoutedEventArgs e)
        {
            if (cur_user_list_box.SelectedItem == null || business_grid.SelectedItem == null)
            {
                return;
            }

            string selected_y_user_id = cur_user_list_box.SelectedItem.ToString();

            Business cur_biz = (Business)business_grid.SelectedItem;
            string selected_y_business_id = cur_biz.business_id;

            TipWindow tipWindow = new TipWindow(selected_y_user_id, selected_y_business_id);
            tipWindow.Show();
        }

        private void show_checkins_btn_Click(object sender, RoutedEventArgs e)
        {
            Business cur_biz = (Business)business_grid.SelectedItem;
            string selected_y_business_id = cur_biz.business_id;

            Form1 checkinWindow = new Form1(selected_y_business_id);

            checkinWindow.Show();
        }

       /************************************************************************************
       * Name:           edit_lat_long_btn_Click()
       * Behavior:       Allows user to edit the location coordinates of given user
       * Last Changed:   4/22/2022
       * Changed By:     Reagan Kelley
       * Change Details: Initial implementation
       ************************************************************************************/
        private void edit_lat_long_btn_Click(object sender, RoutedEventArgs e)
        {
            // Only edit location coordinates if user is selected
            if (cur_user_list_box.SelectedIndex > -1)
            {
                u_lat_tbox.IsEnabled = true;
                u_long_tbox.IsEnabled = true;
            }

        }

        /************************************************************************************
        * Name:           update_lat_long_btn_Click()
        * Behavior:       Updates the location coordinates of the selected user
        * Last Changed:   4/22/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void update_lat_long_btn_Click(object sender, RoutedEventArgs e)
        {
            // Only update location coordinates if user is selected and edit button had been clicked
            if ((cur_user_list_box.SelectedIndex > -1) && u_lat_tbox.IsEnabled == true)
            {

                u_lat_tbox.IsEnabled = false;
                u_long_tbox.IsEnabled = false;

                string selected_y_user_id = cur_user_list_box.SelectedItem.ToString();
                double new_latitude = 0.0f;
                double new_longitude = 0.0f;

                new_latitude = Convert.ToDouble(u_lat_tbox.Text);
                new_longitude = Convert.ToDouble(u_long_tbox.Text);

                //sql update users location coordinates
                string sqlStr = "UPDATE Users SET y_latitude = " + new_latitude + ", y_longitude = " + new_longitude + "WHERE y_user_id = '" + selected_y_user_id + "'; " +
                                 "SELECT distinct y_longitude, y_latitude FROM Users WHERE y_user_id = '" + selected_y_user_id + "';";
                execute_query(sqlStr, addLocation); // redisplay on UI for verification (text should not change)
            }
        }

        /************************************************************************************
        * Name:           price_range_Checked()
        * Behavior:       Updates the business grid with price range filter
        * Last Changed:   4/26/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void price_range_Checked(object sender, RoutedEventArgs e)
        {

            if (state_list.SelectedItem == null || City_list_box.SelectedItem == null || Zipcode_list_box.SelectedItem == null)
            {
                return;
            }

            bool[] selected = new bool[4];
            if ((bool)price_1.IsChecked) { selected[0] = true; } else { selected[0] = false; }
            if ((bool)price_2.IsChecked) { selected[1] = true; } else { selected[1] = false; }
            if ((bool)price_3.IsChecked) { selected[2] = true; } else { selected[2] = false; }
            if ((bool)price_4.IsChecked) { selected[3] = true; } else { selected[3] = false; }

            if((selected[0] || selected[1] || selected[2] || selected[3]))
            {


                string price_str = "SELECT DISTINCT business_grid.y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, business_grid.y_business_id, haversine " +
                    "FROM business_grid, Attribute WHERE business_grid.y_business_id = Attribute.y_business_id " +
                    "AND y_attribute_name = 'RestaurantsPriceRange2' AND (";
                bool or = false;

                // build where conditions for selected price ranges
                if (selected[0]) { price_str += "y_attribute_value = '1'"; or = true; }
                if (selected[1]) { if (or) { price_str += " OR "; } price_str += "y_attribute_value = '2'"; or = true; }
                if (selected[2]) { if (or) { price_str += " OR "; } price_str += "y_attribute_value = '3'"; or = true; }
                if (selected[3]) { if (or) { price_str += " OR "; } price_str += "y_attribute_value = '4'"; or = true; }
                price_str += ")";

                // will apply new filters to existing business grid results
                update_business_grid(price_str);
            }
        }

        /************************************************************************************
        * Name:           attribute_Clicked()
        * Behavior:       Updates the business grid with attribute filters
        * Last Changed:   4/26/2022
        * Changed By:     Reagan Kelley
        * Change Details: Initial implementation
        ************************************************************************************/
        private void attribute_Clicked(object sender, RoutedEventArgs e)
        {
            if (state_list.SelectedItem == null || City_list_box.SelectedItem == null || Zipcode_list_box.SelectedItem == null)
            {
                return;
            }

            Dictionary<string, bool> attributes = new Dictionary<string, bool>();

            // Get the attributes selected
            attributes.Add("Accepts Credit Cards", (bool)att_credit_c.IsChecked);
            attributes.Add("Bike Parking", (bool)att_bike.IsChecked);
            attributes.Add("Delivery", (bool)att_deliv.IsChecked);
            attributes.Add("Free Wi-Fi", (bool)att_free_wifi.IsChecked);
            attributes.Add("Good for Groups", (bool)att_groups.IsChecked);
            attributes.Add("Good for Kids", (bool)att_kids.IsChecked);
            attributes.Add("Outdoor Seating", (bool)att_outdoor.IsChecked);
            attributes.Add("Take Out", (bool)att_take_out.IsChecked);
            attributes.Add("Takes Reservations", (bool)att_reservation.IsChecked);
            attributes.Add("Wheelchair Accesible", (bool)att_wheelchair.IsChecked);
            attributes.Add("Breakfast", (bool)meal_breakfast.IsChecked);
            attributes.Add("Lunch", (bool)meal_lunch.IsChecked);
            attributes.Add("Brunch", (bool)meal_brunch.IsChecked);
            attributes.Add("Dinner", (bool)meal_dinner.IsChecked);
            attributes.Add("Dessert", (bool)meal_dessert.IsChecked);
            attributes.Add("Late Night", (bool)meal_late_night.IsChecked);

            string sqlStr = "SELECT business_grid.y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, business_grid.y_business_id, haversine " +
                    "FROM business_grid, Attribute WHERE business_grid.y_business_id = Attribute.y_business_id ";
            bool or = false;
            bool and = true;

            int attribute_count = -1;

            // Build sql query for selected attributes
            if (attributes["Accepts Credit Cards"]) { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'BusinessAcceptsCreditCards'"; }
            if (attributes["Bike Parking"])         { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'BikeParking'"; }
            if (attributes["Delivery"])             { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'RestaurantsDelivery'"; }
            if (attributes["Free Wi-Fi"])           { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'WiFi'"; }
            if (attributes["Good for Groups"])      { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'RestaurantsGoodForGroups'"; }
            if (attributes["Good for Kids"])        { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'GoodForKids'"; }
            if (attributes["Outdoor Seating"])      { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'OutdoorSeating'"; }
            if (attributes["Take Out"])             { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'RestaurantsTakeOut'"; }
            if (attributes["Takes Reservations"])   { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'RestaurantsReservations'"; }
            if (attributes["Wheelchair Accesible"]) { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'WheelchairAccessible'"; }
            if (attributes["Breakfast"])            { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'breakfast'"; }
            if (attributes["Lunch"])                { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'lunch'"; }
            if (attributes["Brunch"])               { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'brunch'"; }
            if (attributes["Dinner"])               { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'dinner'"; }
            if (attributes["Dessert"])              { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'dessert'"; }
            if (attributes["Late Night"])           { attribute_count += 1; if (and) { sqlStr += " AND y_attribute_name in ("; and = false; } if (or) { sqlStr += ", "; } else { or = true; } sqlStr += "'latenight'"; }

            if (!and) { sqlStr += ") AND y_attribute_value = 'True'"; }

            sqlStr += " GROUP BY (business_grid.y_business_name, y_address, y_city, y_state, y_rating, y_tip_count, y_checkin_count, business_grid.y_business_id, haversine)" +
                " HAVING COUNT(business_grid.y_business_name) >= " + (attribute_count).ToString();

            sqlStr += ";";

            Debug.WriteLine(sqlStr);

            // will apply new filters to existing business grid results
            update_business_grid(sqlStr);

            //business_grid.Items.Clear();
            //execute_query(sqlStr, addBusiness);
        }


        /* private void comment_text_box_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.Key == Key.Return)
             {
                 if (business_grid.SelectedItem != null)
                 {
                     string tip_text = comment_text_box.Text;
                     comment_text_box.Text = "Tip entered: " + tip_text;

                 }

             }*/
        //Can hardcode user in. 
        /*using (var connection = new NpgsqlConnection(buildConnectionString()))
        {
            connection.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO TIP (y_date, y_tip, FROM business ORDER BY y_state";
                try
                {

                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
<<<<<<< Updated upstream
                }
                finally
                {
                    connection.Close();
                }
=======
                }
                finally
                {
                    connection.Close();
                }
>>>>>>> Stashed changes
            }



        }
*/

    }

    
}



/*WHERE business_id 
    IN (SELECT business_id FROM Catery WHERE cname = '')
AND IN(SELECT business_id FROM C WHERE cname = '')*/
