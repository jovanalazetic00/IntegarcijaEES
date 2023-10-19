using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Equipment : PowerSystemResource
    {
        private bool aggregate;
        private bool normallyService;

        public Equipment(long globalId) : base(globalId)
        {
        }

        public bool Aggregate { get => aggregate; set => aggregate = value; }
        public bool NormallyService { get => normallyService; set => normallyService = value; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Equipment x = (Equipment)obj;
                return (x.aggregate == this.aggregate &&
                       x.normallyService == this.normallyService);
            }
            else
            {
                return false;
            }
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.EQUIPMENT_AGGREGATE:
                case ModelCode.EQUIPMENT_NORMALLYSERVICE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.EQUIPMENT_AGGREGATE:
                    property.SetValue(aggregate);
                    break;
                case ModelCode.EQUIPMENT_NORMALLYSERVICE:
                    property.SetValue(normallyService);
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
                case ModelCode.EQUIPMENT_AGGREGATE:
                    aggregate = property.AsBool();
                    break;
                case ModelCode.EQUIPMENT_NORMALLYSERVICE:
                    normallyService = property.AsBool();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
