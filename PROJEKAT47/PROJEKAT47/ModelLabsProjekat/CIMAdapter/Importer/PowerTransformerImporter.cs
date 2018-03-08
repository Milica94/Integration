using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    /// <summary>
    /// PowerTransformerImporter
    /// </summary>
    public class PowerTransformerImporter
    {
        /// <summary> Singleton </summary>
        private static PowerTransformerImporter ptImporter = null;
        private static object singletoneLock = new object();

        private ConcreteModel concreteModel;
        private Delta delta;
        private ImportHelper importHelper;
        private TransformAndLoadReport report;


        #region Properties
        public static PowerTransformerImporter Instance
        {
            get
            {
                if (ptImporter == null)
                {
                    lock (singletoneLock)
                    {
                        if (ptImporter == null)
                        {
                            ptImporter = new PowerTransformerImporter();
                            ptImporter.Reset();
                        }
                    }
                }
                return ptImporter;
            }
        }

        public Delta NMSDelta
        {
            get
            {
                return delta;
            }
        }
        #endregion Properties


        public void Reset()
        {
            concreteModel = null;
            delta = new Delta();
            importHelper = new ImportHelper();
            report = null;
        }

        public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
        {
            LogManager.Log("Importing Profile47 Elements...", LogLevel.Info);
            report = new TransformAndLoadReport();
            concreteModel = cimConcreteModel;
            delta.ClearDeltaOperations();

            if ((concreteModel != null) && (concreteModel.ModelMap != null))
            {
                try
                {
                    // convert into DMS elements
                    ConvertModelAndPopulateDelta();
                }
                catch (Exception ex)
                {
                    string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
                    LogManager.Log(message);
                    report.Report.AppendLine(ex.Message);
                    report.Success = false;
                }
            }
            LogManager.Log("Importing Profile47 Elements - END.", LogLevel.Info);
            return report;
        }

        /// <summary>
        /// Method performs conversion of network elements from CIM based concrete model into DMS model.
        /// </summary>
        private void ConvertModelAndPopulateDelta()
        {
            LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)//DA LI MORA REDOM?-mora
            ImportBay();
            ImportSeriesCompensator();
            ImportACLineSegment();
            ImportDCLineSegment();
            ImportConnectivityNode();
            ImportTerminal();
           

            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
        }

        private void ImportBay()
        {
            SortedDictionary<string, object> bays = concreteModel.GetAllObjectsOfType("FTN.Bay");
            if (bays != null)
            {
                foreach (KeyValuePair<string, object> baysPair in bays)
                {
                    FTN.Bay bay = baysPair.Value as FTN.Bay;

                    ResourceDescription rd = CreateBaysResourceDescription(bay);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Bay ID = ").Append(bay.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Bay ID = ").Append(bay.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateBaysResourceDescription(Bay bay)
        {
            ResourceDescription rd = null;
            if (bay != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BAY, importHelper.CheckOutIndexForDMSType(DMSType.BAY));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(bay.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateBaysProperties(bay, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportDCLineSegment()
        {
            SortedDictionary<string, object> dclines = concreteModel.GetAllObjectsOfType("FTN.DCLineSegment");
            if (dclines != null)
            {
                foreach (KeyValuePair<string, object> dclinesPair in dclines)
                {
                    FTN.DCLineSegment dcline = dclinesPair.Value as FTN.DCLineSegment;

                    ResourceDescription rd = CreateDCLineSegmentResourceDescription(dcline);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("DCLineSegment ID = ").Append(dcline.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("DCLineSegment ID = ").Append(dcline.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateDCLineSegmentResourceDescription(DCLineSegment dcline)
        {
            ResourceDescription rd = null;
            if (dcline != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DCLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.DCLINESEGMENT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(dcline.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateDCLineSegmentProperties(dcline, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportACLineSegment()
        {
            SortedDictionary<string, object> aclines = concreteModel.GetAllObjectsOfType("FTN.ACLineSegment");
            if (aclines != null)
            {
                foreach (KeyValuePair<string, object> aclinesPair in aclines)
                {
                    FTN.ACLineSegment acline = aclinesPair.Value as FTN.ACLineSegment;

                    ResourceDescription rd = CreateACLineSegmentResourceDescription(acline);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ACLineSegment ID = ").Append(acline.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ACLineSegment ID = ").Append(acline.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateACLineSegmentResourceDescription(ACLineSegment acline)
        {
            ResourceDescription rd = null;
            if (acline != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ACLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.ACLINESEGMENT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(acline.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateACLineSegmentProperties(acline, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportSeriesCompensator()
        {
            SortedDictionary<string, object> seriess = concreteModel.GetAllObjectsOfType("FTN.SeriesCompensator");
            if (seriess != null)
            {
                foreach (KeyValuePair<string, object> seriessPair in seriess)
                {
                    FTN.SeriesCompensator series = seriessPair.Value as FTN.SeriesCompensator;

                    ResourceDescription rd = CreateSeriesCompensatorResourceDescription(series);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SeriesCompensatorID = ").Append(series.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SeriesCompensatorID = ").Append(series.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateSeriesCompensatorResourceDescription(SeriesCompensator series)
        {
            ResourceDescription rd = null;
            if (series != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SERIESCOMPENSATOR, importHelper.CheckOutIndexForDMSType(DMSType.SERIESCOMPENSATOR));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(series.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateSeriesCompensatorProperties(series, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportConnectivityNode()
        {
            SortedDictionary<string, object> cimCNodes = concreteModel.GetAllObjectsOfType("FTN.ConnectivityNode");
            if (cimCNodes != null)
            {
                foreach (KeyValuePair<string, object> cimCNodesPair in cimCNodes)
                {
                    FTN.ConnectivityNode cimCNode = cimCNodesPair.Value as FTN.ConnectivityNode;

                    ResourceDescription rd = CreateNodeResourceDescription(cimCNode);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ConnectivityNode ID = ").Append(cimCNode.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ConnectivityNodeID = ").Append(cimCNode.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }

        }

        private ResourceDescription CreateNodeResourceDescription(ConnectivityNode cimCNode)
        {
            ResourceDescription rd = null;
            if (cimCNode != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CNODE, importHelper.CheckOutIndexForDMSType(DMSType.CNODE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimCNode.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateNodeProperties(cimCNode, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportTerminal()
        {
            SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
            if (cimTerminals != null)
            {
                foreach (KeyValuePair<string, object> cimTerminalPair in cimTerminals)
                {
                    FTN.Terminal cimTerminal = cimTerminalPair.Value as FTN.Terminal;

                    ResourceDescription rd = CreateTerminalResourceDescription(cimTerminal);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateTerminalResourceDescription(Terminal cimTerminal)
        {
            ResourceDescription rd = null;
            if (cimTerminal != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTerminal.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);
            }
            return rd;
        }

    }
}

