using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StDanmaku
{
    public interface IFireAble
    {
        void Fire(FireData fireData, System.Object userData, Action<object, Collider2D> onHit,
            Action onEnd);
    }

    public abstract class FireAble : IFireAble
    {
        private Bullet bulletPrefab;
        private Transform parent;

        private List<Bullet> activeBulletList = new List<Bullet>();
        private Queue<Bullet> bulletQueue = new Queue<Bullet>();

        protected FireAble(Bullet bulletPrefab, Transform parent)
        {
            this.bulletPrefab = bulletPrefab;
            this.parent = parent;
        }

        protected Bullet GetBullet()
        {
            if (bulletQueue.Count > 0)
            {
                var bullet = bulletQueue.Dequeue();
                bullet.gameObject.SetActive(true);
                activeBulletList.Add(bullet);
                return bullet;
            }
            else
            {
                var bullet = Object.Instantiate(bulletPrefab, parent, true);
                activeBulletList.Add(bullet);
                return bullet;
            }
        }

        public void TickAllBullet(float deltaTime)
        {
            var temp = new List<Bullet>();
            foreach (var b in activeBulletList)
            {
                b.Tick(deltaTime);
                if (b.isDead)
                {
                    temp.Add(b);
                }
            }

            foreach (var b in temp)
            {
                activeBulletList.Remove(b);
                bulletQueue.Enqueue(b);
                b.gameObject.SetActive(false);
            }
        }

        public abstract void Fire(FireData fireData, object userData = null, Action<object, Collider2D> onHit = null,
            Action onEnd = null);
    }
}