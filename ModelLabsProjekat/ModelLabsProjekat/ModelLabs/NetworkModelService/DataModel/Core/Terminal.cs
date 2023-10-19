using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private List<long> regulatingControls = new List<long>();

        public List<long> RegulatingControls { get => regulatingControls; set => regulatingControls = value; }

        public Terminal(long globalId) : base(globalId)
        {
        }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return (CompareHelper.CompareLists(x.regulatingControls, this.regulatingControls));
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
                case ModelCode.TERMINAL_REGULATINGCTRLS:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TERMINAL_REGULATINGCTRLS:
                    prop.SetValue(regulatingControls);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }
        #endregion IAccess implementation

        #region IReferences implemntation
        public override bool IsReferenced
        {
            get
            {
                return regulatingControls.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (regulatingControls != null && regulatingControls.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_REGULATINGCTRLS] = regulatingControls.GetRange(0, regulatingControls.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATINGCONTROL_TERMINAL:
                    regulatingControls.Add(globalId);
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
                case ModelCode.REGULATINGCONTROL_TERMINAL:
                    if (regulatingControls.Contains(globalId))
                    {
                        regulatingControls.Remove(globalId);
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


        #endregion IReferences implementation
    }
}