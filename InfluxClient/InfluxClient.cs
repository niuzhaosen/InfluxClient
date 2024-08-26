namespace InfluxClient
{
    public class InfluxClient
    {
        public string Token { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Organization { get; set; }
        public string Bucket { get; set; }
        HttpRequest HttpRequest = new HttpRequest();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口</param>
        /// <param name="organization">组织</param>
        /// <param name="bucket">桶</param>
        public InfluxClient(string token, string ip, int port, string organization, string bucket)
        {
            Token = token;
            Ip = ip;
            Port = port;
            Organization = organization;
            Bucket = bucket;
        }
        public void Read()
        {

        }
        public void Write(string measurement, Dictionary<string, string> tags, Dictionary<string, Field> fields, long timestamp = 0)
        {
            if (timestamp == 0)
                timestamp = GetTimeStamp();
            string writeStr = "";
            writeStr += measurement;
            foreach (var tag in tags)
            {
                writeStr += "," + tag.Key + "=" + tag.Value;
            }
            writeStr += " ";
            int fieldrow = 0;
            foreach (var field in fields)
            {
                if (fieldrow != 0)
                    writeStr += ",";
                switch (field.Value.Type)
                {
                    case FieldType.Integer: writeStr += field.Key + "=" + field.Value.Value.ToString() + "i"; break;
                    case FieldType.UInteger: writeStr += field.Key + "=" + field.Value.Value.ToString()+"u"; break; 
                    case FieldType.Float: writeStr += field.Key + "=" + field.Value.Value.ToString(); break;
                    case FieldType.String: writeStr += field.Key + "=\"" + field.Value.Value.ToString() + "\""; break;

                }
                fieldrow++;
            }
            writeStr += " ";
            writeStr += timestamp.ToString();
            HttpRequest.HttpPost( string.Format("http://{0}:{1}/api/v2/write?org={2}&bucket={3}&precision=ns",Ip,Port,Organization,Bucket), Token, writeStr);
        }
        private long GetTimeStamp()
        {
            DateTimeOffset epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            TimeSpan timeSpan = DateTimeOffset.UtcNow - epoch;
            return (long)timeSpan.TotalMilliseconds * 1000 * 1000;
        }
        public class Field
        {
            public dynamic Value { get; set; }
            public FieldType Type { get; set; }
        }
        public enum FieldType
        {
            String,
            Float,
            Integer,
            UInteger
        }
    }
}
