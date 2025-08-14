using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Main是绑定在Canvas上的核心控制器脚本，主要功能是管理UI面板上的所有交互组件（文本框、开关、下拉菜单等）
// 负责将用户输入的数据保存到存档，并将存档数据加载到UI组件中进行显示
// 同时处理动画控制器（.controller文件）的动态创建、初始化和配置

public class Main : MonoBehaviour , 存档相关接口
{
    // 使用字典来存储所有的组件，为了方便使用名称动态的创建和访问
    public Dictionary<string, InputField> 文本容器;
    public Dictionary<string, Toggle> 开关容器;
    public Dictionary<string, Dropdown> 下拉容器;
    public List<角色信息> 已创建角色信息;
    public List<功能单项> 已创建功能信息;
    List<存档相关接口> myInterfaces;// 在读档的时候初始化
    public Transform 演示角色位置;

    public void 运行Demo()
    {
        SceneManager.LoadScene(1);
    }
    public void 读取存档初始化数据()
    {
        已创建角色信息 = 存档控制器.存档文件_.已创建角色信息;
        已创建功能信息 = 存档控制器.存档文件_.已创建功能单项信息;
        if (已创建角色信息.Count == 0)
        {
            角色信息 temp = new 角色信息();temp.角色ID_ = "";
            已创建角色信息.Add(temp);
        }
    }
    public AnimatorStateMachine stateMachine;
    public AnimatorController animatorController;

    public bool 数据写入存档文件()
    {
        //if (文本容器["连段时间"].text =="" || 文本容器["连段时间"].text==null )
        //{
        //    系统提示C.instance.打开("连段时间必须大于0");
        //    return false;
        //}
        foreach (var temp in 文本容器)
        {
            PropertyInfo p = 已创建角色信息[0].GetType().GetProperty(temp.Key);
            if (p !=null)
            {
                p.SetValue(已创建角色信息[0], temp.Value.text);
            }
        }
        foreach (var temp in 开关容器)
        {
            PropertyInfo p = 已创建角色信息[0].GetType().GetProperty(temp.Key);
            if (p != null)
            {
                p.SetValue(已创建角色信息[0], temp.Value.isOn?"1":"0");
            }
        }
        foreach (var temp in 下拉容器)
        {
            PropertyInfo p = 已创建角色信息[0].GetType().GetProperty(temp.Key);
            if (p != null)
            {
                p.SetValue(已创建角色信息[0], temp.Value.value+"");
            }
        }
        存档控制器.存档文件_.已创建角色信息 = 已创建角色信息;
        存档控制器.存档文件_.已创建功能单项信息 = 已创建功能信息;

        File.Delete("Assets/Resources/动画控制器/控制xxx.controller");
        animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/Resources/动画控制器/控制xxx.controller");
        stateMachine = animatorController.layers[0].stateMachine;
        Dictionary<string, AnimatorState> 状态字典 = new Dictionary<string, AnimatorState>();

        stateMachine.defaultState = 状态字典["ID"];

        状态字典.Add("ID",stateMachine.AddState("ID"));
        状态字典.Add("run", stateMachine.AddState("run"));
        状态字典.Add("Atk1", stateMachine.AddState("Atk1"));
        状态字典.Add("Atk2", stateMachine.AddState("Atk2"));
        状态字典.Add("Atk3", stateMachine.AddState("Atk3"));

        状态字典["ID"].motion = Resources.Load<AnimationClip>("动作列表/预设不可选动作/" +"ID");
        状态字典["run"].motion = Resources.Load<AnimationClip>("动作列表/预设不可选动作/" + "run");

        状态字典["Atk1"].motion = Resources.Load<AnimationClip>("动作列表/技能动作/" + "Atk1");
        状态字典["Atk1"].AddStateMachineBehaviour<anid>();

        状态字典["Atk2"].motion = Resources.Load<AnimationClip>("动作列表/技能动作/" + "Atk2");
        状态字典["Atk2"].AddStateMachineBehaviour<anid>();

        状态字典["Atk3"].motion = Resources.Load<AnimationClip>("动作列表/技能动作/" + "Atk3");
        状态字典["Atk3"].AddStateMachineBehaviour<anid>();

        animatorController.AddParameter("是否循环",AnimatorControllerParameterType.Bool);
        animatorController.AddParameter("循环时间", AnimatorControllerParameterType.Float);
        animatorController.AddParameter("当前动作", AnimatorControllerParameterType.Int);
        animatorController.AddParameter("移动", AnimatorControllerParameterType.Bool);

        AnimatorStateTransition trans = 状态字典["ID"].AddTransition(状态字典["run"],false);
        trans.AddCondition(AnimatorConditionMode.If,1,"移动");
        trans = 状态字典["run"].AddTransition(状态字典["ID"], false);
        trans.AddCondition(AnimatorConditionMode.IfNot, 1, "移动");

       
        if (已创建角色信息[0].普攻动作1 !="" && 已创建角色信息[0].普攻动作1 != null)
        {
            状态字典.Add("普攻Atk1", stateMachine.AddState("普攻Atk1"));
            状态字典["普攻Atk1"].motion = Resources.Load<AnimationClip>("动作列表/普攻动作/" + 已创建角色信息[0].普攻动作1);
            状态字典["普攻Atk1"].AddExitTransition(true);
             
        }
        if (已创建角色信息[0].普攻动作2 != "" && 已创建角色信息[0].普攻动作2 != null)
        {
            状态字典.Add("普攻Atk2", stateMachine.AddState("普攻Atk2"));
            状态字典["普攻Atk2"].motion = Resources.Load<AnimationClip>("动作列表/普攻动作/" + 已创建角色信息[0].普攻动作2);
            状态字典["普攻Atk2"].AddExitTransition(true);

        }
        if (已创建角色信息[0].普攻动作3 != "" && 已创建角色信息[0].普攻动作3 != null)
        {
            状态字典.Add("普攻Atk3", stateMachine.AddState("普攻Atk3"));
            状态字典["普攻Atk3"].motion = Resources.Load<AnimationClip>("动作列表/普攻动作/" + 已创建角色信息[0].普攻动作3);
            状态字典["普攻Atk3"].AddExitTransition(true);

        }
        if (已创建角色信息[0].普攻动作4 != "" && 已创建角色信息[0].普攻动作4 != null)
        {
            状态字典.Add("普攻Atk4", stateMachine.AddState("普攻Atk4"));
            状态字典["普攻Atk4"].motion = Resources.Load<AnimationClip>("动作列表/普攻动作/" + 已创建角色信息[0].普攻动作4);
            状态字典["普攻Atk4"].AddExitTransition(true);

        }
        普攻绑定事件(已创建角色信息[0]);


        return true;

    }

    RuntimeAnimatorController run;// 原动画控制器
    public Animator ani;
    public void 普攻绑定事件( 角色信息 角色信息_)
    {   
        // 在 Unity 的动画系统中，不能直接修改原始 Animator Controller 的资源文件（.controller 文件）
        // 所以，我们只能创建一个副本，对副本进行修改，再把副本赋给 Animator

        run = ani.runtimeAnimatorController;
        var ride = new AnimatorOverrideController(run);// 创建一个副本
        //ride.runtimeAnimatorController = ani.runtimeAnimatorController;
        var _event = new AnimationEvent();// 添加动画事件
        _event.functionName = "打开攻击碰撞体";
        float temp;
        float.TryParse(角色信息_.普攻伤害时间1 , out temp);// 根据伤害的时机设置事件触发的时间
        _event.time = temp;
        if (角色信息_.普攻动作1 !="")
        {
            if (ride[角色信息_.普攻动作1].events.Length == 0)
            {
                ride[角色信息_.普攻动作1].AddEvent(_event);
            }
            else
            {
                ride[角色信息_.普攻动作1].events[0] = _event;
            }
        }
       
        float.TryParse(角色信息_.普攻伤害时间2, out temp);
        _event.time = temp;
        if (角色信息_.普攻动作2 != "")
        {
            if (ride[角色信息_.普攻动作2].events.Length == 0)
            {
                ride[角色信息_.普攻动作2].AddEvent(_event);
            }
            else
            {
                ride[角色信息_.普攻动作2].events[0] = _event;
            }
        }
        float.TryParse(角色信息_.普攻伤害时间3, out temp);
        _event.time = temp;
        if (角色信息_.普攻动作3 != "")
        {
            if (ride[角色信息_.普攻动作3].events.Length == 0)
            {
                ride[角色信息_.普攻动作3].AddEvent(_event);
            }
            else
            {
                ride[角色信息_.普攻动作3].events[0] = _event;
            }
        }
        float.TryParse(角色信息_.普攻伤害时间4, out temp);
        _event.time = temp;
        if (角色信息_.普攻动作4 != "")
        {
            if (ride[角色信息_.普攻动作4].events.Length == 0)
            {
                ride[角色信息_.普攻动作4].AddEvent(_event);
            }
            else
            {
                ride[角色信息_.普攻动作4].events[0] = _event;
            }
        }
        ani.Rebind();
        ani.runtimeAnimatorController = ride; 
    }

    private void OnEnable()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "选模型");
        NotificationCenter.DefaultCenter.AddObserver(this, "选动作");
        NotificationCenter.DefaultCenter.AddObserver(this, "选特效");
    }
    private void OnDisable()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, "选模型");
        NotificationCenter.DefaultCenter.RemoveObserver(this, "选动作");
        NotificationCenter.DefaultCenter.RemoveObserver(this, "选特效");
    }
    void 选模型(Notification data)
    {
        if (演示角色位置.childCount > 0)
        {
            Destroy(演示角色位置.GetChild(0).gameObject);
        }
        文本容器["角色模型"].text = data.data + "";
        GameObject obj =   Instantiate(模型选择.instance.获取选择模型());
        obj.transform.parent = 演示角色位置;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        ani = obj.GetComponent<Animator>();
    }
    public void 预览技能动画()
    {
        if (ani == null)
        {
            return;
        }
        获取动画并播放(1);
    } 
    public bool 获取动画并播放(int 编号)
    {
        string ID = "是否循环" + 编号;
        string 循环时间 = "循环时间" + 编号;
        string 动作 = "动作" + 编号;
        if (文本容器[动作].text == "" || 文本容器[动作].text == null)
        {
            return false;
        }
        if (开关容器[ID].isOn)
        {
            ani.SetBool("是否循环", true);
        }
        if (文本容器[循环时间].text != "" && 文本容器[循环时间].text != null)
        {
            ani.SetFloat("循环时间", float.Parse(文本容器[循环时间].text));
        }
        ani.SetInteger("当前动作" , 编号);
        if (文本容器[动作].text != "" && 文本容器[动作].text != null)
        {
            ani.Play(文本容器[动作].text, 0);
        }
        return true;
    }
    public bool 获取特效并播放(int 编号)
    {
        if (编号 == 1)
        {
            if (已创建角色信息[0].动作1 == "" || 已创建角色信息[0].动作1 ==null)
            {
                return false;
            }
        }
       else if (编号 == 2)
        {
            if (已创建角色信息[0].动作2 == "" || 已创建角色信息[0].动作2 == null)
            {
                return false;
            }
        }
        else if (编号 == 3)
        {
            if (已创建角色信息[0].动作3 == "" || 已创建角色信息[0].动作3 == null)
            {
                return false;
            }
        }
        switch (编号)
        {
            case 1:
                if (已创建角色信息[0].特效1 != "" && 已创建角色信息[0].特效1 != null)
                {
                    Instantiate(Resources.Load<GameObject>("特效/" + 已创建角色信息[0].特效1),PlayerC.instance.transform);
                }
                break;
            case 2:
                if (已创建角色信息[0].特效2 != "" && 已创建角色信息[0].特效2 != null)
                {
                    Instantiate(Resources.Load<GameObject>("特效/" + 已创建角色信息[0].特效2), PlayerC.instance.transform);
                }
                break;
            case 3:
                if (已创建角色信息[0].特效3 != "" && 已创建角色信息[0].特效3 != null)
                {
                    Instantiate(Resources.Load<GameObject>("特效/" + 已创建角色信息[0].特效3), PlayerC.instance.transform);
                }
                break;
        }
        return true;
    }
    public int 根据编号获取时机(int 编号)
    {
        switch (编号)
        {
            case 1:
                if (已创建角色信息[0].特效产生时机1_ == "" || 已创建角色信息[0].特效产生时机1_ == null)
                {
                    return -1;
                }
                if (已创建角色信息[0].特效产生时机1_ == "0")
                {
                    return 0;
                }else
                {
                    return 1;
                } 
            case 2:
                if (已创建角色信息[0].特效产生时机2_ == "" || 已创建角色信息[0].特效产生时机2_ == null)
                {
                    return -1;
                }
                if (已创建角色信息[0].特效产生时机2_ == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            case 3:
                if (已创建角色信息[0].特效产生时机3_ == "" || 已创建角色信息[0].特效产生时机3_ == null)
                {
                    return -1;
                }
                if (已创建角色信息[0].特效产生时机3_ == "0")
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
    void 选动作(Notification data)
    {
        string[] ls = (data.data + "").Split("^");
        文本容器[ls[0]].text = ls[1];
    }
    void 选特效(Notification data)
    {
        string[] ls = (data.data + "").Split("^");
        文本容器[ls[0]].text = ls[1];
    }
    private void Awake()
    {
        Time.timeScale = 1;
        读档();
        绑定可控元素(transform);
        设置输入文本占位符内容();
        if (已创建角色信息!=null && 已创建角色信息.Count >0)
        {
            执行填充(已创建角色信息[0]);
        }
    }
    public void 存档()
    {
        foreach (存档相关接口 item in myInterfaces)
        {
            if (!item.数据写入存档文件())
            {
                return;
            }
        }
        存档控制器.存档();
        系统提示C.instance.打开("保存成功");
    }
    public void 删档()
    {
        //存档控制器.删除存档();
        是否存档 = false; 
    }
    bool 是否存档 = true;
    public GameObject[] 功能列表;// 需要在编辑器中手动添加功能

    public void 打开功能面板(GameObject obj)
    {
        for (int i = 0; i < 功能列表.Length; i ++ )
        {
            功能列表[i].SetActive(false);
        }
        obj.transform.SetAsLastSibling();// 置为最上层
        obj.SetActive(true);
    }
    public void 打开二级面板(GameObject obj)
    {
        obj.transform.SetAsLastSibling();
        obj.SetActive(true);
    }

    void 读档()
    {
        存档控制器.读档();
        myInterfaces = FindAllTypes<存档相关接口>();
        foreach (存档相关接口 item in myInterfaces)
        {
            item.读取存档初始化数据();
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
    void 绑定可控元素(Transform transform_)// 递归绑定所有的UI元素到字典中
    {
        for (int i = 0; i < transform_.childCount;i++)
        {
            Transform child = transform_.GetChild(i);
            执行绑定(child);
            if(child.childCount > 0)
            {
                绑定可控元素(child);
            }
        }

    }
    void 执行绑定(Transform child)// 根据在UI上的tag,把组件绑定到字典中
    {
        if (child.CompareTag("UI文本"))
        {
            if (文本容器 == null)
            {
                文本容器 = new Dictionary<string, InputField>();
            }
            if (!文本容器.ContainsKey(child.name))
            {
                文本容器.Add(child.name,child.GetComponent<InputField>());
            }
        }else if (child.CompareTag("UI开关")) {
            if (开关容器 == null)
            {
                开关容器 = new Dictionary<string, Toggle>();
            }
            开关容器.Add(child.name, child.GetComponent<Toggle>());
        }
        else if (child.CompareTag("UI下拉"))
        {
            if (下拉容器 == null)
            {
                下拉容器 = new Dictionary<string, Dropdown>();
            }
            下拉容器.Add(child.name, child.GetComponent<Dropdown>());
        }
    }
    void 设置输入文本占位符内容()
    {   // 把所有Tag为“占位符”的GameObject的 Text 组件文本设置为父物体的名称，然后去掉Tag
        // 这个是为了优化用户体验，也为了减少工作量，父物体的名称为这个组件应该填入的内容
        // 例：如果父物体叫"技能cd" 那么运行的时候，这个输入框的提示占位符就会显示浅灰色的"技能cd"
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Placeholder");
        foreach (var temp in gameObjects)
        {
            temp.GetComponent<Text>().text = temp.transform.parent.name;
            temp.tag = "Untagged";
        }
    }
    void 执行填充( System.Object data)// 把从存档中读取的参数填充到UGUI的=组件中
    {
        if (data == null)
        {
            return;
        }
        foreach (PropertyInfo info in data.GetType().GetProperties())
        {
            if (文本容器 !=null)
            {
                if (文本容器.ContainsKey(info.Name))
                {
                    if (data.GetType().GetProperty(info.Name).GetValue(data,null)!=null)
                    {
                        文本容器[info.Name].text = data.GetType().GetProperty(info.Name).GetValue(data, null) + "";
                    }
                }
            }
            if (下拉容器 != null)
            {
                if (下拉容器.ContainsKey(info.Name))
                {
                    if (data.GetType().GetProperty(info.Name).GetValue(data, null) != null)
                    {
                        下拉容器[info.Name].value = int.Parse(data.GetType().GetProperty(info.Name).GetValue(data, null) + "");
                    }
                }
            }
            if (开关容器 != null)
            {
                if (开关容器.ContainsKey(info.Name))
                {
                    if (data.GetType().GetProperty(info.Name).GetValue(data, null) != null)
                    {
                        开关容器[info.Name].isOn = int.Parse(data.GetType().GetProperty(info.Name).GetValue(data, null) + "") == 1 ? true:false ;
                    }
                }
            }
        }
    }

    void Start()
    {// 角色模型不为空就实例化角色模型
        if (文本容器["角色模型"].text !=null && 文本容器["角色模型"].text != "")
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("角色模型/" + 文本容器["角色模型"].text));
            obj.transform.parent = 演示角色位置;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
            ani = obj.GetComponent<Animator>(); 

        }
    }
    public void 点击关闭(GameObject obj)
    {
        obj.SetActive(false);
    }
    public Dropdown 功能选择;// 本Demo中没有使用 功能选择 和 新增功能()
    public void 新增功能()
    {
        switch(功能选择.value){
            case 0:
                打开速度面板();
                break;
        }
    }
    public GameObject 速度调整面板;
    void 打开速度面板()
    {
        速度调整面板.SetActive(true);
    }
}
