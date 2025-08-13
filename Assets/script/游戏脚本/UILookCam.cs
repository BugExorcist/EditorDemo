using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCam : MonoBehaviour
{
    [SerializeField]
    bool 只朝向Y轴;

    [SerializeField]
    bool is逆向 = false;
    // Update is called once per frame
    void LateUpdate()
    {
        if (Camera.main == null)
        {
            return;
        }
        if (只朝向Y轴)
        {
            Vector3 v = Camera.main.transform.eulerAngles;
            v.x = 0;
            v.z = 0;
            if (is逆向)
            {
                v.y = -1 * v.y;
            }
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(v), Time.deltaTime * 8);
            return;
        }
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
