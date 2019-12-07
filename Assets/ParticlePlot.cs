using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ParticlePlot : MonoBehaviour
{
    public string inputfile;

    private List<Dictionary<string, object>> pointList;

    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;
    public int columnT = 3;
 
    // Full column names
    public string xName;
    public string yName;
    public string zName;
    public string tName;


    //can change scale of plot
    public float plotScale = 100;

    public GameObject PointPrefab;

    public GameObject PointHolder;

    // Start is called before the first frame update
    void Start()
    {
        pointList = CSVReader.Read(inputfile);

        Debug.Log(pointList);
        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pointList[1].Keys);
 
        // Print number of keys (using .count)
    Debug.Log("There are " + columnList.Count + " columns in CSV");
 
    foreach (string key in columnList)
    Debug.Log("Column name is " + key);

    // Assign column name from columnList to Name variables
    xName = columnList[columnX];
    yName = columnList[columnY];
    zName = columnList[columnZ];
    tName = columnList[columnT];

    // Get maxes of each axis
    float xMax = FindMaxValue(xName);
    float yMax = FindMaxValue(yName);
    float zMax = FindMaxValue(zName);
    float tMax = FindMaxValue(tName);

    // Get minimums of each axis
    float xMin = FindMinValue(xName);
    float yMin = FindMinValue(yName);
    float zMin = FindMinValue(zName);
    float tMin = FindMinValue(tName);

    //Loop through Pointlist
    for (var i = 0; i < pointList.Count; i++)
    {
    // Get value in poinList at ith "row", in "column" Name
    //loat x = System.Convert.ToSingle(pointList[i][xName]);
    //float y = System.Convert.ToSingle(pointList[i][yName]);
    //float z = System.Convert.ToSingle(pointList[i][zName]);
 

    // Get value in poinList at ith "row", in "column" Name, normalize
    float x = 
    (System.Convert.ToSingle(pointList[i][xName]) - xMin) / (xMax - xMin);
 
    float y = 
    (System.Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin);
 
    float z = 
    (System.Convert.ToSingle(pointList[i][zName]) - zMin) / (zMax - zMin);

    //how do we want to scale t?
    //float t = Math.Sqrt(pointList[i][tName]);

    float t = 
    (System.Convert.ToSingle(pointList[i][tName]) - tMin) / (tMax - tMin);


    float t2 = Convert.ToSingle(Math.Sqrt(Convert.ToDouble(t)));
    //instantiate the prefab with coordinates defined above
    //Instantiate(PointPrefab, new Vector3(x, y, z), Quaternion.identity);
 
    // Instantiate as gameobject variable so that it can be manipulated within loop
    GameObject dataPoint = Instantiate(
    PointPrefab, 
    new Vector3(x, y, z)* plotScale, 
    Quaternion.identity);

    dataPoint.transform.parent = PointHolder.transform;

    // Assigns original values to dataPointName
    string dataPointName = 
    pointList[i][xName] + " "
    + pointList[i][yName] + " "
    + pointList[i][zName];

    // Assigns name to the prefab, prob don't want this for when we do BH particles
    dataPoint.transform.name = dataPointName;

    // Gets material color and sets it to a new RGBA color we define
    dataPoint.GetComponent<Renderer>().material.color = 
    new Color(t,0,1-t, t);


    }

    

    }
 private float FindMaxValue(string columnName)
    {
        //set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);
 
        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }
 
        //Spit out the max value
        return maxValue;
    }
 
    private float FindMinValue(string columnName)
    {
 
        float minValue = Convert.ToSingle(pointList[0][columnName]);
 
        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < pointList.Count; i++)
        {
            if (Convert.ToSingle(pointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(pointList[i][columnName]);
        }
 
        return minValue;
    }




}
