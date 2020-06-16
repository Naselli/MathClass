using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Shapes;

public class BezierCurves : MonoBehaviour
{
    [ Range( 0, 1 ) ] 
    public float t = 0;
    public Transform[] p;
    public Camera cam;

    private void OnDrawGizmos(){
        Vector3[] pts = p.Select( x => x.position ).ToArray();
        Draw.Color = Color.white;
        Draw.Line(pts[0], pts[1], .7f);
        Draw.Line(pts[2], pts[3], .7f);
        Draw.Line(pts[1], pts[2], .7f);

        DrawBezier( pts );

        Vector3 curveTangent = GetTangent( t , pts );
        cam.transform.position = GetPoint( t , pts );
        cam.transform.rotation = Quaternion.LookRotation( curveTangent , Vector3.up );
    }

    static Vector3 GetPoint( float t , Vector3[] pts ){
        Vector3 a = Vector3.Lerp(pts[0], pts[1], t );
        Vector3 b = Vector3.Lerp(pts[1], pts[2], t );
        Vector3 c = Vector3.Lerp(pts[2], pts[3], t );
        
        Vector3 d = Vector3.Lerp( a , b ,t );
        Vector3 e = Vector3.Lerp( b, c, t );
        
        Vector3 f = Vector3.Lerp( d, e, t );
        return f;
    }
    
    static Vector3 GetTangent( float t , Vector3[] pts ){
        Vector3 a = Vector3.Lerp(pts[0], pts[1], t );
        Vector3 b = Vector3.Lerp(pts[1], pts[2], t );
        Vector3 c = Vector3.Lerp(pts[2], pts[3], t );
        Vector3 d = Vector3.Lerp( a , b ,t );
        Vector3 e = Vector3.Lerp( b, c, t );
        Vector3 f = Vector3.Lerp( d, e, t );
        
        Draw.Color = Color.red;
        Draw.Line(a,b, .3f);
        Draw.Line(b,c, .3f);
        Draw.Line(a,b, .3f);
        Draw.Line(b,c, .3f);
        Draw.Line(d,e, .3f);
        
        Gizmos.color = new Color( 0.96f , 0.9f , 1f );
        
        return ( e - d ).normalized;

    }

    void DrawBezier(Vector3[] pts){
        const int DETAIL = 32;
        Vector3[] drawPts = new Vector3[DETAIL];
        for( int i = 0 ; i < DETAIL ; i++ ){
            float tDraw = i / ( DETAIL - 1f );
            drawPts[ i ] = GetPoint( tDraw , pts );
        }

        Handles.color = Color.yellow;
        Handles.DrawAAPolyLine( drawPts );
    }
}
