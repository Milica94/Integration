// -----------------------------------------------------------------------
// <copyright file="Bay.cs" company="">
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
    public class Bay : EquipmentContainer
    {
        private bool bayEnergyMassFlag;

        private bool bayPowerMassFlag;

        public Bay(long globalId)
            : base(globalId)
        {
        }

        public bool BayEnergyMassFlag
        {
            get
            {
                return bayEnergyMassFlag;
            }

            set
            {
                bayEnergyMassFlag = value;
            }
        }

        public bool BayPowerMassFlag
        {
            get
            {
                return bayPowerMassFlag;
            }

            set
            {
                bayPowerMassFlag = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Bay x = (Bay)obj;
                return ((x.BayEnergyMassFlag == this.BayEnergyMassFlag) && (x.BayPowerMassFlag == this.BayPowerMassFlag));

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
                case ModelCode.BAY_ENERGYFLAG:
                case ModelCode.BAY_POWERFLAG:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.BAY_ENERGYFLAG:
                    prop.SetValue(bayEnergyMassFlag);
                    break;
                case ModelCode.BAY_POWERFLAG:
                    prop.SetValue(bayPowerMassFlag);
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
                case ModelCode.BAY_POWERFLAG:
                    bayPowerMassFlag = property.AsBool();
                    break;
                case ModelCode.BAY_ENERGYFLAG:
                    bayEnergyMassFlag = property.AsBool();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation


    }
}


