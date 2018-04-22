using UnityEngine;

public class Player : MonoBehaviour
{
    public int Force;
    public float MaxForce;

    private LineRenderer line;
    private Transform trans;
    private Rigidbody2D rigBody;
    private Vector3 mouseDownStart;
    private LevelScore levelScore;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        trans = GetComponent<Transform>();
        rigBody = GetComponent<Rigidbody2D>();
        levelScore = GetComponentInParent<LevelScore>();
    }

    void FixedUpdate()
    {
    }
    
    void Update()
    {
        //trans.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
        
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDownStart = new Vector3(mousePos.x, mousePos.y, -2);
        }

        if (Input.GetMouseButtonUp(0))
        {
            rigBody.AddForce((line.GetPosition(1) - line.GetPosition(0)) * Force);
            levelScore.Shots++;
        }

        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, new Vector3(trans.position.x, trans.position.y, -2));
            SetLineEnd();
        }
        else
        {
            line.SetPosition(0, new Vector3(trans.position.x, trans.position.y, -2));
            line.SetPosition(1, new Vector3(trans.position.x, trans.position.y, -2));
        }
    }

    private void SetLineEnd()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePosFlat = new Vector3(mousePos.x, mousePos.y, -2);
        var endVector = trans.position - Vector3.ClampMagnitude(mousePosFlat - mouseDownStart, MaxForce);
        
        line.SetPosition(1, endVector);
    }
}
