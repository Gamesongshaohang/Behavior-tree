
using Core.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.AI.Boss
{
    /// <summary>
    /// 面向玩家节点
    /// </summary>
    class FaceToPlayer: EnemyAction
    {
        private float dir; //面朝向
        public override void OnStart()
        {
            dir = player.transform.position.x > this.transform.position.x ? 1 : -1;
            this.transform.localScale = new Vector3(dir, 1, 1);
        }

    }

    
}
