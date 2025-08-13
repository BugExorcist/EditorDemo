using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCam : MonoBehaviour
{
    [SerializeField]
    bool ֻ����Y��;

    [SerializeField]
    bool is���� = false;
    // Update is called once per frame
    void LateUpdate()
    {
        if (Camera.main == null)
        {
            return;
        }
        if (ֻ����Y��)
        {
            Vector3 v = Camera.main.transform.eulerAngles;
            v.x = 0;
            v.z = 0;
            if (is����)
            {
                v.y = -1 * v.y;
            }
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(v), Time.deltaTime * 8);
            return;
        }
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
