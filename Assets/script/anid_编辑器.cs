using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anid_�༭�� : StateMachineBehaviour
{
   
    public Main script_;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (script_ == null)
        {
            script_ =    FindObjectOfType<Main>();
        }
        bool rsl; int ʱ�� = -1;
       int r = animator.GetInteger("��ǰ����");
        if (r < 3)
        {

            if (script_ == null)
            {
                ʱ�� = ��Ϸ��ʼ��.instance.���ݱ�Ż�ȡʱ��(r);
            }
            else
            {
                ʱ�� = script_.���ݱ�Ż�ȡʱ��(r); 
            }
          
            if (ʱ��==0)
            {
                if (script_ == null)
                {
                    rsl = ��Ϸ��ʼ��.instance.��ȡ��Ч���ݲ�����(r);
                }
                else
                {
                    rsl = script_.��ȡ��Ч������(r);
                }
            }
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (stateInfo.normalizedTime >=1)
        {
            if (animator.GetBool("�Ƿ�ѭ��"))
            {
                if (stateInfo.normalizedTime >= animator.GetFloat("ѭ��ʱ��"))
                {
                    ��������(animator);
                }
            }
            else
            {
                ��������(animator);
            }
    
        }
    }


    void ��������(Animator animator)
    {
        animator.SetFloat("ѭ��ʱ��", 0);
        animator.SetBool("�Ƿ�ѭ��", false);
        int r = animator.GetInteger("��ǰ����");
        if (r  < 3)
        {
            bool rsl;
            if (script_ == null)
            {
                rsl = ��Ϸ��ʼ��.instance.��ȡ�������ݲ�����(r + 1);
            }
            else
            {
                  rsl = script_.��ȡ����������(r + 1);
            } 

            if (!rsl)
            {
                if (PlayerC.instance!=null)
                {
                    Core.�ͷż��ܲ����ƶ� = false;
                }
              
                animator.Play("ID");
            }
        }
        else
        {
            if (PlayerC.instance != null)
            {
                Core.�ͷż��ܲ����ƶ� = false;
            }
            animator.Play("ID");
        }
        
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool rsl; int ʱ�� = -1;
        int r = animator.GetInteger("��ǰ����");
        if (r <= 3)
        {
          
            if (script_ == null)
            {
                ʱ�� = ��Ϸ��ʼ��.instance.���ݱ�Ż�ȡʱ��(r);
            }
            else
            {
                ʱ�� = script_.���ݱ�Ż�ȡʱ��(r);
            }
            if (ʱ�� == 1)
            {
                if (script_ == null)
                {
                    rsl = ��Ϸ��ʼ��.instance.��ȡ��Ч���ݲ�����(r);
                }
                else
                {
                    rsl = script_.��ȡ��Ч������(r);
                }
            }
        }
        Debug.LogError("OnStateExit");
    }
     
}
