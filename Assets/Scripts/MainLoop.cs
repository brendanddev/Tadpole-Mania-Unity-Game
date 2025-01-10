using UnityEngine;
using TMPro;

public class MainLoop : MonoBehaviour
{
    // Tadpole (Controlled by user)
    public GameObject Tadpole;
    public float tadpoleSpeed;
    Vector2 tadpoleSize;

    // Fish (Enemy)
    public GameObject Fish;
    public int maxFish = 6;
    int currentFish = 0;

    public int freq = 3;

    // Game screen boundary
    Vector3 boundary;

    // Audio player
    AudioSource audioPlayer;
    AudioSource musicAudioPlayer;
    public AudioClip move_sound;
    public AudioClip eaten_sound;
    public AudioClip music;

    // Game control
    bool gameIsRunning = false;

    // Title and instructions text objects
    GameObject title;
    GameObject instructions;

    // Score text object
    GameObject scoreObject; 
    private int scoreValue = 0; // the value of the score
    float scoreTimer = 0;

    // High score text object
    GameObject highScoreObject;
    private int highScoreValue; // value of the highest score


    void Start() // called before first frame
    {
        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        tadpoleSize = new Vector2(Tadpole.GetComponent<SpriteRenderer>().bounds.size.x, Tadpole.GetComponent<SpriteRenderer>().bounds.size.y);
        audioPlayer = Tadpole.GetComponent<AudioSource>();
        musicAudioPlayer = gameObject.AddComponent<AudioSource>();
        title = GameObject.Find("Title");
        instructions = GameObject.Find("Instructions");
        scoreObject = GameObject.Find("Score");
        highScoreObject = GameObject.Find("HighScore");

        highScoreObject.GetComponent<TextMeshPro>().SetText("High Score: " + highScoreValue); // high score is always shown

        musicAudioPlayer.clip = music; // background music clip
        musicAudioPlayer.loop = true; // loop the audio
        musicAudioPlayer.volume = 0.3f;
        
    }


    void Update() // called once per frame
    {
        if (gameIsRunning)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 1)
            {
                scoreTimer = 0;
                scoreValue++;
                scoreObject.GetComponent<TextMeshPro>().SetText("Score: {0}", scoreValue);
            }
            if (Input.GetKey(KeyCode.W) && Tadpole.transform.position.y < boundary.y - tadpoleSize.y / 2)
            {
                MoveTadpole(new Vector2(0, 1)); // move up
            }

            if (Input.GetKey(KeyCode.S) && Tadpole.transform.position.y > -boundary.y + tadpoleSize.y / 2)
            {
                MoveTadpole(new Vector2(0, -1)); // move down
            }

            if (Input.GetKey(KeyCode.D) && Tadpole.transform.position.x < boundary.x - tadpoleSize.x / 2)
            {
                MoveTadpole(new Vector2(1, 0)); // move right
            }

            if (Input.GetKey(KeyCode.A) && Tadpole.transform.position.x > -boundary.x + tadpoleSize.x / 2)
            {
                MoveTadpole(new Vector2(-1, 0)); // move left
            }

            if (currentFish < maxFish && Random.Range(0, freq) == 0) // every '3' frames, new fish
            {
                SpawnFish();
            }

        } else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.K)) { // start new game
            
            gameIsRunning = true; // game is now running
            title.SetActive(false); // hide the title
            instructions.SetActive(false); // hide the instructions
            scoreValue = 0; // reset the score
            scoreTimer = 0;
            musicAudioPlayer.Play();
        }
    }


    void MoveTadpole(Vector2 direction)
    {
        Tadpole.transform.Translate(direction * tadpoleSpeed * Time.deltaTime);
        audioPlayer.clip = move_sound;
        audioPlayer.Play();
    }

    void SpawnFish()
    {
        GameObject fish = Instantiate(Fish);
        fish.transform.position = new Vector3(boundary.x + 10, Random.Range(-boundary.y, boundary.y), -1);
        currentFish++;
    }

    public void RemoveFish()
    {
        currentFish--;
    }

    public void EatTadpole()
    {
        audioPlayer.clip = eaten_sound;
        audioPlayer.Play();

        if (scoreValue > highScoreValue) // check to see if the games current score is higher than the high
        {
            highScoreValue = scoreValue;
            highScoreObject.GetComponent<TextMeshPro>().SetText("High Score: " + highScoreValue); // show new high score

        }
        gameIsRunning = false;
        title.SetActive(true);
        instructions.SetActive(true);

        musicAudioPlayer.Stop();
    }


}
