using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Transform[] terrain;   // foregrounds and backgrounds to be parallaxed
    [SerializeField] float smoothing = 1f;

    float[] parallaxScales;     // proportion of camera's movement to move backgrounds by
    Transform cam;
    Vector3 prevCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        prevCamPos = cam.position;
        parallaxScales = new float[terrain.Length];
        for (int i = 0; i < parallaxScales.Length; i++)
            parallaxScales[i] = terrain[i].position.z * -1;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < terrain.Length; i++)
        {
            // parallax is opposite of camera movement because previous frame is multiplied by scale
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is current position plus parallax
            float terrainTargetPosX = terrain[i].position.x + parallax;

            // create a target position which is terrain's current position with it's target position
            Vector3 terrainTargetPos = new Vector3(terrainTargetPosX,
                terrain[i].position.y, terrain[i].position.z);
            
            // fade between current position and target position
            terrain[i].position = Vector3.Lerp(terrain[i].position,
                terrainTargetPos, smoothing * Time.deltaTime);
        }

        prevCamPos = cam.position;
    }
}
