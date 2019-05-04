﻿/**
 * Copyright (c) 2019 LG Electronics, Inc.
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject simulatorCameraPrefab;
    public GameObject simulatorCamera { get; private set; }

    public void Init()
    {
        simulatorCamera = Instantiate(simulatorCameraPrefab, transform);
    }
}