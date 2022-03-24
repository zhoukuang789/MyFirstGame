using game;
using UnityEngine;

namespace com
{
    public class StartGameService : MonoBehaviour
    {
        public static StartGameService instance;

        void Awake()
        {
            instance = this;
        }

        //checkUpdate/assetbundle
        void Start()
        {

        }


        //once only
        public void LoginFinishStartGame()
        {
        }
    }
}