using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private float endV;
    [SerializeField] private float duration;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        volume.profile.TryGet(out colorAdjustments);
    }

    public void ActiveVolume()
    {
        float value = 0;
        DOTween.To(() => value, x => value = x, endV, duration).SetEase(Ease.InBounce)
            .OnUpdate(() =>
            {
                colorAdjustments.saturation.value = value;
            })
            .OnComplete(() => colorAdjustments.saturation.value = 0);
    }
}
