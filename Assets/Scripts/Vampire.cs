using UnityEngine;
using System.Collections;

public class Vampire : MonoBehaviour
{
    public GameObject vampire1Prefab;
    public GameObject vampire2Prefab;
    public GameObject vampire3Prefab;

    private GameObject currentVampire;
    public GameObject smokePuffPrefab;

    void Start()
    {
        currentVampire = Instantiate(vampire1Prefab, transform.position, Quaternion.identity, transform);
    }

    void Update()
    {
        CheckTimeAndSwap();
    }

    private void CheckTimeAndSwap()
    {
        int second = TimeManager.Second;

        if (second >= 20 && !IsCurrentVampire(vampire3Prefab))
        {
            SwapVampire(vampire3Prefab);
        }
        else if (second >= 10 && second < 20 && !IsCurrentVampire(vampire2Prefab))
        {
            SwapVampire(vampire2Prefab);
        }
    }

    private void SwapVampire(GameObject newVampirePrefab)
    {
        StartCoroutine(HandleVampireSwamp(newVampirePrefab));
        // Vector3 position = currentVampire.transform.position;
        // Destroy(currentVampire);
        // currentVampire = Instantiate(newVampirePrefab, position, Quaternion.identity, transform);
    }

    private IEnumerator HandleVampireSwamp(GameObject newVampirePrefab)
    {
        Vector3 position = currentVampire.transform.position;

        var currentScript = currentVampire.GetComponent<MonoBehaviour>();
        if (currentScript != null) currentScript.enabled = false;

        if (smokePuffPrefab != null) Instantiate(smokePuffPrefab, position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Destroy(currentVampire);

        currentVampire = Instantiate(newVampirePrefab, position, Quaternion.identity, transform);
    }

    private bool IsCurrentVampire(GameObject prefab)
    {
        return currentVampire != null && currentVampire.name.Contains(prefab.name);
    }
}
