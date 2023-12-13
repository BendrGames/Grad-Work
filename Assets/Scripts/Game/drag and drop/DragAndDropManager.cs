using Game.drag_and_drop;
using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
     private GameObject selectedPiece;
    private GameObject startPiece;
    private GameObject endPiece;

    [SerializeField]
    private LineRenderer lineRenderer;

    void Update()
    {
        Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenToWorldPoint.y = 0;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object clicked has a draggable component
                selectedPiece = hit.collider.gameObject;
                if (selectedPiece.GetComponent<Draggable>() != null)
                {
                    PieceView pieceView = selectedPiece.GetComponent<PieceView>();

                    // Only allow dragging if the selected piece is a player piece
                    if (pieceView != null && pieceView.pieceType == PieceType.Player)
                    {
                        startPiece = selectedPiece;

                        // Start rendering the line only if the starting piece is a player piece
                        lineRenderer.positionCount = 2;
                        lineRenderer.SetPosition(0, startPiece.transform.position);
                        lineRenderer.SetPosition(1, screenToWorldPoint);
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) && startPiece != null)
        {
            // Update the line renderer during the drag
            lineRenderer.SetPosition(1, screenToWorldPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object released has a draggable component
                selectedPiece = hit.collider.gameObject;
                if (selectedPiece.GetComponent<Draggable>() != null)
                {
                    PieceView startPieceView = startPiece.GetComponent<PieceView>();
                    PieceView endPieceView = selectedPiece.GetComponent<PieceView>();

                    // Only allow dropping if the start piece is a player piece and the end piece is an enemy piece
                    if (startPieceView != null && endPieceView != null &&
                        startPieceView.pieceType == PieceType.Player && endPieceView.pieceType == PieceType.AIPiece)
                    {
                        endPiece = selectedPiece;  // Set endPiece to the correct object

                        // Handle the drag-and-drop completion
                        HandleDragAndDrop();
                    }
                }
            }

            // Reset the line renderer when the drag ends
            lineRenderer.positionCount = 0;
        }
    }

    void HandleDragAndDrop()
    {
        // Do something with startPiece and endPiece
        Debug.Log("Drag started on: " + startPiece.name);
        Debug.Log("Drag ended on: " + endPiece.name);

        PieceView startPieceView = startPiece.GetComponent<PieceView>();
        PieceView endPieceView = endPiece.GetComponent<PieceView>();
        Gameloop.Instance.stateMachine.GamePlayerState.PlayerAttack(startPieceView, endPieceView);

        // Reset the pieces
        startPiece = null;
        endPiece = null;
    }
}
