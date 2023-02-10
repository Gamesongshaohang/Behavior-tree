using Core.AI;
using Core.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;
using Object = UnityEngine.Object;
using UnityEngine;
using Core.Character;

namespace Assets.Core.AI.Boss
{
    /// <summary>
    /// 射击节点
    /// </summary>
    class Shoot:EnemyAction
    {
        public List<Weapon> weapons;//发射的武器
        public bool shakeCamera;//镜头是否震动

        public override TaskStatus OnUpdate()
        {
            //遍历每一个子弹发射出去
            foreach (var weapon in weapons)
            {
                //创建子弹
  
                var projectile = Object.Instantiate(weapon.projectilePrefab,weapon.weaponTransform.position,Quaternion.identity);
                projectile.Shooter = gameObject;
                var dirx = this.transform.localScale.x;
                var force = new Vector2(weapon.horizontalForce*dirx,weapon.verticalForce);
                projectile.SetForce(force); //施加力
             

                //镜头震动
                if (shakeCamera)
                {
                    CameraController.Instance.ShakeCamera(0.5f);
                }


            }

            return TaskStatus.Success;
        }
    }
}
