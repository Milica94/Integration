// -----------------------------------------------------------------------
// <copyright file="ConnectivityNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FTN.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConnectivityNode : IdentifiedObject
    {

        private List<long> terminals1 = new List<long>();

        private string description = string.Empty;

        private long connNodeContainer = 0;




        public ConnectivityNode(long globalId)
            : base(globalId)
        {
        }


        public long ConnNodeContainer
        {
            get
            {
                return connNodeContainer;
            }

            set
            {
                connNodeContainer = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public List<long> Terminals1
        {
            get
            {
                return terminals1;
            }

            set
            {
                terminals1 = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ConnectivityNode x = (ConnectivityNode)obj;
                return (x.Description == this.Description && x.ConnNodeContainer == this.ConnNodeContainer &&
                        CompareHelper.CompareLists(x.Terminals1, this.Terminals1));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.CNODE_CNC:
                case ModelCode.CNODE_DESC:
                case ModelCode.CNODE_TERMINALS:

                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CNODE_CNC:
                    property.SetValue(connNodeContainer);
                    break;

                case ModelCode.CNODE_DESC:
                    property.SetValue(description);
                    break;

                case ModelCode.CNODE_TERMINALS:
                    property.SetValue(terminals1);
                    break;



                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CNODE_CNC:
                    connNodeContainer = property.AsReference();
                    break;

                case ModelCode.CNODE_DESC:
                    description = property.AsString();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return terminals1.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (connNodeContainer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CNODE_CNC] = new List<long>();
                references[ModelCode.CNODE_CNC].Add(connNodeContainer);
            }

            if (terminals1 != null && terminals1.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CNODE_TERMINALS] = terminals1.GetRange(0, terminals1.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TERMINAL_CN:
                    terminals1.Add(globalId);
                    break;


                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TERMINAL_CN:

                    if (terminals1.Contains(globalId))
                    {
                        terminals1.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation
    }
}

