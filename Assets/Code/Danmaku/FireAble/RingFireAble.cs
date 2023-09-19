using System;
using UnityEngine;

namespace StDanmaku
{
    [Serializable]
    public class RingFireAble : FireAble
    {
        public int count;

        public RingFireAble(Bullet bulletPrefab, Transform parent, int count) : base(bulletPrefab, parent)
        {
            this.count = count;
        }

        public override void Fire(FireData fireData, object userData = null, Action<object, Collider2D> onHit = null,
            Action onEnd = null)
        {
            var deltaAngle = 360 / count;
            for (int i = 0; i < count; i++)
            {
                var bullet = GetBullet();
                bullet.name = "Bullet" + i;
                fireData.srcRotate = deltaAngle * i;
                bullet.SetFireData(fireData);
                bullet.userData = userData;
                bullet.onHit = onHit;
                bullet.onEnd = onEnd;
            }
        }
    }
}