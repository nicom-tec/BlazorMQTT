using System.Diagnostics;
using static BlazorMQTT.Data.MQTTState;

namespace BlazorMQTT.Data
{
    public class MQTTState
    {

        public static List<Entry> MQTTTree = new List<Entry>();

        public unsafe void addEntry(string _longpath, string _value)
        {
            string[] topicSequence = _longpath.Split('/');
            int d = 0;
            List<Entry>* intermediateRoot = &MQTTTree;
            List<Entry>* root = intermediateRoot;
            foreach (Entry entry in *root)
            {
                
                if (entry.topic == topicSequence[d])
                {
                    root = &root->entry;
                    
                    
                }
                }
            }
        }

        public unsafe void addEntryBranch(Entry* rootBranch)
        {
            foreach(Entry entry in rootBranch->entry)
            {
                if(entry.topic == rootBranch->topic)
                {
                    rootBranch->addEntry(rootBranch->entry);
                }
            }
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
