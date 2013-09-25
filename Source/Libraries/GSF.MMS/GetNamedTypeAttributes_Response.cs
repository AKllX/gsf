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
    [ASN1Sequence(Name = "GetNamedTypeAttributes_Response", IsSet = false)]
    public class GetNamedTypeAttributes_Response : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(GetNamedTypeAttributes_Response));
        private Identifier accessControlList_;

        private bool accessControlList_present;


        private string meaning_;

        private bool meaning_present;
        private bool mmsDeletable_;
        private TypeSpecification typeSpecification_;

        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "mmsDeletable", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public bool MmsDeletable
        {
            get
            {
                return mmsDeletable_;
            }
            set
            {
                mmsDeletable_ = value;
            }
        }

        [ASN1Element(Name = "typeSpecification", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public TypeSpecification TypeSpecification
        {
            get
            {
                return typeSpecification_;
            }
            set
            {
                typeSpecification_ = value;
            }
        }

        [ASN1Element(Name = "accessControlList", IsOptional = true, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public Identifier AccessControlList
        {
            get
            {
                return accessControlList_;
            }
            set
            {
                accessControlList_ = value;
                accessControlList_present = true;
            }
        }

        [ASN1String(Name = "",
            StringType = UniversalTags.VisibleString, IsUCS = false)]
        [ASN1Element(Name = "meaning", IsOptional = true, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public string Meaning
        {
            get
            {
                return meaning_;
            }
            set
            {
                meaning_ = value;
                meaning_present = true;
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

        public bool isAccessControlListPresent()
        {
            return accessControlList_present;
        }

        public bool isMeaningPresent()
        {
            return meaning_present;
        }
    }
}