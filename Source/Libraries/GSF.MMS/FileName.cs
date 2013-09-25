//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System.Collections.Generic;
using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;

namespace GSF.MMS
{
    [ASN1PreparedElement]
    [ASN1BoxedType(Name = "FileName")]
    public class FileName : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(FileName));
        private ICollection<string> val;

        [ASN1String(Name = "", StringType = UniversalTags.GraphicString, IsUCS = false)]
        [ASN1SequenceOf(Name = "FileName", IsSetOf = false)]
        //[ASN1Element(Name = "FileName", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false, IsImplicitTag = true)]
        public ICollection<string> Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }

        public void initWithDefaults()
        {
        }


        public IASN1PreparedElementData PreparedData
        {
            get
            {
                return preparedData;
            }
        }

        public void initValue()
        {
            Value = new List<string>();
        }

        public void Add(string item)
        {
            Value.Add(item);
        }
    }
}