using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{

    public class HPitem : MonoBehaviour
    {
        public GameObject Players; //仮の親オブジェク
        float timer;
        bool isGet;

        // Use this for initialization
        void Start()
        {
            isGet = false;
            timer = 0.17f; //効果音の長さによって
            GoRandom();
        }

        // Update is called once per frame
        void Update()
        {
            if (isGet)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Start();
                    gameObject.SetActive(false); //自体破壊
                }
            }
        }

        void OnTriggerEnter(Collider hit) //Unity内蔵の当たり判定
        {
            if (hit.gameObject.name == "Player1") //プレイヤー1が貰ったら
            {
                isGet = true;
                gameObject.GetComponent<AudioSource>().Play();
                Players.transform.GetChild(0).gameObject.GetComponent<TankHealth>().TakeHealer();
            }
            if (hit.gameObject.name == "Player2") //プレイヤー2が貰ったら
            {
                isGet = true;
                gameObject.GetComponent<AudioSource>().Play();
                Players.transform.GetChild(1).gameObject.GetComponent<TankHealth>().TakeHealer();
            }
        }

        void GoRandom() //五つアイテムのランダム位置に生成する
        {
            int r = Random.Range(0, 6);

            switch (r)
            {
                case 0:
                    gameObject.transform.localPosition = new Vector3(-17, 1, 1);
                    break;
                case 1:
                    gameObject.transform.localPosition = new Vector3(-28, 1, 23);
                    break;
                case 2:
                    gameObject.transform.localPosition = new Vector3(26, 1, -5);
                    break;
                case 3:
                    gameObject.transform.localPosition = new Vector3(10, 1, 42);
                    break;
                case 4:
                    gameObject.transform.localPosition = new Vector3(-6, 1, -15);
                    break;
                case 5:
                    gameObject.transform.localPosition = new Vector3(26, 1, 18);
                    break;
            }
        }
    }
}
