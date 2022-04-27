using System;
using System.Collections;
using System.Collections.Generic;
using File = System.IO.File;
using System.Net;
using UnityEngine;
using UnityEngine.Windows;

public class TransitLoader
{
    public TransitLoader(string url, string path)
    {
        LoadFile(url,path);
    }
    
    public Transit ParseTransit(Vector2 curruntPosition)
    {
        var transit = JsonUtility.FromJson<Transit>(File.ReadAllText(Application.streamingAssetsPath + "/transit.json"));
        float dist = 0;
        var position = curruntPosition;
        foreach (var point in transit.Points)
        {
            dist += Vector2.Distance(position, point);
            position = point;
        }
        transit.Speed = dist / transit.TransitTime;

        return transit;
    }
    
    private void LoadFile(string url, string path)
    {
        if (File.Exists(path))
        {
            return;
        }
        WebClient webClient = new WebClient();
        webClient.DownloadFile(new Uri(url), Application.streamingAssetsPath + "/transit.json"); 
    }
}
