using System.Diagnostics;
using static BlazorMQTT.Data.MQTTState;

namespace BlazorMQTT.Data
{
    public class MQTTState
    {

        public static List<Entry> MQTTTree = new List<Entry>();
        public static event Action OnChange;
        private static void NotifyStateChanged() => OnChange?.Invoke();

        public static void addEntry(string _longpath, string _value)
        {
            string[] topicSequence = _longpath.Split('/');
            foreach (Entry entry in MQTTTree)
            {
                if (string.Join('/',entry.topicSequence) == _longpath)
                {
                    entry.value = _value;
                    entry.NotifyStateChanged();
                    return;
                }
            }
            MQTTTree.Add(new Entry(_longpath, _value));
            MQTTTree.Sort((a,b) => string.Join('/', a.topicSequence).CompareTo(string.Join('/', b.topicSequence)));
            NotifyStateChanged();
        }




        //public class MQTTBranch
        //{
        //    public List<Entry> EntryBranch = new List<Entry>();

        //    public MQTTBranch(List<Entry> _EntryBranch)
        //    {
        //        this.EntryBranch = _EntryBranch;
        //    }
        //}




        public class Entry
        {
            public string[] topicSequence;
            public string? value;

            public event Action OnChange;


            public Entry(string _topic, string _value)
            {
                this.topicSequence = _topic.Split('/');
                this.value = _value;


            }

            public Entry(string _topic)
            {
                this.topicSequence = _topic.Split('/');
            }

            public void NotifyStateChanged() => this.OnChange?.Invoke();

        }
    }
}
