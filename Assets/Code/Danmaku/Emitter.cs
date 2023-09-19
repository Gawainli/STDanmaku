using System;
using UnityEngine;

namespace StDanmaku
{
    public class Emitter : MonoBehaviour
    {
        public Bullet bulletPrefab;
        public FireAbleType fireAbleType;
        public float speed;
        public float duration;
        public float angularSpeed;
        public Color color;
        public float fireInterval;

        #region Firable

        public int count;
        public float radius;
        public bool twoSide;

        #endregion

        #region Bullet

        public object userData;
        public Action<object, Collider2D> onHit;
        public Action onEnd;

        #endregion

        private float timer;
        private FireAble fireAble;


        // Start is called before the first frame update
        void Start()
        {
            if (bulletPrefab == null)
            {
                return;
            }

            switch (fireAbleType)
            {
                case FireAbleType.Line:
                    fireAble = new LineFireAble(bulletPrefab, transform, twoSide);
                    break;
                case FireAbleType.Arc:
                    fireAble = new ArcFireAble(bulletPrefab, transform, count, radius);
                    break;
                case FireAbleType.Ring:
                    fireAble = new RingFireAble(bulletPrefab, transform, count);
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (fireAble == null)
            {
                return;
            }

            timer += Time.deltaTime;
            fireAble.TickAllBullet(Time.deltaTime);

            if (timer > fireInterval)
            {
                timer = 0;
                var fireData = new FireData();
                fireData.srcSpeed = speed;
                fireData.srcColor = color;
                fireData.srcAngularSpeed = angularSpeed;
                fireData.srcDuration = duration;
                fireData.srcRotate = 0;
                fireAble.Fire(fireData, userData, onHit, onEnd);
            }
        }
    }
}