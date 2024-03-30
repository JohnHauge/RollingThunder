using Runtime.Game;
using UnityEngine;
using System;
using System.Collections;

namespace Runtime.Level
{
    public class HazzardObject : LaneObject
    {
        [SerializeField] private float minConsumeScale = 3f;
        [SerializeField] private int damage = 1;
        [SerializeField] private GameObject slopeObject;
        [SerializeField] private GameObject snowballObject;
        public override Snowball Snowball { get; set; }

        private void OnEnable()
        {
            move = true;
            slopeObject.SetActive(true);
            snowballObject.SetActive(false);
        }

        protected void OnDisable()
        {
            slopeObject.SetActive(false);
            snowballObject.SetActive(false);
        }

        public override void OnHit(Snowball snowball)
        {
            if (snowball.transform.localScale.x < minConsumeScale)
            {
                snowball.OnHazzardCollision(damage);
                ReturnToPool();
            }
            else
            {
                move = false;
                transform.SetParent(null);
                slopeObject.SetActive(false);
                snowballObject.SetActive(true);
                var snowballChild = snowball.transform.GetChild(0);
                var position = snowball.GetClosestLanePosition(transform.position);
                position.y -= snowball.transform.localScale.x; 
                transform.position = position;
                transform.SetParent(snowballChild);
                transform.eulerAngles = new Vector3(0f, 0f, 180f);
                StartCoroutine(RotationTrackerRoutine(snowball));
            }
        }

        private IEnumerator RotationTrackerRoutine(Snowball snowball)
        {
            var startAngle = snowball.Angle;
            var currentAngle = 0f;
            while (currentAngle < 270f)
            {
                currentAngle = snowball.Angle - startAngle;
                yield return null;
            }
            ReturnToPool();
        }
    }
}