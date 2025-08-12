using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static List<角色信息> 已创建角色信息 = new List<角色信息>();
    public static List<功能单项> 已创建功能单项信息 = new List<功能单项>();
    public static List<动作单项> 已创建动作单项信息 = new List<动作单项>();
    public static List<动作连接单项> 已创建动作连接单项信息 = new List<动作连接单项>();
    public static bool 释放技能不能移动;
    public static GameObject 角色;
    public static 功能单项 技能Q, 技能W, 技能E;

    public static float 移动速度 { get; set;}
    public static float 连段时间 { get; set; }
    public static float 记录移动速度;
    List<I存档相关接口> myInterfaces;
    static Transform 角色tf;
    static Animator ani;

    public static void 动画事件处理(Animator ani)
    {
        bool res;

        int 当前动作 = ani.GetInteger("当前动作");
        if (当前动作 < 3)
        {
            int 时机 = 根据编号获取时机(当前动作);
            if (时机 == 0)
            {
                res = 获取特效数据并播放(当前动作);
            }
        }
    }

    public static int 根据编号获取时机(int 编号)
    {
        switch (编号)
        {
            case 1:
                if (已创建角色信息[0].特效产生时机1_ == "" || 已创建角色信息[0].特效产生时机1_ == null)
                {
                    return -1;
                }
                if (已创建角色信息[0].特效产生时机1_ == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            case 2:
                if (已创建角色信息[0].特效产生时机2_ == "" || 已创建角色信息[0].特效产生时机2_ == null)
                {
                    return -1;
                }
                if (已创建角色信息[0].特效产生时机1_ == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            case 3:
                if (已创建角色信息[0].特效产生时机3_ == "" || 已创建角色信息[0].特效产生时机3_ == null)
                {
                    return -1;
                }
                if (已创建角色信息[0].特效产生时机3_ == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
        }
        return -1;
    }
    public static bool 获取特效数据并播放(int 编号)
    {
        if (编号 == 1)
        {
            if (已创建角色信息[0].动作1_ == "" || 已创建角色信息[0].动作1_ == null)
            {
                return false;
            }
        }
        else if (编号 == 2)
        {
            if (已创建角色信息[0].动作2_ == "" || 已创建角色信息[0].动作2_ == null)
            {
                return false;
            }
        }
        else if (编号 == 3)
        {
            if (已创建角色信息[0].动作3_ == "" || 已创建角色信息[0].动作3_ == null)
            {
                return false;
            }
        }
        switch (编号)
        {
            case 1:
                if (已创建角色信息[0].特效1 != null && 已创建角色信息[0].特效1 != "")
                {
                    GameObject.Instantiate(Resources.Load<GameObject>("特效/" + 已创建角色信息[0].特效1));
                }
                break;
            case 2:
                if (已创建角色信息[0].特效2 != null && 已创建角色信息[0].特效2 != "")
                {
                    GameObject.Instantiate(Resources.Load<GameObject>("特效/" + 已创建角色信息[0].特效2));
                }
                break;
            case 3:
                if (已创建角色信息[0].特效3 != null && 已创建角色信息[0].特效3 != "")
                {
                    GameObject.Instantiate(Resources.Load<GameObject>("特效/" + 已创建角色信息[0].特效3));
                }
                break;
        }
        return true;
    }
    public static void 结束动画(Animator ani)
    {
        ani.SetFloat("循环时间", 0);
        ani.SetBool("是否循环", false);
        int 编号 = ani.GetInteger("当前动作");
        if (编号 < 3)
        {
            bool rsl = 获取动画数据并播放(编号 + 1);
            if (!rsl)
            {
                释放技能不能移动 = false;
                ani.Play("ID");
            }
        }
        else
        {
            释放技能不能移动 = false;
            ani.Play("ID");
        }
    }
    public static 动作连接单项 根据ID查找动作连接单项(int ID)
    {
        for (int i = 0; i < 已创建动作连接单项信息.Count; i++)
        {
            if (已创建动作连接单项信息[i].ID == ID)
            {
                return 已创建动作连接单项信息[i];
            }
        }
        return null;
    }
    public static 动作单项 根据ID查找动作单项(int ID)
    {
        for (int i = 0; i < 已创建动作单项信息.Count; i++)
        {
            if (已创建动作单项信息[i].ID == ID)
            {
                return 已创建动作单项信息[i];
            }
        }
        return null;
    }
    public static 功能单项 根据ID查找功能单项(int ID)
    {
        for (int i = 0; i < 已创建功能单项信息.Count; i++)
        {
            if (已创建功能单项信息[i].ID == ID)
            {
                return 已创建功能单项信息[i];
            }
        }
        return null;
    }
    public static 角色信息 获取当前角色数据()
    {
        return 已创建角色信息[0];
    }
    public void 读档()
    {
        存档控制器.读档();
        myInterfaces = FindAllTypes<I存档相关接口>();
        foreach (I存档相关接口 item in myInterfaces)
        {
            item.读取存档初始化数据();
        }
        Time.timeScale = 1;
    }
    public static List<T> FindAllTypes<T>()
    {
        List<T> interfaces = new List<T>();
        var types = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<T>();
        foreach (T t in types)
        {
            interfaces.Add(t);
        }
        return interfaces;
    }
    public static void 读取存档初始化数据()
    {
        已创建角色信息 = 存档控制器.存档文件_.已创建角色信息;
        已创建功能单项信息 = 存档控制器.存档文件_.已创建功能单项信息;
        已创建动作单项信息 = 存档控制器.存档文件_.已创建动作单项信息;
        已创建动作连接单项信息 = 存档控制器.存档文件_.已创建动作连接单项信息;
    }
    public static void Start()
    {
        if (已创建角色信息[0].移动速度 != "" && 已创建角色信息[0].移动速度 != null)
        {
            移动速度 = float.Parse(已创建角色信息[0].移动速度);
        }
        if (已创建角色信息[0].连段时间 != "" && 已创建角色信息[0].连段时间 != null)
        {
            连段时间 = float.Parse(已创建角色信息[0].连段时间);
        }
    }
    public static bool 获取普攻动画并播放(int 编号)
    {
        ani.Play("普攻Atk" + 编号);
        return true;
    }
    public static bool 获取动画数据并播放(int 编号)
    {
        string ID = "是否循环" + 编号;
        string 循环时间 = "循环时间" + 编号;
        string 动作 = "动作" + 编号;
        释放技能不能移动 = true;
        if (编号 == 1)
        {
            if (已创建角色信息[0].动作1_ == "" || 已创建角色信息[0].动作1_ == null)
            {
                return false;
            }
            ani.SetBool("是否循环", 已创建角色信息[0].是否循环1 == "1" ? true : false);
            if (已创建角色信息[0].是否循环1 == "1")
            {
                ani.SetFloat("循环时间", int.Parse(已创建角色信息[0].循环时间1));
            }
            ani.Play(已创建角色信息[0].动作1, 0);
        }
        else if (编号 == 2)
        {
            if (已创建角色信息[0].动作2_ == "" || 已创建角色信息[0].动作2_ == null)
            {
                return false;
            }
            ani.SetBool("是否循环", 已创建角色信息[0].是否循环2 == "1" ? true : false);
            if (已创建角色信息[0].是否循环2 == "1")
            {
                ani.SetFloat("循环时间", int.Parse(已创建角色信息[0].循环时间2));
            }
            ani.Play(已创建角色信息[0].动作2, 0);
        }
        else if (编号 == 3)
        {
            if (已创建角色信息[0].动作3_ == "" || 已创建角色信息[0].动作3 == null)
            {
                return false;
            }
            ani.SetBool("是否循环", 已创建角色信息[0].是否循环3 == "1" ? true : false);
            if (已创建角色信息[0].是否循环3 == "1")
            {
                ani.SetFloat("循环时间", int.Parse(已创建角色信息[0].循环时间3));
            }
            ani.Play(已创建角色信息[0].动作3, 0);
        }
        ani.SetInteger("当前动作", 编号);
        return true;
    }
    public static void 设置ani(Animator ani_)
    {
        ani = ani_;
    }
    public static GameObject 加载角色()
    {
        GameObject obj = Resources.Load<GameObject>("角色模型/" + 已创建角色信息[0].角色模型);
        角色 = GameObject.Instantiate(obj);
        if (GameObject.Find("角色出生点") != null)
        {
            角色.transform.position = GameObject.Find("角色出生点").transform.transform.position;
            角色tf = 角色.transform;
        }
        return 角色;
    }
    public void 返回编辑器()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
