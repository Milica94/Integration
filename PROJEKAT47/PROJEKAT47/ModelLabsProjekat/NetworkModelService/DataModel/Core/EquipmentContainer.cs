// -----------------------------------------------------------------------
// <copyright file="EquipmentContainer.cs" company="">
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
    public class EquipmentContainer : ConnectivityNodeContainer
    {

        private List<long> equipments = new List<long>();


        public EquipmentContainer(long globalId) : base(globalId)
        {
        }



        public List<long> Equipments
        {
            get { return equipments; }
            set { equipments = value; }
        }



        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                EquipmentContainer x = (EquipmentContainer)obj;
                return (CompareHelper.CompareLists(x.Equipments, this.Equipments, true));
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.EQUIPMENTCONTAINER_EQUIPMENTS:

                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.EQUIPMENTCONTAINER_EQUIPMENTS:
                    prop.SetValue(equipments);
                    break;


                default:
                    base.GetProperty(prop);
                    break;
            }
        }

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
        public override bool IsReferenced
        {
            get
            {
                return equipments.Count != 0 || base.IsReferenced;
            }
        }
        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (equipments != null && equipments.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.EQUIPMENTCONTAINER_EQUIPMENTS] = equipments.GetRange(0, equipments.Count);
            }


            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.EQUIPMENT_EC:
                    equipments.Add(globalId);
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
                case ModelCode.EQUIPMENT_EC:

                    if (equipments.Contains(globalId))
                    {
                        equipments.Remove(globalId);
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
    }

}
