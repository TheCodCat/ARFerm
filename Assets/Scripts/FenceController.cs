using DG.Tweening;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    [field: SerializeField] public bool isActive { get; private set; }
    [SerializeField] private GameObject fenceObj;
    private Sequence sequence;
    public void FenceChangeState(GameStateEnum gameStateEnum)
    {
        Debug.Log(gameStateEnum);
        isActive = gameStateEnum == GameStateEnum.Fence ? true : false;

        if (isActive)
        {
            sequence.Kill();

            sequence = DOTween.Sequence()
                .Append(fenceObj.transform.DOMoveY(0.1f, 1)).SetEase(Ease.Linear)
                .Append(fenceObj.transform.DOMoveY(0f, 1)).SetEase(Ease.Linear)
                .Append(fenceObj.transform.DOMoveY(-0.1f, 1)).SetEase(Ease.Linear)
                .Append(fenceObj.transform.DOMoveY(0f, 1)).SetEase(Ease.Linear)
                .SetLoops(-1);
        }
        else
        {
            sequence.Kill();
            fenceObj.transform.DOMoveY(0, 1).SetEase(Ease.Linear);
        }
    }
}
