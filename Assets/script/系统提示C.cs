using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 系统提示C : MonoBehaviour
{
    public Text 文字;
    public GameObject 提示板;
    public static 系统提示C instance;
    float 倒计时 = 1;
    private void Awake()
    {
        instance = this;
        提示板.SetActive(false);
    }
    public void 打开(string ls =null)
    {
        this.transform.SetAsLastSibling();
        提示板.SetActive(true);
        if (ls !=null)
        {
            文字.text = ls;
        }
        倒计时 = 1;
    }
    private void Update()
    {
        倒计时 -= Time.deltaTime;
        if (倒计时 < 0)
        {
            提示板.SetActive(false);
        }
    }
}
