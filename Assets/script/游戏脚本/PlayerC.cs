using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class PlayerC : MonoBehaviour
{
    public static PlayerC instance;
    private void Awake()
    {
        instance = this;
    }
    public float 连段时间 { get { return Core.连段时间; } }
    public float walkSpeed { get { return Core.移动速度; } }
    Vector2 m_Movement;
    public GameObject weaponObj;
    public Animator ani;
    //public bool 释放技能不能移动;
    bool m_atkTrigger;
    // Start is called before the first frame update
    public CharacterController cc;


    public AnimationClip  动作;
    AnimatorStateInfo asinfo;
    static RaycastHit[] rsl = new RaycastHit[30];
    static LayerMask layer = 1 << 6;   //31 敌人飞行道具
    攻击数据包 攻击数据包_;
    public   功能单项[] 技能组 = new 功能单项[3]; 
    void Start()
    {
        cc = GetComponent<CharacterController>(); 
        ani = GetComponent<Animator>();
        
        技能组[0] = Core.根据ID查找功能单项(int.Parse( Core.已创建角色信息[0].Q技能));
        技能组[1] = Core.根据ID查找功能单项(int.Parse(Core.已创建角色信息[0].W技能));
        技能组[2] = Core.根据ID查找功能单项(int.Parse(Core.已创建角色信息[0].E技能));

        RuntimeAnimatorController[] temp=  Resources.LoadAll<RuntimeAnimatorController>("动画控制器");

        temp = Resources.LoadAll<RuntimeAnimatorController>("动画控制器");
        var temp2 = Resources.Load<RuntimeAnimatorController>("动画控制器/控制xxx"); 
        ani.runtimeAnimatorController = temp2; 
        if (ani.runtimeAnimatorController == null)
        {
            temp2 = Resources.Load<RuntimeAnimatorController>("动画控制器/控制xxx");
            ani.runtimeAnimatorController = temp2;
        }
        Debug.Log("设置ani  : " + ani + " : controller是 " + ani.runtimeAnimatorController);
        Core.设置ani(ani);
    }

    // Update is called once per frame
    void 状态计算()
    {
        if (当前攻击动作>1 && 攻击动作倒计时>0)
        {
            攻击动作倒计时 -= Time.deltaTime;
            if (攻击动作倒计时 <= 0)
            {
                初始状态 = true;
                攻击动作倒计时 = 0;
                当前攻击动作 = 1;
            }
        }
        if (允许攻击倒计时 > 0)
        {
            允许攻击倒计时 -= Time.deltaTime;

            if (允许攻击倒计时 < 0)
            {
                允许攻击 = true;
                  允许攻击倒计时 = 0; 
            }
        }

    }
    void Update()
    {
        状态计算();
        getInput();
        技能检测();
        
        move();
        atk();
    }

    void 技能检测()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            技能执行(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            技能执行(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            技能执行(2);
        }
    }
   
    void 技能执行(int index)
    {
        if (技能组[index] == null) return;
        switch (技能组[index].类型)
        {
            case 0:  // 加速

                Core.记录移动速度 = Core.移动速度;
                Core.移动速度 *= (1 + (float)技能组[index].数值组[3] / 100f);
                StartCoroutine(加速协程((float)技能组[index].数值组[4]));
                Core.获取动画数据并播放((int)技能组[index].数值组[0]);
                break;
            case 1: // 小范围攻击   动作, 特效, 音效 , 范围伤害,  , 击退级别

               
                StartCoroutine(动作连接播放协程((int)技能组[index].数值组[0]));
                StartCoroutine(范围伤害协程(index));
                break;
            case 2: // 次元斩   多阶段攻击 动作, 特效, 音效 ,   伤害值3 ,伤害范围 4爆炸伤害5

                StartCoroutine(动作连接播放协程((int)技能组[index].数值组[0]));
               
                StartCoroutine(次元斩协程(4, (int)技能组[index].数值组[3], 技能组[index].数值组[4], 技能组[index].数值组[5], 技能组[index].数值组[6], 技能组[index].数值组[7]));
                break;
        }
    }
    IEnumerator 动作连接播放协程(int 动作编号)
    {
        var temp = Core.根据ID查找动作连接单项(动作编号);
        Debug.Log(" temp.动作.Length " + temp.动作.Length);
        for (int i = 0; i < temp.动作.Length; i++)  // 
        {
            Debug.Log("持续时间" + temp.持续时间[i]);
            if (temp.动作[i] != "" && temp.动作[i] != null && temp.持续时间[i] != "" && temp.持续时间[i] != null)
            {
                Debug.Log("播放动作 " + i);
            
                ani.Play(Core.根据ID查找动作单项(int.Parse(temp.动作[i])).动作[0],0,0 );
                yield return new WaitForSeconds(float.Parse(temp.持续时间[i]));
            }
           
        }
    }
    IEnumerator 次元斩协程(int 阶段数,  float 伤害值, float 爆炸伤害 ,float 第一段延迟, float 段间延迟, float 终结延迟)
    {
        float 伤害范围 = 2;
           yield return new WaitForSeconds(第一段延迟);
        for (int i =0; i< 阶段数; i++)
        {
            if (i > 0)
            {
                yield return new WaitForSeconds(段间延迟);
            }
            yield return new WaitForSeconds(终结延迟);
            范围伤害(伤害范围, 伤害值);
        }
        yield return new WaitForSeconds(0.5f);
        范围伤害(伤害范围, 爆炸伤害);
    }
    IEnumerator 范围伤害协程(int index)
    {
        yield return new WaitForSeconds(技能组[index].数值组[5]);
        范围伤害(技能组[index].数值组[2], 技能组[index].数值组[3], (int)技能组[index].数值组[4]);
    } 
    void 范围伤害(float 伤害范围,float 范围伤害 ,int 击退级别= 0 )
    {
        if (伤害范围 == 0) 伤害范围 = 3;
        Physics.SphereCastNonAlloc(this.transform.position + this.transform.forward * 0.5f,
           伤害范围, this.transform.forward, rsl, 0.3f, layer); // Physics.SphereCastNonAlloc(起点, 半径, 方向, rsl,  1f ,layer); //
        ;
        攻击数据包_ = new 攻击数据包();
        攻击数据包_.攻击者坐标 = this.transform.position; 
        攻击数据包_.伤害 = 范围伤害;
        攻击数据包_.击退级别 = 击退级别;
        for (int i = 0; i < rsl.Length; i++)
        {
            if (rsl[i].collider == null)
            {
                continue;
            }
            rsl[i].collider.gameObject.GetComponent<DamageAbleObj>().beHit(攻击数据包_);

        }
    }
    IEnumerator 加速协程(float 时间)
    {
        yield return new WaitForSeconds(时间);
        Core.移动速度 = Core.记录移动速度;
    }

    public int 当前攻击动作 = 1;
    float 攻击动作倒计时;
    float 允许攻击倒计时;
    bool 初始状态 = true;
    bool 允许攻击 = true;
    void atk()
    {
        if (m_atkTrigger && 允许攻击)
        {
            
           if(游戏初始化.instance.获取普攻动画并播放(当前攻击动作))
            {
                //初始状态 = false;
                允许攻击 = false;
                   攻击动作倒计时 = 连段时间;
                ++当前攻击动作;
             
                if (当前攻击动作 == 5)
                {
                    允许攻击倒计时 = 1.5f;
                }
                else
                {
                    允许攻击倒计时 = 0.5f;
                }
                if (当前攻击动作 >= 5)
                {
                    当前攻击动作 = 1;
                }
            }
         
        }
    }
    void getInput()
    {
        m_Movement = InputC.instance.m_Movement;
        m_atkTrigger = InputC.instance.m_atkTrigger;
    }
    private void move()
    {
        if (Core.释放技能不能移动) return; 
        Debug.Log("ani.: " + ani);

        Debug.Log("ani.runtimeAnimatorController: " + ani.runtimeAnimatorController);
 
        if (m_Movement!=Vector2.zero && walkSpeed>0)
        {
            ani.SetBool("移动",true);
        }
        else
        {
            ani.SetBool("移动", false);
            //ani.Play("ID");
        }

        // 如何把世界坐标系的方向, 转化为 角色自身坐标系的方向.
         Vector3 dir = transform.TransformDirection(new Vector3(m_Movement.x ,0,m_Movement.y));


        //Vector3 dir = new Vector3(m_Movement.x, 0, m_Movement.y);
        cc.Move(dir * walkSpeed * Time.deltaTime);
        //transform.Translate(dir * walkSpeed * Time.deltaTime, Space.World); //
                               
    }

    public void enbaleWeapon()
    {
        weaponObj.SetActive(true);
    }
    public void closeWeapon()
    {
        weaponObj.SetActive(false);
    }
}
