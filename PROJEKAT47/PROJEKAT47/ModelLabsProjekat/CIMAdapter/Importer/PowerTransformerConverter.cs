namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    using FTN.Common;

    /// <summary>
    /// PowerTransformerConverter has methods for populating
    /// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
    /// </summary>
    public static class PowerTransformerConverter
    {


        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }

                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIAS, cimIdentifiedObject.AliasName));
                }
            }
        }


        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);


            }
        }



        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);
                if(cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYSERVICE, cimEquipment.NormallyInService));
                }
                if (cimEquipment.EquipmentContainerHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimEquipment.EquipmentContainer.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimEquipment.ID);
                        report.Report.Append("\" - Failed to set reference to EquipmentContainer: rdfID \"").Append(cimEquipment.EquipmentContainer.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_EC, gid));
                }


            }
        }



        public static void PopulateTerminalProperties(Terminal cimTerminal, ResourceDescription rd, ImportHelper import, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);
               // PowerTransformerConverter.PopulateConductingEquipmentProperties(cimTerminal, rd, import, report);

                if(cimTerminal.ConnectedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTED, cimTerminal.Connected));
                }
                if (cimTerminal.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_SEQUENCENUMBER, cimTerminal.SequenceNumber));
                }
                if (cimTerminal.PhasesHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_PHASES, (short)cimTerminal.Phases));
                }
                if (cimTerminal.ConnectivityNodeHasValue)
                {
                    long gid = import.GetMappedGID(cimTerminal.ConnectivityNode.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to ConnectivityNode: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CN, gid));
                }
                if (cimTerminal.ConductingEquipmentHasValue)
                {
                    long gid = import.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to ConductingEquipment: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CE, gid));
                }

            }
        }

        public static void PopulateNodeProperties(ConnectivityNode cimCNode, ResourceDescription rd, ImportHelper import, TransformAndLoadReport report)
        {
            if ((cimCNode != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCNode, rd);

                if (cimCNode.DescriptionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CNODE_DESC, cimCNode.Description));
                }
                if (cimCNode.ConnectivityNodeContainerHasValue)
                {
                    long gid = import.GetMappedGID(cimCNode.ConnectivityNodeContainer.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimCNode.GetType().ToString()).Append(" rdfID = \"").Append(cimCNode.ID);
                        report.Report.Append("\" - Failed to set reference to ConnectivityNodeContainer: rdfID \"").Append(cimCNode.ConnectivityNodeContainer.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.CNODE_CNC, gid));
                }

            }
        }



        public static void PopulateSeriesCompensatorProperties(SeriesCompensator series, ResourceDescription rd, ImportHelper import, TransformAndLoadReport report)
        {
            if ((series != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(series, rd, import, report);

                if (series.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_R, series.R));
                }
                if (series.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_R0, series.R0));
                }
                if (series.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_X0, series.X0));
                }
                if (series.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENSATOR_X, series.X));
                }
            }
        }

        public static void PopulateConductingEquipmentProperties(ConductingEquipment conductingEquipment, ResourceDescription rd, ImportHelper import, TransformAndLoadReport report)
        {
            if ((conductingEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(conductingEquipment, rd, import, report);
            }
        }

        public static void PopulateACLineSegmentProperties(ACLineSegment acline, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((acline != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductorProperties(acline, rd, importHelper, report);
                if (acline.B0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_B0CH, acline.B0ch));

                }
                if (acline.BchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_BCH, acline.Bch));
                }
                if (acline.GchHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_GCH, acline.Gch));
                }
                if (acline.G0chHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_G0CH, acline.G0ch));
                }

                if (acline.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_X, acline.X));
                }
                if (acline.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_X0, acline.X0));
                }
                if (acline.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_R, acline.R));
                }
                if (acline.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ACLINESEGMENT_R0, acline.R0));
                }

            }
        }

        public static void PopulateConductorProperties(Conductor conductor, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((conductor != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(conductor, rd, importHelper, report);

                if (conductor.LengthHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CONDUCTOR_LENGTH, conductor.Length));
                }
            }
        }

        public static void PopulateDCLineSegmentProperties(DCLineSegment dcline, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((dcline != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductorProperties(dcline, rd, importHelper, report);
                if(dcline.DcSegmentInductanceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DCLINESEGMENT_INDUTANCE, dcline.DcSegmentInductance));
                }
                if (dcline.DcSegmentResistanceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DCLINESEGMENT_RESISTANCE, dcline.DcSegmentResistance));
                }
            }
        }


        public static void PopulateBaysProperties(Bay bay, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((bay != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentContainerProperties(bay, rd, importHelper, report);
                if (bay.BayEnergyMeasFlagHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BAY_ENERGYFLAG, bay.BayEnergyMeasFlag));
                }
                if (bay.BayPowerMeasFlagHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BAY_POWERFLAG, bay.BayPowerMeasFlag));
                }
            }
        }

        public static void PopulateEquipmentContainerProperties(EquipmentContainer equipContainer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((equipContainer != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConnectivityNodeContainerProperties(equipContainer, rd, importHelper, report);
            }
        }

        public static void PopulateConnectivityNodeContainerProperties(ConnectivityNodeContainer cnc, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {

            if ((cnc != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cnc, rd, importHelper, report);
            }
        }
      /*  public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
        {
            switch (phases)
            {
                case FTN.PhaseCode.A:
                    return PhaseCode.A;
                case FTN.PhaseCode.AB:
                    return PhaseCode.AB;
                case FTN.PhaseCode.ABC:
                    return PhaseCode.ABC;
                case FTN.PhaseCode.ABCN:
                    return PhaseCode.ABCN;
                case FTN.PhaseCode.ABN:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.AC:
                    return PhaseCode.AC;
                case FTN.PhaseCode.ACN:
                    return PhaseCode.ACN;
                case FTN.PhaseCode.AN:
                    return PhaseCode.AN;
                case FTN.PhaseCode.B:
                    return PhaseCode.B;
                case FTN.PhaseCode.BC:
                    return PhaseCode.BC;
                case FTN.PhaseCode.BCN:
                    return PhaseCode.BCN;
                case FTN.PhaseCode.BN:
                    return PhaseCode.BN;
                case FTN.PhaseCode.C:
                    return PhaseCode.C;
                case FTN.PhaseCode.CN:
                    return PhaseCode.CN;
                case FTN.PhaseCode.N:
                    return PhaseCode.N;
                case FTN.PhaseCode.s12N:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.s1N:
                    return PhaseCode.AN;
                case FTN.PhaseCode.s2N:
                    return PhaseCode.BN;
                default: return PhaseCode.Unknown;
            }
        }*/
    }
}
