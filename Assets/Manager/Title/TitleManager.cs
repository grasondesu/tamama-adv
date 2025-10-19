using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleManager : MonoBehaviour
{
    [Header("????????")]
    [SerializeField] private string courseSelectSceneName = "CourseSelectScene";

    [Header("????")]
    [SerializeField] private VideoPlayer videoPlayer; // Canvas?RawImage???????VideoPlayer

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play(); // ???????????
        }
    }

    void Update()
    {
        // PC: ???????
        if (Input.GetMouseButtonDown(0))
        {
            LoadCourseSelect();
        }

        // ???: ???
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                LoadCourseSelect();
            }
        }
    }

    private void LoadCourseSelect()
    {
        Debug.Log("??????/????? ? ??");
        SceneManager.LoadScene(courseSelectSceneName);
    }
}
