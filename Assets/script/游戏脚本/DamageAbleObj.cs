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
    bool ����״̬;
    public ����C ����C_;
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
        if (other.CompareTag("����"))
        {
            ����״̬ = false;
        }
    }

    void Update()
    {
        if (����״̬)
        {
            this.transform.position += Vector3.down * Time.deltaTime;
        }
    }
    private void LateUpdate()
    {
       
    }
    public void beHit(�������ݰ� �������ݰ�_)
    {
       
        if (�������ݰ�_.���˼��� >0)
        {
            Vector3 ���˷��� = this.transform.position - �������ݰ�_.���������� ;
            ���˷���.y = 0;
            this.transform.position += ���˷��� * 0.5f* �������ݰ�_.���˼���;

        }
        if (�������ݰ�_.���ռ��� > 0 )
        {
            ����״̬ = true;
            this.transform.position += Vector3.up * 1f* �������ݰ�_.���ռ���;
        }
        if (����C_ != null)
        {
            ����C_.��ǰ״̬ = ����C.״̬.�ܻ�;
        }
        HP -= �������ݰ�_.�˺�;
        Debug.Log("�ܵ��˺� " + �������ݰ�_.�˺�);
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
