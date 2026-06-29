using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingManager : MonoBehaviour
{
    public GameObject cubePrefab;

    private ARTrackedImageManager manager;

    private void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        manager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    private void OnDisable()
    {
        manager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var image in args.added)
        {
            Instantiate(
                cubePrefab,
                image.transform.position,
                image.transform.rotation,
                image.transform);
        }
    }
}