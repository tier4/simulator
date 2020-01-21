using System.Collections;
using System.Collections.Generic;

namespace UniCom
{
    public class ComData<T>
    {
        public ComData(string topic, T data)
        {
            _topic = topic;
            _hash = typeof(T).GetHashCode();
            _data = data;
            return;
        }

        public int hash
        {
            get { return _hash; }
        }

        public string topic
        {
            get { return _topic; }
        }

        public T data
        {
            get { return _data; }
        }

        private int _hash;
        private string _topic;
        private T _data;
    }
}