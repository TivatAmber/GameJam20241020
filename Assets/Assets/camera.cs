using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
  [SerializeField]  private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
