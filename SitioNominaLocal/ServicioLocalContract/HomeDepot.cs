namespace ServicioLocalContract
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "requestForPayment")]
    public partial class HomeDepotRequestForPayment
    {

        private HomeDepotRequestForPaymentRequestForPaymentIdentification requestForPaymentIdentificationField;

        private HomeDepotRequestForPaymentSpecialInstruction[] specialInstructionField;

        private HomeDepotRequestForPaymentOrderIdentification orderIdentificationField;

        private HomeDepotRequestForPaymentReferenceIdentification[] additionalInformationField;

        private HomeDepotRequestForPaymentDeliveryNote deliveryNoteField;

        private HomeDepotRequestForPaymentBuyer buyerField;

        private HomeDepotRequestForPaymentSeller sellerField;

        private HomeDepotRequestForPaymentShipTo shipToField;

        private HomeDepotRequestForPaymentInvoiceCreator invoiceCreatorField;

        private HomeDepotRequestForPaymentCustoms[] customsField;

        private HomeDepotRequestForPaymentCurrency[] currencyField;

        private HomeDepotRequestForPaymentPaymentTerms paymentTermsField;

        private object shipmentDetailField;

        private HomeDepotRequestForPaymentAllowanceCharge[] allowanceChargeField;

        private HomeDepotRequestForPaymentLineItem[] lineItemField;

        private HomeDepotRequestForPaymentTotalAmount totalAmountField;

        private HomeDepotRequestForPaymentTotalAllowanceCharge[] totalAllowanceChargeField;

        private HomeDepotRequestForPaymentBaseAmount baseAmountField;

        private HomeDepotRequestForPaymentTax[] taxField;

        private HomeDepotRequestForPaymentPayableAmount payableAmountField;

        private string typeField = "SimpleInvoiceType";

        private string contentVersionField = "1.3.1";

        private string documentStructureVersionField;

        private HomeDepotRequestForPaymentDocumentStatus documentStatusField;

        private System.DateTime deliveryDateField;

        private bool deliveryDateFieldSpecified;

        public HomeDepotRequestForPayment()
        {
            this.typeField = "SimpleInvoiceType";
            this.contentVersionField = "1.3.1";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentRequestForPaymentIdentification requestForPaymentIdentification
        {
            get { return this.requestForPaymentIdentificationField; }
            set { this.requestForPaymentIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("specialInstruction",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentSpecialInstruction[] specialInstruction
        {
            get { return this.specialInstructionField; }
            set { this.specialInstructionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentOrderIdentification orderIdentification
        {
            get { return this.orderIdentificationField; }
            set { this.orderIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("referenceIdentification",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public HomeDepotRequestForPaymentReferenceIdentification[] AdditionalInformation
        {
            get { return this.additionalInformationField; }
            set { this.additionalInformationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentDeliveryNote DeliveryNote
        {
            get { return this.deliveryNoteField; }
            set { this.deliveryNoteField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentBuyer buyer
        {
            get { return this.buyerField; }
            set { this.buyerField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentSeller seller
        {
            get { return this.sellerField; }
            set { this.sellerField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentShipTo shipTo
        {
            get { return this.shipToField; }
            set { this.shipToField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentInvoiceCreator InvoiceCreator
        {
            get { return this.invoiceCreatorField; }
            set { this.invoiceCreatorField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Customs", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentCustoms[] Customs
        {
            get { return this.customsField; }
            set { this.customsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("currency", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentCurrency[] currency
        {
            get { return this.currencyField; }
            set { this.currencyField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentPaymentTerms paymentTerms
        {
            get { return this.paymentTermsField; }
            set { this.paymentTermsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object shipmentDetail
        {
            get { return this.shipmentDetailField; }
            set { this.shipmentDetailField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("allowanceCharge",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentAllowanceCharge[] allowanceCharge
        {
            get { return this.allowanceChargeField; }
            set { this.allowanceChargeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("lineItem", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItem[] lineItem
        {
            get { return this.lineItemField; }
            set { this.lineItemField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentTotalAmount totalAmount
        {
            get { return this.totalAmountField; }
            set { this.totalAmountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TotalAllowanceCharge",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentTotalAllowanceCharge[] TotalAllowanceCharge
        {
            get { return this.totalAllowanceChargeField; }
            set { this.totalAllowanceChargeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentBaseAmount baseAmount
        {
            get { return this.baseAmountField; }
            set { this.baseAmountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tax", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentTax[] tax
        {
            get { return this.taxField; }
            set { this.taxField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentPayableAmount payableAmount
        {
            get { return this.payableAmountField; }
            set { this.payableAmountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string contentVersion
        {
            get { return this.contentVersionField; }
            set { this.contentVersionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string documentStructureVersion
        {
            get { return this.documentStructureVersionField; }
            set { this.documentStructureVersionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentDocumentStatus documentStatus
        {
            get { return this.documentStatusField; }
            set { this.documentStatusField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime DeliveryDate
        {
            get { return this.deliveryDateField; }
            set { this.deliveryDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryDateSpecified
        {
            get { return this.deliveryDateFieldSpecified; }
            set { this.deliveryDateFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentRequestForPaymentIdentification
    {

        private HomeDepotRequestForPaymentRequestForPaymentIdentificationEntityType entityTypeField;

        private string uniqueCreatorIdentificationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentRequestForPaymentIdentificationEntityType entityType
        {
            get { return this.entityTypeField; }
            set { this.entityTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string uniqueCreatorIdentification
        {
            get { return this.uniqueCreatorIdentificationField; }
            set { this.uniqueCreatorIdentificationField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentRequestForPaymentIdentificationEntityType
    {

        /// <remarks/>
        INVOICE,

        /// <remarks/>
        DEBIT_NOTE,

        /// <remarks/>
        CREDIT_NOTE,

        /// <remarks/>
        LEASE_RECEIPT,

        /// <remarks/>
        HONORARY_RECEIPT,

        /// <remarks/>
        PARTIAL_INVOICE,

        /// <remarks/>
        TRANSPORT_DOCUMENT,

        /// <remarks/>
        AUTO_INVOICE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentSpecialInstruction
    {

        private string[] textField;

        private string[] textField1;

        private HomeDepotRequestForPaymentSpecialInstructionCode codeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("text", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get { return this.textField1; }
            set { this.textField1 = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentSpecialInstructionCode code
        {
            get { return this.codeField; }
            set { this.codeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentSpecialInstructionCode
    {

        /// <remarks/>
        AAB,

        /// <remarks/>
        DUT,

        /// <remarks/>
        PUR,

        /// <remarks/>
        ZZZ,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentOrderIdentification
    {

        private HomeDepotRequestForPaymentOrderIdentificationReferenceIdentification[] referenceIdentificationField;

        private System.DateTime referenceDateField;

        private bool referenceDateFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("referenceIdentification",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentOrderIdentificationReferenceIdentification[] referenceIdentification
        {
            get { return this.referenceIdentificationField; }
            set { this.referenceIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
            DataType = "date")]
        public System.DateTime ReferenceDate
        {
            get { return this.referenceDateField; }
            set { this.referenceDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReferenceDateSpecified
        {
            get { return this.referenceDateFieldSpecified; }
            set { this.referenceDateFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentOrderIdentificationReferenceIdentification
    {

        private HomeDepotRequestForPaymentOrderIdentificationReferenceIdentificationType typeField;

        private string valueField;

        public HomeDepotRequestForPaymentOrderIdentificationReferenceIdentification()
        {
            this.typeField = HomeDepotRequestForPaymentOrderIdentificationReferenceIdentificationType.ON;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentOrderIdentificationReferenceIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentOrderIdentificationReferenceIdentificationType
    {

        /// <remarks/>
        ON,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentReferenceIdentification
    {

        private HomeDepotRequestForPaymentReferenceIdentificationType typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentReferenceIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentReferenceIdentificationType
    {

        /// <remarks/>
        AAE,

        /// <remarks/>
        CK,

        /// <remarks/>
        ACE,

        /// <remarks/>
        ATZ,

        /// <remarks/>
        DQ,

        /// <remarks/>
        IV,

        /// <remarks/>
        ON,

        /// <remarks/>
        AWR,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentDeliveryNote
    {

        private string[] referenceIdentificationField;

        private System.DateTime referenceDateField;

        private bool referenceDateFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("referenceIdentification",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] referenceIdentification
        {
            get { return this.referenceIdentificationField; }
            set { this.referenceIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
            DataType = "date")]
        public System.DateTime ReferenceDate
        {
            get { return this.referenceDateField; }
            set { this.referenceDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReferenceDateSpecified
        {
            get { return this.referenceDateFieldSpecified; }
            set { this.referenceDateFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentBuyer
    {

        private string glnField;

        private HomeDepotRequestForPaymentBuyerContactInformation contactInformationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string gln
        {
            get { return this.glnField; }
            set { this.glnField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentBuyerContactInformation contactInformation
        {
            get { return this.contactInformationField; }
            set { this.contactInformationField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentBuyerContactInformation
    {

        private HomeDepotRequestForPaymentBuyerContactInformationPersonOrDepartmentName personOrDepartmentNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentBuyerContactInformationPersonOrDepartmentName personOrDepartmentName
        {
            get { return this.personOrDepartmentNameField; }
            set { this.personOrDepartmentNameField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentBuyerContactInformationPersonOrDepartmentName
    {

        private string textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentSeller
    {

        private string glnField;

        private HomeDepotRequestForPaymentSellerAlternatePartyIdentification alternatePartyIdentificationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string gln
        {
            get { return this.glnField; }
            set { this.glnField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentSellerAlternatePartyIdentification alternatePartyIdentification
        {
            get { return this.alternatePartyIdentificationField; }
            set { this.alternatePartyIdentificationField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentSellerAlternatePartyIdentification
    {

        private HomeDepotRequestForPaymentSellerAlternatePartyIdentificationType typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentSellerAlternatePartyIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentSellerAlternatePartyIdentificationType
    {

        /// <remarks/>
        SELLER_ASSIGNED_IDENTIFIER_FOR_A_PARTY,

        /// <remarks/>
        IEPS_REFERENCE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentShipTo
    {

        private string glnField;

        private HomeDepotRequestForPaymentShipToNameAndAddress nameAndAddressField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string gln
        {
            get { return this.glnField; }
            set { this.glnField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentShipToNameAndAddress nameAndAddress
        {
            get { return this.nameAndAddressField; }
            set { this.nameAndAddressField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentShipToNameAndAddress
    {

        private string[] nameField;

        private string[] streetAddressOneField;

        private string[] cityField;

        private string[] postalCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("name", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("streetAddressOne",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] streetAddressOne
        {
            get { return this.streetAddressOneField; }
            set { this.streetAddressOneField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("city", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] city
        {
            get { return this.cityField; }
            set { this.cityField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("postalCode", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] postalCode
        {
            get { return this.postalCodeField; }
            set { this.postalCodeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentInvoiceCreator
    {

        private string glnField;

        private HomeDepotRequestForPaymentInvoiceCreatorAlternatePartyIdentification alternatePartyIdentificationField;

        private HomeDepotRequestForPaymentInvoiceCreatorNameAndAddress nameAndAddressField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string gln
        {
            get { return this.glnField; }
            set { this.glnField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentInvoiceCreatorAlternatePartyIdentification alternatePartyIdentification
        {
            get { return this.alternatePartyIdentificationField; }
            set { this.alternatePartyIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentInvoiceCreatorNameAndAddress nameAndAddress
        {
            get { return this.nameAndAddressField; }
            set { this.nameAndAddressField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentInvoiceCreatorAlternatePartyIdentification
    {

        private HomeDepotRequestForPaymentInvoiceCreatorAlternatePartyIdentificationType typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentInvoiceCreatorAlternatePartyIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentInvoiceCreatorAlternatePartyIdentificationType
    {

        /// <remarks/>
        VA,

        /// <remarks/>
        IA,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentInvoiceCreatorNameAndAddress
    {

        private string nameField;

        private string streetAddressOneField;

        private string cityField;

        private string postalCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string streetAddressOne
        {
            get { return this.streetAddressOneField; }
            set { this.streetAddressOneField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string city
        {
            get { return this.cityField; }
            set { this.cityField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string postalCode
        {
            get { return this.postalCodeField; }
            set { this.postalCodeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentCustoms
    {

        private string glnField;

        private HomeDepotRequestForPaymentCustomsAlternatePartyIdentification alternatePartyIdentificationField;

        private System.DateTime referenceDateField;

        private HomeDepotRequestForPaymentCustomsNameAndAddress nameAndAddressField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string gln
        {
            get { return this.glnField; }
            set { this.glnField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentCustomsAlternatePartyIdentification alternatePartyIdentification
        {
            get { return this.alternatePartyIdentificationField; }
            set { this.alternatePartyIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
            DataType = "date")]
        public System.DateTime ReferenceDate
        {
            get { return this.referenceDateField; }
            set { this.referenceDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentCustomsNameAndAddress nameAndAddress
        {
            get { return this.nameAndAddressField; }
            set { this.nameAndAddressField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentCustomsAlternatePartyIdentification
    {

        private HomeDepotRequestForPaymentCustomsAlternatePartyIdentificationType typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentCustomsAlternatePartyIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentCustomsAlternatePartyIdentificationType
    {

        /// <remarks/>
        TN,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentCustomsNameAndAddress
    {

        private string nameField;

        private string cityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string city
        {
            get { return this.cityField; }
            set { this.cityField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentCurrency
    {

        private HomeDepotRequestForPaymentCurrencyCurrencyFunction[] currencyFunctionField;

        private decimal rateOfChangeField;

        private bool rateOfChangeFieldSpecified;

        private HomeDepotRequestForPaymentCurrencyCurrencyISOCode currencyISOCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("currencyFunction",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentCurrencyCurrencyFunction[] currencyFunction
        {
            get { return this.currencyFunctionField; }
            set { this.currencyFunctionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal rateOfChange
        {
            get { return this.rateOfChangeField; }
            set { this.rateOfChangeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool rateOfChangeSpecified
        {
            get { return this.rateOfChangeFieldSpecified; }
            set { this.rateOfChangeFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentCurrencyCurrencyISOCode currencyISOCode
        {
            get { return this.currencyISOCodeField; }
            set { this.currencyISOCodeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentCurrencyCurrencyFunction
    {

        /// <remarks/>
        BILLING_CURRENCY,

        /// <remarks/>
        PRICE_CURRENCY,

        /// <remarks/>
        PAYMENT_CURRENCY,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentCurrencyCurrencyISOCode
    {

        /// <remarks/>
        MXN,

        /// <remarks/>
        XEU,

        /// <remarks/>
        USD,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentPaymentTerms
    {

        private HomeDepotRequestForPaymentPaymentTermsNetPayment netPaymentField;

        private HomeDepotRequestForPaymentPaymentTermsDiscountPayment discountPaymentField;

        private HomeDepotRequestForPaymentPaymentTermsPaymentTermsEvent paymentTermsEventField;

        private bool paymentTermsEventFieldSpecified;

        private HomeDepotRequestForPaymentPaymentTermsPaymentTermsRelationTime paymentTermsRelationTimeField;

        private bool paymentTermsRelationTimeFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentPaymentTermsNetPayment netPayment
        {
            get { return this.netPaymentField; }
            set { this.netPaymentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentPaymentTermsDiscountPayment discountPayment
        {
            get { return this.discountPaymentField; }
            set { this.discountPaymentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentPaymentTermsPaymentTermsEvent paymentTermsEvent
        {
            get { return this.paymentTermsEventField; }
            set { this.paymentTermsEventField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool paymentTermsEventSpecified
        {
            get { return this.paymentTermsEventFieldSpecified; }
            set { this.paymentTermsEventFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentPaymentTermsPaymentTermsRelationTime PaymentTermsRelationTime
        {
            get { return this.paymentTermsRelationTimeField; }
            set { this.paymentTermsRelationTimeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PaymentTermsRelationTimeSpecified
        {
            get { return this.paymentTermsRelationTimeFieldSpecified; }
            set { this.paymentTermsRelationTimeFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentPaymentTermsNetPayment
    {

        private HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriod paymentTimePeriodField;

        private HomeDepotRequestForPaymentPaymentTermsNetPaymentNetPaymentTermsType netPaymentTermsTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriod paymentTimePeriod
        {
            get { return this.paymentTimePeriodField; }
            set { this.paymentTimePeriodField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentPaymentTermsNetPaymentNetPaymentTermsType netPaymentTermsType
        {
            get { return this.netPaymentTermsTypeField; }
            set { this.netPaymentTermsTypeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriod
    {

        private HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriodTimePeriodDue timePeriodDueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriodTimePeriodDue timePeriodDue
        {
            get { return this.timePeriodDueField; }
            set { this.timePeriodDueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriodTimePeriodDue
    {

        private string valueField;

        private HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriodTimePeriodDueTimePeriod timePeriodField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriodTimePeriodDueTimePeriod timePeriod
        {
            get { return this.timePeriodField; }
            set { this.timePeriodField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentPaymentTermsNetPaymentPaymentTimePeriodTimePeriodDueTimePeriod
    {

        /// <remarks/>
        DAYS,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentPaymentTermsNetPaymentNetPaymentTermsType
    {

        /// <remarks/>
        BASIC_NET,

        /// <remarks/>
        END_OF_MONTH,

        /// <remarks/>
        BASIC_DISCOUNT_OFFERED,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentPaymentTermsDiscountPayment
    {

        private string percentageField;

        private HomeDepotRequestForPaymentPaymentTermsDiscountPaymentDiscountType discountTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string percentage
        {
            get { return this.percentageField; }
            set { this.percentageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentPaymentTermsDiscountPaymentDiscountType discountType
        {
            get { return this.discountTypeField; }
            set { this.discountTypeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentPaymentTermsDiscountPaymentDiscountType
    {

        /// <remarks/>
        ALLOWANCE_BY_PAYMENT_ON_TIME,

        /// <remarks/>
        SANCTION,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentPaymentTermsPaymentTermsEvent
    {

        /// <remarks/>
        DATE_OF_INVOICE,

        /// <remarks/>
        EFFECTIVE_DATE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentPaymentTermsPaymentTermsRelationTime
    {

        /// <remarks/>
        REFERENCE_AFTER,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentAllowanceCharge
    {

        private HomeDepotRequestForPaymentAllowanceChargeSpecialServicesType specialServicesTypeField;

        private bool specialServicesTypeFieldSpecified;

        private HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentage monetaryAmountOrPercentageField;

        private HomeDepotRequestForPaymentAllowanceChargeAllowanceChargeType allowanceChargeTypeField;

        private HomeDepotRequestForPaymentAllowanceChargeSettlementType settlementTypeField;

        private string sequenceNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentAllowanceChargeSpecialServicesType specialServicesType
        {
            get { return this.specialServicesTypeField; }
            set { this.specialServicesTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool specialServicesTypeSpecified
        {
            get { return this.specialServicesTypeFieldSpecified; }
            set { this.specialServicesTypeFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentage monetaryAmountOrPercentage
        {
            get { return this.monetaryAmountOrPercentageField; }
            set { this.monetaryAmountOrPercentageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentAllowanceChargeAllowanceChargeType allowanceChargeType
        {
            get { return this.allowanceChargeTypeField; }
            set { this.allowanceChargeTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentAllowanceChargeSettlementType settlementType
        {
            get { return this.settlementTypeField; }
            set { this.settlementTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sequenceNumber
        {
            get { return this.sequenceNumberField; }
            set { this.sequenceNumberField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentAllowanceChargeSpecialServicesType
    {

        /// <remarks/>
        AA,

        /// <remarks/>
        AJ,

        /// <remarks/>
        ADO,

        /// <remarks/>
        ADT,

        /// <remarks/>
        ADS,

        /// <remarks/>
        ABZ,

        /// <remarks/>
        DA,

        /// <remarks/>
        EAA,

        /// <remarks/>
        EAB,

        /// <remarks/>
        PI,

        /// <remarks/>
        TAE,

        /// <remarks/>
        SAB,

        /// <remarks/>
        RAA,

        /// <remarks/>
        PAD,

        /// <remarks/>
        FG,

        /// <remarks/>
        FA,

        /// <remarks/>
        TD,

        /// <remarks/>
        TS,

        /// <remarks/>
        TX,

        /// <remarks/>
        TZ,

        /// <remarks/>
        ZZZ,

        /// <remarks/>
        VAB,

        /// <remarks/>
        UM,

        /// <remarks/>
        DI,

        /// <remarks/>
        CAC,

        /// <remarks/>
        COD,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("EAB")] EAB1,

        /// <remarks/>
        FC,

        /// <remarks/>
        FI,

        /// <remarks/>
        HD,

        /// <remarks/>
        QD,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentage
    {

        private HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentageRate rateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentageRate rate
        {
            get { return this.rateField; }
            set { this.rateField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentageRate
    {

        private decimal percentageField;

        private HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentageRateBase baseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal percentage
        {
            get { return this.percentageField; }
            set { this.percentageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentageRateBase @base
        {
            get { return this.baseField; }
            set { this.baseField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentAllowanceChargeMonetaryAmountOrPercentageRateBase
    {

        /// <remarks/>
        INVOICE_VALUE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentAllowanceChargeAllowanceChargeType
    {

        /// <remarks/>
        ALLOWANCE_GLOBAL,

        /// <remarks/>
        CHARGE_GLOBAL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentAllowanceChargeSettlementType
    {

        /// <remarks/>
        BILL_BACK,

        /// <remarks/>
        OFF_INVOICE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItem
    {

        private HomeDepotRequestForPaymentLineItemTradeItemIdentification tradeItemIdentificationField;

        private HomeDepotRequestForPaymentLineItemAlternateTradeItemIdentification[]
            alternateTradeItemIdentificationField;

        private HomeDepotRequestForPaymentLineItemTradeItemDescriptionInformation tradeItemDescriptionInformationField;

        private HomeDepotRequestForPaymentLineItemInvoicedQuantity invoicedQuantityField;

        private HomeDepotRequestForPaymentLineItemAditionalQuantity[] aditionalQuantityField;

        private HomeDepotRequestForPaymentLineItemGrossPrice grossPriceField;

        private HomeDepotRequestForPaymentLineItemNetPrice netPriceField;

        private HomeDepotRequestForPaymentLineItemAdditionalInformation additionalInformationField;

        private HomeDepotRequestForPaymentLineItemCustoms[] customsField;

        private HomeDepotRequestForPaymentLineItemLogisticUnits logisticUnitsField;

        private HomeDepotRequestForPaymentLineItemPalletInformation palletInformationField;

        private HomeDepotRequestForPaymentLineItemLotNumber[] extendedAttributesField;

        private HomeDepotRequestForPaymentLineItemAllowanceCharge[] allowanceChargeField;

        private HomeDepotRequestForPaymentLineItemTradeItemTaxInformation[] tradeItemTaxInformationField;

        private HomeDepotRequestForPaymentLineItemTotalLineAmount totalLineAmountField;

        private string typeField;

        private string numberField;

        public HomeDepotRequestForPaymentLineItem()
        {
            //this.typeField = "SimpleInvoiceLineItemType";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTradeItemIdentification tradeItemIdentification
        {
            get { return this.tradeItemIdentificationField; }
            set { this.tradeItemIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("alternateTradeItemIdentification",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAlternateTradeItemIdentification[] alternateTradeItemIdentification
        {
            get { return this.alternateTradeItemIdentificationField; }
            set { this.alternateTradeItemIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTradeItemDescriptionInformation tradeItemDescriptionInformation
        {
            get { return this.tradeItemDescriptionInformationField; }
            set { this.tradeItemDescriptionInformationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemInvoicedQuantity invoicedQuantity
        {
            get { return this.invoicedQuantityField; }
            set { this.invoicedQuantityField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("aditionalQuantity",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAditionalQuantity[] aditionalQuantity
        {
            get { return this.aditionalQuantityField; }
            set { this.aditionalQuantityField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemGrossPrice grossPrice
        {
            get { return this.grossPriceField; }
            set { this.grossPriceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemNetPrice netPrice
        {
            get { return this.netPriceField; }
            set { this.netPriceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAdditionalInformation AdditionalInformation
        {
            get { return this.additionalInformationField; }
            set { this.additionalInformationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Customs", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemCustoms[] Customs
        {
            get { return this.customsField; }
            set { this.customsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemLogisticUnits LogisticUnits
        {
            get { return this.logisticUnitsField; }
            set { this.logisticUnitsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemPalletInformation palletInformation
        {
            get { return this.palletInformationField; }
            set { this.palletInformationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("lotNumber", Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
            IsNullable = false)]
        public HomeDepotRequestForPaymentLineItemLotNumber[] extendedAttributes
        {
            get { return this.extendedAttributesField; }
            set { this.extendedAttributesField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("allowanceCharge",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAllowanceCharge[] allowanceCharge
        {
            get { return this.allowanceChargeField; }
            set { this.allowanceChargeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tradeItemTaxInformation",
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTradeItemTaxInformation[] tradeItemTaxInformation
        {
            get { return this.tradeItemTaxInformationField; }
            set { this.tradeItemTaxInformationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTotalLineAmount totalLineAmount
        {
            get { return this.totalLineAmountField; }
            set { this.totalLineAmountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        
        public string type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
        public string number
        {
            get { return this.numberField; }
            set { this.numberField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemTradeItemIdentification
    {

        private string gtinField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string gtin
        {
            get { return this.gtinField; }
            set { this.gtinField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemAlternateTradeItemIdentification
    {

        private HomeDepotRequestForPaymentLineItemAlternateTradeItemIdentificationType typeField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemAlternateTradeItemIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemAlternateTradeItemIdentificationType
    {

        /// <remarks/>
        BUYER_ASSIGNED,

        /// <remarks/>
        SUPPLIER_ASSIGNED,

        /// <remarks/>
        SERIAL_NUMBER,

        /// <remarks/>
        GLOBAL_TRADE_ITEM_IDENTIFICATION,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemTradeItemDescriptionInformation
    {

        private string longTextField;

        private HomeDepotRequestForPaymentLineItemTradeItemDescriptionInformationLanguage languageField;

        private bool languageFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string longText
        {
            get { return this.longTextField; }
            set { this.longTextField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemTradeItemDescriptionInformationLanguage language
        {
            get { return this.languageField; }
            set { this.languageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool languageSpecified
        {
            get { return this.languageFieldSpecified; }
            set { this.languageFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemTradeItemDescriptionInformationLanguage
    {

        /// <remarks/>
        ES,

        /// <remarks/>
        EN,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemInvoicedQuantity
    {

        private string unitOfMeasureField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "NMTOKEN")]
        public string unitOfMeasure
        {
            get { return this.unitOfMeasureField; }
            set { this.unitOfMeasureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemAditionalQuantity
    {

        private HomeDepotRequestForPaymentLineItemAditionalQuantityQuantityType quantityTypeField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemAditionalQuantityQuantityType QuantityType
        {
            get { return this.quantityTypeField; }
            set { this.quantityTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemAditionalQuantityQuantityType
    {

        /// <remarks/>
        NUM_CONSUMER_UNITS,

        /// <remarks/>
        FREE_GOODS,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemGrossPrice
    {

        private decimal amountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemNetPrice
    {

        private decimal amountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemAdditionalInformation
    {

        private HomeDepotRequestForPaymentLineItemAdditionalInformationReferenceIdentification
            referenceIdentificationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAdditionalInformationReferenceIdentification referenceIdentification
        {
            get { return this.referenceIdentificationField; }
            set { this.referenceIdentificationField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemAdditionalInformationReferenceIdentification
    {

        private HomeDepotRequestForPaymentLineItemAdditionalInformationReferenceIdentificationType typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemAdditionalInformationReferenceIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemAdditionalInformationReferenceIdentificationType
    {

        /// <remarks/>
        ON,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemCustoms
    {

        private string glnField;

        private HomeDepotRequestForPaymentLineItemCustomsAlternatePartyIdentification alternatePartyIdentificationField;

        private System.DateTime referenceDateField;

        private HomeDepotRequestForPaymentLineItemCustomsNameAndAddress nameAndAddressField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string gln
        {
            get { return this.glnField; }
            set { this.glnField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemCustomsAlternatePartyIdentification alternatePartyIdentification
        {
            get { return this.alternatePartyIdentificationField; }
            set { this.alternatePartyIdentificationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
            DataType = "date")]
        public System.DateTime ReferenceDate
        {
            get { return this.referenceDateField; }
            set { this.referenceDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemCustomsNameAndAddress nameAndAddress
        {
            get { return this.nameAndAddressField; }
            set { this.nameAndAddressField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemCustomsAlternatePartyIdentification
    {

        private HomeDepotRequestForPaymentLineItemCustomsAlternatePartyIdentificationType typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemCustomsAlternatePartyIdentificationType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemCustomsAlternatePartyIdentificationType
    {

        /// <remarks/>
        TN,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemCustomsNameAndAddress
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemLogisticUnits
    {

        private HomeDepotRequestForPaymentLineItemLogisticUnitsSerialShippingContainerCode
            serialShippingContainerCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemLogisticUnitsSerialShippingContainerCode serialShippingContainerCode
        {
            get { return this.serialShippingContainerCodeField; }
            set { this.serialShippingContainerCodeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemLogisticUnitsSerialShippingContainerCode
    {

        private HomeDepotRequestForPaymentLineItemLogisticUnitsSerialShippingContainerCodeType typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemLogisticUnitsSerialShippingContainerCodeType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemLogisticUnitsSerialShippingContainerCodeType
    {

        /// <remarks/>
        BJ,

        /// <remarks/>
        SRV,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemPalletInformation
    {

        private string palletQuantityField;

        private HomeDepotRequestForPaymentLineItemPalletInformationDescription descriptionField;

        private HomeDepotRequestForPaymentLineItemPalletInformationTransport transportField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string palletQuantity
        {
            get { return this.palletQuantityField; }
            set { this.palletQuantityField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemPalletInformationDescription description
        {
            get { return this.descriptionField; }
            set { this.descriptionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemPalletInformationTransport transport
        {
            get { return this.transportField; }
            set { this.transportField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemPalletInformationDescription
    {

        private HomeDepotRequestForPaymentLineItemPalletInformationDescriptionType typeField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemPalletInformationDescriptionType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemPalletInformationDescriptionType
    {

        /// <remarks/>
        EXCHANGE_PALLETS,

        /// <remarks/>
        RETURN_PALLETS,

        /// <remarks/>
        PALLET_80x100,

        /// <remarks/>
        CASE,

        /// <remarks/>
        BOX,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemPalletInformationTransport
    {

        private HomeDepotRequestForPaymentLineItemPalletInformationTransportMethodOfPayment methodOfPaymentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemPalletInformationTransportMethodOfPayment methodOfPayment
        {
            get { return this.methodOfPaymentField; }
            set { this.methodOfPaymentField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemPalletInformationTransportMethodOfPayment
    {

        /// <remarks/>
        PREPAID_BY_SELLER,

        /// <remarks/>
        PAID_BY_BUYER,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemLotNumber
    {

        private System.DateTime productionDateField;

        private bool productionDateFieldSpecified;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime productionDate
        {
            get { return this.productionDateField; }
            set { this.productionDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool productionDateSpecified
        {
            get { return this.productionDateFieldSpecified; }
            set { this.productionDateFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemAllowanceCharge
    {

        private HomeDepotRequestForPaymentLineItemAllowanceChargeSpecialServicesType specialServicesTypeField;

        private bool specialServicesTypeFieldSpecified;

        private HomeDepotRequestForPaymentLineItemAllowanceChargeMonetaryAmountOrPercentage
            monetaryAmountOrPercentageField;

        private HomeDepotRequestForPaymentLineItemAllowanceChargeAllowanceChargeType allowanceChargeTypeField;

        private HomeDepotRequestForPaymentLineItemAllowanceChargeSettlementType settlementTypeField;

        private bool settlementTypeFieldSpecified;

        private string sequenceNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAllowanceChargeSpecialServicesType specialServicesType
        {
            get { return this.specialServicesTypeField; }
            set { this.specialServicesTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool specialServicesTypeSpecified
        {
            get { return this.specialServicesTypeFieldSpecified; }
            set { this.specialServicesTypeFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAllowanceChargeMonetaryAmountOrPercentage monetaryAmountOrPercentage
        {
            get { return this.monetaryAmountOrPercentageField; }
            set { this.monetaryAmountOrPercentageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemAllowanceChargeAllowanceChargeType allowanceChargeType
        {
            get { return this.allowanceChargeTypeField; }
            set { this.allowanceChargeTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentLineItemAllowanceChargeSettlementType settlementType
        {
            get { return this.settlementTypeField; }
            set { this.settlementTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool settlementTypeSpecified
        {
            get { return this.settlementTypeFieldSpecified; }
            set { this.settlementTypeFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sequenceNumber
        {
            get { return this.sequenceNumberField; }
            set { this.sequenceNumberField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemAllowanceChargeSpecialServicesType
    {

        /// <remarks/>
        AA,

        /// <remarks/>
        ADS,

        /// <remarks/>
        ADO,

        /// <remarks/>
        ABZ,

        /// <remarks/>
        DA,

        /// <remarks/>
        EAA,

        /// <remarks/>
        PI,

        /// <remarks/>
        TAE,

        /// <remarks/>
        SAB,

        /// <remarks/>
        RAA,

        /// <remarks/>
        PAD,

        /// <remarks/>
        FG,

        /// <remarks/>
        FA,

        /// <remarks/>
        TD,

        /// <remarks/>
        TS,

        /// <remarks/>
        TX,

        /// <remarks/>
        TZ,

        /// <remarks/>
        ZZZ,

        /// <remarks/>
        VAB,

        /// <remarks/>
        UM,

        /// <remarks/>
        DI,

        /// <remarks/>
        ADT,

        /// <remarks/>
        AJ,

        /// <remarks/>
        CAC,

        /// <remarks/>
        COD,

        /// <remarks/>
        EAB,

        /// <remarks/>
        FC,

        /// <remarks/>
        FI,

        /// <remarks/>
        HD,

        /// <remarks/>
        QD,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemAllowanceChargeMonetaryAmountOrPercentage
    {

        private string percentagePerUnitField;

        private HomeDepotRequestForPaymentLineItemAllowanceChargeMonetaryAmountOrPercentageRatePerUnit ratePerUnitField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string percentagePerUnit
        {
            get { return this.percentagePerUnitField; }
            set { this.percentagePerUnitField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemAllowanceChargeMonetaryAmountOrPercentageRatePerUnit ratePerUnit
        {
            get { return this.ratePerUnitField; }
            set { this.ratePerUnitField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemAllowanceChargeMonetaryAmountOrPercentageRatePerUnit
    {

        private string amountPerUnitField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string amountPerUnit
        {
            get { return this.amountPerUnitField; }
            set { this.amountPerUnitField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemAllowanceChargeAllowanceChargeType
    {

        /// <remarks/>
        ALLOWANCE_GLOBAL,

        /// <remarks/>
        CHARGE_GLOBAL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemAllowanceChargeSettlementType
    {

        /// <remarks/>
        OFF_INVOICE,

        /// <remarks/>
        CHARGE_TO_BE_PAID_BY_VENDOR,

        /// <remarks/>
        CHARGE_TO_BE_PAID_BY_CUSTOMER,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemTradeItemTaxInformation
    {

        private HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTaxTypeDescription taxTypeDescriptionField;

        private string referenceNumberField;

        private HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTradeItemTaxAmount tradeItemTaxAmountField;

        private HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTaxCategory taxCategoryField;

        private bool taxCategoryFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTaxTypeDescription taxTypeDescription
        {
            get { return this.taxTypeDescriptionField; }
            set { this.taxTypeDescriptionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string referenceNumber
        {
            get { return this.referenceNumberField; }
            set { this.referenceNumberField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTradeItemTaxAmount tradeItemTaxAmount
        {
            get { return this.tradeItemTaxAmountField; }
            set { this.tradeItemTaxAmountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTaxCategory taxCategory
        {
            get { return this.taxCategoryField; }
            set { this.taxCategoryField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool taxCategorySpecified
        {
            get { return this.taxCategoryFieldSpecified; }
            set { this.taxCategoryFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTaxTypeDescription
    {

        /// <remarks/>
        GST,

        /// <remarks/>
        VAT,

        /// <remarks/>
        LAC,

        /// <remarks/>
        AAA,

        /// <remarks/>
        ADD,

        /// <remarks/>
        FRE,

        /// <remarks/>
        LOC,

        /// <remarks/>
        STT,

        /// <remarks/>
        OTH,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTradeItemTaxAmount
    {

        private decimal taxPercentageField;

        private decimal taxAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal taxPercentage
        {
            get { return this.taxPercentageField; }
            set { this.taxPercentageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal taxAmount
        {
            get { return this.taxAmountField; }
            set { this.taxAmountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentLineItemTradeItemTaxInformationTaxCategory
    {

        /// <remarks/>
        TRANSFERIDO,

        /// <remarks/>
        RETENIDO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemTotalLineAmount
    {

        private HomeDepotRequestForPaymentLineItemTotalLineAmountGrossAmount grossAmountField;

        private HomeDepotRequestForPaymentLineItemTotalLineAmountNetAmount netAmountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTotalLineAmountGrossAmount grossAmount
        {
            get { return this.grossAmountField; }
            set { this.grossAmountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentLineItemTotalLineAmountNetAmount netAmount
        {
            get { return this.netAmountField; }
            set { this.netAmountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemTotalLineAmountGrossAmount
    {

        private decimal amountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentLineItemTotalLineAmountNetAmount
    {

        private decimal amountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentTotalAmount
    {

        private decimal amountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentTotalAllowanceCharge
    {

        private HomeDepotRequestForPaymentTotalAllowanceChargeSpecialServicesType specialServicesTypeField;

        private bool specialServicesTypeFieldSpecified;

        private decimal amountField;

        private bool amountFieldSpecified;

        private HomeDepotRequestForPaymentTotalAllowanceChargeAllowanceOrChargeType allowanceOrChargeTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentTotalAllowanceChargeSpecialServicesType specialServicesType
        {
            get { return this.specialServicesTypeField; }
            set { this.specialServicesTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool specialServicesTypeSpecified
        {
            get { return this.specialServicesTypeFieldSpecified; }
            set { this.specialServicesTypeFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmountSpecified
        {
            get { return this.amountFieldSpecified; }
            set { this.amountFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentTotalAllowanceChargeAllowanceOrChargeType allowanceOrChargeType
        {
            get { return this.allowanceOrChargeTypeField; }
            set { this.allowanceOrChargeTypeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentTotalAllowanceChargeSpecialServicesType
    {

        /// <remarks/>
        AA,

        /// <remarks/>
        ADS,

        /// <remarks/>
        ADO,

        /// <remarks/>
        ABZ,

        /// <remarks/>
        DA,

        /// <remarks/>
        EAA,

        /// <remarks/>
        PI,

        /// <remarks/>
        TAE,

        /// <remarks/>
        SAB,

        /// <remarks/>
        RAA,

        /// <remarks/>
        PAD,

        /// <remarks/>
        FG,

        /// <remarks/>
        FA,

        /// <remarks/>
        TD,

        /// <remarks/>
        TS,

        /// <remarks/>
        TX,

        /// <remarks/>
        TZ,

        /// <remarks/>
        ZZZ,

        /// <remarks/>
        VAB,

        /// <remarks/>
        UM,

        /// <remarks/>
        DI,

        /// <remarks/>
        ADT,

        /// <remarks/>
        AJ,

        /// <remarks/>
        CAC,

        /// <remarks/>
        COD,

        /// <remarks/>
        EAB,

        /// <remarks/>
        FC,

        /// <remarks/>
        FI,

        /// <remarks/>
        HD,

        /// <remarks/>
        QD,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentTotalAllowanceChargeAllowanceOrChargeType
    {

        /// <remarks/>
        ALLOWANCE,

        /// <remarks/>
        CHARGE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentBaseAmount
    {

        private decimal amountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentTax
    {

        private decimal taxPercentageField;

        private bool taxPercentageFieldSpecified;

        private decimal taxAmountField;

        private bool taxAmountFieldSpecified;

        private HomeDepotRequestForPaymentTaxTaxCategory taxCategoryField;

        private bool taxCategoryFieldSpecified;

        private HomeDepotRequestForPaymentTaxType typeField;

        private bool typeFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal taxPercentage
        {
            get { return this.taxPercentageField; }
            set { this.taxPercentageField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool taxPercentageSpecified
        {
            get { return this.taxPercentageFieldSpecified; }
            set { this.taxPercentageFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal taxAmount
        {
            get { return this.taxAmountField; }
            set { this.taxAmountField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool taxAmountSpecified
        {
            get { return this.taxAmountFieldSpecified; }
            set { this.taxAmountFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public HomeDepotRequestForPaymentTaxTaxCategory taxCategory
        {
            get { return this.taxCategoryField; }
            set { this.taxCategoryField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool taxCategorySpecified
        {
            get { return this.taxCategoryFieldSpecified; }
            set { this.taxCategoryFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public HomeDepotRequestForPaymentTaxType type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeSpecified
        {
            get { return this.typeFieldSpecified; }
            set { this.typeFieldSpecified = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentTaxTaxCategory
    {

        /// <remarks/>
        TRANSFERIDO,

        /// <remarks/>
        RETENIDO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentTaxType
    {

        /// <remarks/>
        GST,

        /// <remarks/>
        VAT,

        /// <remarks/>
        LAC,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class HomeDepotRequestForPaymentPayableAmount
    {

        private decimal amountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public enum HomeDepotRequestForPaymentDocumentStatus
    {

        /// <remarks/>
        ORIGINAL,

        /// <remarks/>
        COPY,

        /// <remarks/>
        REEMPLAZA,

        /// <remarks/>
        DELETE,
    }
}