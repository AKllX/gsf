//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;

namespace GSF.MMS
{
    [ASN1PreparedElement]
    [ASN1Choice(Name = "TypeSpecification")]
    public class TypeSpecification : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(TypeSpecification));
        private TypeDescription typeDescription_;
        private bool typeDescription_selected;
        private ObjectName typeName_;
        private bool typeName_selected;


        [ASN1Element(Name = "typeName", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ObjectName TypeName
        {
            get
            {
                return typeName_;
            }
            set
            {
                selectTypeName(value);
            }
        }


        [ASN1Element(Name = "typeDescription", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public TypeDescription TypeDescription
        {
            get
            {
                return typeDescription_;
            }
            set
            {
                selectTypeDescription(value);
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


        public bool isTypeNameSelected()
        {
            return typeName_selected;
        }


        public void selectTypeName(ObjectName val)
        {
            typeName_ = val;
            typeName_selected = true;


            typeDescription_selected = false;
        }


        public bool isTypeDescriptionSelected()
        {
            return typeDescription_selected;
        }


        public void selectTypeDescription(TypeDescription val)
        {
            typeDescription_ = val;
            typeDescription_selected = true;


            typeName_selected = false;
        }
    }
}