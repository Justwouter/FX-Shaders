using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] Camera[] cameras;
    [SerializeField] int currentIndex;
    [SerializeField] TextMeshProUGUI text;
    private bool isFirst = false;

    public void OnNextCam()
    {
        text.enabled = false;
        cameras[currentIndex].enabled = false;
        currentIndex++;
    }
    public void OnPreviousCam()
    {
        cameras[currentIndex].enabled = false;
        currentIndex--;
    }

    private void CheckIndex()
    {
        if (currentIndex >= cameras.Length)
        {
            currentIndex = 0;
        }
    }

    void Update()
    {
        CheckIndex();
        cameras[currentIndex].enabled = enabled;
    }
}
