using Core.AI;
using Core.Character;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace Assets.Core.AI.Boss
{
    /// <summary>
    /// 跳跃节点
    /// </summary>
    class Jump:EnemyAction
    {
        public float horizontalForce = 5.0f; //水平施加的力
        public float jumpForce = 10.0f; //向上跳跃的力

        public float buildupTime; //跳跃前摇/蓄力时间
        public float jumpTime; //跳跃时间 跳跃向上到落地的时间 注意：此时间必须跟动画匹配

        public string animationTriggerName;//跳跃动画触发的参数名字
        public bool shakeCameraOnLanding; //落地时镜头是否要震动

        private bool hasLander; //是否已落地，落地后结束动画

        private Tween buildupTween; //起跳动画
        private Tween jumpTween; //跳跃动画

        private int dir; //面朝向
        public override void OnStart()
        {
            hasLander = false;
            buildupTween = DOVirtual.DelayedCall(buildupTime,StartJump); //DOTween的延迟方法
            animator.SetTrigger(animationTriggerName);
        }


        private void StartJump()
        {
            //跳跃方向
            //dir = player.transform.position.x > this.transform.position.x ? 1 : -1;

            //施加跳跃的力
            body.AddForce(new Vector2(horizontalForce * dir, jumpForce),ForceMode2D.Impulse); //2d加力的模式，必须设置

            //镜头震动
            jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
            {
                if (shakeCameraOnLanding)
                {
                    CameraController.Instance.ShakeCamera(0.5f);
                }
            });
        }

        public override TaskStatus OnUpdate()
        {
            dir = player.transform.position.x > this.transform.position.x ? 1 : -1;
            //行为树重写的onUpdate方法是return TaskStatus.success，也就是成功后才执行
            //这样是不行的，节点切换错误
            //return base.OnUpdate(); 
            spriteRenderer.flipX = dir < 0;
            return hasLander ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            jumpTween?.Kill();
        }
    }
}
