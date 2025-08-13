using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ID_描述结构
{
    public int ID;
    public string 描述;
}
[Serializable]
public class 动作连接单项
{
    public int ID;
    public string[] 动作 = new string[5];
    public string[] 持续时间 = new string[5];
    public string 描述;
}
[Serializable]
public class 功能单项
{
    public int ID;
    public int 类型;
    public float[] 数值组;
    public string 描述;
}

[Serializable]
public class 动作单项
{
    public int ID;
    public string[] 动作 = new string[3];
    public string[] 特效 = new string[3];
    public bool[] 是否循环 = new bool[3];
    public float[] 循环时间 = new float[3];
    public int[] 特效时机 = new int[3];
    public string 描述;
}
[Serializable]
public class 角色信息
{
    public string 角色ID_;
    public string 角色名称_;
    public string MS移动速度;
    public string 角色ID { get; set; }
    public string 角色名称 { get { return 角色名称_; } set { 角色名称_ = value; } }
    public string 角色模型_;
    public string 角色模型 { get { return 角色模型_; } set { 角色模型_ = value; } }
    public string 角色体型_;
    public string 角色体型 { get { return 角色体型_; } set { 角色体型_ = value; } }
    public string 职业定位_;
    public string 职业定位 { get { return 职业定位_; } set { 职业定位_ = value; } }

    public string 体型半径_;
    public string 体型半径 { get { return 体型半径_; } set { 体型半径_ = value; } }
     
    public string 移动速度 { get { return MS移动速度; } set { MS移动速度 = value; } }
   
    public string 攻击速度_;
    public string 攻击速度 { get { return 攻击速度_; } set { 攻击速度_ = value; } }
    public string 物理防御_;
    public string 物理防御 { get { return 物理防御_; } set { 物理防御_ = value; } }
    public string 魔法防御_;
    public string 魔法防御 { get { return 魔法防御_; } set { 魔法防御_ = value; } }
    public string 生命上限_;
    public string 生命上限 { get { return 生命上限_; } set { 生命上限_ = value; } }
    public string 魔法上限_;
    public string 魔法上限 { get { return 魔法上限_; } set { 魔法上限_ = value; } }
    public string 每秒生命回复_;
    public string 每秒生命回复 { get { return 每秒生命回复_; } set { 每秒生命回复_ = value; } }
    public string 每秒魔法回复_;
    public string 每秒魔法回复 { get { return 每秒魔法回复_; } set { 每秒魔法回复_ = value; } }

    public string Q技能_;
    public string Q技能 { get { return Q技能_; } set { Q技能_ = value; } }
     

    public string W技能_;
    public string W技能 { get { return W技能_; } set { W技能_ = value; } }
 

    public string E技能_;
    public string E技能 { get { return E技能_; } set { E技能_ = value; } }

    public string 动作1_;
    public string 是否循环1_;
    public string 特效产生时机1_;
    public string 特效1_;
    public string 循环时间1_;
    public string 循环时间2_;
    public string 循环时间3_;
    public string 动作2_;
    public string 是否循环2_;
    public string 特效产生时机2_;
    public string 特效2_;
    public string 动作3_;
    public string 是否循环3_;
    public string 特效产生时机3_;
    public string 特效3_;
    public string 动作1 { get { return 动作1_; } set { 动作1_ = value; } }
    public string 是否循环1 { get { return 是否循环1_; } set { 是否循环1_ = value; } }
    public string 特效产生时机1 { get { return 特效产生时机1_; } set { 特效产生时机1_ = value; } }
    public string 特效1 { get { return 特效1_; } set { 特效1_ = value; } }
    public string 循环时间1 { get { return 循环时间1_; } set { 循环时间1_ = value; } }
    public string 循环时间2 { get { return 循环时间2_; } set { 循环时间2_ = value; } }
    public string 循环时间3 { get { return 循环时间3_; } set { 循环时间3_ = value; } }
    public string 动作2 { get { return 动作2_; } set { 动作2_ = value; } }
    public string 是否循环2 { get { return 是否循环2_; } set { 是否循环2_ = value; } }
    public string 特效产生时机2 { get { return 特效产生时机2_; } set { 特效产生时机2_ = value; } }
    public string 特效2 { get { return 特效2_; } set { 特效2_ = value; } }
    public string 动作3 { get { return 动作3_; } set { 动作3_ = value; } }
    public string 是否循环3 { get { return 是否循环3_; } set { 是否循环3_ = value; } }
    public string 特效产生时机3 { get { return 特效产生时机3_; } set { 特效产生时机3_ = value; } }
    public string 特效3 { get { return 特效3_; } set { 特效3_ = value; } }

    public string 连段时间_;
    public string 普攻动作1_;
    public string 普攻浮空1_;
    public string 普攻击退1_;
    public string 普攻伤害时间1_;

    public string 普攻动作2_;
    public string 普攻浮空2_;
    public string 普攻击退2_;
    public string 普攻伤害时间2_;

    public string 普攻动作3_;
    public string 普攻浮空3_;
    public string 普攻击退3_;
    public string 普攻伤害时间3_;

    public string 普攻动作4_;
    public string 普攻浮空4_;
    public string 普攻击退4_;
    public string 普攻伤害时间4_;
    public string 连段时间 { get { return 连段时间_; } set { 连段时间_ = value; } }

    public string 普攻动作1 { get { return 普攻动作1_; } set { 普攻动作1_ = value; } }
    public string 普攻浮空1 { get { return 普攻浮空1_; } set { 普攻浮空1_ = value; } }
    public string 普攻击退1 { get { return 普攻击退1_; } set { 普攻击退1_ = value; } }
    public string 普攻伤害时间1 { get { return 普攻伤害时间1_; } set { 普攻伤害时间1_ = value; } }

    public string 普攻动作2 { get { return 普攻动作2_; } set { 普攻动作2_ = value; } }
    public string 普攻浮空2 { get { return 普攻浮空2_; } set { 普攻浮空2_ = value; } }
    public string 普攻击退2 { get { return 普攻击退2_; } set { 普攻击退2_ = value; } }
    public string 普攻伤害时间2 { get { return 普攻伤害时间2_; } set { 普攻伤害时间2_ = value; } }

    public string 普攻动作3 { get { return 普攻动作3_; } set { 普攻动作3_ = value; } }
    public string 普攻浮空3 { get { return 普攻浮空3_; } set { 普攻浮空3_ = value; } }
    public string 普攻击退3 { get { return 普攻击退3_; } set { 普攻击退3_ = value; } }
    public string 普攻伤害时间3 { get { return 普攻伤害时间3_; } set { 普攻伤害时间3_ = value; } }

    public string 普攻动作4 { get { return 普攻动作4_; } set { 普攻动作4_ = value; } }
    public string 普攻浮空4 { get { return 普攻浮空4_; } set { 普攻浮空4_ = value; } }
    public string 普攻击退4 { get { return 普攻击退4_; } set { 普攻击退4_ = value; } }
    public string 普攻伤害时间4 { get { return 普攻伤害时间4_; } set { 普攻伤害时间4_ = value; } }
    public string 物理攻击_;
    public string 物理攻击 { get { return 物理攻击_; } set { 物理攻击_ = value; } }
}

public class 存档控制器  
{
    public class 存档文件
    {
        public List<角色信息> 已创建角色信息 = new List<角色信息>();
        public List<功能单项> 已创建功能单项信息 = new List<功能单项>();
        public List<动作单项> 已创建动作单项信息 = new List<动作单项>();
        public List<动作连接单项> 已创建动作连接单项信息 = new List<动作连接单项>();
        public 存档文件()
        {

        }
    }
    public static 存档文件 存档文件_;
   public static void 读档()
    {
        if (PlayerPrefs.HasKey("savedata"))
        {
            存档文件 存档文件__ = JsonUtility.FromJson<存档文件>(PlayerPrefs.GetString("savedata"));
            if (存档文件__ != null)
            {
                存档文件_ = 存档文件__;
            }
            else
            {
                存档文件_ = new 存档文件();
            }
        }
        else
        {
            存档文件_ = new 存档文件();
        }
    }
    public static void 存档()// 把数据转化为json, 使用PlayerPrefs持久化保存
    {
        string temp = JsonUtility.ToJson(存档文件_);
        PlayerPrefs.SetString("savedata",temp);
    }
}
public interface 存档相关接口
{ 
    void 读取存档初始化数据();
    bool 数据写入存档文件();
}
