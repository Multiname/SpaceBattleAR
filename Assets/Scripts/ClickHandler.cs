using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    private UiManager uiManager;

    private PointerEventData pointerEventData = new(EventSystem.current);
    private float holdDuration = 1.0f;
    private Coroutine handleLongPress;
    private Spaceship pressedSpaceship;
    private Spaceship spaceshipWithShownInfo;
    private Spaceship selectedAllySpaceship;
    private Spaceship selectedEnemySpaceship;
    private bool cardInfoShown = false;

    private void Update() {
        var click = false;
        var release = false;
        if (Input.GetMouseButtonDown(0)) {
            click = true;
        } else if (Input.GetMouseButtonUp(0)) {
            release = true;
        }

        if (click || release) {
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> clickedUi = new();
            EventSystem.current.RaycastAll(pointerEventData, clickedUi);
            foreach (var ui in clickedUi) {
                Debug.Log("UI: " + ui.gameObject.name);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject clickedObject = null;
            if (Physics.Raycast(ray, out var hitted)) {
                Debug.Log("3D: " + hitted.collider.gameObject.name);
                clickedObject = hitted.collider.gameObject;
            }

            HandleClick(click, clickedUi.Select(ui => ui.gameObject).ToList(), clickedObject);
        }
    }

    private void HandleClick(bool click, List<GameObject> clickedUi, GameObject clickedObject) {
        if (click) {
            if (clickedUi.Count == 0 && spaceshipWithShownInfo == null && !cardInfoShown && clickedObject != null) {
                Spaceship spaceship = clickedObject.GetComponent<Spaceship>();
                if (spaceship != null) {
                    pressedSpaceship = spaceship;
                    handleLongPress = StartCoroutine(HandleLongPress(spaceship));
                }
            }
        } else {
            if (handleLongPress != null) {
                StopCoroutine(handleLongPress);
                handleLongPress = null;
            }

            if (spaceshipWithShownInfo != null) {
                uiManager.HideSpaceshipInfo();

                if (spaceshipWithShownInfo != selectedAllySpaceship && spaceshipWithShownInfo != selectedEnemySpaceship) {
                    spaceshipWithShownInfo.Unselect();
                }

                spaceshipWithShownInfo = null;
            } else if (cardInfoShown) {
                uiManager.HideCardInfo();
                cardInfoShown = false;
            } else if (clickedObject != null && clickedObject.TryGetComponent<Spaceship>(out var spaceship) && spaceship == pressedSpaceship) {
                if (spaceship.Friendly) {
                    if (selectedAllySpaceship != null) {
                        selectedAllySpaceship.Unselect();
                    }

                    selectedAllySpaceship = spaceship;
                    spaceship.Select();
                    uiManager.SelectAllySpaceship(spaceship.Card.Image.sprite);

                    if (selectedEnemySpaceship != null) {
                        uiManager.UnselectEnemySpaceship();
                        selectedEnemySpaceship.Unselect();
                        selectedEnemySpaceship = null;
                    }
                } else if (selectedAllySpaceship != null) {
                    if (spaceship == selectedEnemySpaceship) {
                        uiManager.AttackSpaceship(selectedAllySpaceship, selectedEnemySpaceship);
                        UnselectSpaceships();
                    } else {
                        if (selectedEnemySpaceship != null) {
                            selectedEnemySpaceship.Unselect();
                        }

                        selectedEnemySpaceship = spaceship;
                        spaceship.Select();
                        uiManager.SelectEnemySpaceship(spaceship.Card.Image.sprite);
                    }
                }
            } else if (selectedAllySpaceship != null) {
                UnselectSpaceships();
            }

            pressedSpaceship = null;
        }
    }

    private IEnumerator HandleLongPress(Spaceship spaceship) {
        yield return new WaitForSeconds(holdDuration);
        spaceshipWithShownInfo = spaceship;
        spaceship.Select();
        uiManager.ShowSpaceshipInfo(spaceship);
    }

    public void SetCardInfoShown() {
        StartCoroutine(SetCardInfoShownNextFrame());
    }

    private IEnumerator SetCardInfoShownNextFrame() {
        yield return null;
        cardInfoShown = true;
    }

    private void UnselectSpaceships() {
        uiManager.UnselectSpaceships();

        selectedAllySpaceship.Unselect();
        selectedAllySpaceship = null;

        if (selectedEnemySpaceship != null) {
            selectedEnemySpaceship.Unselect();
        }
    }
}
