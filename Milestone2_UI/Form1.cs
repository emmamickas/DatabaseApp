using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Milestone1_Dellwo
{
    public partial class Form1 : Form
    {
        Series checkinSeries;

        Dictionary<int, int> dateCounts;

        string businessId = string.Empty;

        public class CheckinMonth
        {
            public int checkin_count { get; set; }

            public int checkin_date { get; set; }
        }

        public Form1(string businessId)
        {
            this.businessId = businessId;

            InitializeComponent();

            loadCheckins();
        }

        private void loadCheckins()
        {
            Checkin_Chart.Series.Clear();

            if (Checkin_Chart.Titles.Count < 1)
            {
                Checkin_Chart.Titles.Add("Checkins Per Month");
            }

            checkinSeries = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Checkins",
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            //this.Checkin_Chart.Legends.Add("# of Checkins");

            dateCounts = new Dictionary<int, int>();

            dateCounts[1] = 0;
            dateCounts[2] = 0;
            dateCounts[3] = 0;
            dateCounts[4] = 0;
            dateCounts[5] = 0;
            dateCounts[6] = 0;
            dateCounts[7] = 0;
            dateCounts[8] = 0;
            dateCounts[9] = 0;
            dateCounts[10] = 0;
            dateCounts[11] = 0;
            dateCounts[12] = 0;

            string y_business_id = this.businessId;

            if (y_business_id != null)
            {
                string sqlStr = "SELECT date_part('month',y_check_in_time), COUNT(y_business_id)"
                    + " FROM check_in"
                    + " WHERE y_business_id = '" + y_business_id.ToString()
                    + "' GROUP BY date_part('month', y_check_in_time)";

                execute_query(sqlStr, addCheckinData);
            }

            checkinSeries.Points.AddXY("January", dateCounts[1]);
            checkinSeries.Points.AddXY("February", dateCounts[2]);
            checkinSeries.Points.AddXY("March", dateCounts[3]);
            checkinSeries.Points.AddXY("April", dateCounts[4]);
            checkinSeries.Points.AddXY("May", dateCounts[5]);
            checkinSeries.Points.AddXY("June", dateCounts[6]);
            checkinSeries.Points.AddXY("July", dateCounts[7]);
            checkinSeries.Points.AddXY("August", dateCounts[8]);
            checkinSeries.Points.AddXY("September", dateCounts[9]);
            checkinSeries.Points.AddXY("October", dateCounts[10]);
            checkinSeries.Points.AddXY("November", dateCounts[11]);
            checkinSeries.Points.AddXY("December", dateCounts[12]);

            this.Checkin_Chart.Series.Add(checkinSeries);
        }

        private void addCheckinData(NpgsqlDataReader R)
        {
            CheckinMonth checkinMonth = new CheckinMonth() { checkin_date = (int)R.GetDouble(0), checkin_count = (int)R.GetInt32(1) };

            dateCounts[checkinMonth.checkin_date] += checkinMonth.checkin_count;
        }

        private void addCheckin(NpgsqlDataReader R)
        {
            loadCheckins();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2test; password = Password";
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

        private void Checkin_Button_Click(object sender, EventArgs e)
        {
            string y_business_id = this.businessId;

            Console.WriteLine("Checkin count before: " + dateCounts[4]);

            if (y_business_id != null)
            {
                string time = DateTime.Now.ToString("MM-dd-yyyy h:mm:ss");
                string sqlStr = "INSERT INTO Check_in (y_business_id, y_check_in_time) VALUES ( '" + y_business_id + "','" + time + "')";

                execute_query(sqlStr, addCheckin);

                loadCheckins();
            }

            Console.WriteLine("Checkin count after: " + dateCounts[4]);
        }
    }
}
