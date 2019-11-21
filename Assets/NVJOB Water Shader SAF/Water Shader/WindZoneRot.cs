﻿// Copyright (c) 2016 Unity Technologies. MIT license - license_unity.txt
// #NVJOB Water Shader - simple and fast. MIT license - license_nvjob.txt
// #NVJOB Water Shader - simple and fast V1.4.5 - https://nvjob.github.io/unity/nvjob-water-shader
// #NVJOB Nicholas Veselov (independent developer) - https://nvjob.github.io


using UnityEngine;



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



public class WindZoneRot : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    Transform tr;



    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private void Awake()
    {
        //---------------------------------

        tr = transform;

        //---------------------------------
    }



    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void LateUpdate()
    {
        //---------------------------------

        tr.rotation = Quaternion.LookRotation(new Vector3(Shader.GetGlobalFloat("_WaterLocalUvNX"), 0, Shader.GetGlobalFloat("_WaterLocalUvNZ")), Vector3.zero) * Quaternion.Euler(0, -40, 0);

        //---------------------------------
    }




    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
