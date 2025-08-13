using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 敌人C : MonoBehaviour
{
    // Start is called before the first frame update
    public enum 状态
    {
        待机,
        追击,
        攻击,
        攻击中,
        受击,
        受击中,
    }
    public float 警戒圈 = 3; 
    public 状态 当前状态;
    public float 转身速度 = 25;
    public float 移动速度 = 5;
    public float 攻击距离 = 1.5f;

    public Transform 追击目标;
    public Animator ani;
    public float 攻击预备时长 = 0.35f;
    public float 攻击时长 = 1;
    public float 受击时长 = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerC.instance == null)
        {
            return;
        }
        switch (当前状态)
        {
            case 状态.待机:
                待机();
                break;
            case 状态.追击:
                追击();
                break;
            case 状态.攻击:
                攻击();
                break;
            case 状态.攻击中:
                攻击中();
                break;
            case 状态.受击:
                受击();
                break;
            case 状态.受击中:
                受击中();
                break;
        }
    }
    void 待机()
    {
        if (Vector3.Distance(this.transform.position, PlayerC.instance.transform.position) < 警戒圈)
        {
            当前状态 = 状态.追击;
            追击目标 = PlayerC.instance.transform;
        }
    }
    void 攻击(   )
    {
        攻击预备时长 -= Time.deltaTime;
        if (攻击预备时长<=0)
        {
            攻击时长 = 1;
            ani.SetTrigger("atk");
            当前状态 = 状态.攻击中;
        }
       
    }
    void 攻击中()
    {
        攻击时长 -= Time.deltaTime;
        if (攻击时长<=0)
        { 
            当前状态 = 状态.待机;
        }
    }
    void 受击()
    {
        当前状态 = 状态.受击;
        受击时长 = 0.5f;
    }
    void 受击中()
    {
        受击时长 -= Time.deltaTime;
        if (受击时长 <= 0)
        {
            当前状态 = 状态.待机;
        }
    }
    void 追击(   )
    {
        if (Vector3.Distance(this.transform.position, PlayerC.instance.transform.position) < 攻击距离)
        {
            //ani.SetFloat("垂直速度", 0);
            攻击预备时长 = 0.35f;
            当前状态 = 状态.攻击; 
            return;
        }
        //ani.SetFloat("垂直速度", 1);
        Vector3 pos = 追击目标.position;
        pos.y = this.transform.position.y;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(pos - this.transform.position, Vector3.up), Time.deltaTime * 转身速度);
        this.transform.Translate(2 * this.transform.forward * Time.deltaTime * 移动速度 , Space.World);
      
    }
}
