using Godot;

namespace UISystem.MenuSystem;

/// <summary>
/// this code is translated from gdscript
/// https://github.com/godotengine/godot-demo-projects/blob/master/viewport/gui_in_3d/gui_3d.gd
/// I didn't bother including method for billboard display.
/// </summary>
public partial class GuiPanel3D : Node
{
    [Export] private SubViewport _subViewport;
    [Export] private MeshInstance3D _quad;
    [Export] private Area3D _area3d;
    [Export] private Control _menusParent;
    [Export] private Control _popupsParent;

    private bool _isMouseInside;
    private Vector2 _lastEventPosition2D;
    private float _lastEventTime = -1;

    /// <summary>
    /// Gets the subviewport.
    /// </summary>
    public SubViewport SubViewport => _subViewport;

    /// <summary>
    /// Gets menus parent.
    /// </summary>
    public Control MenusParent => _menusParent;

    /// <summary>
    /// Gets popups parent.
    /// </summary>
    public Control PopupsParent => _popupsParent;

    /// <inheritdoc/>
    public override void _Ready()
    {
        _area3d.MouseEntered += MouseEnteredArea;
        _area3d.MouseExited += MouseExitedArea;
        _area3d.InputEvent += MouseInputEvent;
    }

    private void MouseEnteredArea() => _isMouseInside = true;

    private void MouseExitedArea() => _isMouseInside = false;

    private void MouseInputEvent(Node camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, long shapeIndex)
    {
        if (@event is not InputEventMouse mouseEvent)
            return;

        // Get mesh size to detect edges and make conversions. This code only support PlaneMesh and QuadMesh.
        var quadMeshSize = (_quad.Mesh as QuadMesh).Size;

        // Event position in Area3D in world coordinate space.
        var eventPosition3D = eventPosition;

        // Current time in seconds since engine start.
        var now = Time.GetTicksMsec() / 1000.0f;

        // Convert position to a coordinate space relative to the Area3D node.
        // NOTE: affine_inverse accounts for the Area3D node's scale, rotation, and position in the scene!
        eventPosition3D = _quad.GlobalTransform.AffineInverse() * eventPosition3D;

        // TODO: Adapt to bilboard mode or avoid completely.
        Vector2 eventPosition2D = Vector2.Zero;

        if (_isMouseInside)
        {
            // Convert the relative event position from 3D to 2D.
            eventPosition2D = new Vector2(eventPosition3D.X, -eventPosition3D.Y);

            // Right now the event position's range is the following: (-quad_size/2) -> (quad_size/2)
            // We need to convert it into the following range: -0.5 -> 0.5
            eventPosition2D.X /= quadMeshSize.X;
            eventPosition2D.Y /= quadMeshSize.Y;

            // Then we need to convert it into the following range: 0 -> 1
            eventPosition2D.X += 0.5f;
            eventPosition2D.Y += 0.5f;

            // Finally, we convert the position to the following range: 0 -> viewport.size
            // We need to do these conversions so the event's position is in the viewport's coordinate system.
            eventPosition2D.X *= _subViewport.Size.X;
            eventPosition2D.Y *= _subViewport.Size.Y;
        }
        else if (!_lastEventPosition2D.IsEqualApprox(Vector2.Zero))
        {
            // Fall back to the last known event position.
            eventPosition2D = _lastEventPosition2D;
        }

        // Set the event's position and global position.
        mouseEvent.Position = eventPosition2D;
        mouseEvent.GlobalPosition = eventPosition2D;

        // Calculate the relative event distance.
        if (@event is InputEventMouseMotion motion)
        {
            // If there is not a stored previous position, then we'll assume there is no relative motion.
            if (_lastEventPosition2D.IsEqualApprox(Vector2.Zero))
            {
                motion.Relative = Vector2.Zero;
            }
            else
            {
                // If there is a stored previous position, then we'll calculate the relative position by subtracting
                // the previous position from the new position. This will give us the distance the event traveled from prev_pos.
                motion.Relative = eventPosition2D - _lastEventPosition2D;
                motion.Velocity = motion.Relative / (now - _lastEventTime);
            }
        }
        else if (@event is InputEventScreenDrag drag)
        {
            // If there is not a stored previous position, then we'll assume there is no relative motion.
            if (_lastEventPosition2D.IsEqualApprox(Vector2.Zero))
            {
                drag.Relative = Vector2.Zero;
            }
            else
            {
                // If there is a stored previous position, then we'll calculate the relative position by subtracting
                // the previous position from the new position. This will give us the distance the event traveled from prev_pos.
                drag.Relative = eventPosition2D - _lastEventPosition2D;
                drag.Velocity = drag.Relative / (now - _lastEventTime);
            }
        }

        // Update last_event_pos2D with the position we just calculated.
        _lastEventPosition2D = eventPosition2D;

        // Update last_event_time to current time.
        _lastEventTime = now;

        // Finally, send the processed input event to the viewport.
        _subViewport.PushInput(@event);
    }
}
