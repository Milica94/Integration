// -----------------------------------------------------------------------
// <copyright file="SeriesCompensator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FTN.Services.NetworkModelService.DataModel.Core;
    using FTN.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SeriesCompensator : ConductingEquipment
    {
        private float r0 = 0;
        private float r = 0;
        private float x = 0;
        private float x0 = 0;

        public SeriesCompensator(long globalId)
           : base(globalId)
        {
        }

        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }


        public float X0
        {
            get
            {
                return x0;
            }

            set
            {
                x0 = value;
            }
        }

        public float R
        {
            get
            {
                return r;
            }

            set
            {
                r = value;
            }
        }

        public float R0
        {
            get
            {
                return r0;
            }

            set
            {
                r0 = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeriesCompensator y = (SeriesCompensator)obj;
                return (y.R == this.R && y.X == this.X && y.X0 == this.X0 && y.R0 == this.R0);
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

                case ModelCode.SERIESCOMPENSATOR_R:
                case ModelCode.SERIESCOMPENSATOR_R0:
                case ModelCode.SERIESCOMPENSATOR_X:
                case ModelCode.SERIESCOMPENSATOR_X0:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SERIESCOMPENSATOR_R:
                    property.SetValue(r);
                    break;
                case ModelCode.SERIESCOMPENSATOR_R0:
                    property.SetValue(r0);
                    break;
                case ModelCode.SERIESCOMPENSATOR_X:
                    property.SetValue(x);
                    break;
                case ModelCode.SERIESCOMPENSATOR_X0:
                    property.SetValue(x0);
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
                case ModelCode.SERIESCOMPENSATOR_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_X0:
                    x0 = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

    }
}
