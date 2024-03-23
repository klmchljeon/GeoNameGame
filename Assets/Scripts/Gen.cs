using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour
{
    public GameObject Country;

    private List<Transform> CountryList;
    private List<int> selcetedIdx;

    // Start is called before the first frame update
    void Start()
    {
        CountryList = GetAllChildren(Country.transform);
        selcetedIdx = RandomSelector.PickNumbers(CountryList.Count, 25);

        Active(CountryList, selcetedIdx);
    }

    private void Active(List<Transform> objectList, List<int> idx)
    {
        for (int i = 0; i < CountryList.Count; i++)
        {
            LineRenderer lineRenderer = objectList[i].GetComponent<LineRenderer>();

            bool contain = idx.Contains(i);
            if (contain)
            {
                Color brightRed = new Color(1f, 0.5f, 0.5f, 1f); // R, G, B, A
                lineRenderer.material.color = brightRed;
                lineRenderer.startWidth = 2f;
                lineRenderer.endWidth = 2f;
            }
            else
            {
                lineRenderer.startWidth = 0.2f;
                lineRenderer.endWidth = 0.2f;
                objectList[i].GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
    }

    private List<Transform> GetAllChildren(Transform parent)
    {
        List<Transform> childList = new List<Transform>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            childList.Add(child);
        }

        return childList;
    }

}
