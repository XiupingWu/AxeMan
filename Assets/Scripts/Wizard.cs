﻿using UnityEngine;

namespace AxeMan.GameSystem
{
    public class Wizard : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(GameCore.AxeManCore.GetComponent<GameCore>().Hello);
            //Debug.Log(GetComponent<GameCore>().Hello);
            //Debug.Log(FindObjectOfType<GameCore>().Hello);

            GameObject dummy;
            for (float i = -6; i < -1.5f; i += 0.5f)
            {
                for (float j = -4; j < 0.5f; j += 0.5f)
                {
                    if ((j == 0) || (i == -2))
                    {
                        dummy = Instantiate(Resources.Load("Dummy") as GameObject);
                        dummy.transform.localPosition = new Vector3(i, j);
                    }
                }
            }
        }
    }
}
