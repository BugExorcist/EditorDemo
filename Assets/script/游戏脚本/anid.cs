using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anid : StateMachineBehaviour
{ 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Core.�����¼�����(animator); 
    } 
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        if (stateInfo.normalizedTime >=1)
        {
            if (animator.GetBool("�Ƿ�ѭ��"))
            {
                if (stateInfo.normalizedTime >= animator.GetFloat("ѭ��ʱ��"))
                {
                    Core.��������(animator);
                }
            }
            else
            {
                Core.��������(animator);
            } 
        }
    } 
     
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         int ʱ�� = -1;
        int r = animator.GetInteger("��ǰ����");
        if (r <= 3)
        {  
                ʱ�� = Core.���ݱ�Ż�ȡʱ��(r); 
            if (ʱ�� == 1)
            { 
                { 
                    Core.��ȡ��Ч���ݲ�����(r);
                }
              
            }
        } 
    }

    
}
