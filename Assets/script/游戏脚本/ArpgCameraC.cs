using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArpgCameraC : MonoBehaviour
{
     
    // Start is called before the first frame update
    public Transform target;
    float xSpeed =400;
    float ySpeed = 125;

    float x = 20;
    float y = 0;

    float distance = 4;
    float zoomRate = 80;
    float yMinLimit = -10;
    float yMaxlimit = 70;
    float disMinLimit = 2;
    float disMaxLimit = 10;

    Vector3 offset = new Vector3(0,1,0);
    Vector3 rotateOffset = new Vector3(0.1f,0,-1);
    public GameObject ÔÝÍ£²Ëµ¥;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    bool boolÔÝÍ£ = false;
    public void »Ö¸´()
    {
        ÔÝÍ£(false);
    }
    public void ÔÝÍ£(bool v)
    {
        boolÔÝÍ£ = v;
        if (boolÔÝÍ£)
        {
            Time.timeScale = 0;
            ÔÝÍ£²Ëµ¥.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            ÔÝÍ£²Ëµ¥.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ÔÝÍ£(!boolÔÝÍ£);
        }
        x += InputC.instance.m_Camera.x * xSpeed * Time.deltaTime;
        y -= InputC.instance.m_Camera.y * ySpeed * Time.deltaTime;
        y = clampAngle(y,yMinLimit,yMaxlimit );
        Quaternion rotation = Quaternion.Euler(y,x,0);
        transform.rotation = rotation;
        if (InputC.instance.m_Movement.x !=0 || InputC.instance.m_Movement.y !=0)
        {
            target.transform.rotation = Quaternion.Euler(0,x,0);
        }
        distance -= (InputC.instance.m_Camera.z * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance , disMinLimit, disMaxLimit);
        transform.position = target.position + offset + rotation * (rotateOffset * distance) ;
        
    }

    float  clampAngle(float angle, float min ,float max)
    {
        if (angle >360)
        {
            angle -= 360;
        }else if (angle < -360)
        {
            angle += 360;
        }
        return Mathf.Clamp(angle,min,max);
    }

}
