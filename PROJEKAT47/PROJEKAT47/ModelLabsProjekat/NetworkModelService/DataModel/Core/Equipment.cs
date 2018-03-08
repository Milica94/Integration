using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Equipment : PowerSystemResource
    {
        private long equipmentContainer = 0;
        private bool aggregate;
        private bool normallyService;
        public Equipment(long globalId) : base(globalId)
        {
        }

        public long EquipmentContainer
        {
            get
            {
                return equipmentContainer;
            }

            set
            {
                equipmentContainer = value;
            }
        }

        public bool Aggregate
        {
            get
            {
                return aggregate;
            }

            set
            {
                aggregate = value;
            }
        }

        public bool NormallyService
        {
            get
            {
                return normallyService;
            }

            set
            {
                normallyService = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Equipment x = (Equipment)obj;
                return (x.EquipmentContainer == this.EquipmentContainer && x.Aggregate==this.Aggregate && x.NormallyService==this.NormallyService);
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
                case ModelCode.EQUIPMENT_EC:
                case ModelCode.EQUIPMENT_AGGREGATE:
                case ModelCode.EQUIPMENT_NORMALLYSERVICE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.EQUIPMENT_EC:
                    property.SetValue(equipmentContainer);
                    break;
                case ModelCode.EQUIPMENT_NORMALLYSERVICE:
                    property.SetValue(normallyService);
                    break;
                case ModelCode.EQUIPMENT_AGGREGATE:
                    property.SetValue(aggregate);
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
                case ModelCode.EQUIPMENT_EC:
                    equipmentContainer = property.AsReference();
                    break;
                case ModelCode.EQUIPMENT_AGGREGATE:
                    aggregate = property.AsBool();
                    break;
                case ModelCode.EQUIPMENT_NORMALLYSERVICE:
                    normallyService = property.AsBool();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (equipmentContainer != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.EQUIPMENT_EC] = new List<long>();
                references[ModelCode.EQUIPMENT_EC].Add(equipmentContainer);
            }

            base.GetReferences(references, refType);
        }
    }
}
