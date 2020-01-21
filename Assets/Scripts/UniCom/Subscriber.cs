using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace UniCom
{
    public class Subscriber<T>
    {
        public Subscriber(string topic, Action<T> func)
        {
            _topic_initialized = true;
            _topic = topic;
            _func = func;
            MessageBroker.Default.Receive<ComData<T>>().Subscribe(
                x =>
                {
                    if(x.hash != typeof(T).GetHashCode())
                    {
                        Debug.LogError("Hashcode does not match. Chekck data type");
                        return;
                    }
                    if(x.topic == _topic)
                    {
                        _func(x.data);
                    }
                }
                );
            return;
        }

        public Subscriber(Action<T> func)
        {
            _func = func;
            _topic_initialized = false;
        }

        public void SetTopic(string topic)
        {
            if(!_topic_initialized)
            {
                _topic = topic;
                _topic_initialized = true;
                MessageBroker.Default.Receive<ComData<T>>().Subscribe(
                    x =>
                    {
                        if (x.hash != typeof(T).GetHashCode())
                        {
                            Debug.LogError("Hashcode does not match. Chekck data type");
                            return;
                        }
                        if (x.topic == _topic)
                        {
                            _func(x.data);
                        }
                    }
                    );
            }
        }

        private string _topic;
        private ComData<T> _data;
        private Action<T> _func;
        private bool _topic_initialized = false;
    }
}