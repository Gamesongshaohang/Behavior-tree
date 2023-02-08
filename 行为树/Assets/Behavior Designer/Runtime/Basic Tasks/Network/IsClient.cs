using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
    public class IsClient : Conditional
    {
        public override TaskStatus OnUpdate()
        {
#if UNITY_2019_1_OR_NEWER
            throw new InvalidOperationException("Unity2019�Ժ�汾����������HLAPI��");
#else
            return NetworkClient.active ? TaskStatus.Success : TaskStatus.Failure;
#endif
        }
    }
}