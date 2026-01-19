using DG.Tweening;
using UnityEngine;

public class ShovelController : MonoBehaviour
{
    [field: SerializeField] public bool isActive { get; private set; }
    [SerializeField] private GameObject shovelObj;
    private Sequence sequence;
    public void ShovelChangeState(GameStateEnum gameStateEnum)
    {
        Debug.Log(gameStateEnum);
        isActive = gameStateEnum == GameStateEnum.Shovel ? true : false;

        if (isActive)
        {
            sequence.Kill();

            sequence = DOTween.Sequence()
                .Append(shovelObj.transform.DOMoveY(0.1f, 1)).SetEase(Ease.Linear)
                .Append(shovelObj.transform.DOMoveY(0f, 1)).SetEase(Ease.Linear)
                .Append(shovelObj.transform.DOMoveY(-0.1f, 1)).SetEase(Ease.Linear)
                .Append(shovelObj.transform.DOMoveY(0f, 1)).SetEase(Ease.Linear)
                .SetLoops(-1);
        }
        else
        {
            sequence.Kill();
            shovelObj.transform.DOMoveY(0, 1).SetEase(Ease.Linear);
        }
    }
}
