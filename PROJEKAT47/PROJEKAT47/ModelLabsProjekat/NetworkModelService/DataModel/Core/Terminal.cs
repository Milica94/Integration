// -----------------------------------------------------------------------
// <copyright file="Terminal.cs" company="">
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
    public class Terminal : IdentifiedObject
    {

        private long connectivityNode = 0;

        private long conductingEquipment = 0;

        private bool connected;

        private int sequenceNum = 0;

        private PhaseCode phases;


        public Terminal(long globalId)
            : base(globalId)
        {
        }


        public PhaseCode Phases
        {
            get
            {
                return phases;
            }

            set
            {
                phases = value;
            }
        }
        public int SequenceNum
        {
            get
            {
                return sequenceNum;
            }

            set
            {
                sequenceNum = value;
            }

        }
        public bool Connected
        {
            get
            {
                return connected;
            }

            set
            {
                connected = value;
            }
        }
        public long ConductingEquipment
        {
            get
            {
                return conductingEquipment;
            }

            set
            {
                conductingEquipment = value;
            }
        }


        public long ConnectivityNode
        {
            get
            {
                return connectivityNode;
            }

            set
            {
                connectivityNode = value;
            }
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return (x.ConductingEquipment == this.ConductingEquipment && x.ConnectivityNode == this.ConnectivityNode && x.Connected == this.Connected && x.SequenceNum == this.SequenceNum && x.phases == this.phases);

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


        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.TERMINAL_CE:
                case ModelCode.TERMINAL_CN:
                case ModelCode.TERMINAL_CONNECTED:
                case ModelCode.TERMINAL_PHASES:
                case ModelCode.TERMINAL_SEQUENCENUMBER:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TERMINAL_CN:
                    prop.SetValue(connectivityNode);
                    break;
                case ModelCode.TERMINAL_CE:
                    prop.SetValue(conductingEquipment);
                    break;
                case ModelCode.TERMINAL_CONNECTED:
                    prop.SetValue(connected);
                    break;
                case ModelCode.TERMINAL_PHASES:
                    prop.SetValue((short)phases);
                    break;
                case ModelCode.TERMINAL_SEQUENCENUMBER:
                    prop.SetValue(sequenceNum);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CN:
                    connectivityNode = property.AsReference();
                    break;
                case ModelCode.TERMINAL_CE:
                    conductingEquipment = property.AsReference();
                    break; 
                case ModelCode.TERMINAL_CONNECTED:
                    connected = property.AsBool();
                    break;
                case ModelCode.TERMINAL_PHASES:
                    phases = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.TERMINAL_SEQUENCENUMBER:
                    sequenceNum = property.AsInt();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }





        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (connectivityNode != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CN] = new List<long>();
                references[ModelCode.TERMINAL_CN].Add(connectivityNode);
            }


            if (conductingEquipment != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CE] = new List<long>();
                references[ModelCode.TERMINAL_CE].Add(conductingEquipment);
            }
            base.GetReferences(references, refType);
        }


    }
}