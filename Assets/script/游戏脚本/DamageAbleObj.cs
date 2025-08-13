using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageAbleObj : MonoBehaviour
{

    public float HP = 1010;
    public float HPmax = 1010;
    public GameObject deadFx ;
    public Vector3 FxOffset;
    public GameObject drop;
    public BoxCollider2D collider_;
    bool 浮空状态;
    public 敌人C 敌人C_;
    public Image hpbar;
    private void Awake()
    {
        if (collider_)
        {
            collider_.enabled = false;
        }
        if (hpbar==null)
        {
            return;
        }
        hpbar.fillAmount = HP / HPmax;
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        if (collider_)
        {
            collider_.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("地面"))
        {
            浮空状态 = false;
        }
    }

    void Update()
    {
        if (浮空状态)
        {
            this.transform.position += Vector3.down * Time.deltaTime;
        }
    }
    private void LateUpdate()
    {
       
    }
    public void beHit(攻击数据包 攻击数据包_)
    {
       
        if (攻击数据包_.击退级别 >0)
        {
            Vector3 击退方向 = this.transform.position - 攻击数据包_.攻击者坐标 ;
            击退方向.y = 0;
            this.transform.position += 击退方向 * 0.5f* 攻击数据包_.击退级别;

        }
        if (攻击数据包_.浮空级别 > 0 )
        {
            浮空状态 = true;
            this.transform.position += Vector3.up * 1f* 攻击数据包_.浮空级别;
        }
        if (敌人C_ != null)
        {
            敌人C_.当前状态 = 敌人C.状态.受击;
        }
        HP -= 攻击数据包_.伤害;
        Debug.Log("受到伤害 " + 攻击数据包_.伤害);
        hpbar.fillAmount = HP / HPmax;
        if (HP<=0)
        {
            if (drop != null)
            {
               GameObject obj = Instantiate(drop, this.transform.position, Quaternion.LookRotation(Vector3.forward));
                Vector3 pos = obj.transform.position;
                pos.y = -1.562f;
                obj.transform.position = pos;
                obj.transform.localEulerAngles = new Vector3(0,180,0);
            }
            Destroy(this.gameObject);
        }
    }


}
