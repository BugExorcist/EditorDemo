using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anid : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Core.�����¼�����(animator);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
        }
        else
        {
            Core.��������(animator);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int ��� = animator.GetInteger("��ǰ����");
        if (��� <=3)
        {
            int ʱ�� = Core.���ݱ�Ż�ȡʱ��(���);
            if (ʱ��==1)
            {
                Core.��ȡ��Ч���ݲ�����(���);
            }
        }
    }
}
