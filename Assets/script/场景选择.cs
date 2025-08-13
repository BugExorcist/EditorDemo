using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 场景选择 : MonoBehaviour
{
    public static 场景选择 instance;
    public static string 当前选择的场景 = null;
    int 选择index = -1;
    public Transform content;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        当前选择的场景 = null;
    }
    public GameObject[] 模型列表;
    public GameObject 获取选择模型()
    {
        return 模型列表[选择index];
    }
    private void Start()
    {
        模型列表 = Resources.LoadAll<GameObject>("场景模型");
        Transform obj = content.GetChild(0);
        if (模型列表.Length > 0)
        {
            obj.GetChild(0).GetComponent<Text>().text = 模型列表[0].name;
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
            obj.name = 模型列表[0].name;
            obj.GetComponent<Button>().onClick.AddListener(()=> {
                string name = obj.name;
                选择index = 0;
                当前选择的场景 = obj.name;
                gameObject.SetActive(false);
            });
            for (int i = 1; i<模型列表.Length; i++)
            {
                Transform tf = Instantiate(obj);
                tf.GetChild(0).GetComponent<Text>().text = 模型列表[i].name;
                tf.GetComponent<Button>().onClick.RemoveAllListeners();
                tf.name = 模型列表[i].name;
                int index = i;
                tf.GetComponent<Button>().onClick.AddListener(() => {
                    string name = tf.name;
                    选择index = index;
                    当前选择的场景 = name;
                    gameObject.SetActive(false);
                });
            }
        }
    }
}
