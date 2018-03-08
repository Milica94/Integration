// -----------------------------------------------------------------------
// <copyright file="ACLineSegment.cs" company="">
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
    public class ACLineSegment : Conductor
    {
        private float b0ch = 0;
        private float bch = 0;
        private float g0ch = 0;
        private float gch = 0;
        private float r0 = 0;
        private float r = 0;
        private float x = 0;
        private float x0 = 0;

        public ACLineSegment(long globalId)
           : base(globalId)
        {
        }

        public float B0ch
        {
            get
            {
                return b0ch;
            }

            set
            {
                b0ch = value;
            }
        }

        public float Bch
        {
            get
            {
                return bch;
            }

            set
            {
                bch = value;
            }
        }

        public float G0ch
        {
            get
            {
                return g0ch;
            }

            set
            {
                g0ch = value;
            }

        }

        public float Gch
        {
            get
            {
                return gch;
            }

            set
            {
                gch = value;
            }
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
                ACLineSegment y = (ACLineSegment)obj;
                return (y.R == this.R && y.X == this.X && y.X0 == this.X0 && y.R0 == this.R0 && y.G0ch == this.G0ch && y.B0ch == this.B0ch && y.Bch == this.Bch && y.Gch == this.Gch);

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
                case ModelCode.ACLINESEGMENT_B0CH:
                case ModelCode.ACLINESEGMENT_BCH:
                case ModelCode.ACLINESEGMENT_G0CH:
                case ModelCode.ACLINESEGMENT_GCH:
                case ModelCode.ACLINESEGMENT_R:
                case ModelCode.ACLINESEGMENT_R0:
                case ModelCode.ACLINESEGMENT_X:
                case ModelCode.ACLINESEGMENT_X0:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.ACLINESEGMENT_X0:
                    prop.SetValue(x0);
                    break;
                case ModelCode.ACLINESEGMENT_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.ACLINESEGMENT_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.ACLINESEGMENT_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.ACLINESEGMENT_GCH:
                    prop.SetValue(gch);
                    break;
                case ModelCode.ACLINESEGMENT_G0CH:
                    prop.SetValue(g0ch);
                    break;
                case ModelCode.ACLINESEGMENT_B0CH:
                    prop.SetValue(b0ch);
                    break;
                case ModelCode.ACLINESEGMENT_BCH:
                    prop.SetValue(bch);
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
                case ModelCode.ACLINESEGMENT_B0CH:
                    b0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_BCH:
                    bch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_G0CH:
                    g0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_GCH:
                    gch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_X0:
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
