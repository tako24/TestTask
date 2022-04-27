using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;

public class CircleMoving : MonoBehaviour
{
    private Transit _transit;
    private Vector2 _currentTarget;
    private int _currentIndex;
    private bool _isButtonPressed;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        var transitLoader = new TransitLoader(@"https://drive.google.com/uc?id=1HM2cllbTznEPFWSFCQTOiQrf_y6qeQx9&export=download",
            Application.streamingAssetsPath + "/transit.json");
        _transit = transitLoader.ParseTransit(transform.position);
        _currentTarget = _transit.Points[0];
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void DrawPath()
    {
        _lineRenderer.loop = _transit.IsLoop;
        _lineRenderer.positionCount = _transit.Points.Length;
        
        for (int i = 0; i <_transit.Points.Length; i++)
            _lineRenderer.SetPosition(i,_transit.Points[i]);
    }
    
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DrawPath();
            _isButtonPressed = true;
        }

        if (!_isButtonPressed)
            return;
        
        if (_transit.IsLoop)
        {
            if ((Vector2)transform.position == _currentTarget )
            {
                if (_currentIndex == _transit.Points.Length - 1)
                    _currentTarget = _transit.Points[_currentIndex =0];
                else
                    _currentTarget = _transit.Points[++_currentIndex];
            }
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget, Time.deltaTime *_transit.Speed);
        }
        else
        {
            if ((Vector2)transform.position == _currentTarget && _currentIndex!=_transit.Points.Length-1)
                _currentTarget = _transit.Points[++_currentIndex];
            
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget, Time.deltaTime *_transit.Speed);
        }
    }
}