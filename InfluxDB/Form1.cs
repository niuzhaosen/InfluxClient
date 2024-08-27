namespace InfluxDB
{
    public partial class Form1 : Form
    {
        InfluxDBHelper InfluxDBHelper = new InfluxDBHelper();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int z = 0;
            string s = "test2,name=niu,name2=sen,name3=zhao v2=" + (z + 100).ToString() + "\n";

            while (z < 100)
            {
                z++;
                s += "test2,name=niu,name2=sen,name3=zhao" + z + " v2=" + (z + 100).ToString() + "\n";

                // Thread.Sleep(100);
            }
            InfluxDBHelper.Write(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "SELECT * from Test3";
            var x = InfluxDBHelper.Query(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            InfluxClient.InfluxClient influxClient = new InfluxClient.InfluxClient(
                "bZeUVk5A1kc-riK5XRhPBE4aI7FiJ625VdB2ymMF5lRadV0xiiCkpFr71567nqGnolt9fxq3z8JC2MDGKas6Sw==",
                "127.0.0.1",
                8086,
                "test1",
                "test1");
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("tag1", "±Í«©");
            Dictionary<string, InfluxClient.InfluxClient.Field> keyValuePairs1 = new Dictionary<string, InfluxClient.InfluxClient.Field>();
            keyValuePairs1.Add("v1", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.Integer, Value = -1 });
            keyValuePairs1.Add("v2", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.Float, Value = 1.22 });
            keyValuePairs1.Add("v3", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.UInteger, Value = 1 });
            keyValuePairs1.Add("v4", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.String, Value = "≤‚ ‘" });
            influxClient.Write("Test3", keyValuePairs, keyValuePairs1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InfluxClient.InfluxClient influxClient = new InfluxClient.InfluxClient(
               "bZeUVk5A1kc-riK5XRhPBE4aI7FiJ625VdB2ymMF5lRadV0xiiCkpFr71567nqGnolt9fxq3z8JC2MDGKas6Sw==",
               "127.0.0.1",
               8086,
               "test1",
               "test1");

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("tag1", "±Í«©");
            Dictionary<string, InfluxClient.InfluxClient.Field> keyValuePairs1 = new Dictionary<string, InfluxClient.InfluxClient.Field>();
            keyValuePairs1.Add("v1", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.Integer, Value = -1 });
            keyValuePairs1.Add("v2", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.Float, Value = 1.22 });
            keyValuePairs1.Add("v3", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.UInteger, Value = 1 });
            keyValuePairs1.Add("v4", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.String, Value = "≤‚ ‘" });

            Dictionary<string, string> keyValuePairs11 = new Dictionary<string, string>();
            keyValuePairs11.Add("tag1", "±Í«©2");
            Dictionary<string, InfluxClient.InfluxClient.Field> keyValuePairs111 = new Dictionary<string, InfluxClient.InfluxClient.Field>();
            keyValuePairs111.Add("v1", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.Integer, Value = -1 });
            keyValuePairs111.Add("v2", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.Float, Value = 1.22 });
            keyValuePairs111.Add("v3", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.UInteger, Value = 1 });
            keyValuePairs111.Add("v4", new InfluxClient.InfluxClient.Field() { Type = InfluxClient.InfluxClient.FieldType.String, Value = "≤‚ ‘" });
            influxClient.Write(new List<string>() { "Test3", "Test4" }, new List<Dictionary<string, string>>() { keyValuePairs, keyValuePairs11 }, new List<Dictionary<string, InfluxClient.InfluxClient.Field>>() { keyValuePairs1, keyValuePairs111 });
        }
    }
}
