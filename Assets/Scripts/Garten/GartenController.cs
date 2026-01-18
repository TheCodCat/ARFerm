using DG.Tweening;
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
    [SerializeField] private VolumeController volumeController;
    [SerializeField] private ParticleSystem particleSystemPut;
    private ShovelController shovelController;
    [Header("Мaterials")]
    [SerializeField] private Material installMaterial;
    [Space()]
    [SerializeField] GameObject fertilizer;
    [SerializeField] GameObject gartenFull;
    [SerializeField] private AudioClip audioSowing;

    private void Start()
    {
        ChangeState(GartenEnum.expectation);
        shovelController = FindFirstObjectByType<ShovelController>();
        volumeController = FindFirstObjectByType<VolumeController>();
    }

    /// <summary>
    /// метод изменяющий состояние грядки
    /// </summary>
    /// <param name="gartenEnum"></param>
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
                    transform.DOPunchPosition(Vector3.up * 0.1f,1, 4);
                }
                break;

            case GartenEnum.sowing:
                if (!shovelController.isActive && this.gartenEnum == GartenEnum.installed)
                {
                    fertilizer.SetActive(true);

                    fertilizer.transform.DOPunchPosition(Vector3.up * 0.1f, 1, 4);
                    AudioSource.PlayClipAtPoint(audioSowing, transform.position);
                    this.gartenEnum = GartenEnum.sowing;
                    StartCoroutine(GartenFull());
                }
                break;

            case GartenEnum.harvest:
                if (!shovelController.isActive && this.gartenEnum == GartenEnum.harvest)
                {
                    ExpressionGarten();
                    volumeController.ActiveVolume();
                    particleSystemPut.Play();

                    gartenFull.transform.DOPunchScale(Vector3.one * 0,1,4);
                    gartenFull.SetActive(false);

                    fertilizer.transform.DOPunchScale(Vector3.one * 0, 1, 4);
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

    /// <summary>
    /// рост урожая, корутина
    /// </summary>
    /// <returns></returns>
    private IEnumerator GartenFull()
    {
        var time = Random.Range(10,16);
        yield return new WaitForSeconds(time);

        gartenFull.SetActive(true);
        this.gartenEnum = GartenEnum.harvest;
    }
}
