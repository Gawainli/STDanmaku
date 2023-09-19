using System;
using Unity.VisualScripting;
using UnityEngine;

namespace StDanmaku
{
    public class FireData
    {
        public float srcSpeed;
        public float srcRotate;
        public Color srcColor;
        public float srcAngularSpeed;
        public float srcDuration;
        
        public FireData()
        {
            srcSpeed = 0;
            srcRotate = 0;
            srcColor = Color.white;
            srcAngularSpeed = 0;
            srcDuration = 0;
        }
        
        public FireData(FireData fireData)
        {
            srcSpeed = fireData.srcSpeed;
            srcRotate = fireData.srcRotate;
            srcColor = fireData.srcColor;
            srcAngularSpeed = fireData.srcAngularSpeed;
            srcDuration = fireData.srcDuration;
        }
        
        public FireData Clone()
        {
            var fireData = new FireData();
            fireData.srcSpeed = srcSpeed;
            fireData.srcRotate = srcRotate;
            fireData.srcColor = srcColor;
            fireData.srcAngularSpeed = srcAngularSpeed;
            fireData.srcDuration = srcDuration;
            return fireData;
        }
        
        public void Reset()
        {
            srcSpeed = 0;
            srcRotate = 0;
            srcColor = Color.white;
            srcAngularSpeed = 0;
            srcDuration = 0;
        }
        
        public void CopyFrom(FireData fireData)
        {
            srcSpeed = fireData.srcSpeed;
            srcRotate = fireData.srcRotate;
            srcColor = fireData.srcColor;
            srcAngularSpeed = fireData.srcAngularSpeed;
            srcDuration = fireData.srcDuration;
        }
    }
    
    public class Bullet : MonoBehaviour
    {
        public float direction;
        public float speed;
        public float duration;
        public float angularSpeed;
        public Color color;
        public Collider2D selfCollider;

        public FireData fireData;
        public System.Object userData;
        public bool isDead = false;

        public Action<object, Collider2D> onHit;
        public Action onEnd;

        private void Awake()
        {
            selfCollider = this.GetComponent<Collider2D>();
        }
        
        
        public void SetFireData(FireData fireData)
        {
            if (this.fireData == null) 
            {
                this.fireData = new FireData(fireData);
            }
            else
            {
                this.fireData.CopyFrom(fireData);
            }
            
            this.fireData = fireData;
            this.speed = fireData.srcSpeed;
            this.angularSpeed = fireData.srcAngularSpeed;
            this.color = fireData.srcColor;
            this.duration = fireData.srcDuration;
            this.direction = fireData.srcRotate;
            this.transform.localRotation = Quaternion.Euler(0, 0, direction);
            this.transform.localPosition = Vector3.zero;
            isDead = false;
        }

        public void Tick(float deltaTime)
        {
            Move(deltaTime);
            Rotate(deltaTime);
            CheckDuration(deltaTime);
        }

        private void Move(float deltaTime)
        {
            Vector3 displacement = speed * transform.up;
            transform.localPosition += displacement * deltaTime;
        }
        
        private void Rotate(float deltaTime)
        {
            transform.Rotate(Vector3.forward, angularSpeed * deltaTime);
        }
        
        private void CheckDuration(float deltaTime)
        {
            duration -= deltaTime;
            if (duration <= 0)
            {
                isDead = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (selfCollider == null)
            {
                return;
            }
            
            if (other == selfCollider)
            {
                return;
            }
            
            if (onHit != null)
            {
                onHit(userData, other);
            }
        }
        
        private void OnEnd()
        {
            fireData.Reset();
            if (onEnd != null)
            {
                onEnd();
            }
        }
    }
}