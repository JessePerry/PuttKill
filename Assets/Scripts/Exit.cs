using UnityEngine;

public class Exit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.GoToEndLevel();
    }
}
