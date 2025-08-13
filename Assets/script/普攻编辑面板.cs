using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 普攻编辑面板 : MonoBehaviour
{
    public Main main;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        NotificationCenter.DefaultCenter.AddObserver(this,"选普攻动作");
    }
    private void OnDisable()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this,"选普攻动作");
    }
    void 选普攻动作(Notification data)
    {
        string[] ls = (data.data + "").Split("^");
        main.文本容器["普攻" + ls[0]].text = ls[1];
    }
}
