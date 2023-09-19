using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace StDanmaku
{
    [Serializable]
    public class LineFireAble : FireAble
    {
        public bool twoSide;

        public LineFireAble(Bullet bulletPrefab, Transform parent, bool twoSide) : base(bulletPrefab, parent)
        {
            this.twoSide = twoSide;
        }

        public override void Fire(FireData fireData, object userData = null, Action<object, Collider2D> onHit = null,
            Action onEnd = null)
        {
            var bullet = GetBullet();
            bullet.name = "Bullet";
            bullet.SetFireData(fireData);
            bullet.userData = userData;
            bullet.onHit = onHit;
            bullet.onEnd = onEnd;
            if (twoSide)
            {
                fireData.srcRotate = 180;
                var bullet2 = GetBullet();
                bullet2.name = "Bullet2";
                bullet2.SetFireData(fireData);
                bullet2.userData = userData;
                bullet2.onEnd = onEnd;
                bullet2.onHit = onHit;
            }
        }
    }
}