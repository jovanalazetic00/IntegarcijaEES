using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModelKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private long terminal = 0;
        private List<long> regulatinCondEqs = new List<long>();

        public RegulatingControl(long globalId) : base(globalId)
        {
        }

        public bool Discrete { get => discrete; set => discrete = value; }
        public RegulatingControlModelKind Mode { get => mode; set => mode = value; }
        public PhaseCode MonitoredPhase { get => monitoredPhase; set => monitoredPhase = value; }
        public float TargetRange { get => targetRange; set => targetRange = value; }
        public float TargetValue { get => targetValue; set => targetValue = value; }
        public long Terminal { get => terminal; set => terminal = value; }
        public List<long> RegulatinCondEqs { get => regulatinCondEqs; set => regulatinCondEqs = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl x = (RegulatingControl)obj;
                return (x.Discrete == this.Discrete &&
                        x.mode == this.mode &&
                        x.MonitoredPhase == this.MonitoredPhase &&
                        x.targetRange == this.targetRange &&
                        x.TargetValue == this.TargetValue &&
                        x.terminal == this.terminal &&
                        CompareHelper.CompareLists(x.regulatinCondEqs, this.regulatinCondEqs));
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
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                case ModelCode.REGULATINGCONTROL_MODE:
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                case ModelCode.REGULATINGCONTROL_TERMINAL:
                case ModelCode.REGULATINGCONTROL_REGCONDEQS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    prop.SetValue(discrete);
                    break;
                case ModelCode.REGULATINGCONTROL_MODE:
                    prop.SetValue((short)mode);
                    break;
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                    prop.SetValue((short)monitoredPhase);
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                    prop.SetValue(targetRange);
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                    prop.SetValue(targetValue);
                    break;
                case ModelCode.REGULATINGCONTROL_TERMINAL:
                    prop.SetValue(terminal);
                    break;
                case ModelCode.REGULATINGCONTROL_REGCONDEQS:
                    prop.SetValue(regulatinCondEqs);
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
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    discrete = property.AsBool();
                    break;
                case ModelCode.REGULATINGCONTROL_MODE:
                    mode = (RegulatingControlModelKind)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                    targetRange = property.AsFloat();
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                    targetValue = property.AsFloat();
                    break;
                case ModelCode.REGULATINGCONTROL_TERMINAL:
                    terminal = property.AsReference();
                    break;
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
                return regulatinCondEqs.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (terminal != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULATINGCONTROL_TERMINAL] = new List<long>();
                references[ModelCode.REGULATINGCONTROL_TERMINAL].Add(terminal);
            }

            if (regulatinCondEqs != null && regulatinCondEqs.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULATINGCONTROL_REGCONDEQS] = regulatinCondEqs.GetRange(0, regulatinCondEqs.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATINGCONDEQ_REGULATINGCTRL:
                    regulatinCondEqs.Add(globalId);
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
                case ModelCode.REGULATINGCONDEQ_REGULATINGCTRL:

                    if (regulatinCondEqs.Contains(globalId))
                    {
                        regulatinCondEqs.Remove(globalId);
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
