using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System.Linq; 
public class 游戏初始化 : MonoBehaviour, 存档相关接口
{

    public static 游戏初始化 instance; 
    List<存档相关接口> myInterfaces;

    public 角色信息 获取当前角色数据()
    {

        return Core.获取当前角色数据(); 
    }


    private void Awake()
    {
        instance = this; 
        读档();
    }
    public void 读档()
    {
        存档控制器.读档();
        myInterfaces = FindAllTypes<存档相关接口>();
        foreach (存档相关接口 item in myInterfaces)
        {
            item.读取存档初始化数据();
        }
        Time.timeScale = 1;
    }
    public static List<T> FindAllTypes<T>()
    {
        List<T> interfaces = new List<T>();

        var types = UnityEngine.MonoBehaviour.FindObjectsOfType<MonoBehaviour>().OfType<T>();

        foreach (T t in types)
        {
            interfaces.Add(t);
        }

        return interfaces;
    }
    public void 读取存档初始化数据()
    {
        Core.读取存档初始化数据(); 
    }
    public bool 数据写入存档文件()
    {
        return true;
    }
     

    void Start()
    {
        加载角色();
        Core.Start();
       
    }
     
  
    public int 根据编号获取时机(int 编号)
    {
        return Core.根据编号获取时机(编号);
    }
    public bool 获取特效数据并播放(int 编号)
    {
        return Core.获取特效数据并播放(编号);
    }
  
    public bool 获取普攻动画并播放( int 编号  )
    {
        return Core.获取普攻动画并播放(编号);


       
    }
    public bool 获取动画数据并播放(int 编号)
    {
        return Core.获取动画数据并播放(编号);
    }
     

    public static ArpgCameraC cameraC;
    void 加载角色()
    {
        cameraC = GameObject.Find("摄像机").transform.GetChild(0).GetComponent<ArpgCameraC>();
       GameObject 角色 =  Core.加载角色();
        角色.AddComponent<PlayerC>();
        cameraC.target = 角色.transform;
        cameraC.gameObject.SetActive(true);
    }

   public void 返回编辑器()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
   
}
