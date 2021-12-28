using UnityEngine;
using Random = UnityEngine.Random;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator;

    public void CameraShake()
    {
        var rand = Random.Range(1, 5);
        var trigger = "Shake_0" + rand;
        cameraAnimator.SetTrigger(trigger);
    }
}
