﻿using System.Collections;
using UnityEngine;

namespace GameSystem.Operator
{
    [AddComponentMenu("|Operator/Transformer")]
    public class Transformer : MonoBehaviour
    {
        [MinsHeader("Transformer", SummaryType.TitleYellow, 0)]

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public Vector3 targetPosition;
        [Label] public AnimationCurve moveCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [Label] public float time = 1;
        [Label(true)] public bool sendMovementEvent = false;

        // Output
        [MinsHeader("Output", SummaryType.Header, 3)]
        [ConditionalShow("sendMovementEvent", true)]
        [Label] public FloatEvent movementOutput;


        [ContextMenu("MoveToInTime")]
        public void MoveToInTime()
        {
            MoveToInTime(time);
        }
        public void MoveToInTime(float time)
        {
            StopAllCoroutines();
            StartCoroutine(moveToInTime(targetPosition, time));
        }
        public void MoveToInTime(Vector3 targetPosition)
        {
            StopAllCoroutines();
            StartCoroutine(moveToInTime(targetPosition, time));
        }
        public void MoveToInTime(Vector3 targetPosition, float time)
        {
            StopAllCoroutines();
            StartCoroutine(moveToInTime(targetPosition, time));
        }
        IEnumerator moveToInTime(Vector3 targetPosition, float time)
        {
            float timer = 0;
            Vector3 oPos = transform.position;
            while (timer < 1)
            {
                yield return 0;
                transform.position = Vector3.LerpUnclamped(oPos, targetPosition, moveCurve.Evaluate(timer));
                timer += Time.deltaTime / time;
            }
            transform.position = targetPosition;
        }

        [ContextMenu("MoveToInTimeLocally")]
        public void MoveToInTimeLocally()
        {
            MoveToInTimeLocally(time);
        }
        public void MoveToInTimeLocally(float time)
        {
            StopAllCoroutines();
            StartCoroutine(moveToInTimeLocally(targetPosition, time));
        }
        public void MoveToInTimeLocally(Vector3 targetPosition)
        {
            StopAllCoroutines();
            StartCoroutine(moveToInTimeLocally(targetPosition, time));
        }
        public void MoveToInTimeLocally(Vector3 targetPosition, float time)
        {
            StopAllCoroutines();
            StartCoroutine(moveToInTimeLocally(targetPosition, time));
        }
        IEnumerator moveToInTimeLocally(Vector3 targetPosition, float time)
        {
            float timer = 0;
            Vector3 oPos = transform.localPosition;
            while (timer < 1)
            {
                yield return 0;
                transform.localPosition = Vector3.LerpUnclamped(oPos, targetPosition, moveCurve.Evaluate(timer));
                timer += Time.deltaTime / time;
            }
            transform.localPosition = targetPosition;
        }

        [ContextMenu("MoveToInTimeRelativeLocally")]
        public void MoveToInTimeRelativeLocally()
        {
            MoveToInTimeRelativeLocally(time);
        }
        public void MoveToInTimeRelativeLocally(float time)
        {
            StopAllCoroutines();
            if (sendMovementEvent) StartCoroutine(moveToInTimeRelativeLocallyAndOutput(targetPosition, time));
            else
                StartCoroutine(moveToInTimeRelativeLocally(targetPosition, time));
        }
        public void MoveToInTimeRelativeLocally(Vector3 targetPosition)
        {
            StopAllCoroutines();
            if (sendMovementEvent) StartCoroutine(moveToInTimeRelativeLocallyAndOutput(targetPosition, time));
            else
                StartCoroutine(moveToInTimeRelativeLocally(targetPosition, time));
        }
        public void MoveToInTimeRelativeLocally(Vector3 targetPosition, float time)
        {
            StopAllCoroutines();
            if (sendMovementEvent) StartCoroutine(moveToInTimeRelativeLocallyAndOutput(targetPosition, time));
            else
                StartCoroutine(moveToInTimeRelativeLocally(targetPosition, time));
        }
        IEnumerator moveToInTimeRelativeLocally(Vector3 targetPosition, float time)
        {
            float timer = 0;
            Vector3 oPos = transform.localPosition;
            Vector3 tPosition = oPos + targetPosition;
            while (timer < 1)
            {
                yield return 0;
                transform.localPosition = Vector3.LerpUnclamped(oPos, tPosition, moveCurve.Evaluate(timer));
                timer += Time.deltaTime / time;
            }
            transform.localPosition = tPosition;
        }
        IEnumerator moveToInTimeRelativeLocallyAndOutput(Vector3 targetPosition, float time)
        {
            float timer = 0;
            Vector3 oPos = transform.localPosition;
            Vector3 tPosition = oPos + targetPosition;
            float distance = Vector3.Distance(oPos, tPosition);
            float currentD = 0;
            while (timer < 1)
            {
                yield return 0;
                float t = moveCurve.Evaluate(timer) * distance;
                movementOutput?.Invoke(t - currentD);
                currentD = t;
                transform.localPosition = Vector3.LerpUnclamped(oPos, tPosition, moveCurve.Evaluate(timer));
                timer += Time.deltaTime / time;
            }
            movementOutput?.Invoke(distance - currentD);
            transform.localPosition = tPosition;
        }

        [ContextMenu("MoveTo")]
        public void MoveTo()
        {
            transform.position = targetPosition;
        }
        public void MoveTo(Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }

        [ContextMenu("MoveToLocally")]
        public void MoveToLocally()
        {
            transform.localPosition += targetPosition;
        }
        public void MoveToLocally(Vector3 targetPosition)
        {
            transform.localPosition += targetPosition;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.1f);
            Gizmos.DrawWireCube(transform.position, Vector3.one * 0.1f);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(transform.localToWorldMatrix * targetPosition));
            Gizmos.color = new Color(0.7f, 0.5f, 0.5f, 0.1f);
            Gizmos.DrawWireCube(transform.position + (Vector3)(transform.localToWorldMatrix * targetPosition), Vector3.one * 0.1f);
            Gizmos.color = Color.white;
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.65f, 0.5f, 0.5f);
            Gizmos.DrawWireCube(transform.position, Vector3.one * 0.1f);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(transform.localToWorldMatrix * targetPosition));
            Gizmos.color = new Color(0.9f, 0.5f, 0.5f);
            Gizmos.DrawWireCube(transform.position + (Vector3)(transform.localToWorldMatrix * targetPosition), Vector3.one * 0.1f);
            Gizmos.color = Color.white;
        }
    }
}