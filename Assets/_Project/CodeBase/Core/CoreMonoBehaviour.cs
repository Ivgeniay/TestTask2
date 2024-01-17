using System.Collections;
using UnityEngine;

namespace BarrelHide.Core
{
    public class CoreMonoBehaviour : MonoBehaviour
    {
        protected Bootstraper core { get => Bootstraper.Instance; }
        protected virtual IEnumerator Start()
        {
            yield return new WaitUntil(() => core.IsLoaded);
        }
    }
}
