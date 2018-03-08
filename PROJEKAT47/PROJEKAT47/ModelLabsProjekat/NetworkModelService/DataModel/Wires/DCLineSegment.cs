// -----------------------------------------------------------------------
// <copyright file="DCLineSegment.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FTN.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DCLineSegment : Conductor
    {

        private float inductance = 0;
        private float resistance = 0;
        public DCLineSegment(long globalId)
            : base(globalId)
        {
        }

        public float Inductance
        {
            get
            {
                return inductance;
            }

            set
            {
                inductance = value;
            }
        }

        public float Resistance
        {
            get
            {
                return resistance;
            }

            set
            {
                resistance = value;
            }
        }

        public override bool Equals(object obj)
            {
                if (base.Equals(obj))
                {
                    DCLineSegment x = (DCLineSegment)obj;
                return (x.Resistance == this.Resistance && x.Inductance== this.Inductance);
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
                case ModelCode.DCLINESEGMENT_INDUTANCE:
                case ModelCode.DCLINESEGMENT_RESISTANCE:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.DCLINESEGMENT_RESISTANCE:
                    property.SetValue(resistance);
                    break;
                case ModelCode.DCLINESEGMENT_INDUTANCE:
                    property.SetValue(inductance);
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
                case ModelCode.DCLINESEGMENT_INDUTANCE:
                    inductance = property.AsFloat();
                    break;
                case ModelCode.DCLINESEGMENT_RESISTANCE:
                    resistance = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

    }
}
