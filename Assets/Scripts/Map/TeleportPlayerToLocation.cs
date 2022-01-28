using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerToLocation : MonoBehaviour
{
    public Vector2 PositionToTeleportPlayer;
    public Vector2 PositionToMoveCamera;
    public GameObject FadeOutInScreen;

    public GameObject mainCamera;

    private float transparency;
    private float greenValue;

    private GameObject Player;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = collision.gameObject;
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        StartCoroutine(TeleportPlayer());
    }

    private IEnumerator TeleportPlayer()
    {
        yield return StartCoroutine(ScreenFadeOut());

        Player.transform.position = new Vector3(PositionToTeleportPlayer.x, PositionToTeleportPlayer.y, -1);
        mainCamera.transform.position = new Vector3(PositionToMoveCamera.x, PositionToMoveCamera.y, -10);

        yield return StartCoroutine(ScreenFadeIn());
    }

    private IEnumerator ScreenFadeOut()
    {
        FadeOutInScreen.SetActive(true);
        transparency = 0f;
        greenValue = 250f;

        while (true)
        {
            if (transparency > 0 && transparency <= 250)
            {
                if (transparency % 14 == 0)
                {
                    FadeOutInScreen.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, greenValue / 250f, 0f, transparency / 250f);
                }

                greenValue -= 2;
                transparency += 2;
            }
            else if (transparency < 250)
            {
                greenValue -= 2;
                transparency += 2;
            }
            else if (transparency >= 250)
                break;

            if(transparency % 20 == 0)
                yield return new WaitForFixedUpdate();
        }

        FadeOutInScreen.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, 1f);
    }

    private IEnumerator ScreenFadeIn()
    {
        transparency = 250f;
        greenValue = 0f;

        while (true)
        {
            if (transparency > 0 && transparency <= 250)
            {
                if (transparency % 14 == 0)
                {
                    FadeOutInScreen.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, greenValue / 250f, 0f, transparency / 250f);
                }

                greenValue += 2;
                transparency -= 2;
            }

            else if (transparency <= 0)
                break;

            if (transparency % 20 == 0)
                yield return new WaitForFixedUpdate();
        }

        FadeOutInScreen.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0f, 0f);

        FadeOutInScreen.SetActive(false);
    }
}
