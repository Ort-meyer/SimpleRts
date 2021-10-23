﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : Singleton<ResourceManager>
{
    Dictionary<int, Sprite> m_portraitImages = new Dictionary<int, Sprite>();
    const string m_portraitsFolder = @"Graphics\Portraits\";
    const string m_portraitMapFileName = @"portraitMap";

    // Use this for initialization
    void Start()
    {
        M_LoadPortraits();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void M_LoadPortraits()
    {
        TextAsset txt = Resources.Load<TextAsset>(m_portraitsFolder + m_portraitMapFileName);
        //foreach (string line in File.ReadAllLines(@"Assets\Resources\" + m_portraitsFolder + m_portraitMapFileName))
        foreach (string line in txt.text.Replace("\r", "").Split('\n'))
        {
            int index = Int32.Parse(line.Split(';')[0]);
            string portraitFileName = line.Split(';')[1];
            //WWW www = new WWW(m_portraitsFolder + portraitFileName);
            //while (!www.isDone)
            //    yield return null;

            Sprite portrait = Resources.Load<Sprite>(m_portraitsFolder + portraitFileName);
            m_portraitImages.Add(index, portrait);
        }
    }

    public Sprite M_GetPortrait(int index)
    {
        return m_portraitImages[index];
    }
}