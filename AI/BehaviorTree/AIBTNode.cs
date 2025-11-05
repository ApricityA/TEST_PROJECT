using System.Collections.Generic;
public enum BTState
{
	BT_SUCCESS,
	BT_FAILURE,
	BT_RUNNING,
}
public abstract class AIBTNode
{
    public abstract BTState Execute();
}

public class RootNode : AIBTNode
{
    public AIBTNode child { get; set; }

    public override BTState Execute()
    {
        return child.Execute();
    }
}

public class SelectorNode : AIBTNode
{
    public List<AIBTNode> children { get; set; } = new List<AIBTNode>();
    public override BTState Execute()
    {
        foreach (var child in children)
        {
            BTState state = child.Execute();
            if (state != BTState.BT_FAILURE)
            {
                return state;
            }
        }
        return BTState.BT_FAILURE;
    }
}

public class SequenceNode : AIBTNode
{
    public List<AIBTNode> children { get; set; } = new List<AIBTNode>();
    public override BTState Execute()
    {
        foreach (var child in children)
        {
            BTState state = child.Execute();
            if (state != BTState.BT_SUCCESS)
            {
                return state;
            }
        }
        return BTState.BT_SUCCESS;
    }
}