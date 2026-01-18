using System.Collections;
using UnityEngine;

[SelectionBase]
public class GartenController : MonoBehaviour
{
    [Header("Текущее состояние")]
    [SerializeField] private GartenEnum gartenEnum;
    [Header("Базовые значения")]
    [SerializeField] private Material expectationMaterial;
    [SerializeField] private MeshRenderer gartenRender;
    private ShovelController shovelController;
    [Header("Мaterials")]
    [SerializeField] private Material installMaterial;
    [Space()]
    [SerializeField] GameObject fertilizer;
    [SerializeField] GameObject gartenFull;

    private void Start()
    {
        ChangeState(GartenEnum.expectation);
        shovelController = FindFirstObjectByType<ShovelController>();
    }

    public void ChangeState(GartenEnum gartenEnum)
    {
        switch (gartenEnum)
        {
            case GartenEnum.expectation:
                ExpressionGarten();
                this.gartenEnum = GartenEnum.expectation;
                break;

            case GartenEnum.installed:
                if (shovelController.isActive && this.gartenEnum == GartenEnum.expectation)
                {
                    gartenRender.material = installMaterial;
                    this.gartenEnum = GartenEnum.installed;
                }
                break;

            case GartenEnum.sowing:
                if (!shovelController.isActive && this.gartenEnum == GartenEnum.installed)
                {
                    fertilizer.SetActive(true);
                    this.gartenEnum = GartenEnum.sowing;
                    StartCoroutine(GartenFull());
                }
                break;

            case GartenEnum.harvest:
                if (!shovelController.isActive && this.gartenEnum == GartenEnum.harvest)
                {
                    ExpressionGarten();
                    gartenFull.SetActive(false);
                    fertilizer.SetActive(false);
                    this.gartenEnum = GartenEnum.expectation;
                }
                break;
        }

    }

    /// <summary>
    /// Установка прозрачного материала
    /// </summary>
    private void ExpressionGarten()
    {
        gartenRender.material = expectationMaterial;
    }

    private void OnMouseDown()
    {
        var stateEnum = gartenEnum switch
        {
            GartenEnum.expectation => GartenEnum.installed,
            GartenEnum.installed => GartenEnum.sowing,
            GartenEnum.sowing => GartenEnum.harvest,
            GartenEnum.harvest => GartenEnum.harvest,
            _ => GartenEnum.expectation,
        };

        ChangeState(stateEnum);
    }

    private IEnumerator GartenFull()
    {
        var time = Random.Range(2,4);
        yield return new WaitForSeconds(time);

        gartenFull.SetActive(true);
        this.gartenEnum = GartenEnum.harvest;
    }
}
