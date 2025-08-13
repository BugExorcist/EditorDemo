using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 特效选择 : MonoBehaviour
{
    public static 特效选择 instance;
    int 选择index = 0;
    private void Awake()
    {
        instance = this;
    }
    public GameObject 获取选择()
    {
        return 列表[选择index];
    }
    int 选择的 = 0;
    public GameObject[] 列表;
    public void 选择(int v)
    {
        选择的 = v;
    }
    public Transform content;
    private void Start()
    {
        列表 = Resources.LoadAll<GameObject>("特效");
        Transform obj = content.GetChild(0);
        if (列表.Length > 0)
        {
            obj.GetChild(0).GetComponent<Text>().text = 列表[0].name;
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
            obj.name = 列表[0].name;
            obj.GetComponent<Button>().onClick.AddListener(()=> {
                string name = obj.name;
                NotificationCenter.DefaultCenter.PostNotification(this,"选特效","特效"+选择的+"^"+name);
                选择index = 0;
                gameObject.SetActive(false);
            });
            int index = 0;
            foreach (var temp in 列表)
            {
                if (index == 0)
                {
                    index++;
                    continue;
                }
                Transform tf = Instantiate(obj);
                tf.SetParent(content);
                tf.name = temp.name;
                tf.GetChild(0).GetComponent<Text>().text = temp.name;
                tf.GetComponent<Button>().onClick.RemoveAllListeners();

                tf.GetComponent<Button>().onClick.AddListener(() => {
                    string name = tf.name;
                    NotificationCenter.DefaultCenter.PostNotification(this, "选特效", "特效" + 选择的 + "^" + name);
                    选择index = index;
                    gameObject.SetActive(false);
                });



            }
        }
    }


}
