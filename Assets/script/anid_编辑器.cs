using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anid_编辑器 : StateMachineBehaviour
{
   
    public Main script_;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (script_ == null)
        {
            script_ =    FindObjectOfType<Main>();
        }
        bool rsl; int 时机 = -1;
       int r = animator.GetInteger("当前动作");
        if (r < 3)
        {

            if (script_ == null)
            {
                时机 = 游戏初始化.instance.根据编号获取时机(r);
            }
            else
            {
                时机 = script_.根据编号获取时机(r); 
            }
          
            if (时机==0)
            {
                if (script_ == null)
                {
                    rsl = 游戏初始化.instance.获取特效数据并播放(r);
                }
                else
                {
                    rsl = script_.获取特效并播放(r);
                }
            }
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (stateInfo.normalizedTime >=1)
        {
            if (animator.GetBool("是否循环"))
            {
                if (stateInfo.normalizedTime >= animator.GetFloat("循环时间"))
                {
                    结束动画(animator);
                }
            }
            else
            {
                结束动画(animator);
            }
    
        }
    }


    void 结束动画(Animator animator)
    {
        animator.SetFloat("循环时间", 0);
        animator.SetBool("是否循环", false);
        int r = animator.GetInteger("当前动作");
        if (r  < 3)
        {
            bool rsl;
            if (script_ == null)
            {
                rsl = 游戏初始化.instance.获取动画数据并播放(r + 1);
            }
            else
            {
                  rsl = script_.获取动画并播放(r + 1);
            } 

            if (!rsl)
            {
                if (PlayerC.instance!=null)
                {
                    Core.释放技能不能移动 = false;
                }
              
                animator.Play("ID");
            }
        }
        else
        {
            if (PlayerC.instance != null)
            {
                Core.释放技能不能移动 = false;
            }
            animator.Play("ID");
        }
        
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool rsl; int 时机 = -1;
        int r = animator.GetInteger("当前动作");
        if (r <= 3)
        {
          
            if (script_ == null)
            {
                时机 = 游戏初始化.instance.根据编号获取时机(r);
            }
            else
            {
                时机 = script_.根据编号获取时机(r);
            }
            if (时机 == 1)
            {
                if (script_ == null)
                {
                    rsl = 游戏初始化.instance.获取特效数据并播放(r);
                }
                else
                {
                    rsl = script_.获取特效并播放(r);
                }
            }
        }
        Debug.LogError("OnStateExit");
    }
     
}
