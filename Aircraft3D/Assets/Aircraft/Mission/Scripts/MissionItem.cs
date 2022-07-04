using System;

namespace Mission {
    public class MissionItem {

        private string name;

        private string description;

        private Action onStart;
        private Action onFail;
        private Action onComplete;

        private MissionStatus status;

        private int totalProgress;
        private int currentProgress = 0;

        /// <summary>
        /// 进度更新事件，当进度有更新时，广播此事件
        /// </summary>
        private event Action progressUpdateEvent;


        public string GetName() {
            return name;
        }
        public MissionItem SetName(string Name) {
            this.name = name;
            return this;
        }

        public string GetDescription() {
            return description;
        }
        public MissionItem SetDescription(string description) {
            this.description = description;
            return this;
        }

        public Action GetOnStart() {
            return onStart;
        }
        public MissionItem SetOnStart(Action onStart) {
            this.onStart += onStart;
            return this;
        }

        public Action GetOnFail() {
            return onFail;
        }
        public MissionItem SetOnFail(Action onFail) {
            this.onFail += onFail;
            return this;
        }

        public Action GetOnComplete() {
            return onComplete;
        }
        public MissionItem SetOnComplete(Action onComplete) {
            this.onComplete += onComplete;
            return this;
        }

        public MissionStatus GetStatus() {
            return status;
        }
        public MissionItem SetStatus(MissionStatus status) {
            this.status = status;
            return this;
        }

        public int GetTotalProgress() {
            return totalProgress;
        }
        public MissionItem SetTotalProgress(int totalProgress) {
            this.totalProgress = totalProgress;
            return this;
        }
        
        public int GetCurrentProgress() {
            return currentProgress;
        }
        public void UpdateCurrentProgress() {
            currentProgress++;
            if (progressUpdateEvent != null) {
                progressUpdateEvent();
            }
        }

        public void AddProgressUpdateEventListener(Action action) {
            progressUpdateEvent += action;
        }

        public void RemoveProgressUpdateEventListener(Action action) {
            progressUpdateEvent -= action;
        }

    }
}