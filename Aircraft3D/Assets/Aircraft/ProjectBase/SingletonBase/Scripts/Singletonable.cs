namespace ProjectBase.SingletonBase {

    /// <summary>
    /// 单例模式基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singletonable<T> where T : new() {

        private static T instance;

        public static T GetInstance() {
            if (instance == null) {
                instance = new T();
            }

            return instance;
        }

        public static void SetInstance(T t)
        {
            instance = t;
        }
    }
}