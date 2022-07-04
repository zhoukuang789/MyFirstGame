using System;
using System.Collections.Generic;
using Airplane;
using ProjectBase.SingletonBase;

namespace Record {
    
    /// <summary>
    /// 玩家击杀记录
    /// </summary>
    public class RecordService : Singletonable<RecordService> {
        
        /// <summary>
        /// 击杀记录
        /// </summary>
        private Dictionary<PlaneType, int> killRecordMap = new Dictionary<PlaneType, int>();
        
        /// <summary>
        /// 当击杀记录变化时广播的事件
        /// </summary>
        private event Action<Dictionary<PlaneType, int>> killRecordChangeEvent;
        
        /// <summary>
        /// 根据敌军类型获取击杀记录
        /// </summary>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        public int GetKillRecord(PlaneType enemyType) {
            if (!killRecordMap.ContainsKey(enemyType)) {
                killRecordMap.Add(enemyType, 0);
            }

            return killRecordMap[enemyType];
        }
        
        /// <summary>
        /// 添加击杀记录
        /// </summary>
        /// <param name="enemyType"></param>
        public void AddKillRecord(PlaneType enemyType) {
            if (!killRecordMap.ContainsKey(enemyType)) {
                killRecordMap.Add(enemyType, 0);
            }
            killRecordMap[enemyType]++;
            if (killRecordChangeEvent != null) killRecordChangeEvent(killRecordMap);
        }

        /// <summary>
        /// 订阅当击杀记录变化时广播的事件
        /// </summary>
        /// <param name="action"></param>
        public void AddKillRecordChangeEventListener(Action<Dictionary<PlaneType, int>> action) {
            killRecordChangeEvent += action;
        }
        
        public void RemoveKillRecordChangeEventListener(Action<Dictionary<PlaneType, int>> action) {
            killRecordChangeEvent -= action;
        }

        

    }
}