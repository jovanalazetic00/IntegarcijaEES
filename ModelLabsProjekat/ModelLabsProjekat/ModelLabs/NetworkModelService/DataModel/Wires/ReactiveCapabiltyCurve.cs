using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.IES_Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ReactiveCapabilityCurve : Curve
    {
        private List<long> synchronousMachines = new List<long>();
        public ReactiveCapabilityCurve(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ReactiveCapabilityCurve x = (ReactiveCapabilityCurve)obj;
                return (CompareHelper.CompareLists(x.synchronousMachines, this.synchronousMachines));
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
                case ModelCode.REACTIVECAPABILITYCURVE_SYNMACHINES:
                    return true;
                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REACTIVECAPABILITYCURVE_SYNMACHINES:
                    prop.SetValue(synchronousMachines);
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
        #endregion IAccess implemntation

        #region IReferences implementation

        public override bool IsReferenced
        {
            get
            {
                return synchronousMachines.Count != 0 || base.IsReferenced;
            }
        }
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (synchronousMachines != null && synchronousMachines.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REACTIVECAPABILITYCURVE_SYNMACHINES] = synchronousMachines.GetRange(0, synchronousMachines.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTCAPCURVE:
                    synchronousMachines.Add(globalId);
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
                case ModelCode.SYNCHRONOUSMACHINE_REACTCAPCURVE:

                    if (synchronousMachines.Contains(globalId))
                    {
                        synchronousMachines.Remove(globalId);
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
