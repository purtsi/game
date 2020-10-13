using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : Singleton<GameManager>
    {
        public static void Initialize()
        {
            //Debug.Log(Instance.name);
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("GameManager: Start");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(SceneManager.GetActiveScene().name == "main")
                SceneManager.LoadScene("test");
                else
                    SceneManager.LoadScene("main");
            }
        }
    }
}