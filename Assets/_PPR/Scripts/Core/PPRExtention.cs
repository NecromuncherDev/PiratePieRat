using System;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace PPR.Core
{
    public static class PPRExtention
    {
        public static void WaitForAnimationComplete(this Animator animator, PPRMonoBehaviour monoBehaviour, Action onComplete)
        {
            var animationTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            monoBehaviour.WaitForFrame(() =>
            {
                monoBehaviour.WaitForSeconds(animationTime, delegate
                {
                    onComplete?.Invoke();
                });
            });
        }

        public static int HoursToSeconds(this int hours)
        {
            return hours * 60 * 60;
        }

        public static int HoursToMin(this int hours)
        {
            return hours * 60;
        }

        public static int MinToSeconds(this int min)
        {
            return min * 60;
        }

        public static T GetRandomFromArray<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static int SecToMilli(this float seconds)
        {
            return (int)(seconds * 1000);
        }

        public static Tweener DOLookAt2D(this Transform transform, Vector2 dir, float duration)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return transform.DORotate(new Vector3(0, 0, angle), duration, RotateMode.Fast);
        }

        public static Color ResetAlpha(this Color color, float newAlpha = 1)
        {
            var tempColor = color;
            tempColor.a = newAlpha;
            color = tempColor;
            return color;
        }
    }
}