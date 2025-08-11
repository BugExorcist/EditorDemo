using UnityEngine;
using UnityEngine.UI;

public class 模型选择 : MonoBehaviour
{
    public static 模型选择 instance;
    int 选择index = -1;
    public GameObject[] 模型列表;
    public Transform content;

    private void Awake()
    {
        instance = this;
    }
    public GameObject 获取选择模型()
    {
        return 模型列表[选择index];
    }
    private void Start()
    {
        模型列表 = Resources.LoadAll<GameObject>("角色模型");
        Transform obj = content.GetChild(0);
        if (模型列表.Length > 0)
        {
            obj.GetChild(0).GetComponent<Text>().text = 模型列表[0].name;
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
            obj.name = 模型列表[0].name;
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                string name = obj.name;
                选择index = 0;
                NotificationCenter.DefaultCenter.PostNotification(this, "选模型", name);
                this.gameObject.SetActive(false);
            });
            for (int i = 1; i < 模型列表.Length; i++)
            {
                Transform tf = Instantiate(obj);
                tf.SetParent(content);
                tf.name = 模型列表[i].name;
                tf.GetComponent<Button>().onClick.RemoveAllListeners();
                int index = i;
                tf.GetComponent<Button>().onClick.AddListener(() =>
                {
                    string name = tf.name;
                    选择index = index;
                    NotificationCenter.DefaultCenter.PostNotification(this, "选模型", name);
                    this.gameObject.SetActive(false);
                });
            }
        }
    }
}
