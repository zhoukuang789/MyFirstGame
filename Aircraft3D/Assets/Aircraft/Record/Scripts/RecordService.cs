using System;
using System.Collections.Generic;
using ProjectBase.SingletonBase;

namespace Record {
    
    /// <summary>
    /// 玩家击杀记录
    /// </summary>
    public class RecordService : Singletonable<RecordService> {
        
        private Dictionary<string, int> killRecordMap = new Dictionary<string, int>();
        public int GetKillRecord(string enemyType) {
            if (!killRecordMap.ContainsKey(enemyType)) {
                killRecordMap.Add(enemyType, 0);
            }

            return killRecordMap[enemyType];
        }

        private event Action<Dictionary<string, int>> killRecordChangeEvent;
        public void AddKillRecordChangeEventListener(Action<Dictionary<string, int>> action) {
            killRecordChangeEvent += action;
        }

        public void AddKillRecord(string enemyType) {
            if (!killRecordMap.ContainsKey(enemyType)) {
                killRecordMap.Add(enemyType, 0);
            }
            killRecordMap[enemyType]++;
            if (killRecordChangeEvent != null) killRecordChangeEvent(killRecordMap);
        }

    }
}