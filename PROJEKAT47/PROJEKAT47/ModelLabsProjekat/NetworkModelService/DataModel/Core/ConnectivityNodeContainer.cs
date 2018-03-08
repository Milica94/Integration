// -----------------------------------------------------------------------
// <copyright file="ConnectivityNodeContainer.cs" company="">
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
    public class ConnectivityNodeContainer : PowerSystemResource
    {
        private List<long> cnodes = new List<long>();

        public ConnectivityNodeContainer(long globalId) : base(globalId)
        {
        }

        public List<long> CNodes
        {
            get
            {
                return cnodes;
            }

            set
            {
                cnodes = value;
            }
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ConnectivityNodeContainer x = (ConnectivityNodeContainer)obj;
                return (CompareHelper.CompareLists(x.CNodes, this.CNodes, true));
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
                case ModelCode.CNCONTAINER_CNS:

                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CNCONTAINER_CNS:
                    prop.SetValue(cnodes);
                    break;



                default:
                    base.GetProperty(prop);
                    break;
            }
        }
        //klijentska strana moze da uzme properti ali ne sme da setuje properti tipa niz referenci
        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {


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
                return cnodes.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (cnodes != null && cnodes.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CNCONTAINER_CNS] = cnodes.GetRange(0, cnodes.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CNODE_CNC:
                    cnodes.Add(globalId);
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
                case ModelCode.CNODE_CNC:

                    if (cnodes.Contains(globalId))
                    {
                        cnodes.Remove(globalId);
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

