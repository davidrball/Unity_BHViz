using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlot : MonoBehaviour
{
    public string inputfile;

    private List<Dictionary<string, object>> pointList;

    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;
 
    // Full column names
    public string xName;
    public string yName;
    public string zName;

    public GameObject PointPrefab;

    public GameObject PointPrefab2;

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

    //Loop through Pointlist
    for (var i = 0; i < pointList.Count; i++)
    {
    // Get value in poinList at ith "row", in "column" Name
    float x = System.Convert.ToSingle(pointList[i][xName]);
    float y = System.Convert.ToSingle(pointList[i][yName]);
    float z = System.Convert.ToSingle(pointList[i][zName]);
 
    //instantiate the prefab with coordinates defined above
    Instantiate(PointPrefab, new Vector3(x, y, z), Quaternion.identity);
 
    }

    }

}
