using System;
using UnityEngine;

namespace GameManager {
    public class Timer {
        public string TimerName { get; private set; }
        public bool IsRuning { get; private set; }
        private float m_CurTime;
        private int m_CurLoop;
        private float m_Interval;
        private int m_Loop;

        public Action OnStartAction { get; private set; }
        public Action<int> OnLoopAction { get; private set; }
        public Action OnComplectAction { get; private set; }
    
        public void Init(Action startAction = null, Action<int> loopAction = null,
            Action completeAction = null, string name = null, int loop = 0, float interval = 0)
        {
            TimerName = name;
            m_Loop = loop;
            m_Interval = interval;
            OnStartAction = startAction;
            OnLoopAction = loopAction;
            OnComplectAction = completeAction;
        }

        public void Start()
        {
            TimerManager.instance.RegisterTimer(this);
            m_CurTime = Time.time;
            IsRuning = true;
            OnStartAction?.Invoke();
        }

        public void OnUpdate()
        {
            if (!IsRuning)
                return;
            if (Time.time > m_CurTime)
            {
                m_CurTime += m_Interval;
                m_CurLoop++;
                OnLoopAction?.Invoke(m_CurLoop);
                if (m_CurLoop > 0)
                {
                    if (m_CurLoop > m_Loop)
                    {
                        Stop();
                    }
                }
            }
        }

        public void Stop()
        {
            OnComplectAction?.Invoke();
            IsRuning = false;
            TimerManager.instance.RemoveTimer(this);
        }
    }
}