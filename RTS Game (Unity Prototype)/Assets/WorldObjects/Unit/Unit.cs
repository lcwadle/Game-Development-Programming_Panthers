using UnityEngine;
using System.Collections;
using RTS;

/// <summary>
/// Class for any world object in the game.
/// This includes units and buildings.
/// </summary>

public class Unit : WorldObject {

    protected bool moving, rotating;
    private Vector3 destination;
    private Quaternion targetRotation;
    public float moveSpeed, rotateSpeed;
 
    /// <summary>
    /// Initializes variables at creation.
    /// </summary>
    
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Initializes variables and functions at script
    /// call.
    /// </summary>

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Game loop functions.
    /// </summary>

    protected override void Update()
    {
        base.Update();

        if (rotating)
            TurnToTarget();
        else if (moving)
            MakeMove();
    }

    /// <summary>
    /// GUI functions.
    /// </summary>

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    /// <summary>
    /// Changes the cursor icon to the "Move" action if a unit is 
    /// selected and the cursor at a allowed location.
    /// </summary>
    /// <param name="hoverObject"></param>

    public override void SetHoverState(GameObject hoverObject)
    {
        base.SetHoverState(hoverObject);

        if (player && player.human && currentlySelected)
            if (hoverObject.name == "Ground")
                player.hud.SetCursorState(CursorState.Move);
    }

    /// <summary>
    /// Begins the move action if a unit is selected 
    /// and the location clicked is an allowed move position.
    /// </summary>
    /// <param name="hitObject"></param>
    /// <param name="hitPoint"></param>
    /// <param name="controller"></param>

    public override void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        base.MouseClick(hitObject, hitPoint, controller);

        if (player && player.human && currentlySelected)
            if (hitObject.name == "Ground" && hitPoint != ResourceManager.InvalidPosition)
            {
                float x = hitPoint.x;
                float y = player.SelectedObject.transform.position.y;
                float z = hitPoint.z;
                Vector3 destination = new Vector3(x, y, z);
                StartMove(destination);
            }
    }

    /// <summary>
    /// Begins the rotation step of the move action.
    /// </summary>
    /// <param name="destination"></param>

    public void StartMove(Vector3 destination)
    {
        this.destination = destination;
        targetRotation = Quaternion.LookRotation(destination - transform.position);
        rotating = true;
        moving = false;
    }

    /// <summary>
    /// Performs the rotation of the unit towards the
    /// destination.
    /// </summary>

    private void TurnToTarget()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);

        Quaternion inverseTargetRotation = new Quaternion(-targetRotation.x, -targetRotation.y, -targetRotation.z, -targetRotation.w);
        if (transform.rotation == targetRotation || transform.rotation == inverseTargetRotation)
        {
            rotating = false;
            moving = true;
        }
        CalculateBounds();
    }

    /// <summary>
    /// Performs the transform of the unit towards
    /// the destination.
    /// </summary>

    private void MakeMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
        if (transform.position == destination)
            moving = false;
        CalculateBounds();
    }
}
