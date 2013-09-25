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
    [ASN1Sequence(Name = "Read_Request", IsSet = false)]
    public class Read_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Read_Request));
        private bool specificationWithResult_;


        private VariableAccessSpecification variableAccessSpecification_;

        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "specificationWithResult", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = true)]
        public bool SpecificationWithResult
        {
            get
            {
                return specificationWithResult_;
            }
            set
            {
                specificationWithResult_ = value;
            }
        }

        [ASN1Element(Name = "variableAccessSpecification", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public VariableAccessSpecification VariableAccessSpecification
        {
            get
            {
                return variableAccessSpecification_;
            }
            set
            {
                variableAccessSpecification_ = value;
            }
        }


        public void initWithDefaults()
        {
            bool param_SpecificationWithResult =
                false;
            SpecificationWithResult = param_SpecificationWithResult;
        }


        public IASN1PreparedElementData PreparedData
        {
            get
            {
                return preparedData;
            }
        }
    }
}