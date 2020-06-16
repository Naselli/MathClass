using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using  Shapes;

[ExecuteAlways]
public class PlayingWithShapes : MonoBehaviour
{

    [ Range( 0 , 1 ) ] public float lineWidth = 1;

    private void OnEnable() => Camera.onPostRender += OnCameraPostRender;
    private void OnDisable() => Camera.onPostRender -= OnCameraPostRender;

    void OnCameraPostRender( Camera cam ){
        //called for every camera
        Draw.LineThickness = lineWidth;
        Draw.LineGeometry = LineGeometry.Flat2D;
        Draw.BlendMode = ShapesBlendMode.Additive;

        Draw.Matrix = Matrix4x4.identity;
        
        Draw.Line( Vector3.zero , Vector3.right , Color.red );
        Draw.Line( Vector3.zero , Vector3.up , Color.green );
    }

    private void Update(){
        
    }
}
