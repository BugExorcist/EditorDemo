using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anid : StateMachineBehaviour
{
    // 当动画开始播放时调用
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Core.动画事件处理(animator);
    }
    // 当动画播放时每帧调用
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // stateInfo.normalizedTime表示动画播放的百分比 区间为0-1 
        if (stateInfo.normalizedTime >=1)// 当动画播放完毕时调用
        {
            if (animator.GetBool("是否循环"))
            {
                if (stateInfo.normalizedTime >= animator.GetFloat("循环时间"))// 写的是循环时间，其实是循环次数
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
    // 当动画播放完毕时调用
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
