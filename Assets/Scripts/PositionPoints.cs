using UnityEngine;

public class PositionPoints : MonoBehaviour {
    [SerializeField] private Transform entrancePoint, playerPoint, exitPoint;
    private void Start() {
        Invoke(nameof(SetPoints), .001f);
    }

    private void SetPoints() {
        GameManager.instance.UpdatePoints(entrancePoint, playerPoint, exitPoint);
    }
}
