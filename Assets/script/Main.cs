using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour , �浵��ؽӿ�
{
    public Dictionary<string, InputField> �ı�����;
    public Dictionary<string, Toggle> ��������;
    public Dictionary<string, Dropdown> ��������;
    public List<��ɫ��Ϣ> �Ѵ�����ɫ��Ϣ;
    public List<���ܵ���> �Ѵ���������Ϣ;
    List<�浵��ؽӿ�> myInterfaces;
    public Transform ��ʾ��ɫλ��;

    public void ����Demo()
    {
        SceneManager.LoadScene(1);
    }
    public void ��ȡ�浵��ʼ������()
    {
        �Ѵ�����ɫ��Ϣ = �浵������.�浵�ļ�_.�Ѵ�����ɫ��Ϣ;
        �Ѵ���������Ϣ = �浵������.�浵�ļ�_.�Ѵ������ܵ�����Ϣ;
        if (�Ѵ�����ɫ��Ϣ.Count == 0)
        {
            ��ɫ��Ϣ temp = new ��ɫ��Ϣ();temp.��ɫID_ = "";
            �Ѵ�����ɫ��Ϣ.Add(temp);
        }
    }
    public AnimatorStateMachine stateMachine;
    public AnimatorController animatorController;

    public bool ����д��浵�ļ�()
    {
        //if (�ı�����["����ʱ��"].text =="" || �ı�����["����ʱ��"].text==null )
        //{
        //    ϵͳ��ʾC.instance.��("����ʱ��������0");
        //    return false;
        //}
        foreach (var temp in �ı�����)
        {
            PropertyInfo p = �Ѵ�����ɫ��Ϣ[0].GetType().GetProperty(temp.Key);
            if (p !=null)
            {
                p.SetValue(�Ѵ�����ɫ��Ϣ[0], temp.Value.text);
            }
        }
        foreach (var temp in ��������)
        {
            PropertyInfo p = �Ѵ�����ɫ��Ϣ[0].GetType().GetProperty(temp.Key);
            if (p != null)
            {
                p.SetValue(�Ѵ�����ɫ��Ϣ[0], temp.Value.isOn?"1":"0");
            }
        }
        foreach (var temp in ��������)
        {
            PropertyInfo p = �Ѵ�����ɫ��Ϣ[0].GetType().GetProperty(temp.Key);
            if (p != null)
            {
                p.SetValue(�Ѵ�����ɫ��Ϣ[0], temp.Value.value+"");
            }
        }
        �浵������.�浵�ļ�_.�Ѵ�����ɫ��Ϣ = �Ѵ�����ɫ��Ϣ;
        �浵������.�浵�ļ�_.�Ѵ������ܵ�����Ϣ = �Ѵ���������Ϣ;

        File.Delete("Assets/Resources/����������/����xxx.controller");
        animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/Resources/����������/����xxx.controller");
        stateMachine = animatorController.layers[0].stateMachine;
        Dictionary<string, AnimatorState> ״̬�ֵ� = new Dictionary<string, AnimatorState>();
        ״̬�ֵ�.Add("ID",stateMachine.AddState("ID"));
        stateMachine.defaultState = ״̬�ֵ�["ID"];
        ״̬�ֵ�.Add("run", stateMachine.AddState("run"));
        ״̬�ֵ�["ID"].motion = Resources.Load<AnimationClip>("�����б�/Ԥ�費��ѡ����/" +"ID");
        ״̬�ֵ�["run"].motion = Resources.Load<AnimationClip>("�����б�/Ԥ�費��ѡ����/" + "run");
        animatorController.AddParameter("�Ƿ�ѭ��",AnimatorControllerParameterType.Bool);
        animatorController.AddParameter("ѭ��ʱ��", AnimatorControllerParameterType.Float);
        animatorController.AddParameter("��ǰ����", AnimatorControllerParameterType.Int);
        animatorController.AddParameter("�ƶ�", AnimatorControllerParameterType.Bool);

        AnimatorStateTransition trans = ״̬�ֵ�["ID"].AddTransition(״̬�ֵ�["run"],false);
        trans.AddCondition(AnimatorConditionMode.If,1,"�ƶ�");
        trans = ״̬�ֵ�["run"].AddTransition(״̬�ֵ�["ID"], false);
        trans.AddCondition(AnimatorConditionMode.IfNot, 1, "�ƶ�");

        ״̬�ֵ�.Add("Atk1", stateMachine.AddState("Atk1"));
        ״̬�ֵ�["Atk1"].motion = Resources.Load<AnimationClip>("�����б�/���ܶ���/" + "Atk1");
        ״̬�ֵ�["Atk1"].AddStateMachineBehaviour<anid>();
        ״̬�ֵ�.Add("Atk2", stateMachine.AddState("Atk2"));
        ״̬�ֵ�["Atk2"].motion = Resources.Load<AnimationClip>("�����б�/���ܶ���/" + "Atk2");
        ״̬�ֵ�["Atk2"].AddStateMachineBehaviour<anid>();
        ״̬�ֵ�.Add("Atk3", stateMachine.AddState("Atk3"));
        ״̬�ֵ�["Atk3"].motion = Resources.Load<AnimationClip>("�����б�/���ܶ���/" + "Atk3");
        ״̬�ֵ�["Atk3"].AddStateMachineBehaviour<anid>();

        if (�Ѵ�����ɫ��Ϣ[0].�չ�����1 !="" && �Ѵ�����ɫ��Ϣ[0].�չ�����1 != null)
        {
            ״̬�ֵ�.Add("�չ�Atk1", stateMachine.AddState("�չ�Atk1"));
            ״̬�ֵ�["�չ�Atk1"].motion = Resources.Load<AnimationClip>("�����б�/�չ�����/" + �Ѵ�����ɫ��Ϣ[0].�չ�����1);
            ״̬�ֵ�["�չ�Atk1"].AddExitTransition(true);
             
        }
        if (�Ѵ�����ɫ��Ϣ[0].�չ�����2 != "" && �Ѵ�����ɫ��Ϣ[0].�չ�����2 != null)
        {
            ״̬�ֵ�.Add("�չ�Atk2", stateMachine.AddState("�չ�Atk2"));
            ״̬�ֵ�["�չ�Atk2"].motion = Resources.Load<AnimationClip>("�����б�/�չ�����/" + �Ѵ�����ɫ��Ϣ[0].�չ�����2);
            ״̬�ֵ�["�չ�Atk2"].AddExitTransition(true);

        }
        if (�Ѵ�����ɫ��Ϣ[0].�չ�����3 != "" && �Ѵ�����ɫ��Ϣ[0].�չ�����3 != null)
        {
            ״̬�ֵ�.Add("�չ�Atk3", stateMachine.AddState("�չ�Atk3"));
            ״̬�ֵ�["�չ�Atk3"].motion = Resources.Load<AnimationClip>("�����б�/�չ�����/" + �Ѵ�����ɫ��Ϣ[0].�չ�����3);
            ״̬�ֵ�["�չ�Atk3"].AddExitTransition(true);

        }
        if (�Ѵ�����ɫ��Ϣ[0].�չ�����4 != "" && �Ѵ�����ɫ��Ϣ[0].�չ�����4 != null)
        {
            ״̬�ֵ�.Add("�չ�Atk4", stateMachine.AddState("�չ�Atk4"));
            ״̬�ֵ�["�չ�Atk4"].motion = Resources.Load<AnimationClip>("�����б�/�չ�����/" + �Ѵ�����ɫ��Ϣ[0].�չ�����4);
            ״̬�ֵ�["�չ�Atk4"].AddExitTransition(true);

        }
        �չ����¼�(�Ѵ�����ɫ��Ϣ[0]);


        return true;

    }
    RuntimeAnimatorController run;
    public Animator ani;
    public void �չ����¼�( ��ɫ��Ϣ ��ɫ��Ϣ_)
    {
        run = ani.runtimeAnimatorController;
        var ride = new AnimatorOverrideController();
        ride.runtimeAnimatorController = ani.runtimeAnimatorController;
        var _event = new AnimationEvent();
        _event.functionName = "�򿪹�����ײ��";
        float temp;
        float.TryParse(��ɫ��Ϣ_.�չ��˺�ʱ��1 , out temp);
        _event.time = temp;
        if (��ɫ��Ϣ_.�չ�����1 !="")
        {
            if (ride[��ɫ��Ϣ_.�չ�����1].events.Length == 0)
            {
                ride[��ɫ��Ϣ_.�չ�����1].AddEvent(_event);
            }
            else
            {
                ride[��ɫ��Ϣ_.�չ�����1].events[0] = _event;
            }
        }
       
        float.TryParse(��ɫ��Ϣ_.�չ��˺�ʱ��2, out temp);
        _event.time = temp;
        if (��ɫ��Ϣ_.�չ�����2 != "")
        {
            if (ride[��ɫ��Ϣ_.�չ�����2].events.Length == 0)
            {
                ride[��ɫ��Ϣ_.�չ�����2].AddEvent(_event);
            }
            else
            {
                ride[��ɫ��Ϣ_.�չ�����2].events[0] = _event;
            }
        }
        float.TryParse(��ɫ��Ϣ_.�չ��˺�ʱ��3, out temp);
        _event.time = temp;
        if (��ɫ��Ϣ_.�չ�����3 != "")
        {
            if (ride[��ɫ��Ϣ_.�չ�����3].events.Length == 0)
            {
                ride[��ɫ��Ϣ_.�չ�����3].AddEvent(_event);
            }
            else
            {
                ride[��ɫ��Ϣ_.�չ�����3].events[0] = _event;
            }
        }
        float.TryParse(��ɫ��Ϣ_.�չ��˺�ʱ��4, out temp);
        _event.time = temp;
        if (��ɫ��Ϣ_.�չ�����4 != "")
        {
            if (ride[��ɫ��Ϣ_.�չ�����4].events.Length == 0)
            {
                ride[��ɫ��Ϣ_.�չ�����4].AddEvent(_event);
            }
            else
            {
                ride[��ɫ��Ϣ_.�չ�����4].events[0] = _event;
            }
        }
        ani.Rebind();
        ani.runtimeAnimatorController = ride; 
    }

    private void OnEnable()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "ѡģ��");
        NotificationCenter.DefaultCenter.AddObserver(this, "ѡ����");
        NotificationCenter.DefaultCenter.AddObserver(this, "ѡ��Ч");
    }
    private void OnDisable()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, "ѡģ��");
        NotificationCenter.DefaultCenter.RemoveObserver(this, "ѡ����");
        NotificationCenter.DefaultCenter.RemoveObserver(this, "ѡ��Ч");
    }
    void ѡģ��(Notification data)
    {
        if (��ʾ��ɫλ��.childCount > 0)
        {
            Destroy(��ʾ��ɫλ��.GetChild(0).gameObject);
        }
        �ı�����["��ɫģ��"].text = data.data + "";
        GameObject obj =   Instantiate(ģ��ѡ��.instance.��ȡѡ��ģ��());
        obj.transform.parent = ��ʾ��ɫλ��;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        ani = obj.GetComponent<Animator>();
    }
    public void Ԥ�����ܶ���()
    {
        if (ani == null)
        {
            return;
        }
        ��ȡ����������(1);
    } 
    public bool ��ȡ����������(int ���)
    {
        string ID = "�Ƿ�ѭ��" + ���;
        string ѭ��ʱ�� = "ѭ��ʱ��" + ���;
        string ���� = "����" + ���;
        if (�ı�����[����].text == "" || �ı�����[����].text == null)
        {
            return false;
        }
        if (��������[ID].isOn)
        {
            ani.SetBool("�Ƿ�ѭ��", true);
        }
        if (�ı�����[ѭ��ʱ��].text != "" && �ı�����[ѭ��ʱ��].text != null)
        {
            ani.SetFloat("ѭ��ʱ��", float.Parse(�ı�����[ѭ��ʱ��].text));
        }
        ani.SetInteger("��ǰ����" , ���);
        if (�ı�����[����].text != "" && �ı�����[����].text != null)
        {
            ani.Play(�ı�����[����].text, 0);
        }
        return true;
    }
    public bool ��ȡ��Ч������(int ���)
    {
        if (��� == 1)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����1 == "" || �Ѵ�����ɫ��Ϣ[0].����1 ==null)
            {
                return false;
            }
        }
       else if (��� == 2)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����2 == "" || �Ѵ�����ɫ��Ϣ[0].����2 == null)
            {
                return false;
            }
        }
        else if (��� == 3)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����3 == "" || �Ѵ�����ɫ��Ϣ[0].����3 == null)
            {
                return false;
            }
        }
        switch (���)
        {
            case 1:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч1 != "" && �Ѵ�����ɫ��Ϣ[0].��Ч1 != null)
                {
                    Instantiate(Resources.Load<GameObject>("��Ч/" + �Ѵ�����ɫ��Ϣ[0].��Ч1),PlayerC.instance.transform) ;
                }
                break;
            case 2:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч2 != "" && �Ѵ�����ɫ��Ϣ[0].��Ч2 != null)
                {
                    Instantiate(Resources.Load<GameObject>("��Ч/" + �Ѵ�����ɫ��Ϣ[0].��Ч2), PlayerC.instance.transform);
                }
                break;
            case 3:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч3 != "" && �Ѵ�����ɫ��Ϣ[0].��Ч3 != null)
                {
                    Instantiate(Resources.Load<GameObject>("��Ч/" + �Ѵ�����ɫ��Ϣ[0].��Ч3), PlayerC.instance.transform);
                }
                break;
        }
        return true;
    }
    public int ���ݱ�Ż�ȡʱ��(int ���)
    {
        switch (���)
        {
            case 1:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��1_ == "" || �Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��1_ == null)
                {
                    return -1;
                }
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��1_ == "0")
                {
                    return 0;
                }else
                {
                    return 1;
                } 
            case 2:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��2_ == "" || �Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��2_ == null)
                {
                    return -1;
                }
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��2_ == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            case 3:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��3_ == "" || �Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��3_ == null)
                {
                    return -1;
                }
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��3_ == "0")
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
    void ѡ����(Notification data)
    {
        string[] ls = (data.data + "").Split("^");
        �ı�����[ls[0]].text = ls[1];
    }
    void ѡ��Ч(Notification data)
    {
        string[] ls = (data.data + "").Split("^");
        �ı�����[ls[0]].text = ls[1];
    }
    private void Awake()
    {
        Time.timeScale = 1;
        ����();
        �󶨿ɿ�Ԫ��(transform);
        ��();
        if (�Ѵ�����ɫ��Ϣ!=null && �Ѵ�����ɫ��Ϣ.Count >0)
        {
            ִ�����(�Ѵ�����ɫ��Ϣ[0]);
        }
    }
    public void �浵()
    {
        foreach (�浵��ؽӿ� item in myInterfaces)
        {
            if (!item.����д��浵�ļ�())
            {
                return;
            }
        }
        �浵������.�浵();
        ϵͳ��ʾC.instance.��("����ɹ�");
    }
    public void ɾ��()
    {
        //�浵������.ɾ���浵();
        �Ƿ�浵 = false; 
    }
    bool �Ƿ�浵 = true;
    public GameObject[] �����б�;

    public void �򿪹������(GameObject obj)
    {
        for (int i = 0; i < �����б�.Length; i ++ )
        {
            �����б�[i].SetActive(false);
        }
        obj.transform.SetAsLastSibling();
        obj.SetActive(true);
    }
    public void �򿪶������(GameObject obj)
    {
        obj.transform.SetAsLastSibling();
        obj.SetActive(true);
    }

    void ����()
    {
        �浵������.����();
        myInterfaces = FindAllTypes<�浵��ؽӿ�>();
        foreach (�浵��ؽӿ� item in myInterfaces)
        {
            item.��ȡ�浵��ʼ������();
        }
    }
    public static List<T> FindAllTypes<T>()
    {
        List<T> interfaces = new List<T>();
        var types = FindObjectsOfType<MonoBehaviour>().OfType<T>();
        foreach (T t in types)
        {
            interfaces.Add(t);
        }
        return interfaces;
    }
    void �󶨿ɿ�Ԫ��(Transform transform_)
    {
        for (int i = 0; i < transform_.childCount;i++)
        {
            Transform child = transform_.GetChild(i);
            ִ�а�(child);
            if(child.childCount > 0)
            {
                �󶨿ɿ�Ԫ��(child);
            }
        }

    }
    void ִ�а�(Transform child)
    {
        if (child.CompareTag("UI�ı�"))
        {
            if (�ı����� == null)
            {
                �ı����� = new Dictionary<string, InputField>();
            }
            if (!�ı�����.ContainsKey(child.name))
            {
                �ı�����.Add(child.name,child.GetComponent<InputField>());
            }
        }else if (child.CompareTag("UI����")) {
            if (�������� == null)
            {
                �������� = new Dictionary<string, Toggle>();
            }
            ��������.Add(child.name, child.GetComponent<Toggle>());
        }
        else if (child.CompareTag("UI����"))
        {
            if (�������� == null)
            {
                �������� = new Dictionary<string, Dropdown>();
            }
            ��������.Add(child.name, child.GetComponent<Dropdown>());
        }
    }
    void ��()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Placeholder");
        foreach (var temp in gameObjects)
        {
            temp.GetComponent<Text>().text = temp.transform.parent.name;
            temp.tag = "Untagged";
        }
    }
    void ִ�����( System.Object data)
    {
        if (data == null)
        {
            return;
        }
        foreach (PropertyInfo info in data.GetType().GetProperties())
        {
            if (�ı����� !=null)
            {
                if (�ı�����.ContainsKey(info.Name))
                {
                    if (data.GetType().GetProperty(info.Name).GetValue(data,null)!=null)
                    {
                        �ı�����[info.Name].text = data.GetType().GetProperty(info.Name).GetValue(data, null) + "";
                    }
                }
            }
            if (�������� != null)
            {
                if (��������.ContainsKey(info.Name))
                {
                    if (data.GetType().GetProperty(info.Name).GetValue(data, null) != null)
                    {
                        ��������[info.Name].value = int.Parse(data.GetType().GetProperty(info.Name).GetValue(data, null) + "");
                    }
                }
            }
            if (�������� != null)
            {
                if (��������.ContainsKey(info.Name))
                {
                    if (data.GetType().GetProperty(info.Name).GetValue(data, null) != null)
                    {
                        ��������[info.Name].isOn = int.Parse(data.GetType().GetProperty(info.Name).GetValue(data, null) + "") == 1 ? true:false ;
                    }
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (�ı�����["��ɫģ��"].text !=null && �ı�����["��ɫģ��"].text != "")
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("��ɫģ��/" + �ı�����["��ɫģ��"].text));
            obj.transform.parent = ��ʾ��ɫλ��;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
            ani = obj.GetComponent<Animator>(); 

        }
    }
    public void ����ر�(GameObject obj)
    {
        obj.SetActive(false);
    }
    public Dropdown ����ѡ��;
    public void ��������()
    {
        switch(����ѡ��.value){
            case 0:
                ���ٶ����();
                break;
        }
    }
    public GameObject �ٶȵ������;
    void ���ٶ����()
    {
        �ٶȵ������.SetActive(true);
    }
}
