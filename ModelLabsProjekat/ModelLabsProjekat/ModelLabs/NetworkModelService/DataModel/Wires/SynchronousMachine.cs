using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SynchronousMachine : RotatingMachine
    {
        private long reactiveCapabilityCurve = 0;

        public long ReactiveCapabilityCurve { get => reactiveCapabilityCurve; set => reactiveCapabilityCurve = value; }

        public SynchronousMachine(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SynchronousMachine x = (SynchronousMachine)obj;
                return (x.reactiveCapabilityCurve == this.reactiveCapabilityCurve);
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
                case ModelCode.SYNCHRONOUSMACHINE_REACTCAPCURVE:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTCAPCURVE:
                    prop.SetValue(reactiveCapabilityCurve);
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
                case ModelCode.SYNCHRONOUSMACHINE_REACTCAPCURVE:
                    reactiveCapabilityCurve = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess implementation 

        #region IReference implementation
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (reactiveCapabilityCurve != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SYNCHRONOUSMACHINE_REACTCAPCURVE] = new List<long>();
                references[ModelCode.SYNCHRONOUSMACHINE_REACTCAPCURVE].Add(reactiveCapabilityCurve);
            }
            base.GetReferences(references, refType);
        }


        #endregion IReference implementation
    }
}
