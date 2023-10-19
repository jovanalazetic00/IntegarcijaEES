using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Meas
{
    public class Control : IdentifiedObject
    {
        private long regulatingCondEq = 0;

        public long RegulatingCondEq { get => regulatingCondEq; set => regulatingCondEq = value; }

        public Control(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Control x = (Control)obj;
                return (x.regulatingCondEq == this.regulatingCondEq);
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
                case ModelCode.CONTROL_REGCONDEQUIPMENT:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CONTROL_REGCONDEQUIPMENT:
                    prop.SetValue(regulatingCondEq);
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
                case ModelCode.CONTROL_REGCONDEQUIPMENT:
                    regulatingCondEq = property.AsReference();
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
            if (regulatingCondEq != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONTROL_REGCONDEQUIPMENT] = new List<long>();
                references[ModelCode.CONTROL_REGCONDEQUIPMENT].Add(regulatingCondEq);
            }
            base.GetReferences(references, refType);
        }


        #endregion IReference implementation
    }
}
