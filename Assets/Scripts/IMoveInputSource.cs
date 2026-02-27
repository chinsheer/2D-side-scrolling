using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for abstracting input, allowing for different input sources (e.g., keyboard, AI, network)
public interface IMoveInputSource
{
    Vector2 MoveDirection { get; }
    bool Jump { get; }
}
