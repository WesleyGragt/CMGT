using NaughtyAttributes;
using System;
using UnityEngine; 
public class GraphTester : MonoBehaviour {

    [InfoBox("Graph", EInfoBoxType.Normal)]
    [SerializeField]
    [TextArea(10, 10)]
    private string contents = "";

    private void Awake()
    {
        // Receive debug.log callbacks
        Application.logMessageReceived += logHandler;
    }

    private void logHandler(string info, string stackTrace, LogType type)
    {
        contents += info + Environment.NewLine;
    }

    private void OnApplicationQuit()
    {
        Application.logMessageReceived -= logHandler;
    }

    void Start() { 
        Graph<string> graph = new Graph<string>(); 
        graph.AddNode("B"); 
        graph.AddNode("C"); 
        graph.AddNode("D"); 

        graph.AddEdge("A", "B"); 
        graph.AddEdge("A", "C"); 
        graph.AddEdge("B", "D"); 
        graph.AddEdge("C", "D"); 

        Debug.Log("Graph Structure:"); 
        graph.PrintGraph(); 
    } 

}