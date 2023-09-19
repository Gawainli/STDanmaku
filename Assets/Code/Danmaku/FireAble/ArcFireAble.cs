using System;
using UnityEngine;

namespace StDanmaku
{
    [Serializable]
    public class ArcFireAble : FireAble
    {
        public int count;
        public float radius;

        public ArcFireAble(Bullet bulletPrefab, Transform parent, int count, float radius) : base(bulletPrefab, parent)
        {
            this.count = count;
            this.radius = radius;
        }

        public override void Fire(FireData fireData, object userData = null, Action<object, Collider2D> onHit = null,
            Action onEnd = null)
        {
            //fir bullet in arc
            var deltaAngle = radius / (count - 1);
            for (int i = 0; i < count; i++)
            {
                var bullet = GetBullet();
                bullet.name = "Bullet" + i;
                fireData.srcRotate = -radius * 0.5f + deltaAngle * i;
                bullet.SetFireData(fireData);
                bullet.userData = userData;
                bullet.onHit = onHit;
                bullet.onEnd = onEnd;
            }
        }
    }
}