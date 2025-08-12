using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anid : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Core.动画事件处理(animator);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (stateInfo.normalizedTime >=1)
        {
            if (animator.GetBool("是否循环"))
            {
                if (stateInfo.normalizedTime >= animator.GetFloat("循环时间"))
                {
                    Core.结束动画(animator);
                }
            }
        }
        else
        {
            Core.结束动画(animator);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int 编号 = animator.GetInteger("当前动作");
        if (编号 <=3)
        {
            int 时机 = Core.根据编号获取时机(编号);
            if (时机==1)
            {
                Core.获取特效数据并播放(编号);
            }
        }
    }
}
