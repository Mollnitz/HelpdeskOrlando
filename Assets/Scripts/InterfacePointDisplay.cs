using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;

public class InterfacePointDisplay : MonoBehaviour
{
    private int currentPoints = 0;
    private int lastStringInt = 0;

    Vector3 org;

    bool runningAnim = false;

    Text textField;

    // Start is called before the first frame update
    void Start()
    {
        org = transform.position;
        textField = GetComponent<Text>();
        UpdateText(0);
        GameManager.PointEvent.AddListener(x =>
        {
            currentPoints += (int)x;
            if (!runningAnim)
            {
                runningAnim = true;
                StartCoroutine(FancyIncreasePoints());
            }

        });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            GameManager.PointEvent.Invoke(1000);
        }
    }

    private void UpdateText(int points)
    {
        if (points > currentPoints)
            return;
        textField.text = points.ToString();
        lastStringInt = points;
    }

    IEnumerator FancyIncreasePoints()
    {
        while(lastStringInt != currentPoints)
        {

            int distance = Mathf.Max(currentPoints - lastStringInt, 120);

            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForEndOfFrame();
                UpdateText(distance / 120 + lastStringInt);

                var shake = Shake2D(Time.time);
                transform.localPosition = transform.localPosition + new Vector3(shake.x, shake.y);
            }

            


            if (lastStringInt + 10 >= currentPoints )
            {
                UpdateText(currentPoints);
                break;
            }

        }

        runningAnim = false;
        transform.position = org;
    }

    //https://forum.unity.com/threads/using-mathf-perlinnoise-for-camera-shake.208456/
    public static Vector2 Shake2D(float time, float amplitude = 0.6f, float frequency = 0.98f, int octaves = 2, float persistance = 0.2f, float lacunarity = 20f, float burstFrequency = 0.5f, int burstContrast = 2)
    {
        float valX = 0;
        float valY = 0;

        float iAmplitude = 1;
        float iFrequency = frequency;
        float maxAmplitude = 0;

        // Burst frequency
        float burstCoord = time / (1 - burstFrequency);

        // Sample diagonally trough perlin noise
        float burstMultiplier = Mathf.PerlinNoise(burstCoord, burstCoord);

        //Apply contrast to the burst multiplier using power, it will make values stay close to zero and less often peak closer to 1
        burstMultiplier = Mathf.Pow(burstMultiplier, burstContrast);

        for (int i = 0; i < octaves; i++) // Iterate trough octaves
        {
            float noiseFrequency = time / (1 - iFrequency) / 10;

            float perlinValueX = Mathf.PerlinNoise(noiseFrequency, 0.5f);
            float perlinValueY = Mathf.PerlinNoise(0.5f, noiseFrequency);

            // Adding small value To keep the average at 0 and   *2 - 1 to keep values between -1 and 1.
            perlinValueX = (perlinValueX + 0.0352f) * 2 - 1;
            perlinValueY = (perlinValueY + 0.0345f) * 2 - 1;

            valX += perlinValueX * iAmplitude;
            valY += perlinValueY * iAmplitude;

            // Keeping track of maximum amplitude for normalizing later
            maxAmplitude += iAmplitude;

            iAmplitude *= persistance;
            iFrequency *= lacunarity;
        }

        valX *= burstMultiplier;
        valY *= burstMultiplier;

        // normalize
        valX /= maxAmplitude;
        valY /= maxAmplitude;

        valX *= amplitude;
        valY *= amplitude;

        return new Vector2(valX, valY);
    }

}
