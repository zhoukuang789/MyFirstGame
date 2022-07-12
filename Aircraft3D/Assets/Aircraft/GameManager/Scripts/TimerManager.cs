using System.Collections.Generic;
using UnityEngine;

namespace GameManager {
    public class TimerManager: MonoBehaviour {
    public static TimerManager instance { get; private set; }
    private Queue<Timer> timerQueue;
    private LinkedList<Timer> m_TimerList;

    private void Awake() {
        instance = this;
        m_TimerList = new LinkedList<Timer>();
        timerQueue = new Queue<Timer>();
    }

    void Update() {
        for (LinkedListNode<Timer> cur = m_TimerList.First; cur != null; cur = cur.Next) {
            cur.Value.OnUpdate();
        }
    }

    private void OnDestroy() {
        Dispose();
    }

    public void RegisterTimer(Timer timer) {
        m_TimerList.AddLast(timer);
    }

    public void RemoveTimer(Timer timer) {
        m_TimerList.Remove(timer);
        timerQueue.Enqueue(timer);
    }

    public void Dispose() {
        m_TimerList.Clear();
    }

    public Timer GetTimer() {
        Timer timer;
        if (timerQueue.Count > 0) {
            timer = timerQueue.Dequeue();
        } else {
            timer = new Timer();
        }

        return timer;
    }
    }
}