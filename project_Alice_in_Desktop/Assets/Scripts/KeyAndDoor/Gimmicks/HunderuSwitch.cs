using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class HunderuSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject player; // プレイヤーを取得
        [SerializeField] private GameObject gimmick; // ギミックを取得
        private bool addSwitch; // スイッチのON、OFF
        private bool stayFlg;   // 滞在しているかいないか

        void Start()
        {
            addSwitch = false;
            stayFlg = false;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
            {
                stayFlg = true; // 滞在している
                if (stayFlg == true)
                {
                    IHitSwitch hitGimmick = gimmick.GetComponent<IHitSwitch>();
                    if (hitGimmick != null)
                    {
                        addSwitch = true; // スイッチON
                        hitGimmick.Switch(addSwitch); // Switchメソッドにfalseを渡す
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false; // 滞在していない
                if (stayFlg == false)
                {
                    IHitSwitch hitGimmick = gimmick.GetComponent<IHitSwitch>();
                    if (hitGimmick != null)
                    {
                        addSwitch = false; // スイッチOFF
                        hitGimmick.Switch(addSwitch); // Switchメソッドにfalseを渡す
                    }
                }
            }
        }
    }
}
