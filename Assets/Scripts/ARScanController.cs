using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARScanController : MonoBehaviour
{
    [SerializeField] private GameObject cells;
    [SerializeField] private bool isInstance;
    [SerializeField] private GameObject[] objs;

    private void Awake()
    {
        cells.SetActive(false);
        foreach (var item in objs)
        {
            item.SetActive(false);
        }
    }
    public void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARPlane> changes)
    {
        foreach (var plane in changes.added)
        {
            if (!isInstance)
            {
                cells.SetActive(true);
                Debug.Log(plane);
                isInstance = true;
                foreach (var item in objs)
                {
                    item.SetActive(true);
                }
            }
        }
    }
}
