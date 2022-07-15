using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetManager : MonoBehaviour
{
    private CameraMultiTarget cmt;
    public void FindAndSetTargets()
    {
        cmt = FindObjectOfType<CameraMultiTarget>();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        cmt.SetTargets(targets);
    }
}
