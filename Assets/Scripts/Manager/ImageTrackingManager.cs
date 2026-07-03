using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject suspectPrefab;
    public GameObject letterPrefab;
    public GameObject safePrefab;

    public SafeUIManager safeUIManager;

    private ARTrackedImageManager manager;

    private Dictionary<string, GameObject> spawnedObjects = new();

    private void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        manager.trackablesChanged.AddListener(OnImagesChanged);
    }

    private void OnDisable()
    {
        manager.trackablesChanged.RemoveListener(OnImagesChanged);
    }

    private void OnImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var image in args.added)
            HandleImage(image);

        foreach (var image in args.updated)
            HandleImage(image);

        foreach (var pair in args.removed)
        {
            ARTrackedImage image = pair.Value;

            string name = image.referenceImage.name;

            if (spawnedObjects.ContainsKey(name))
            {
                Destroy(spawnedObjects[name]);
                spawnedObjects.Remove(name);
            }
        }
    }

    private void HandleImage(ARTrackedImage image)
    {
        string imageName = image.referenceImage.name;

        GameObject prefab = GetPrefab(imageName);

        if (prefab == null)
            return;

        // اگر هنوز ساخته نشده
        if (!spawnedObjects.ContainsKey(imageName))
        {
            GameObject obj = Instantiate(prefab, image.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            spawnedObjects.Add(imageName, obj);

            if (imageName == "Suspect")
            {
                AudioManager.Instance.PlayHologram();
            }

            if (imageName == "Letter")
            {
                AudioManager.Instance.PlayLetter();
            }
        }
        else
        {
            GameObject obj = spawnedObjects[imageName];

            // فقط آپدیت موقعیت
            obj.transform.SetParent(image.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            obj.SetActive(image.trackingState == TrackingState.Tracking);
        }
        if (imageName == "Safe")
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                safeUIManager.ShowPanel();
            }
            else
            {
                safeUIManager.HidePanel();
            }
        }
    }

    private GameObject GetPrefab(string imageName)
    {
        switch (imageName)
        {
            case "Suspect":
                return suspectPrefab;

            case "Letter":
                return letterPrefab;

            case "Safe":
                return safePrefab;

            default:
                return null;
        }
    }
}