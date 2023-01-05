using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesPrint : MonoBehaviour
{
    public static List<float> TRANSFORM_LIST_FOI_X = new List<float>();
    public static List<float> TRANSFORM_LIST_FOI_Y = new List<float>();
    public static List<float> TRANSFORM_LIST_FOI_Z = new List<float>();

    string X ="X ";
    string Y = "Y ";
    string Z = "Z ";
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.gameObject.transform.GetChildCount(); i++)
        {
            for (int j = 0; j < this.gameObject.transform.GetChild(i).gameObject.transform.GetChildCount(); j++)
            {
                
                TRANSFORM_LIST_FOI_X.Add(this.gameObject.transform.GetChild(i).gameObject.transform.GetChild(j).position.x);
                X = X + this.gameObject.transform.GetChild(i).gameObject.transform.GetChild(j).position.x.ToString() + ",";
                TRANSFORM_LIST_FOI_Y.Add(this.gameObject.transform.GetChild(i).gameObject.transform.GetChild(j).position.y);
                Y = Y + this.gameObject.transform.GetChild(i).gameObject.transform.GetChild(j).position.y.ToString() + ",";
                TRANSFORM_LIST_FOI_Z.Add(this.gameObject.transform.GetChild(i).gameObject.transform.GetChild(j).position.z);
                Z = Z + this.gameObject.transform.GetChild(i).gameObject.transform.GetChild(j).position.z.ToString() + ",";
            }
        }

        Debug.Log(X);
        Debug.Log(Y);
        Debug.Log(Z);

    }

}
