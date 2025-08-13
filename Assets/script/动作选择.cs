using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 动作选择 : MonoBehaviour
{
    public static 动作选择 instance;
    int 选择index;
    int 选择的动作 = 0 ;
    public Transform content;
    private void Awake()
    {
        instance = this;
    }
    public AnimationClip[] 列表;
    public AnimationClip 获取选择()
    {
        return 列表[选择index];
    }
    public void 选择动作(int v)
    {
        选择的动作 = v;
    }
    private void Start()
    {
        列表 = Resources.LoadAll<AnimationClip>("动作列表/技能动作");
        Transform obj = content.GetChild(0);
        if (列表.Length > 0)
        {
            obj.GetChild(0).GetComponent<Text>().text = 列表[0].name;
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
            obj.name = 列表[0].name;
            obj.GetComponent<Button>().onClick.AddListener(()=> {
                string name = obj.name;
                NotificationCenter.DefaultCenter.PostNotification(this,"选动作","动作"+选择的动作+"^"+name );
                选择index = 0;
                gameObject.SetActive(false);
            });
            int index = 0;
            foreach (var temp in 列表)
            {
                if (index == 0 )
                {
                    index++;
                    continue;
                }
                Transform tf = Instantiate(obj);
                tf.SetParent(content);
                tf.name = temp.name;
                tf.GetChild(0).GetComponent<Text>().text =  temp.name;
                tf.GetComponent<Button>().onClick.RemoveAllListeners();

                tf.GetComponent<Button>().onClick.AddListener(() => {
                    string name = tf.name;
                    NotificationCenter.DefaultCenter.PostNotification(this, "选动作", "动作" + 选择的动作 + "^" + name);
                    选择index = index;
                    gameObject.SetActive(false);
                });
            }
        }
    }

}
