using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 功能编辑 : MonoBehaviour , I存档相关接口
{
    public Transform content;
    public List<功能单项> 功能单项列表 = new List<功能单项>();
    public InputField[] T数值;
    public Text[] T标签;
    public InputField T描述;
    bool is编辑;
    int 当前编辑index;
    const int 数值最大长度 = 12;
    public GameObject 技能编辑面板;
    int 选择的技能模板 = 0;
    public InputField 输入编号;
    enum 类型enum
    {
        改变速度,
        范围击退,
        次元斩
    }
    public bool 数据写入存档文件()
    {
        存档控制器.存档文件_.已创建功能单项信息 = 功能单项列表;
        return true;
    }
    public void 读取存档初始化数据()
    {
        功能单项列表 = 存档控制器.存档文件_.已创建功能单项信息;
    }
    private void Start()
    {
        刷新();
        gameObject.SetActive(false);
    }
    void 刷新()
    {
        for (int i = content.childCount - 1; i > 0; i --)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        content.GetChild(0).gameObject.SetActive(false);
        Transform obj = content.GetChild(0);
        if (功能单项列表.Count > 0 )
        {
            obj.GetChild(0).GetComponent<Text>().text = 功能单项列表[0].ID + "";
            obj.GetChild(1).GetComponent<Text>().text = ((类型enum)功能单项列表[0].类型).ToString();
            obj.GetChild(2).GetComponent<Text>().text = 功能单项列表[0].描述;
            obj.GetChild(3).GetComponent<Button>().onClick.AddListener(()=> {
                is编辑 = true;
                当前编辑index = 0;
                选择的技能模板 = 功能单项列表[0].类型;
                编辑面板刷新();
                填充数据(当前编辑index);

            });
            obj.gameObject.SetActive(true);
            int index = 0;
            foreach (var temp in 功能单项列表)
            {
                if (index == 0 )
                {
                    index++;
                    continue;
                }
                Transform tf = Instantiate(obj);
                tf.SetParent(content);
                tf.GetChild(0).GetComponent<Text>().text = 功能单项列表[index].ID + "";
                tf.GetChild(1).GetComponent<Text>().text = ((类型enum)功能单项列表[index].类型).ToString();
                tf.GetChild(2).GetComponent<Text>().text = 功能单项列表[index].描述;
                int index__ = index;
                tf.GetChild(3).GetComponent<Button>().onClick.AddListener(() => {
                    is编辑 = true;
                    当前编辑index = index__;
                    选择的技能模板 = 功能单项列表[index__].类型;
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
        for (int i = 0; i < T标签.Length;i++)
        {
            if (i> 2)
            {
                T标签[i].transform.parent.gameObject.SetActive(false);
            }
            T数值[i].text = "";
        }
        switch (选择的技能模板)
        {
            case 0:
                T标签[3].text = "加成百分比";
                T标签[3].transform.parent.gameObject.SetActive(true);
                T标签[4].text = "持续时间";
                T标签[4].transform.parent.gameObject.SetActive(true);
                break;
            case 1:
                T标签[3].text = "范围伤害";
                T标签[3].transform.parent.gameObject.SetActive(true);
                T标签[4].text = "震退级别";
                T标签[4].transform.parent.gameObject.SetActive(true);
                T标签[5].text = "生效延迟";
                T标签[5].transform.parent.gameObject.SetActive(true);
                break;
            case 2:
                T标签[3].text = "次元斩伤害";
                T标签[3].transform.parent.gameObject.SetActive(true);
                T标签[4].text = "爆炸伤害";
                T标签[4].transform.parent.gameObject.SetActive(true);
                T标签[5].text = "生效延迟";
                T标签[5].transform.parent.gameObject.SetActive(true);
                T标签[6].text = "段间延迟";
                T标签[6].transform.parent.gameObject.SetActive(true);
                T标签[7].text = "终结延迟";
                T标签[7].transform.parent.gameObject.SetActive(true);
                T标签[8].text = "次元斩范围";
                T标签[8].transform.parent.gameObject.SetActive(true);
                T标签[9].text = "爆炸范围";
                T标签[9].transform.parent.gameObject.SetActive(true);
                break;
        }
    }
    void 填充数据(int index =-1)
    {
        if (index == -1)
        {
            return;
        }
        T描述.text = 功能单项列表[index].描述;
        for (int i= 0; i< 数值最大长度; i++)
        {
            if (i> 功能单项列表[index].数值组.Length)
            {
                break;
            }
            T数值[i].text = 功能单项列表[index].数值组[i] + "";
        }
    }
    int 找到历史最大的ID()
    {
        int 最大ID = 0;
        foreach ( var v in 功能单项列表)
        {
            if (v.ID>最大ID)
            {
                最大ID = v.ID;
            }
        }
        return 最大ID;
    }
   public void 选择下拉菜单(int value)
    {
        选择的技能模板 = value;
    }
    public void 新增一个技能()
    {
        is编辑 = false;
        编辑面板刷新();
        填充数据();
    }
    public void 数据写入列表()
    {
        if (is编辑)
        {
            功能单项 data = new 功能单项();
            data.ID = 功能单项列表[当前编辑index].ID;
            data.类型 = 选择的技能模板;
            data.描述 = T描述.text;
            switch (选择的技能模板)
            {// 选0、1、3的逻辑都一样
                case 0:
                    //data.数值组 = new float[数值最大长度];
                    //for (int i = 0; i < 数值最大长度; i ++)
                    //{
                    //    float.TryParse(T数值[i].text , out data.数值组[i]);
                    //}
                    //break;
                case 1:
                    //data.数值组 = new float[数值最大长度];
                    //for (int i = 0; i < 数值最大长度; i++)
                    //{
                    //    float.TryParse(T数值[i].text, out data.数值组[i]);
                    //}
                    //break;
                case 2:
                    data.数值组 = new float[数值最大长度];
                    for (int i = 0; i < 数值最大长度; i++)
                    {
                        float.TryParse(T数值[i].text, out data.数值组[i]);
                    }
                    break; 
            }
            功能单项列表[当前编辑index] = data;
        }
        else
        {
            int id = 找到历史最大的ID() + 1;
            功能单项 data = new 功能单项();
            data.ID = id;
            data.类型 = 选择的技能模板;
            data.描述 = T描述.text;
            switch (选择的技能模板)
            {
                case 0:
                    //data.数值组 = new float[数值最大长度];
                    //for (int i = 0; i < 数值最大长度; i++)
                    //{
                    //    if (T数值[i].text == "" || T数值[i].text==null)
                    //    {
                    //        data.数值组[i] = 0;
                    //    }
                    //    else
                    //    {
                    //        data.数值组[i] = float.Parse(T数值[i].text);
                    //    } 
                    //}
                    //break;
                case 1:
                    //data.数值组 = new float[数值最大长度];
                    //for (int i = 0; i < 数值最大长度; i++)
                    //{
                    //    if (T数值[i].text == "" || T数值[i].text == null)
                    //    {
                    //        data.数值组[i] = 0;
                    //    }
                    //    else
                    //    {
                    //        data.数值组[i] = float.Parse(T数值[i].text);
                    //    }
                    //}
                    //break;
                case 2:
                    data.数值组 = new float[数值最大长度];
                    for (int i = 0; i < 数值最大长度; i++)
                    {
                        if (T数值[i].text == "" || T数值[i].text == null)
                        {
                            data.数值组[i] = 0;
                        }
                        else
                        {
                            data.数值组[i] = float.Parse(T数值[i].text);
                        }
                    }
                    break;
            }

            功能单项列表.Add(data);
        }
        技能编辑面板.SetActive(false);
        刷新();
    }



    public void 关闭数值编辑面板()
    {
        技能编辑面板.SetActive(false);
    }
    public void 删除按钮()
    {
        int ID = int.Parse(输入编号.text);
        for (int i = 0; i < 功能单项列表.Count; i++)
        {
            if (功能单项列表[i].ID == ID)
            {
                功能单项列表.RemoveAt(i);
                刷新();
                break;
            }
        }
    }


}
