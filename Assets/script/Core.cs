using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static List<��ɫ��Ϣ> �Ѵ�����ɫ��Ϣ = new List<��ɫ��Ϣ>();
    public static List<���ܵ���> �Ѵ������ܵ�����Ϣ = new List<���ܵ���>();
    public static List<��������> �Ѵ�������������Ϣ = new List<��������>();
    public static List<�������ӵ���> �Ѵ����������ӵ�����Ϣ = new List<�������ӵ���>();
    public static bool �ͷż��ܲ����ƶ�;
    public static GameObject ��ɫ;
    public static ���ܵ��� ����Q, ����W, ����E;

    public static float �ƶ��ٶ� { get; set;}
    public static float ����ʱ�� { get; set; }
    public static float ��¼�ƶ��ٶ�;
    List<I�浵��ؽӿ�> myInterfaces;
    static Transform ��ɫtf;
    static Animator ani;

    public static void �����¼�����(Animator ani)
    {
        bool res;

        int ��ǰ���� = ani.GetInteger("��ǰ����");
        if (��ǰ���� < 3)
        {
            int ʱ�� = ���ݱ�Ż�ȡʱ��(��ǰ����);
            if (ʱ�� == 0)
            {
                res = ��ȡ��Ч���ݲ�����(��ǰ����);
            }
        }
    }

    public static int ���ݱ�Ż�ȡʱ��(int ���)
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
                }
                else
                {
                    return 1;
                }
            case 2:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��2_ == "" || �Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��2_ == null)
                {
                    return -1;
                }
                if (�Ѵ�����ɫ��Ϣ[0].��Ч����ʱ��1_ == "0")
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
    public static bool ��ȡ��Ч���ݲ�����(int ���)
    {
        if (��� == 1)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����1_ == "" || �Ѵ�����ɫ��Ϣ[0].����1_ == null)
            {
                return false;
            }
        }
        else if (��� == 2)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����2_ == "" || �Ѵ�����ɫ��Ϣ[0].����2_ == null)
            {
                return false;
            }
        }
        else if (��� == 3)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����3_ == "" || �Ѵ�����ɫ��Ϣ[0].����3_ == null)
            {
                return false;
            }
        }
        switch (���)
        {
            case 1:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч1 != null && �Ѵ�����ɫ��Ϣ[0].��Ч1 != "")
                {
                    GameObject.Instantiate(Resources.Load<GameObject>("��Ч/" + �Ѵ�����ɫ��Ϣ[0].��Ч1));
                }
                break;
            case 2:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч2 != null && �Ѵ�����ɫ��Ϣ[0].��Ч2 != "")
                {
                    GameObject.Instantiate(Resources.Load<GameObject>("��Ч/" + �Ѵ�����ɫ��Ϣ[0].��Ч2));
                }
                break;
            case 3:
                if (�Ѵ�����ɫ��Ϣ[0].��Ч3 != null && �Ѵ�����ɫ��Ϣ[0].��Ч3 != "")
                {
                    GameObject.Instantiate(Resources.Load<GameObject>("��Ч/" + �Ѵ�����ɫ��Ϣ[0].��Ч3));
                }
                break;
        }
        return true;
    }
    public static void ��������(Animator ani)
    {
        ani.SetFloat("ѭ��ʱ��", 0);
        ani.SetBool("�Ƿ�ѭ��", false);
        int ��� = ani.GetInteger("��ǰ����");
        if (��� < 3)
        {
            bool rsl = ��ȡ�������ݲ�����(��� + 1);
            if (!rsl)
            {
                �ͷż��ܲ����ƶ� = false;
                ani.Play("ID");
            }
        }
        else
        {
            �ͷż��ܲ����ƶ� = false;
            ani.Play("ID");
        }
    }
    public static �������ӵ��� ����ID���Ҷ������ӵ���(int ID)
    {
        for (int i = 0; i < �Ѵ����������ӵ�����Ϣ.Count; i++)
        {
            if (�Ѵ����������ӵ�����Ϣ[i].ID == ID)
            {
                return �Ѵ����������ӵ�����Ϣ[i];
            }
        }
        return null;
    }
    public static �������� ����ID���Ҷ�������(int ID)
    {
        for (int i = 0; i < �Ѵ�������������Ϣ.Count; i++)
        {
            if (�Ѵ�������������Ϣ[i].ID == ID)
            {
                return �Ѵ�������������Ϣ[i];
            }
        }
        return null;
    }
    public static ���ܵ��� ����ID���ҹ��ܵ���(int ID)
    {
        for (int i = 0; i < �Ѵ������ܵ�����Ϣ.Count; i++)
        {
            if (�Ѵ������ܵ�����Ϣ[i].ID == ID)
            {
                return �Ѵ������ܵ�����Ϣ[i];
            }
        }
        return null;
    }
    public static ��ɫ��Ϣ ��ȡ��ǰ��ɫ����()
    {
        return �Ѵ�����ɫ��Ϣ[0];
    }
    public void ����()
    {
        �浵������.����();
        myInterfaces = FindAllTypes<I�浵��ؽӿ�>();
        foreach (I�浵��ؽӿ� item in myInterfaces)
        {
            item.��ȡ�浵��ʼ������();
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
    public static void ��ȡ�浵��ʼ������()
    {
        �Ѵ�����ɫ��Ϣ = �浵������.�浵�ļ�_.�Ѵ�����ɫ��Ϣ;
        �Ѵ������ܵ�����Ϣ = �浵������.�浵�ļ�_.�Ѵ������ܵ�����Ϣ;
        �Ѵ�������������Ϣ = �浵������.�浵�ļ�_.�Ѵ�������������Ϣ;
        �Ѵ����������ӵ�����Ϣ = �浵������.�浵�ļ�_.�Ѵ����������ӵ�����Ϣ;
    }
    public static void Start()
    {
        if (�Ѵ�����ɫ��Ϣ[0].�ƶ��ٶ� != "" && �Ѵ�����ɫ��Ϣ[0].�ƶ��ٶ� != null)
        {
            �ƶ��ٶ� = float.Parse(�Ѵ�����ɫ��Ϣ[0].�ƶ��ٶ�);
        }
        if (�Ѵ�����ɫ��Ϣ[0].����ʱ�� != "" && �Ѵ�����ɫ��Ϣ[0].����ʱ�� != null)
        {
            ����ʱ�� = float.Parse(�Ѵ�����ɫ��Ϣ[0].����ʱ��);
        }
    }
    public static bool ��ȡ�չ�����������(int ���)
    {
        ani.Play("�չ�Atk" + ���);
        return true;
    }
    public static bool ��ȡ�������ݲ�����(int ���)
    {
        string ID = "�Ƿ�ѭ��" + ���;
        string ѭ��ʱ�� = "ѭ��ʱ��" + ���;
        string ���� = "����" + ���;
        �ͷż��ܲ����ƶ� = true;
        if (��� == 1)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����1_ == "" || �Ѵ�����ɫ��Ϣ[0].����1_ == null)
            {
                return false;
            }
            ani.SetBool("�Ƿ�ѭ��", �Ѵ�����ɫ��Ϣ[0].�Ƿ�ѭ��1 == "1" ? true : false);
            if (�Ѵ�����ɫ��Ϣ[0].�Ƿ�ѭ��1 == "1")
            {
                ani.SetFloat("ѭ��ʱ��", int.Parse(�Ѵ�����ɫ��Ϣ[0].ѭ��ʱ��1));
            }
            ani.Play(�Ѵ�����ɫ��Ϣ[0].����1, 0);
        }
        else if (��� == 2)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����2_ == "" || �Ѵ�����ɫ��Ϣ[0].����2_ == null)
            {
                return false;
            }
            ani.SetBool("�Ƿ�ѭ��", �Ѵ�����ɫ��Ϣ[0].�Ƿ�ѭ��2 == "1" ? true : false);
            if (�Ѵ�����ɫ��Ϣ[0].�Ƿ�ѭ��2 == "1")
            {
                ani.SetFloat("ѭ��ʱ��", int.Parse(�Ѵ�����ɫ��Ϣ[0].ѭ��ʱ��2));
            }
            ani.Play(�Ѵ�����ɫ��Ϣ[0].����2, 0);
        }
        else if (��� == 3)
        {
            if (�Ѵ�����ɫ��Ϣ[0].����3_ == "" || �Ѵ�����ɫ��Ϣ[0].����3 == null)
            {
                return false;
            }
            ani.SetBool("�Ƿ�ѭ��", �Ѵ�����ɫ��Ϣ[0].�Ƿ�ѭ��3 == "1" ? true : false);
            if (�Ѵ�����ɫ��Ϣ[0].�Ƿ�ѭ��3 == "1")
            {
                ani.SetFloat("ѭ��ʱ��", int.Parse(�Ѵ�����ɫ��Ϣ[0].ѭ��ʱ��3));
            }
            ani.Play(�Ѵ�����ɫ��Ϣ[0].����3, 0);
        }
        ani.SetInteger("��ǰ����", ���);
        return true;
    }
    public static void ����ani(Animator ani_)
    {
        ani = ani_;
    }
    public static GameObject ���ؽ�ɫ()
    {
        GameObject obj = Resources.Load<GameObject>("��ɫģ��/" + �Ѵ�����ɫ��Ϣ[0].��ɫģ��);
        ��ɫ = GameObject.Instantiate(obj);
        if (GameObject.Find("��ɫ������") != null)
        {
            ��ɫ.transform.position = GameObject.Find("��ɫ������").transform.transform.position;
            ��ɫtf = ��ɫ.transform;
        }
        return ��ɫ;
    }
    public void ���ر༭��()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
