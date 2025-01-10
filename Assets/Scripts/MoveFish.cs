using UnityEngine;

public class MoveFish : MonoBehaviour
{
    private MainLoop mainLoop;


    public float minSpeed = 2;
    public float maxSpeed = 12;
    float currentSpeed;

    Vector3 boundary;

    AudioSource audioSource;

    void Start()
    {
        mainLoop = GameObject.Find("Main Camera").GetComponent<MainLoop>();
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        currentSpeed = Random.Range(minSpeed, maxSpeed); // randomize each fish speed
    }

    void Update()
    {
        if (transform.position.x > -boundary.x - GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            transform.Translate(new Vector3(-1, 0, 0) * currentSpeed * Time.deltaTime);
        }
        else if (transform.position.x <= -boundary.x - GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            mainLoop = GameObject.Find("Main Camera").GetComponent<MainLoop>();
            mainLoop.RemoveFish();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("You were eaten by a fish!");
            mainLoop.EatTadpole();
        }
    }
}
