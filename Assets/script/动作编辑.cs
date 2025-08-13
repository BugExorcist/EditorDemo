using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class 动作编辑 : MonoBehaviour , 存档相关接口
{
    public Transform content;
    public List<动作单项> 动作单项列表 = new List<动作单项>();
    public InputField[] T数值;
    public InputField T描述;
    bool is编辑;
    int 当前编辑index;
    public GameObject 技能编辑面板;
    int 选择的技能模板 = 0;
    public bool 数据写入存档文件()
    {
        存档控制器.存档文件_.已创建动作单项信息 = 动作单项列表;
        return true;
    }
    public void 读取存档初始化数据()
    {
        动作单项列表 = 存档控制器.存档文件_.已创建动作单项信息;
    }
    private void OnEnable()
    {
        NotificationCenter.DefaultCenter.AddObserver(this,"选动作");

    }
    private void OnDisable()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, "选动作");
    }
    private void Start()
    {
        刷新();
        gameObject.SetActive(false);
    }
    void 刷新()
    {
        for (int i = content.childCount -1; i >0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        content.GetChild(0).gameObject.SetActive(false);
        Transform obj = content.GetChild(0);
        if (动作单项列表.Count > 0 )
        {
            obj.GetChild(0).GetComponent<Text>().text = 动作单项列表[0].ID + "";
            obj.GetChild(1).GetComponent<Text>().text = 动作单项列表[0].描述;
            obj.GetChild(2).GetComponent<Button>().onClick.AddListener(()=> {
                is编辑 = true;
                当前编辑index = 0;
                编辑面板刷新();
                填充数据(当前编辑index);
            });
            obj.gameObject.SetActive(true);
            int index = 0;
            foreach ( var temp in 动作单项列表)
            {
                if (index == 0)
                {
                    index++;
                    continue;
                }
                Transform tf = Instantiate(obj);
                tf.SetParent(content);
                tf.GetChild(0).GetComponent<Text>().text = 动作单项列表[index].ID + "";
                tf.GetChild(1).GetComponent<Text>().text = 动作单项列表[index].描述;
                int index__ = index;
                tf.GetChild(2).GetComponent<Button>().onClick.AddListener(() => {
                    is编辑 = true;
                    当前编辑index = index__ ;
                    编辑面板刷新();
                    填充数据(当前编辑index);
                });
                index++;
            }
        }
    }
    void 编辑面板刷新()
    {
        技能编辑面板.SetActive(true);
        for (int i= 0; i < T数值.Length; i++)
        {
            T数值[i].text = "";
        }
    }
    void 填充数据(int index = - 1)
    {
        if (index == -1)
        {
            return;
        }
        T数值[0].text = 动作单项列表[index].动作[0] + "";
        T数值[1].text = 动作单项列表[index].特效[0] + "";
        T数值[2].text = 动作单项列表[index].循环时间[0] + "";

        T数值[3].text = 动作单项列表[index].动作[1] + "";
        T数值[4].text = 动作单项列表[index].特效[1] + "";
        T数值[5].text = 动作单项列表[index].循环时间[1] + "";

        T数值[6].text = 动作单项列表[index].动作[2] + "";
        T数值[7].text = 动作单项列表[index].特效[2] + "";
        T数值[8].text = 动作单项列表[index].循环时间[2] + "";

        T描述.text = 动作单项列表[index].描述;
    }
    int 找到历史最大的ID()
    {
        int 最大ID = 0;
        foreach (var v in 动作单项列表)
        {
            if (v.ID > 最大ID)
            {
                最大ID = v.ID;
            }
        }
        return 最大ID;
    }
    public void 新增()
    {
        is编辑 = false;
        编辑面板刷新();
        填充数据(-1);
    }
    public void 确定按钮()
    {
        if (!is编辑)
        {
            动作单项 temp = new 动作单项();
            temp.描述 = T描述.text;
            temp.ID = 找到历史最大的ID() + 1;
            temp.动作[0] = T数值[0].text;
            temp.特效[0] = T数值[1].text;
            float.TryParse(T数值[2].text, out temp.循环时间[0]);

            temp.动作[1] = T数值[3].text;
            temp.特效[1] = T数值[4].text;
            float.TryParse(T数值[5].text, out temp.循环时间[1]);

            temp.动作[2] = T数值[6].text;
            temp.特效[2] = T数值[7].text;
            float.TryParse(T数值[8].text, out temp.循环时间[2]);
            动作单项列表.Add(temp);
        }
        else
        {
            动作单项列表[当前编辑index].动作[0] = T数值[0].text;
            动作单项列表[当前编辑index].特效[0] = T数值[1].text; 
            float.TryParse(T数值[2].text, out 动作单项列表[当前编辑index].循环时间[0]);
            动作单项列表[当前编辑index].动作[1] = T数值[3].text;
            动作单项列表[当前编辑index].特效[1] = T数值[4].text;
            float.TryParse(T数值[5].text, out 动作单项列表[当前编辑index].循环时间[1]);
            动作单项列表[当前编辑index].动作[2] = T数值[6].text;
            动作单项列表[当前编辑index].特效[2] = T数值[7].text;
            float.TryParse(T数值[8].text, out 动作单项列表[当前编辑index].循环时间[2]);
            动作单项列表[当前编辑index].描述 = T描述.text;
        }
        技能编辑面板.SetActive(false);
        刷新();
    }
    public void 取消按钮()
    {
        技能编辑面板.SetActive(false);
    }
}
