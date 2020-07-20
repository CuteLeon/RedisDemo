namespace RedisDemo.ViewModels
{
    public class CounterViewModel
    {
        public CounterViewModel(string connectID, int streamID, int count)
        {
            ConnectID = connectID;
            StreamID = streamID;
            Count = count;
        }

        public string ConnectID { get; set; }

        public int StreamID { get; set; }

        public int Count { get; set; }
    }
}
