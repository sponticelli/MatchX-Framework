using System.Collections.Generic;

namespace ZigZaggle.Collapse
{
    public class GroupedLogicActions
    {
        public enum GroupType
        {
            Remove,
            Move
        }
        private List<ILogicAction> actions;
        public IList<ILogicAction> Actions => actions.AsReadOnly();
        public GroupType Type { get;  }
        public GroupedLogicActions(GroupType type)
        {
            this.Type = type;
            actions = new List<ILogicAction>();
        }

        public void Add(ILogicAction action)
        {
            actions.Add(action);
        }
    }
}