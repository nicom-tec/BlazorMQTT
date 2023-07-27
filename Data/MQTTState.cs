using System.Diagnostics;

namespace BlazorMQTT.Data
{
    public class MQTTState
    {

        public static List<MQTTBranch> MQTTTree = new List<MQTTBranch>();

        public void addEntry(string _longpath, string _value)
        {
            string[] topicSequence = _longpath.Split('/');
            int d = 0;
            foreach (Entry entry in entries)
            {
                if (entry.topic = topicSequence[d])
                {

                }
            }
        }
        public class MQTTBranch
        {
            public List<Entry> EntryBranch = new List<Entry>();

            public MQTTBranch(List<Entry> _EntryBranch)
            {
                this.EntryBranch = _EntryBranch;
            }
        }




        public class Entry
        {
            public string topic;
            public string? value;
            public List<Entry>? entry;


            public Entry(string _topic, string _value, List<Entry> _entry)
            {
                this.topic = _topic;
                this.value = _value;
                this.entry = _entry;
            }
            public Entry(string _topic, string _value)
            {
                this.topic = _topic;
                this.value = _value;

            }
            public Entry(string _topic)
            {
                this.topic = _topic;
            }
            public void addEntry(List<Entry> _entry)
            {
                if (this.entry == null)
                {
                    this.entry = new List<Entry>();
                }
                this.entry.AddRange(_entry);
            }
            public void addEntry(Entry _entry)
            {
                this.addEntry(new List<Entry>() { _entry });
            }
        }
    }
}
