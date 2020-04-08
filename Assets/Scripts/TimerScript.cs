using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float timeFromLevelStart;
    private void Update()
    {
        timeFromLevelStart = Time.timeSinceLevelLoad;
    }


}
