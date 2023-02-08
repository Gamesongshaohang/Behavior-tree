using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
    public class IsClient : Conditional
    {
        public override TaskStatus OnUpdate()
        {
#if UNITY_2019_1_OR_NEWER
            throw new InvalidOperationException("Unity2019以后版本必须先引入HLAPI包");
#else
            return NetworkClient.active ? TaskStatus.Success : TaskStatus.Failure;
#endif
        }
    }
}