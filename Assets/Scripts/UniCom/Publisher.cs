using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UniCom
{
    public class Publisher<T>
    {
        public Publisher(string topic)
        {
            _topic = topic;
            _topic_initialized = true;
        }

        public Publisher()
        {
            _topic = null;
            _topic_initialized = false;
        }

        public void SetTopic(string topic)
        {
            if (!_topic_initialized)
            {
                _topic = topic;
                _topic_initialized = true;
            }
            else
            {
                Debug.LogWarning("Topic name was tried overwrited. Please chekc your code.");
            }
        }

        public void Publish(T data)
        {
            if(!_topic_initialized)
            {
                return;
            }
            if(string.IsNullOrEmpty(_topic))
            {
                Debug.LogWarning("Topic name is not valid.");
                return;
            }
            ComData<T> com_data = new ComData<T>(_topic,data);
            MessageBroker.Default.Publish<ComData<T>>(com_data);
        }

        private string _topic;
        private bool _topic_initialized = false;
    }
}