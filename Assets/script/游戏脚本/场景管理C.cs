using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 场景管理C : MonoBehaviour
{
    public GameObject 默认场景;
    void Awake()
    {
        if (场景选择.当前选择的场景 != null)
        {
            Instantiate(Resources.Load<GameObject>("场景模型/" + 场景选择.当前选择的场景) );
        }
        else
        {
            默认场景.SetActive(true);
        }
    }

    
}
