using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct 攻击数据包
{
    public Vector3 攻击者坐标;
    public float 浮空级别;
    public float 击退级别;
    public float 伤害;
}
public class 动画事件C : MonoBehaviour
{
    static LayerMask layer = 1 << 6 ;   //31 敌人飞行道具
    static RaycastHit[] rsl = new RaycastHit[30];
    float 攻击半径 = 1f;
    
    攻击数据包 攻击数据包_;
   角色信息 角色信息;
    private void Start()
    {
        
    }

    public void 打开攻击碰撞体()
    {
      
        Physics.SphereCastNonAlloc(this.transform.position + this.transform.forward*0.5f,
            攻击半径 , this.transform.forward, rsl, 0.3f, layer); // Physics.SphereCastNonAlloc(起点, 半径, 方向, rsl,  1f ,layer); //
        ;
        攻击数据包_ = new 攻击数据包();
        攻击数据包_.攻击者坐标 = this.transform.position; 
         角色信息  = 游戏初始化.instance.获取当前角色数据();
        switch (PlayerC.instance.当前攻击动作)
        {
            case 2:
                攻击数据包_.浮空级别 = float.Parse(角色信息.普攻浮空1);
                攻击数据包_.击退级别 = float.Parse(角色信息.普攻击退1);
                攻击数据包_.伤害= float.Parse(角色信息.物理攻击);
                break;
            case 3:
                攻击数据包_.浮空级别 = float.Parse(角色信息.普攻浮空2);
                攻击数据包_.击退级别 = float.Parse(角色信息.普攻击退2);
                攻击数据包_.伤害 = float.Parse(角色信息.物理攻击);
                break;
            case 4:
                攻击数据包_.浮空级别 = float.Parse(角色信息.普攻浮空3);
                攻击数据包_.击退级别 = float.Parse(角色信息.普攻击退3);
                攻击数据包_.伤害 = float.Parse(角色信息.物理攻击);
                break;
            case 1:
                攻击数据包_.浮空级别 = float.Parse(角色信息.普攻浮空4);
                攻击数据包_.击退级别 = float.Parse(角色信息.普攻击退4);
                攻击数据包_.伤害 = float.Parse(角色信息.物理攻击);
                break;
            default:
                Debug.LogError(11);
                break;
        }
       
     
        for (int i = 0; i < rsl.Length; i++)
        {
            if (rsl[i].collider == null)
            {
                continue;
            }
            rsl[i].collider.gameObject.GetComponent<DamageAbleObj>().beHit(攻击数据包_);

        }
        rsl = new RaycastHit[30];
    }
    public void 关闭攻击碰撞体()
    {

    }
}
