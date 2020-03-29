using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Personagem))]
public class PersonagemEditor : Editor
{

    private void OnSceneGUI()
    {

        Personagem fov = (Personagem)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.raioDaVisao);
        Vector3 verAnguloA = fov.DirecaoDoAngulo(-fov.anguloDaVisao / 2, false);
        Vector3 verAnguloB = fov.DirecaoDoAngulo( fov.anguloDaVisao / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + verAnguloA * fov.raioDaVisao);
        Handles.DrawLine(fov.transform.position, fov.transform.position + verAnguloB * fov.raioDaVisao);

        Handles.color = Color.red;
        foreach(Transform alvoVisivel in fov.alvosVisiveis)
        {
            Handles.DrawLine(fov.transform.position, alvoVisivel.position);
        }
    }
}

